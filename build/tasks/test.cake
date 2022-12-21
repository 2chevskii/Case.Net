#load ../build_data.cake

Task("case.net.core:test/unit")
.IsDependentOn("case.net.core.test.unit:compile")
.DoesForEach(configurations, configuration => TestProject("Case.Net.Core.Test.Unit", configuration));

void TestProject(string projectName, string configuration) {
  var path = buildPaths.Projects[projectName];

  var logFilename = projectName + '.' + versions.CoreVersion.ToString() + '-' + configuration + '_' + DateTimeOffset.UtcNow.ToUnixTimeSeconds() + ".xml";

  DotNetTest(path.ToString(), new DotNetTestSettings {
    Configuration = configuration,
    NoBuild = true,
    ResultsDirectory = buildPaths.TestResultsDir,
    Loggers = {
      $"trx;LogFileName={logFilename}"
    }
  });

  if(AppVeyor.IsRunningOnAppVeyor) {
    var logfilePath = buildPaths.TestResultsDir.CombineWithFilePath(logFilename);

    AppVeyor.UploadTestResults(logfilePath, AppVeyorTestResultsType.MSTest);
  }
}
