// Usings
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

// Arguments
var target = Argument<string>("target", "Build");
var source = Argument<string>("nugetSource", "");
var version = Argument<string>("targetversion", null);
var skipClean = Argument<bool>("skipclean", false);
var skipTests = Argument<bool>("skiptests", false);
var nogit = Argument<bool>("nogit", false);

var solution = Argument<string>("solution", "NFe.Servicos.csproj");
var assembly = Argument<string>("assembly", "NFe.Servicos/VersionAssemblyInfo.cs");
var tagCsproj = Argument<string>("tagCsproj", "//PropertyGroup/VersionPrefix");

// Variables
var configuration = IsRunningOnWindows() ? "Release" : "MonoRelease";
var csProjectFiles = GetFiles("NFe.Servicos*.csproj");

// Directories
var nuget = Directory(".nuget");
var output = Directory("build");
var outputBinaries = output + Directory("binaries");
var outputPackages = output + Directory("packages");
var outputNuGet = output + Directory("nuget");
var outputPerfResults = Directory("perfResults");

///////////////////////////////////////////////////////////////

Task("Update-Version")
  .Does(() =>
{
  if(string.IsNullOrWhiteSpace(version)) {
    throw new CakeException("No version specified!");
  }

  // CreateAssemblyInfo(assembly, new AssemblyInfoSettings {
  //     Version = version,
  //     FileVersion = version
  // });

  UpdateCsProjectVersion(version, csProjectFiles);
});

public void UpdateCsProjectVersion(string version, FilePathCollection filePaths)
{
  Verbose(logAction => logAction("Setting version to {0}", version));
  foreach (var file in filePaths)
  {
    XmlPoke(file, tagCsproj, version);
  }
}

///////////////////////////////////////////////////////////////

Task("Clean")
  .Does(() =>
{
  // Clean artifact directories.
  CleanDirectories(new DirectoryPath[] {
    output, outputBinaries, outputPackages, outputNuGet
  });

  if(!skipClean) {
    // Clean output directories.
    CleanDirectories("./**/bin/" + configuration);
    CleanDirectories("./**/obj/" + configuration);
  }
});

Task("Restore-NuGet-Packages")
  .Description("Restores dependencies")
  .Does(() =>
{
  DotNetCoreRestore();
  
  int result = StartProcess("dotnet", new ProcessSettings { Arguments = "restore -r win-x64" } );
  if (result != 0)
  {
    throw new CakeException($"Restore failed.");
  }
});

///////////////////////////////////////////////////////////////

Task("Compile")
  .Description("Builds the solution")
  .IsDependentOn("Clean")
  .IsDependentOn("Restore-NuGet-Packages")
  .Does(() =>
{
  int result = StartProcess("dotnet", new ProcessSettings { Arguments = "msbuild " + solution + " /p:Configuration=" + configuration } );
  if (result != 0)
  {
    throw new CakeException($"Compilation failed.");
  }
});

Task("Package-NuGet")
  .Description("Generates NuGet packages for each project that contains a nuspec")
  .Does(() =>
{
  var settings = new DotNetCorePackSettings {
    Configuration = configuration,
    OutputDirectory = outputNuGet,
    ArgumentCustomization = args => args.Append("--include-symbols").Append("-s").Append("--no-build")
  };

  foreach(var project in csProjectFiles)
  {
    DotNetCorePack(project.GetDirectory().FullPath, settings);
  }

});

///////////////////////////////////////////////////////////////
 
Task("Publish-NuGet")
  .Description("Pushes the nuget packages in the nuget folder to a NuGet source. Also publishes the packages into the feeds.")
  .Does(() =>
{
  Information("inf " + outputNuGet.Path.FullPath);
  // Upload every package to the provided NuGet source (defaults to nuget.org).
  var packages = GetFiles(outputNuGet.Path.FullPath + "/*" + version + ".nupkg");
  foreach(var package in packages)
  {
    // Push the package.
   NuGetPush(package, new NuGetPushSettings 
    { 
      ToolPath = ".nuget/nuget.exe",
      Source = source,
      ConfigFile = ".nuget/NuGet.Config",
      ApiKey = "VSTS", 
      Verbosity = NuGetVerbosity.Detailed,
    });
  }
});


Task("Prepare-Release")
  .Does(() =>
{
  // Update version.
  UpdateCsProjectVersion(version, csProjectFiles);

    foreach (var file in csProjectFiles)
    {
      if (nogit)
      {
        Information("git commands");
      }
      else
      {
        // add
        StartProcess("git", new ProcessSettings {
          Arguments = string.Format("add {0}", file.FullPath)
        });

        //commit
        StartProcess("git", new ProcessSettings {
          Arguments = string.Format("commit -m \"Bampe version to {0}\"", version)
        });

        // Tag
        StartProcess("git", new ProcessSettings {
          Arguments = string.Format("tag \"v{0}\"", version)
        });

        //Push
        StartProcess("git", new ProcessSettings {
          Arguments = "push origin master"
        });

        StartProcess("git", new ProcessSettings {
          Arguments = "push --tags"
        });
      }
    }
});

///////////////////////////////////////////////////////////////

Task("Build")
  //.IsDependentOn("Update-Version")
  .IsDependentOn("Compile")
  .IsDependentOn("Package-NuGet")
  .IsDependentOn("Publish-NuGet");

Task("Local")
  .IsDependentOn("Update-Version")
  .IsDependentOn("Compile")
  .IsDependentOn("Package-NuGet");
  //.IsDependentOn("Publish-NuGet");

Task("release")
  .IsDependentOn("Prepare-Release");
  
RunTarget(target);
