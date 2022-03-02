using System.Text.RegularExpressions;
using Path = System.IO.Path;
using System;

const string CONFIGURATION = "Release";
const string NUGET_SOURCE = "https://api.nuget.org/v3/index.json";
readonly bool IsTag = EnvironmentVariable("APPVEYOR_REPO_TAG")?.ToLower() == "true";
readonly string TagName = EnvironmentVariable("APPVEYOR_REPO_TAG_NAME", string.Empty);
readonly int BuildNumber = EnvironmentVariable("APPVEYOR_BUILD_NUMBER", 0);
readonly string NugetApiKey = EnvironmentVariable("NUGET_API_KEY");
readonly string GitHubPat = EnvironmentVariable("GITHUB_PAT");
readonly string Cwd = Path.GetFullPath(".");
readonly Regex TagNameSanitizeRegex = new(
  @"Case\.NETv(\d\.\d)(?:-([a-z-\.0-9]+))?",
  RegexOptions.Compiled | RegexOptions.IgnoreCase
);

ReleaseType GetReleaseType() {
  if(!IsTag) {
    return ReleaseType.NONE;
  }

  if(!TagName.StartsWith("release/")) {
    return ReleaseType.NONE;
  }


}

string[] ReadProjectVersion(string projectPath) {
  var version = XmlPeek(projectPath, "/Project/PropertyGroup[1]/Version");

  return version.Split('.');
}

string ReadVersionFromTag() {
  if(!TagName.StartsWith("release/")) {
    throw new CakeException("Failed to read release version from tag name: Invalid tag format")
  }
}

string GetCustomVersion(bool fromTag) {
  if(fromTag) {

  } else {

  }
}

(string, string) GetVersionFromTag() {
  var match = TagNameSanitizeRegex.Match(TagName);

  if(!match.Success) {
    throw new FormatException("Wrong release tag format: " + TagName);
  }

  var core = match.Groups[1].Value + $".{BuildNumber}";
  var suffix = match.Groups[2].Success
  ? match.Groups[2].Value
  : string.Empty;

  return (core, suffix);
}

(string, string) GetVersionFromBranch() {
  var branchName = AppVeyor.Environment.Repository.Branch;
  string suffix;

  if(branchName == "master") {
    suffix = string.Empty;
  } else {
    suffix = branchName.ToLower();
  }

  var peekVer = XmlPeek(VersionProj, "/Project/PropertyGroup/Version");

  var mm = peekVer.Split('.')[0..2];
  var p = BuildNumber.ToString();

  var core = $"{string.Join(".", mm)}.{p}";

  return (core, suffix);
}

Information("--- Build started for Case.NET ---");
Information("Current working directory: {0}", Cwd);
Information("Bin directory: {0}", Bin);
Information("Case.NET project path: {0}", MainProj);
Information("Case.NET.Test project path: {0}", TestProj);

Task("set-version").Does(() => {
  Information("Setting custom build version...");

  (string, string) tuple;

  if(IsTag) {
    Information("Using version from tag name...");
    tuple = GetVersionFromTag();
  } else {
    Information("Using version from branch and project...");
    tuple = GetVersionFromBranch();
  }

  var (core, suffix) = tuple;

  var version = GetVersionName(core, suffix);

  Information("Setting CUSTOM_VERSION to true");
  Environment.SetEnvironmentVariable("CUSTOM_VERSION", "true");

  Information("Setting CUSTOM_VERSION_VALUE to {0}", version);
  Environment.SetEnvironmentVariable("CUSTOM_VERSION_VALUE", version);
});

Task("build").IsDependentOn("set-version").Does(() => {
  Information("Building Case.NET v{0}...", EnvironmentVariable("CUSTOM_VERSION_VALUE"));

  Verbose("Restoring project dependencies...");
  DotNetRestore(Cwd);

  Verbose("Running .NET build...");
  DotNetBuild(MainProj, new DotNetBuildSettings {
    Configuration = CONFIGURATION,
    NoRestore = true,
    Verbosity = DotNetVerbosity.Minimal
  });

  Information("Project build success");
});

Task("build-tests").IsDependentOn("build").Does(() => {
  Information("Building Case.NET.Tests...");

  Verbose("Restoring project dependencies...");
  DotNetRestore(Path.GetDirectoryName(TestProj));

  Verbose("Running .NET build...");
  DotNetBuild(TestProj, new DotNetBuildSettings {
    Configuration = CONFIGURATION,
    NoRestore = true,
    NoDependencies = true,
    Verbosity = DotNetVerbosity.Minimal
  });

  Information("Tests build success");
});

Task("test").IsDependentOn("build-tests").Does(() => {
  Information("Running Case.NET unit tests...");

  DotNetTest(TestProj, new DotNetTestSettings {
    Configuration = CONFIGURATION,
    NoBuild = true,
    NoRestore = true,
    Verbosity = DotNetVerbosity.Normal
  });

  Information("Tests finished");
});

Task("pack").IsDependentOn("build").IsDependentOn("test").Does(() => {
  Information("Creating NuGet package for Case.NET v{0}", EnvironmentVariable("CUSTOM_VERSION_VALUE"));

  DotNetPack(MainProj, new DotNetPackSettings {
    Configuration = CONFIGURATION,
    IncludeSymbols = true,
    NoBuild = true,
    NoRestore = true,
    NoDependencies = true,
    Verbosity = DotNetVerbosity.Minimal
  });

  Information("Packing done");
});

Task("archive").IsDependentOn("pack").Does(() => {
  Information("Packing built files into zip archives...");

  Information("Compressing netstandard2.0 configuration build:\n{0} -> {1}", NetStandard20Dir, NetStandard20Zip);
  Zip(NetStandard20Dir, NetStandard20Zip);
  Information("Compressing netstandard2.1 configuration build:\n{0} -> {1}", NetStandard21Dir, NetStandard21Zip);
  Zip(NetStandard21Dir, NetStandard21Zip);

  Information("Done");
});

Task("upload-artifacts").IsDependentOn("archive")
                        .Does(() => {
                          Information("Uploading build artifacts to AppVeyor");
                          var packagePath = GetPackagePath(EnvironmentVariable("CUSTOM_VERSION_VALUE"));

                          Information("Uploading NuGet package");
                          AppVeyor.UploadArtifact(packagePath);

                          Information("Uploading netstandard2.0 archive");
                          AppVeyor.UploadArtifact(NetStandard20Zip);

                          Information("Uploading netstandard2.1 archive");
                          AppVeyor.UploadArtifact(NetStandard21Zip);
                        });

Task("push-package").Does(() => {
                      var version = EnvironmentVariable("CUSTOM_VERSION_VALUE");
                      Information("Pushing new release to NuGet feed (Case.NET v{0})...", version);

                      if(!IsTag) {
                        throw new CakeException("Cannot push package from non-tag build!");
                      }

                      NuGetPush(GetPackagePath(EnvironmentVariable("CUSTOM_VERSION_VALUE")), new NuGetPushSettings {
                        ApiKey = NugetApiKey,
                        Source = NUGET_SOURCE
                      });

                      NuGetPush(GetPackagePath(EnvironmentVariable("CUSTOM_VERSION_VALUE")).Replace("nupkg", "snupkg"), new NuGetPushSettings {
                        ApiKey = NugetApiKey,
                        Source = NUGET_SOURCE
                      });
                    });

Task("create-github-release").Does(() => {
  Information("Creating new GitHub release...");

  var version = EnvironmentVariable("CUSTOM_VERSION_VALUE");
  var releaseName = $"Case.NET v{version}";

  Information($"Release name: {releaseName}");

  GitReleaseManagerCreate(GitHubPat, "2chevskii", "Case.NET", new GitReleaseManagerCreateSettings {
    Prerelease = version.Contains("-"),
    Name = releaseName,
    TargetCommitish = AppVeyor.Environment.Repository.Commit.Id,
    Assets = ConfigurationBin + "/" + "*.{zip,nupkg,snupkg}"
  });
});

Task("release-build").IsDependentOn("build")
                     .IsDependentOn("test")
                     .IsDependentOn("pack")
                     .IsDependentOn("archive")
                     .IsDependentOn("upload-artifacts")
                     .IsDependentOn("push-package")
                     .IsDependentOn("create-github-release")
                     .Does(() => {
                       Information("Release build completed");
                     });

Task("regular-build").IsDependentOn("build")
                     .IsDependentOn("test")
                     .IsDependentOn("pack")
                     .IsDependentOn("archive")
                     .IsDependentOn("upload-artifacts")
                     .Does(() => {
                       Information("Regular build completed");
                     });

// if(EnvironmentVariable("CI")?.ToLower() != "true") {
//   throw new CakeException("Build failed: non-CI environments are not supported, use standard dotnet tools instead");
// }

// if(IsTag) {
//   Information("Tag build detected, running release build...");
//   RunTarget("release-build");
// } else {
//   Information("Running regular build...");
//   RunTarget("regular-build");
// }

readonly struct ProjectPath {
  public readonly string Root;
  public readonly string Bin;
  public readonly string Release;
  public readonly string NetStandard20;
  public readonly string NetStandard21;
  public readonly string NetStandard20Zip;
  public readonly string NetStandard21Zip;
  public readonly Func<string, string> Package;
  public readonly Func<string, string> SymbolsPackage;
  public readonly string MainProj;
  public readonly string TestProj;
  public readonly string VersionProps;

  public ProjectPath(string cwd, string root, string packageName) {
    Root = Path.GetFullPath(root);
    Bin = Path.Combine(Root, "bin/");
    Release = Path.Combine(Bin, CONFIGURATION);
    NetStandard20 = Path.Combine(Release, "netstandard2.0");
    NetStandard21 = Path.Combine(Release, "netstandard2.1");
    NetStandard20Zip = NetStandard20 + ".zip";
    NetStandard21Zip = NetStandard21 + ".zip";

    string releaseLocal = Release;
    Package = version => Path.Combine(releaseLocal, $"{packageName}.{version}.nupkg");
    SymbolsPackage = version => Path.Combine(releaseLocal,  $"{packageName}.{version}.snupkg");

    string fnwe = Path.GetFileNameWithoutExtension(root);

    MainProj = Path.Combine(Root, $"{fnwe}.csproj");
    TestProj = Path.GetFullPath(Path.Combine(cwd, $"{fnwe}.Test/{fnwe}.Test.csproj"));
    VersionProps = Path.Combine(Root, "Version.props");
  }
}

[Flags]
enum ReleaseType {
  NONE = 0,
  MAIN = 2,
  EXTENSIONS = 4
}
