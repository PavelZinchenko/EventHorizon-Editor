using System;
using System.IO;

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
        private static readonly string[] Candidates =
        {
            Path.Combine("Assets", "Modules", "Database", "Resources", "Database"),
            "Database",
        };

        public static string BuiltinDatabasePath { get; set; }

        public static void LocateBuiltinDatabase(params string[] anchors)
        {
            foreach (var anchor in anchors)
            {
                var path = WalkUp(anchor);
                if (path == null) continue;
                BuiltinDatabasePath = path;
                return;
            }
        }

        // A folder counts as the built-in catalog only when it carries satellite
        // records, so an unrelated Database folder is never mistaken for it.
        private static bool IsBuiltinCatalog(string path) =>
            Directory.Exists(Path.Combine(path, "Satellite"));

        private static string WalkUp(string anchor)
        {
            if (string.IsNullOrEmpty(anchor))
                return null;

            var dir = File.Exists(anchor) ? new FileInfo(anchor).Directory : new DirectoryInfo(anchor);
            if (dir == null || !dir.Exists)
                return null;

            while (dir != null)
            {
                foreach (var candidate in Candidates)
                {
                    var path = Path.Combine(dir.FullName, candidate);
                    if (IsBuiltinCatalog(path))
                        return path;
                }

                dir = dir.Parent;
            }

            return null;
        }
    }
}
