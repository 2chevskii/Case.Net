#load build/build_data.cake
#load build/tasks/*

Setup(context => {
  if(AppVeyor.IsRunningOnAppVeyor) {
      Verbose("Performing setup action in AppVeyor environment");

      if(AppVeyor.Environment.Repository.Tag.IsTag) {
        var tagName = AppVeyor.Environment.Repository.Tag.Name;

        Verbose("Running a tag build: {0}", tagName);

        const string coreTagPrefix = "case.net/core/";

        if(tagName.StartsWith(coreTagPrefix)) {
            var tagVersion = tagName.Substring(coreTagPrefix.Length);
        } /* else if(tagName.StartsWith("case.net/cli/")) {

        } */
        else {
          throw new CakeException("Invalid tag name: " + tagName);
        }
      }
  }
});

Teardown(context => {});

var report = RunTarget(targetName);
