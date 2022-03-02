using System.Text.RegularExpressions;
using Path = System.IO.Path;

var isCIBuild = EnvironmentVariable("CI")?.ToLower() == "true";
var isTag = EnvironmentVariable("APPVEYOR_REPO_TAG")?.ToLower() == "true";
var tagName = !isTag ? string.Empty : EnvironmentVariable("APPVEYOR_REPO_TAG_NAME");
var buildNumber = EnvironmentVariable<int>("APPVEYOR_BUILD_NUMBER", 0);
var task = Argument("t",(string) null )?.ToLower() ?? Argument("task", "default").ToLower();
var configuration = Argument<string>("c", null) ?? Argument("configuration", "all");

var rootDir = Path.GetFullPath(".");
var repoRootDir = Path.GetFullPath("../");
var buildDir = Path.Combine(rootDir, "bin/");
var caseNetProject = Path.Combine(rootDir, "Case.NET.csproj");
var testProject = Path.Combine(repoRootDir, "Case.NET.Test/Case.NET.Test.csproj");

Information("--- Build started for Case.NET ---");
Information("Root directory: {0}", rootDir);
Information("Repo root directory: {0}", repoRootDir);
Information("Build directory: {0}", buildDir);
Information("Case.NET project path: {0}", caseNetProject);
Information("Case.NET.Test project path: {0}", testProject);

var tagNameSanitizeRegex = new Regex(@"Case\.NET\sv(\d\.\d)(?:-([a-z-\.0-9]+))?", RegexOptions.Compiled | RegexOptions.IgnoreCase);

Func<(string, string)> getTaggedVersion = () => {
  var match = tagNameSanitizeRegex.Match(tagName);

  if(!match.Success) {
    throw new FormatException("Wrong release tag format: " + tagName);
  }

  var version = match.Groups[1].Value;
  string suffix;
  if(match.Groups[2].Success) {
      suffix = match.Groups[2].Value;
  } else {
    suffix = string.Empty;
  }

  return (version, suffix);
};

Func<string, string> getConfigurationDir = conf => Path.Combine(buildDir, conf);

string[] Configurations  {
  get {
    if(configuration.ToLower() == "all" || configuration == "*") {
      return new [] {"Debug", "Release"};
    }

    var c = configuration.ToLower();

    if(c != "release" && c != "debug") {
      throw new CakeException($"Unknown configuration: {configuration}");
    }

    c = char.ToUpper(c[0]) + c[1..];

    return new [] {c};
  }
}

Task("build").Does(() => {
  Information("Building Case.NET ({0})...", string.Join(", ", Configurations));

  foreach(var config in Configurations)
  {
    Information("Building configuration: {0}...", config);

    Verbose("Restore...");
    DotNetRestore(caseNetProject);

    Verbose("Build...");
    DotNetBuild(caseNetProject, new DotNetBuildSettings {
      Configuration = config,
      NoRestore = true,
      Verbosity = DotNetVerbosity.Minimal
    });
  }
});

Task("build-tests").IsDependentOn("build").Does(() => {
  Information("Building Case.NET.Tests...");

  DotNetRestore(testProject);

  // Testing is only done on Debug configuration to save time
  // since there are no differences between Debug and Release configs code-wise
  // So if we know Debug configuration was not build - we need to fix it
  if(!Configurations.Contains("Debug")) {
    // save original configuration value to restore it later
    var originalConfig = configuration;

    // set configuration to debug
    configuration = "Debug";

    // run build task
    RunTarget("build");

    // restore original configuration
    configuration = originalConfig;
  }

  DotNetBuild(testProject, new DotNetBuildSettings {
    Configuration = "Debug",
    NoDependencies = true,
    Verbosity = DotNetVerbosity.Minimal,
    NoRestore = true
  });
});

Task("test").IsDependentOn("build-tests").Does(() => {
  Information("Running Case.NET unit tests...");

  DotNetTest(testProject, new DotNetTestSettings {
    Configuration = "Debug",
    NoBuild = true,
    NoRestore = true,
    Verbosity = DotNetVerbosity.Normal
  });
});

Task("pack").IsDependentOn("build").IsDependentOn("test").Does(() => {
  Information("Creating new Case.NET nuget package ({0})...", string.Join(", ", Configurations));

  foreach(var config in Configurations) {
    Information("Packing Case.NET/{0} v{1}", config, "<VERSION>");

    DotNetPack(caseNetProject, new DotNetPackSettings {
      Configuration = config,
      NoBuild = true,
      NoRestore = true,
      IncludeSymbols = true,
      Verbosity = DotNetVerbosity.Minimal
    });
  }

});

Task("default").Does(() => {
  Information("Running default build pipeline for Case.NET...");

  if(isTag) {
    Information("Tag build detected - {0}", EnvironmentVariable("APPVEYOR_REPO_TAG_NAME"));
    Information("Setting configuration to release");
    configuration = "Release";

    try {
      Information("Retrieving custom version from tag...");

      var (tagVersion, tagVersionSuffix) = getTaggedVersion();

      Information("Version prefix: {0}\nVersion suffix: {1}\nBuild number: {2}");

      var resultVersion = $"{tagVersion}.{buildNumber}";

      if(tagVersionSuffix.Length != 0) {
        resultVersion += $"-{tagVersionSuffix}";
      }

      Information("Resulting package version will be: {0} {1}", resultVersion, tagVersionSuffix.Length != 0 ? "(pre-release)" : string.Empty);

      Environment.SetEnvironmentVariable("CUSTOM_VERSION", "true");
      Environment.SetEnvironmentVariable("CUSTOM_VERSION_VALUE", resultVersion);

      Information("Running release pipeline...");

      RunTarget("pack");

      Information("Pushing package artifacts to NuGet...");

      var packagePath = Path.Combine(getConfigurationDir("Release"), $"Case.NET-{resultVersion}.nupkg");
      var symbolPackagePath = Path.Combine(getConfigurationDir("Release"), $"Case.NET-{resultVersion}.snupkg");

      var pushSettings = new NuGetPushSettings {ApiKey = EnvironmentVariable("NUGET_API_KEY")};

      Information("Pushing {0}...", packagePath);
      NuGetPush(packagePath, pushSettings);

      Information("Pushing {0}...", symbolPackagePath);
      NuGetPush(symbolPackagePath, pushSettings);

      Information("All packages were successfully pushed to NuGet!");

      Information("Creating archives from build directories...");

      var configDir = getConfigurationDir("Release");
      var netstandard20Dir = Path.Combine(configDir, "netstandard2.0/");
      var netstandard21Dir = Path.Combine(configDir, "netstandard2.1/");
      var netstandard20Zip = Path.Combine(configDir, "netstandard2.0.zip");
      var netstandard21Zip = Path.Combine(configDir, "netstandard2.1.zip");
      Zip(netstandard20Dir, netstandard20Zip);
      Zip(netstandard21Dir, netstandard21Zip);

      Information("Uploading build artifacts to AppVeyor...");

      Information("Uploading NuGet package...");
      AppVeyor.UploadArtifact(packagePath);

      Information("Uploading netstandard2.0 archive...");
      AppVeyor.UploadArtifact(netstandard20Zip);

      Information("Uploading netstandard2.1 archive...");
      AppVeyor.UploadArtifact(netstandard21Zip);

      Information("Done!");
    } catch(FormatException e) {
      Error("Exception occured during tag version retrieval:\n{0}", e.Message);

      Warning("Build pipeline will be restarted as normal build (without NuGet package creation)");

      isTag=false;
      RunTarget("default");
    }
  } else {
    Information("Running default CI build pipeline (build + test)...");

    RunTarget("pack");

    var packagePath = Path.Combine(getConfigurationDir("Release"), $"Case.NET-0.1.{buildNumber}.nupkg");

    Information("Creating archives from build directories...");

    var configDir = getConfigurationDir("Release");
    var netstandard20Dir = Path.Combine(configDir, "netstandard2.0/");
    var netstandard21Dir = Path.Combine(configDir, "netstandard2.1/");
    var netstandard20Zip = Path.Combine(configDir, "netstandard2.0.zip");
    var netstandard21Zip = Path.Combine(configDir, "netstandard2.1.zip");
    Zip(netstandard20Dir, netstandard20Zip);
    Zip(netstandard21Dir, netstandard21Zip);

    Information("Uploading build artifacts to AppVeyor...");

    Information("Uploading NuGet package...");
    AppVeyor.UploadArtifact(packagePath);

    Information("Uploading netstandard2.0 archive...");
    AppVeyor.UploadArtifact(netstandard20Zip);

    Information("Uploading netstandard2.1 archive...");
    AppVeyor.UploadArtifact(netstandard21Zip);

    Information("Build success!");
  }
});

RunTarget(task);
