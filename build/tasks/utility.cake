#load ../build_data.cake
#addin nuget:?package=Cake.Incubator&version=7.0.0
#addin nuget:?package=Cake.FileHelpers&version=5.0.0
#addin nuget:?package=LibGit2Sharp&version=0.26.2

using Cake.Common;
using System.Text.RegularExpressions;

Task("update-appveyor-build-version")
.WithCriteria(AppVeyor.IsRunningOnAppVeyor)
.Does(() => {
  AppVeyor.UpdateBuildVersion(versions.AppVeyorBuildVersion);
});

Task("case.net.core:release/prepare")
/* .WithCriteria(AppVeyor.IsRunningOnAppVeyor, "Should be running in AppVeyor CI")
.WithCriteria(AppVeyor.Environment.Repository.Tag.IsTag, "Should be tag commit")
.WithCriteria(AppVeyor.Environment.Repository.Tag.Name.StartsWith("case.net/core/"), "Tag name should start with case.net/core/") */
.Does(() => {
  /* GitReleaseManagerCreate(EnvironmentVariable("GITHUB_TOKEN"), "2chevskii", "Case.Net", new GitReleaseManagerCreateSettings {
    Name = $"{versions.ReleaseProject} v{versions.ReleaseVersion}",
    InputFilePath = null,
    TargetCommitish = AppVeyor.Environment.Repository.Commit.Id,
    Prerelease = versions.CoreVersion.IsPreRelease
  }); */

  var rnContent = FileReadText(buildPaths.ReleaseNotes);

  var releaseVersion = "1.0.0";

  var pattern = $@"##\sCase\.Net\.Core\sv{releaseVersion.Replace(".", @"\.")}\n((?:.|\n)*?)(?:\n#|$)";

  var match = Regex.Match(rnContent, pattern);

  if(match.Success) {
    var text = match.Groups[1].Value.Trim() + "\n";
  }
});

Task("case.net.core:patch-version")
.Does(() => {
  var versionPrefix = $"{versions.CoreVersion.Major}.{versions.CoreVersion.Minor}.{versions.CoreVersion.Patch}";
  var versionSuffix = $"{versions.CoreVersion.PreRelease}";

  XmlPoke(buildPaths.Projects["Case.Net.Core"], "/Project/PropertyGroup[1]/VersionPrefix", versionPrefix);
  XmlPoke(buildPaths.Projects["Case.Net.Core"], "/Project/PropertyGroup[1]/VersionSuffix", versionSuffix);
});

Task("case.net.core:version-update-commit")
.Does(() => {
  var filesToCommit = new string[] {
    "src/Case.Net.Core/Case.Net.Core.csproj",
    "src/Case.Net.Core/RELEASE_NOTES.md"
  };

  string previousVersion, newVersion;
  previousVersion = newVersion = string.Empty;

  var commitName = $"[skip ci] Bump Case.Net.Core version ({previousVersion} => {newVersion})";


});
