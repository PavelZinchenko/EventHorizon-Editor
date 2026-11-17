//-------------------------------------------------------------------------------
//                                                                               
//    This code was automatically generated.                                     
//    Changes to this file may cause incorrect behavior and will be lost if      
//    the code is regenerated.                                                   
//                                                                               
//-------------------------------------------------------------------------------

using System.Linq;
using EditorDatabase.Enums;
using EditorDatabase.Serializable;
using EditorDatabase.Model;

namespace EditorDatabase.DataModel
{
	public partial class FrontierCommonLevel
	{
		partial void OnDataDeserialized(FrontierCommonLevelSerializable serializable, Database database);
		partial void OnDataSerialized(ref FrontierCommonLevelSerializable serializable);

		public static FrontierCommonLevel Create(FrontierCommonLevelSerializable serializable, Database database)
		{
			if (serializable == null) return DefaultValue;
			return new FrontierCommonLevel(serializable, database);
		}

		private FrontierCommonLevel(FrontierCommonLevelSerializable serializable, Database database)
		{
			try
			{
				Id = new ItemId<FrontierCommonLevel>(serializable.Id, serializable.FileName);
				NormalEnemies = serializable.NormalEnemies;
				RandomEnemies = serializable.RandomEnemies;
				CrimsonEnemies = serializable.CrimsonEnemies;
				ExtraFlagships = serializable.ExtraFlagships;
				RandomShipOrder = serializable.RandomShipOrder;
				EnemyPowerMultiplier = serializable.EnemyPowerMultiplier;
				AllowRareShips = serializable.AllowRareShips;
				AllowHiddenShips = serializable.AllowHiddenShips;
				MinActiveEnemies = serializable.MinActiveEnemies;
				MaxActiveEnemies = serializable.MaxActiveEnemies;
				BoostedEnemyCount = new NumericValue<int>(serializable.BoostedEnemyCount, -2147483648, 2147483647);
				ExtraEnemySpawnCooldown = new NumericValue<int>(serializable.ExtraEnemySpawnCooldown, 0, 2147483647);
				MinEnemyDifficultyClass = serializable.MinEnemyDifficultyClass;
				MaxEnemyDifficultyClass = serializable.MaxEnemyDifficultyClass;
				MaxEnemySizeClass = serializable.MaxEnemySizeClass;
			}
			catch (DatabaseException e)
			{
				throw new DatabaseException(this.GetType() + ": deserialization failed. " + serializable.FileName + " (" + serializable.Id + ")", e);
			}
			OnDataDeserialized(serializable, database);
		}

		public void Save(FrontierCommonLevelSerializable serializable)
		{
			serializable.NormalEnemies = NormalEnemies;
			serializable.RandomEnemies = RandomEnemies;
			serializable.CrimsonEnemies = CrimsonEnemies;
			serializable.ExtraFlagships = ExtraFlagships;
			serializable.RandomShipOrder = RandomShipOrder;
			serializable.EnemyPowerMultiplier = EnemyPowerMultiplier;
			serializable.AllowRareShips = AllowRareShips;
			serializable.AllowHiddenShips = AllowHiddenShips;
			serializable.MinActiveEnemies = MinActiveEnemies;
			serializable.MaxActiveEnemies = MaxActiveEnemies;
			serializable.BoostedEnemyCount = BoostedEnemyCount.Value;
			serializable.ExtraEnemySpawnCooldown = ExtraEnemySpawnCooldown.Value;
			serializable.MinEnemyDifficultyClass = MinEnemyDifficultyClass;
			serializable.MaxEnemyDifficultyClass = MaxEnemyDifficultyClass;
			serializable.MaxEnemySizeClass = MaxEnemySizeClass;
			OnDataSerialized(ref serializable);
		}

		public readonly ItemId<FrontierCommonLevel> Id;

		public string NormalEnemies;
		public string RandomEnemies;
		public string CrimsonEnemies;
		public string ExtraFlagships;
		public bool RandomShipOrder;
		public string EnemyPowerMultiplier;
		public string AllowRareShips;
		public string AllowHiddenShips;
		public string MinActiveEnemies;
		public string MaxActiveEnemies;
		public NumericValue<int> BoostedEnemyCount = new NumericValue<int>(0, -2147483648, 2147483647);
		public NumericValue<int> ExtraEnemySpawnCooldown = new NumericValue<int>(0, 0, 2147483647);
		public DifficultyClass MinEnemyDifficultyClass;
		public DifficultyClass MaxEnemyDifficultyClass;
		public string MaxEnemySizeClass;

		public static FrontierCommonLevel DefaultValue { get; private set; }
	}
}
