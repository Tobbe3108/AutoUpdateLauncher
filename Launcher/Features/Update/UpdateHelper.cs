using System.IO;
using System.Linq;
using Ardalis.GuardClauses;

namespace Launcher.Features.Update
{
  public static class UpdateHelper
  {
    private static System.Version GetLatestRemoteVersion()
    {
      //TODO Get real version
      return System.Version.Parse("1.0.0.1");
    }

    public static bool IsUpdateAvailable(System.Version? version)
    {
      var remoteVersion = GetLatestRemoteVersion();
      return version != remoteVersion;
    }

    public static void DownloadNewest(string? basePath)
    {
      Guard.Against.NullOrWhiteSpace(basePath, nameof(basePath));
      //TODO What / where to download

      //TODO Real version
      const string? version = "1.0.0.1";
      
      //TODO Downgrade
      //TODO Real download
      Directory.Move(Path.Combine(Directory.GetParent(basePath)?.FullName!, version), Path.Combine(basePath, version));
    }

    public static void DeleteOldVersions(string? basePath, int versionsToKeep)
    {
      Guard.Against.NullOrWhiteSpace(basePath, nameof(basePath));
      Guard.Against.Negative(versionsToKeep, nameof(versionsToKeep));

      var directories = Directory.EnumerateDirectories(basePath).ToList();
      var versions = directories.Where(directory => System.Version.TryParse(new DirectoryInfo(directory).Name, out _));
      var toDelete = versions.SkipLast(1).SkipLast(versionsToKeep);
      foreach (var directory in toDelete)
      {
        Directory.Delete(directory);
      }
    }
  }
}