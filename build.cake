using System;
using Cake.Core;
using System.Text.RegularExpressions;
using Path = System.IO.Path;
using FileIO = System.IO.File;

/*
  Strategy:
    Regular commit or tag:
      build all -> build all tests -> test all -> pack nuget all -> archive built all -> upload to artifacts
    Tag starting from release/:
      if release/main/<version>:
        build case.net -> build case.net.test -> test case.net -> pack nuget case.net -> archive built case.net -> upload to artifacts
        -> create new github release "Case.NET v<version>" -> push CaseDotNet to nuget
      if release/ext/<version>:
        build case.net.extensions -> build case.net.extensions.test -> test case.net.extensions -> pack nuget case.net.extensions
        -> archive built case.net.extensions -> create new github release "Case.NET.Extensions v<version>" -> push CaseDotNet.Extensions to nuget
*/

const string CONFIGURATION = "Release";
const string NUGET_SOURCE = "https://api.nuget.org/v3/index.json";
const string NS_20 = "netstandard2.0";
const string NS_21 = "netstandard2.1";
const string CDN = "CaseDotNet";
const string CDNE = "CaseDotNet.Extensions";

readonly string TagName = EnvironmentVariable("APPVEYOR_REPO_TAG_NAME", string.Empty);
readonly int BuildNumber = EnvironmentVariable("APPVEYOR_BUILD_NUMBER", 0);
readonly string NugetApiKey = EnvironmentVariable("NUGET_API_KEY");
readonly string GitHubPat = EnvironmentVariable("GITHUB_PAT");
readonly string Cwd = Path.GetFullPath(".");
readonly bool IsReleaseTag = EnvironmentVariable("APPVEYOR_REPO_TAG")?.ToLower() == "true" && TagName.StartsWith("release/");
readonly bool IsMainRelease = IsReleaseTag && TagName.Split('/').Skip(1).FirstOrDefault() == "main";
readonly bool IsExtRelease = IsReleaseTag && TagName.Split('/').Skip(1).FirstOrDefault() == "ext";
readonly string ReleaseVersion = TagName.Split('/').Skip(2).FirstOrDefault() ?? string.Empty;
readonly string MainProjDir = Path.GetFullPath("./Case.NET");
readonly string ExtProjDir = Path.GetFullPath("./Case.NET.Extensions");
readonly string MainProjPath = Path.Combine(Cwd, "Case.NET", "Case.NET.csproj");
readonly string ExtProjPath = Path.Combine(Cwd, "Case.NET.Extensions", "Case.NET.Extensions.csproj");
readonly string MainProjTest = Path.Combine(Path.GetFullPath("./Case.NET.Test"), "Case.NET.Test.csproj");
readonly string ExtProjTest = Path.Combine(Path.GetFullPath("./Case.NET.Extensions.Test"), "Case.NET.Extensions.Test.csproj");
readonly string MainProjBin = Path.Combine(Cwd, "Case.NET", "bin", CONFIGURATION);
readonly string ExtProjBin = Path.Combine(Cwd, "Case.NET.Extensions", "bin", CONFIGURATION);
readonly Func<string, string, string> BuildOutputDir = (bin, target) =>
  Path.Combine(bin, target);
readonly Func<string, string, string, string> ArchiveOutputPath = (bin, name, target) =>
  Path.Combine(bin, $"{name}_{target}.zip");
readonly Func<string, string, string, string> PackageOutputPath = (bin, name, version) =>
  Path.Combine(bin, $"{name}.{version}.nupkg");
readonly Func<string, string, string, string> SymbolPackageOutputPath = (bin, name, version) =>
  Path.Combine(bin, $"{name}.{version}.snupkg");

List<string> ArtifactsList => new() {
    ArchiveOutputPath(MainProjBin, "Case.NET", NS_20),
    ArchiveOutputPath(MainProjBin, "Case.NET", NS_21),
    ArchiveOutputPath(ExtProjBin, "Case.NET.Extensions", NS_20),
    PackageOutputPath(MainProjBin, "CaseDotNet", EnvironmentVariable("CUSTOM_VERSION_MAIN")),
    PackageOutputPath(ExtProjBin, "CaseDotNet.Extensions", EnvironmentVariable("CUSTOM_VERSION_EXT"))
};

string ReadProjectVersion(string projectPath) => XmlPeek(
  Path.Combine(Path.GetDirectoryName(projectPath), "Version.props"),
  "/Project/PropertyGroup[1]/Version"
);

string GetCustomVersion(string ver) {
  var regex = new Regex(@"(\d+)(?:\.(\d+))(?:\.(\d+))(?:-([-\.a-z0-9]+))", RegexOptions.Compiled | RegexOptions.IgnoreCase);
  var bn = BuildNumber.ToString();

  var match = regex.Match(ver);

  var major = match.Groups[1].Value;
  var minor = match.Groups[2].Value ?? "0";
  var patch = match.Groups[3].Value ?? "0";
  var suffix = match.Groups[4].Value ?? string.Empty;

  return $"{major}.{minor}.{bn}" + (suffix.Length == 0 ? string.Empty : ("-" + suffix));
}

Information("--- Build started for Case.NET ---");
Information("Current working directory: {0}", Cwd);
// Information("Bin directory: {0}", Bin);
// Information("Case.NET project path: {0}", MainProj);
// Information("Case.NET.Test project path: {0}", TestProj);

Task("build-main").IsDependentOn("resolve-version").Does(() => {
  Information("Building Case.NET v{0}...", EnvironmentVariable("CUSTOM_VERSION_MAIN"));

  DotNetBuild(MainProjPath, new DotNetBuildSettings {
    Configuration = CONFIGURATION,
    Verbosity = DotNetVerbosity.Minimal
  });
});

Task("build-main-test").IsDependentOn("build-main").Does(() => {
  Information("Building Case.NET.Test...");

  DotNetBuild(MainProjTest, new DotNetBuildSettings {
    Configuration = CONFIGURATION,
    Verbosity = DotNetVerbosity.Minimal,
    NoDependencies = true
  });
});

Task("test-main").IsDependentOn("build-main-test").Does(() => {
  Information("Running unit tests on Case.NET v{0}...", EnvironmentVariable("CUSTOM_VERSION_MAIN"));

  DotNetTest(MainProjTest, new DotNetTestSettings {
    Configuration = CONFIGURATION,
    Verbosity = DotNetVerbosity.Minimal,
    NoRestore = true,
    NoBuild = true
  });
});

Task("build-ext").IsDependentOn("resolve-version").Does(() => {
  Information("Building Case.NET.Extensions v{0}", EnvironmentVariable("CUSTOM_VERSION_EXT"));

  DotNetBuild(ExtProjPath, new DotNetBuildSettings {
    Configuration = CONFIGURATION,
    Verbosity = DotNetVerbosity.Minimal
  });
});

Task("build-ext-test").IsDependentOn("build-ext").Does(() => {
  Information("Building Case.NET.Extensions.Test...");

  DotNetBuild(ExtProjTest, new DotNetBuildSettings {
    Configuration = CONFIGURATION,
    Verbosity = DotNetVerbosity.Minimal,
    NoDependencies = true
  });
});

Task("test-ext").IsDependentOn("build-ext-test").Does(() => {
  Information("Running unit tests on Case.NET.Extensions v{0}", EnvironmentVariable("CUSTOM_VERSION_EXT"));

  DotNetTest(ExtProjTest, new DotNetTestSettings {
    Configuration = CONFIGURATION,
    Verbosity = DotNetVerbosity.Minimal,
    NoRestore = true,
    NoBuild = true
  });
});

Task("archive-main").IsDependentOn("build-main").IsDependentOn("test-main").Does(() => {
  Information("Compressing build results of Case.NET...");

  Zip(BuildOutputDir(MainProjBin, NS_20), ArchiveOutputPath(MainProjBin, "Case.NET", NS_20));
  Zip(BuildOutputDir(MainProjBin, NS_21), ArchiveOutputPath(MainProjBin, "Case.NET", NS_21));
});

Task("archive-ext").IsDependentOn("build-ext").IsDependentOn("test-ext").Does(() => {
  Information("Compressing build results of Case.NET.Extensions...");

  Zip(BuildOutputDir(ExtProjBin, NS_20), ArchiveOutputPath(ExtProjBin, "Case.NET.Extensions", NS_20));
});

Task("pack-main").IsDependentOn("build-main").IsDependentOn("test-main").Does(() => {
  Information("Creating NuGet package for Case.NET v{0}", EnvironmentVariable("CUSTOM_VERSION_MAIN"));

  DotNetPack(MainProjPath, new DotNetPackSettings {
    Configuration = CONFIGURATION,
    IncludeSymbols = true,
    NoBuild = true,
    NoRestore = true,
    NoDependencies = true,
    Verbosity = DotNetVerbosity.Minimal
  });
});

Task("pack-ext").IsDependentOn("build-ext").IsDependentOn("test-ext").Does(() => {
  Information("Creating NuGet package for Case.NET.Extensions v{0}", EnvironmentVariable("CUSTOM_VERSION_EXT"));

  DotNetPack(ExtProjPath, new DotNetPackSettings {
    Configuration = CONFIGURATION,
    IncludeSymbols = true,
    NoBuild = true,
    NoRestore = true,
    NoDependencies = true,
    Verbosity = DotNetVerbosity.Minimal
  });
});

Task("push-package-main").IsDependentOn("build-main")
                         .IsDependentOn("test-main")
                         .IsDependentOn("pack-main")
                         .Does(() => {
                           Information("Pushing Case.NET v{0} to NuGet...", EnvironmentVariable("CUSTOM_VERSION_MAIN"));

                           NuGetPush(PackageOutputPath(MainProjBin, "CaseDotNet", EnvironmentVariable("CUSTOM_VERSION_MAIN")), new NuGetPushSettings {
                             ApiKey = NugetApiKey,
                             Source = NUGET_SOURCE,
                           });
                         });

Task("push-package-ext").IsDependentOn("build-ext")
                        .IsDependentOn("test-ext")
                        .IsDependentOn("pack-ext")
                        .Does(() => {
                          Information("Pushing Case.NET.Extensions v{0}", EnvironmentVariable("CUSTOM_VERSION_EXT"));

                          NuGetPush(PackageOutputPath(ExtProjBin, "CaseDotNet.Extensions", EnvironmentVariable("CUSTOM_VERSION_EXT")), new NuGetPushSettings {
                            ApiKey = NugetApiKey,
                            Source = NUGET_SOURCE
                          });
                        });

Task("upload-artifacts").Does(() => {
  Information("Uploading build artifacts to AppVeyor...");

  List<string> artifacts = ArtifactsList;

  foreach(var artifact in artifacts) {
    if(FileIO.Exists(artifact)) {
      Information("Uploading artifact: {0}", artifact);
      AppVeyor.UploadArtifact(artifact);
    } else {
      Information("Artifact not found: {0}", artifact);
    }
  }
});

Task("create-release").Does(() => {
  Information("Creating new GitHub release...");

  if(!IsReleaseTag) {
    throw new InvalidOperationException("Cannot run create-release task on non-release build");
  }

  if(IsMainRelease) {
    string v = EnvironmentVariable("CUSTOM_VERSION_MAIN");
    Information("Creating release of Case.NET - v{0}", v);

    GitReleaseManagerCreate(GitHubPat, "2chevskii", "Case.NET", new GitReleaseManagerCreateSettings {
      Prerelease = v.Contains("-"),
      Name = "Case.NET v" + v,
      TargetCommitish = AppVeyor.Environment.Repository.Tag.Name,
      Assets = Path.Combine(MainProjBin, "*.{zip,nupkg,snupkg}")
    });
  } else {
    string v = EnvironmentVariable("CUSTOM_VERSION_EXT");
    Information("Creating release of Case.NET.Extensions - v{0}", v);

    GitReleaseManagerCreate(GitHubPat, "2chevskii", "Case.NET", new GitReleaseManagerCreateSettings {
      Prerelease = v.Contains("-"),
      Name = "Case.NET.Extensions v " + v,
      TargetCommitish = AppVeyor.Environment.Repository.Tag.Name,
      Assets = Path.Combine(ExtProjBin, "*.{zip,nupkg,snupkg}")
    });
  }
});

Task("resolve-version").Does(() => {
  Information("Setting custom build version...");

  Environment.SetEnvironmentVariable("CUSTOM_VERSION", "true");

  if(IsReleaseTag) {
    string cv = GetCustomVersion(ReleaseVersion);
    string ev;

    Information("Using release version: {0}", cv);

    if(IsMainRelease) {
      ev = "CUSTOM_VERSION_MAIN";
    } else {
      ev = "CUSTOM_VERSION_EXT";
    }

    Environment.SetEnvironmentVariable(ev, cv);
  } else {
    var mainProjVersion = ReadProjectVersion(MainProjPath);
    var extProjVersion = ReadProjectVersion(ExtProjPath);

    string mcv = GetCustomVersion(mainProjVersion);
    string ecv = GetCustomVersion(extProjVersion);

    Information("Using project versions: {0} | {1}", mcv, ecv);

    Environment.SetEnvironmentVariable("CUSTOM_VERSION_MAIN", mcv);
    Environment.SetEnvironmentVariable("CUSTOM_VERSION_EXT", ecv);
  }
});

Task("regular-pipeline").IsDependentOn("archive-main")
                        .IsDependentOn("archive-ext")
                        .IsDependentOn("pack-main")
                        .IsDependentOn("pack-ext")
                        .IsDependentOn("upload-artifacts")
                        .Does(() => {
                          AppVeyor.AddInformationalMessage("Regular build pipeline finished");
                        });

Task("release-pipeline-main").IsDependentOn("archive-main")
                             .IsDependentOn("pack-main")
                             .IsDependentOn("push-package-main")
                             .IsDependentOn("create-release")
                             .IsDependentOn("upload-artifacts")
                             .Does(() => {
                              AppVeyor.AddInformationalMessage(
                                "Case.NET v{0} release pipeline finished",
                                EnvironmentVariable("CUSTOM_VERSION_MAIN")
                              );
                             });

Task("release-pipeline-ext").IsDependentOn("archive-ext")
                            .IsDependentOn("pack-ext")
                            .IsDependentOn("push-package-ext")
                            .IsDependentOn("create-release")
                            .IsDependentOn("upload-artifacts")
                            .Does(() => {
                              AppVeyor.AddInformationalMessage(
                                "Case.NET.Extensions v{0} release pipeline finised",
                                EnvironmentVariable("CUSTOM_VERSION_EXT")
                              );
                            });

if(EnvironmentVariable("CI")?.ToLower() != "true") {
  throw new CakeException(1, "Build failed: non-CI environments are not supported, use standard dotnet tools instead");
}

CakeReport report;

if(IsReleaseTag) {
  if(IsMainRelease) {
    report = RunTarget("release-pipeline-main");
  } else {
    report = RunTarget("release-pipeline-ext");
  }
} else {
  report = RunTarget("regular-pipeline");
}

bool flag = false;

foreach(var result in report) {
  if(result.ExecutionStatus == CakeTaskExecutionStatus.Failed) {
    flag = true;

    AppVeyor.AddErrorMessage("Failed task {0}", result.TaskName);
  }
}

if(flag) {
  throw new CakeException(1);
}
