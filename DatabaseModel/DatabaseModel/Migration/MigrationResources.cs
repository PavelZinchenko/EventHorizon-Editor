using System;
using System.IO;

namespace DatabaseMigration
{
    // Migrations that need the built-in catalog use this path. The editor
    // cannot read Unity Resources, so the catalog is located on disk instead:
    // the editor ships inside the game repository and the game scans Mods/
    // inside the repository too, so walking up from the opened database and
    // from the executable finds Assets/Modules/Database/Resources/Database.
    public static class MigrationResources
    {
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

        private static string WalkUp(string anchor)
        {
            if (string.IsNullOrEmpty(anchor))
                return null;

            var dir = File.Exists(anchor) ? new FileInfo(anchor).Directory : new DirectoryInfo(anchor);
            if (dir == null || !dir.Exists)
                return null;

            while (dir != null)
            {
                var candidate = Path.Combine(dir.FullName, "Assets", "Modules", "Database", "Resources", "Database");
                if (Directory.Exists(candidate))
                    return candidate;
                dir = dir.Parent;
            }

            return null;
        }
    }
}
