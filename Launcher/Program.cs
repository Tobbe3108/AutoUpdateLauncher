using System;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Launcher.Features.Launcher;
using Launcher.Features.Update;
using Launcher.Features.Version;
using Microsoft.Extensions.Configuration;

namespace Launcher
{
  internal static class Program
  {
    private static void Main()
    {
      var launcherConfiguration = GetLauncherConfiguration();
      var localVersion = VersionHelper.GetLatestLocalVersion(launcherConfiguration.BasePath);
      
      Task.Run(() =>
      {
        if (!UpdateHelper.IsUpdateAvailable(localVersion)) return;
        UpdateHelper.DownloadNewest(launcherConfiguration.BasePath);
        UpdateHelper.DeleteOldVersions(launcherConfiguration.BasePath, launcherConfiguration.VersionsToKeep);
        //TODO Windows notification
      });

      LauncherHelper.LaunchVersion(launcherConfiguration?.BasePath, localVersion, launcherConfiguration?.AppName);
    }

    private static LauncherConfiguration GetLauncherConfiguration()
    {
      var config = new ConfigurationBuilder()
        .AddJsonFile($"appsettings.json", false, true)
        .Build();

      LauncherConfiguration? launcherConfiguration = new();
      config.Bind("Launcher", launcherConfiguration);

      Guard.Against.Null(launcherConfiguration, nameof(launcherConfiguration));
      Guard.Against.NullOrWhiteSpace(launcherConfiguration.BasePath, nameof(launcherConfiguration.BasePath));

      launcherConfiguration.BasePath = Environment.ExpandEnvironmentVariables(launcherConfiguration.BasePath);
      return launcherConfiguration;
    }
  }
}