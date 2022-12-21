#load ../build_data.cake

Task("sln:clean")
.IsDependentOn("case.net.core:clean")
.IsDependentOn("case.net.core.test.unit:clean");

Task("case.net.core:clean")
.WithCriteria(cleanBuild || targetName is "case.net.core:clean" or "sln:clean")
.DoesForEach(configurations, configuration => CleanProject("Case.Net.Core", configuration));

Task("case.net.core.test.unit:clean")
.WithCriteria(cleanBuild || targetName is "case.net.core.test.unit:clean" or "sln:clean")
.DoesForEach(configurations, configuration => CleanProject("Case.Net.Core.Test.Unit", configuration));

/* Task("artifacts:clean")
.IsDependentOn("artifacts/lib:clean")
.IsDependentOn("artifacts/pkg:clean");

Task("artifacts/lib:clean")
.Description("Clean lib artifacts output folder")
.Does(() => CleanDirectory(buildPaths.LibArtifactsDir));

Task("artifacts/pkg:clean")
.Description("Clean package artifact output folder")
.Does(() => CleanDirectory(buildPaths.PkgArtifactsDir)); */

void CleanProject(string projectName, string configuration) {
  var path = buildPaths.Projects[projectName];

  DotNetClean(path.ToString(), new DotNetCleanSettings {
    Configuration = configuration,
    WorkingDirectory = buildPaths.Cwd
  });
}
