using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseMigration.v1.Enums;
using DatabaseMigration.v1.Serializable;
using EditorDatabase.Storage;

namespace DatabaseMigration.v1
{
	public partial class DatabaseUpgrader
	{
		partial void Migrate_7_8()
		{
			Console.WriteLine("Database migration: v1.7 -> v1.8");

			var marketExclusions = new HashSet<int>();
			var ruinsExclusions = new HashSet<int>();

			foreach (var satellite in Content.SatelliteList)
				CollectExclusions(satellite, marketExclusions, ruinsExclusions);

			CollectInheritedExclusions(marketExclusions, ruinsExclusions);

			UpsertSettings(ItemType.BlackMarketSettings, marketExclusions);
			UpsertSettings(ItemType.RuinsSettings, ruinsExclusions);
		}

		// A 1.7 mod cannot ship the new settings records, so the migration always
		// writes them: the mod's own exclusions plus, for a partial mod, the
		// exclusions the built-in satellites had under the old rules.
		private static void CollectExclusions(SatelliteSerializable satellite, HashSet<int> market, HashSet<int> ruins)
		{
			if (satellite.Disabled) return;

			if (satellite.SizeClass == SizeClass.Titan)
			{
				market.Add(satellite.Id);
				ruins.Add(satellite.Id);
			}
			else if (satellite.NotAvailableInBlackMarket)
			{
				market.Add(satellite.Id);
			}
		}

		private void CollectInheritedExclusions(HashSet<int> market, HashSet<int> ruins)
		{
			if (Content.DatabaseSettings != null && Content.DatabaseSettings.UnloadOriginalDatabase)
				return;

			var path = DatabaseMigration.MigrationResources.BuiltinDatabasePath;
			if (string.IsNullOrEmpty(path))
			{
				Console.WriteLine("Database migration: built-in catalog not found, inheriting no satellite exclusions");
				return;
			}

			var overridden = new HashSet<int>(Content.SatelliteList.Select(item => item.Id));

			var builtin = new Storage.DatabaseContent(new JsonSerializer(), new DatabaseStorage(path));
			foreach (var satellite in builtin.SatelliteList)
			{
				if (overridden.Contains(satellite.Id)) continue;
				CollectExclusions(satellite, market, ruins);
			}
		}

		private void UpsertSettings(ItemType type, IEnumerable<int> satellites)
		{
			var ids = string.Join(",", satellites.OrderBy(item => item));
			var json = "{\"ItemType\":" + (int)type + ",\"ExcludedSatellites\":[" + ids + "]}";
			Content.LoadJson(type + ".json", json);
		}
	}
}
