using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ardalis.GuardClauses;

namespace Launcher.Features.Version
{
  public static class VersionHelper
  {
    public static System.Version? GetLatestLocalVersion(string? basePath)
    {
      Guard.Against.NullOrWhiteSpace(basePath, nameof(basePath));

      var directories = Directory.EnumerateDirectories(basePath).ToList();
      var versions = GetVersions(directories);
      var highestVersion = versions?.Aggregate((highest, next) => next > highest ? next : highest);
      return highestVersion;
    }

    private static IEnumerable<System.Version>? GetVersions(List<string>? directories)
    {
      Guard.Against.Null(directories, nameof(directories));

      foreach (var directory in directories)
      {
        if (System.Version.TryParse(new DirectoryInfo(directory).Name, out var version))
        {
          yield return version;
        }
      }
    }
  }
}