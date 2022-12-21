#load ../build_data.cake

Task("case.net.core:artifacts/lib")
.IsDependentOn("case.net.core:compile")
.DoesForEach(configurations, configuration => {
  var parsedProject = ParseProject(buildPaths.Projects["Case.Net.Core"], configuration);

  parsedProject.GetAssemblyFilePaths()
  .Select(path => path.GetDirectory())
  .ToList()
  .ForEach(path => {
    var framework = path.GetDirectoryName();

    var archiveFilename = framework.ToString() + ".zip";

    var outputPath = buildPaths.LibArtifactsDir
    .Combine("core")
    .Combine(configuration.ToLowerInvariant())
    .CombineWithFilePath(archiveFilename);

    Information("ZIP: {0} => {1}", path, outputPath);

    // ensure output directory exists
    EnsureDirectoryExists(outputPath.GetDirectory());

    Zip(path, outputPath);


    if(AppVeyor.IsRunningOnAppVeyor) {
      var niceName = "Case.Net.Core." + versions.CoreVersion.ToString() + '-' + configuration + '_' + archiveFilename;
      var tempPath = buildPaths.Cwd.CombineWithFilePath(niceName);
      CopyFile(outputPath, tempPath);
      AppVeyor.UploadArtifact(tempPath);
      DeleteFile(tempPath);
    }
  });
});

Task("case.net.core:artifacts/nuget-pack")
.IsDependentOn("case.net.core:compile")
.DoesForEach(configurations, configuration => {
  var outputDir = buildPaths.PkgArtifactsDir.Combine("core").Combine(configuration.ToLowerInvariant());

  DotNetPack(buildPaths.Projects["Case.Net.Core"].ToString(), new DotNetPackSettings {
    Configuration = configuration,
    IncludeSymbols = true,
    NoBuild = true,
    NoLogo = true,
    OutputDirectory = outputDir
  });

  if(AppVeyor.IsRunningOnAppVeyor) {
    var files = GetFiles(outputDir.Combine("*").ToString());
    files.ToList()
    .ForEach(filepath =>
      AppVeyor.UploadArtifact(filepath, settings => settings.SetDeploymentName(filepath.GetFilename().ToString()))
    );
  }
});
