using System.Collections.Generic;
using System.IO;
using System.Linq;
using DatabaseMigration;
using EditorDatabase.Model;
using EditorDatabase.Storage;

namespace EditorDatabase.DataModel
{
	// Exclusion lists hold satellite IDs that a partial mod does not carry as records.
	// The generated constructor resolves a reference through local records only, so an
	// inherited ID becomes zero and saving writes zero back. These partial classes read
	// the raw IDs again and keep the number. The built-in catalog is read for labels only.
	public static class SatelliteExclusions
	{
		public static Wrapper<Satellite>[] Resolve(Database database, int[] ids)
		{
			if (ids == null) return null;
			return ids.Select(id => new Wrapper<Satellite> { Item = Item(database, id) }).ToArray();
		}

		private static ItemId<Satellite> Item(Database database, int id)
		{
			var local = database.GetSatelliteId(id);
			if (!local.IsNull || id <= 0) return local;
			var name = BuiltinNames().TryGetValue(id, out var fileName) ? fileName : "Satellite " + id;
			return new ItemId<Satellite>(id, name);
		}

		private static string _loadedPath;
		private static Dictionary<int, string> _builtinNames;

		private static Dictionary<int, string> BuiltinNames()
		{
			var path = MigrationResources.BuiltinDatabasePath;
			if (_builtinNames != null && _loadedPath == path) return _builtinNames;

			_loadedPath = path;
			_builtinNames = new Dictionary<int, string>();
			if (string.IsNullOrEmpty(path) || !Directory.Exists(Path.Combine(path, "Satellite")))
				return _builtinNames;

			var content = new DatabaseContent(new JsonSerializer(), new DatabaseStorage(path));
			foreach (var satellite in content.SatelliteList)
				_builtinNames[satellite.Id] = satellite.FileName;

			return _builtinNames;
		}
	}
}
