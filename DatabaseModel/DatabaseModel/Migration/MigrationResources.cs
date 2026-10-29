using System;
using System.IO;
using System.Linq;

namespace DatabaseMigration
{
    // Migrations that need the built-in catalog use this path. The editor cannot
    // read Unity Resources, so the catalog is located on disk instead. Two layouts
    // are searched: a game checkout, where the catalog sits under
    // Assets/Modules/Database/Resources/Database, and a packaged release, where a
    // Database folder ships beside the executable. MainWindow can also set the
    // path from a folder picker when both searches miss.
    public static class MigrationResources
    {
        private static readonly string[] GameCheckoutCandidates =
        {
            Path.Combine("Assets", "Modules", "Database", "Resources", "Database"),
        };

        private static readonly string[] PackageCandidates =
        {
            "Database",
        };

        public static string BuiltinDatabasePath { get; set; }
        public static string OpenedDatabasePath { get; private set; }

        public static void LocateBuiltinDatabase(params string[] anchors)
        {
            BuiltinDatabasePath = null;
            OpenedDatabasePath = anchors.Length > 0 ? NormalizePath(anchors[0]) : null;

            foreach (var anchor in anchors)
            {
                bool isBaseDirectory = anchors.Length > 1 && anchor == anchors[anchors.Length - 1];
                var path = WalkUp(anchor, isBaseDirectory);
                if (path == null) continue;

                var normalizedPath = NormalizePath(path);

                // Reject if the found catalog is the opened mod itself.
                if (OpenedDatabasePath != null &&
                    normalizedPath.StartsWith(OpenedDatabasePath, StringComparison.OrdinalIgnoreCase))
                    continue;

                BuiltinDatabasePath = path;
                return;
            }
        }

        // A folder counts as the built-in catalog only when it carries satellite
        // records, so an unrelated Database folder is never mistaken for it.
        private static bool IsBuiltinCatalog(string path) =>
            Directory.Exists(Path.Combine(path, "Satellite"));

        private static string WalkUp(string anchor, bool allowPackageCandidates)
        {
            if (string.IsNullOrEmpty(anchor))
                return null;

            var dir = File.Exists(anchor) ? new FileInfo(anchor).Directory : new DirectoryInfo(anchor);
            if (dir == null || !dir.Exists)
                return null;

            int level = 0;
            while (dir != null)
            {
                // Game-checkout discovery: probe all ancestors.
                foreach (var candidate in GameCheckoutCandidates)
                {
                    var path = Path.Combine(dir.FullName, candidate);
                    if (IsBuiltinCatalog(path))
                        return path;
                }

                // Generic Database candidate: only at supported package locations
                // (beside the executable or beside its bin directory).
                if (allowPackageCandidates && level <= 1)
                {
                    foreach (var candidate in PackageCandidates)
                    {
                        var path = Path.Combine(dir.FullName, candidate);
                        if (IsBuiltinCatalog(path))
                            return path;
                    }
                }

                dir = dir.Parent;
                level++;
            }

            return null;
        }

        public static string NormalizePath(string path)
        {
            if (string.IsNullOrEmpty(path)) return null;
            try
            {
                return Path.GetFullPath(path).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            }
            catch
            {
                return path;
            }
        }
    }
}
