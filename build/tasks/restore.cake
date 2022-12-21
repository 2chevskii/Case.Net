#load ../build_data.cake

Task("sln:restore")
.Description("Restore all projects")
.IsDependentOn("case.net.core:restore")
.IsDependentOn("case.net.core.test.unit:restore");

Task("case.net.core:restore")
.Description("Restore Case.Net.Core project")
.Does(() => RestoreProject("Case.Net.Core"));

Task("case.net.core.test.unit:restore")
.Description("Restore Case.Net.Core.Test.Unit project")
.Does(() => RestoreProject("Case.Net.Core.Test.Unit"));


void RestoreProject(string projectName) {
  var path = buildPaths.Projects[projectName].ToString();

  DotNetRestore(path, new DotNetRestoreSettings {
    NoDependencies = true,
    WorkingDirectory = buildPaths.Cwd
  });
}
