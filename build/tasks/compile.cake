#load ../build_data.cake

Task("sln:compile")
.Description("Compile all projects")
.IsDependentOn("case.net.core:compile")
.IsDependentOn("case.net.core.test.unit:compile");

Task("case.net.core:compile")
.Description("Compile project: Case.Net")
.IsDependentOn("case.net.core:clean")
.IsDependentOn("case.net.core:restore")
.DoesForEach(configurations, configuration => CompileProject("Case.Net.Core", configuration));

Task("case.net.core.test.unit:compile")
.Description("Compile project: Case.Net.Test")
.IsDependentOn("case.net.core.test.unit:clean")
.IsDependentOn("case.net.core.test.unit:restore")
.IsDependentOn("case.net.core:compile")
.DoesForEach(configurations, configuration => CompileProject("Case.Net.Core.Test.Unit", configuration));

void CompileProject(string projectName, string configuration) {
  var path = buildPaths.Projects[projectName].ToString();

  Information("Compiling project {0} under \"{1}\" configuration...", projectName, configuration);

  DotNetBuild(path, new DotNetBuildSettings {
    NoDependencies = true, // Dependencies are built in their respective tasks
    NoLogo = true,
    NoRestore = true, // Restore is done through separate task
    Configuration = configuration,
    WorkingDirectory = Context.Environment.WorkingDirectory
  });
}
