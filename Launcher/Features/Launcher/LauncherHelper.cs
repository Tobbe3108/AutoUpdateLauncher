using System.Diagnostics;
using System.IO;
using Ardalis.GuardClauses;

namespace Launcher.Features.Launcher
{
  public static class LauncherHelper
  {
    public static void LaunchVersion(string? basePath, System.Version? version, string? appName)
    {
      Guard.Against.NullOrWhiteSpace(basePath, nameof(basePath));
      Guard.Against.Null(version, nameof(version));
      Guard.Against.NullOrWhiteSpace(appName, nameof(appName));

      var appPath = Path.Combine(basePath, version.ToString(), appName);
      Process.Start(appPath);
    }
  }
}