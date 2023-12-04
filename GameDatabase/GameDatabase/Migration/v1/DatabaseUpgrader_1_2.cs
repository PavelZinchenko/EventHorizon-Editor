using System;

namespace DatabaseMigration.v1
{
    public partial class DatabaseUpgrader
    {
        partial void Migrate_1_2()
        {
            Console.WriteLine("Database migration: v1.1 -> v1.2");
        }
    }
}
