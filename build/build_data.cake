#addin nuget:?package=Cake.Incubator&version=7.0.0

public readonly BuildPaths buildPaths = new(Context);
public readonly BuildVersions versions = new(Context, buildPaths);
public readonly string targetName = Argument<string>("target", null)?.ToLowerInvariant() ?? Argument("t", "sln:compile").ToLowerInvariant();
public readonly string[] configurations = Arguments<string>("configurations", null as string[])?.ToArray() ?? Arguments<string>("c", new[] {"Debug"}).ToArray();
public readonly bool cleanBuild = HasArgument("clean");

public class BuildPaths {
  public readonly DirectoryPath Cwd;
  public readonly FilePath SolutionFile;
  public readonly DirectoryPath ArtifactsDir,
                                LibArtifactsDir,
                                PkgArtifactsDir,
                                BinArtifactsDir;
  public readonly DirectoryPath TestResultsDir;
  public readonly Dictionary<string, FilePath> Projects;
  public readonly FilePath ReleaseNotes;

  public BuildPaths(ICakeContext context) {
    var root = context.Environment.WorkingDirectory;
    Cwd = root;
    SolutionFile = root.CombineWithFilePath("Case.Net.sln");

    ArtifactsDir = root.Combine("build").Combine("artifacts");
    LibArtifactsDir = ArtifactsDir.Combine("lib");
    PkgArtifactsDir = ArtifactsDir.Combine("packages");
    BinArtifactsDir = ArtifactsDir.Combine("bin");

    context.EnsureDirectoryExists(LibArtifactsDir);
    context.EnsureDirectoryExists(PkgArtifactsDir);
    context.EnsureDirectoryExists(BinArtifactsDir);

    Projects = context.ParseSolution(SolutionFile).Projects
    .Where(project => !project.IsSolutionFolder())
    .ToDictionary(x => x.Name, x => x.Path);

    TestResultsDir = ArtifactsDir.Combine("test-results");
    ReleaseNotes = Cwd.CombineWithFilePath("RELEASE_NOTES.md");
  }

  public DirectoryPath GetProjectOutputPath(string projectName, string configuration, string framework) {
    return Projects[projectName].GetDirectory().Combine("bin").Combine(configuration).Combine(framework);
  }

  public FilePath GetLibArtifactPath(string projectName, string version, string configuration, string framework) {
    return LibArtifactsDir.CombineWithFilePath($"{projectName}.{version}_{configuration}-{framework}.zip");
  }
}

public class BuildVersions {
  public bool IsRelease;
  public string ReleaseProject;
  public string ReleaseVersion;
  public string AppVeyorBuildVersion;
  public SemVersion CoreVersion;

  public BuildVersions(ICakeContext context, BuildPaths paths) {
    if(context.AppVeyor().IsRunningOnAppVeyor) {
      var buildNumber = context.AppVeyor().Environment.Build.Number.ToString("x2");
      var commitHash = context.AppVeyor().Environment.Repository.Commit.Id[..7];

      var buildMeta = $"+{commitHash}.{buildNumber}";

      if(context.AppVeyor().Environment.Repository.Tag.IsTag) {
        var tagName = context.AppVeyor().Environment.Repository.Tag.Name;

        if(tagName.StartsWith("case.net/core/")) {
          if(!SemVersion.TryParse(tagName[14..], out var tagVersion)) {
            throw new CakeException($"Could not parse version from tag name: {tagName}");
          }

          CoreVersion = new SemVersion(tagVersion.Major, tagVersion.Minor, tagVersion.Patch, tagVersion.PreRelease, buildMeta);
          AppVeyorBuildVersion = CoreVersion.ToString();

          ReleaseProject = "Case.Net.Core";
          ReleaseVersion = tagVersion.ToString();
          IsRelease = true;
        } else {
          throw new CakeException($"Invalid tag name: {tagName}");
        }
      } else {
        var branchName = context.AppVeyor().Environment.Repository.Branch;

        string preRelease = string.Empty;

        if(branchName is not "master") {
          preRelease = branchName;
        }

        var parsedProject = context.ParseProject(paths.Projects["Case.Net.Core"], "Release");

        if(!SemVersion.TryParse(parsedProject.GetProjectProperty("VersionPrefix"), out var parsedVersion)) {
          throw new CakeException("Failed to parse version from Case.Net.Core");
        }

        CoreVersion = new SemVersion(parsedVersion.Major, parsedVersion.Minor, parsedVersion.Patch, preRelease, buildMeta);
        AppVeyorBuildVersion = CoreVersion.ToString();
      }
    } else {
      var parsedProject = context.ParseProject(paths.Projects["Case.Net.Core"], "Release");

      if(!SemVersion.TryParse(parsedProject.GetProjectProperty("VersionPrefix"), out var parsedVersion)) {
        throw new CakeException("Failed to parse version from Case.Net.Core");
      }

      CoreVersion = parsedVersion;
      AppVeyorBuildVersion = string.Empty;
    }

    context.Information(
      "Appveyor build version: {0}\nCore project version: {1}",
      AppVeyorBuildVersion,
      CoreVersion.ToString()
    );
  }
}
