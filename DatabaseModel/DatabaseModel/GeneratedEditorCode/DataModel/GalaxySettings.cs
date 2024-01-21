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
	public partial class GalaxySettings
	{
		partial void OnDataDeserialized(GalaxySettingsSerializable serializable, Database database);
		partial void OnDataSerialized(ref GalaxySettingsSerializable serializable);

		public static GalaxySettings Create(GalaxySettingsSerializable serializable, Database database)
		{
			if (serializable == null) return DefaultValue;
			return new GalaxySettings(serializable, database);
		}

		private GalaxySettings(GalaxySettingsSerializable serializable, Database database)
		{
			AbandonedStarbaseFaction = database.GetFactionId(serializable.AbandonedStarbaseFaction);
			StartingShipBuilds = serializable.StartingShipBuilds?.Select(id => new Wrapper<ShipBuild> { Item = database.GetShipBuildId(id) }).ToArray();
			StartingInventory = database.GetLootId(serializable.StartingInventory);
			SupporterPackShip = database.GetShipBuildId(serializable.SupporterPackShip);
			DefaultStarbaseBuild = database.GetShipBuildId(serializable.DefaultStarbaseBuild);
			MaxEnemyShipsLevel = new NumericValue<int>(serializable.MaxEnemyShipsLevel, 100, 500);
			EnemyLevel = serializable.EnemyLevel;
			ShipMinSpawnDistance = serializable.ShipMinSpawnDistance;
			CaptureStarbaseQuest = database.GetQuestId(serializable.CaptureStarbaseQuest);
			OnDataDeserialized(serializable, database);
		}

		public void Save(GalaxySettingsSerializable serializable)
		{
			serializable.AbandonedStarbaseFaction = AbandonedStarbaseFaction.Value;
			if (StartingShipBuilds == null || StartingShipBuilds.Length == 0)
			    serializable.StartingShipBuilds = null;
			else
			    serializable.StartingShipBuilds = StartingShipBuilds.Select(wrapper => wrapper.Item.Value).ToArray();
			serializable.StartingInventory = StartingInventory.Value;
			serializable.SupporterPackShip = SupporterPackShip.Value;
			serializable.DefaultStarbaseBuild = DefaultStarbaseBuild.Value;
			serializable.MaxEnemyShipsLevel = MaxEnemyShipsLevel.Value;
			serializable.EnemyLevel = EnemyLevel;
			serializable.ShipMinSpawnDistance = ShipMinSpawnDistance;
			serializable.CaptureStarbaseQuest = CaptureStarbaseQuest.Value;
			OnDataSerialized(ref serializable);
		}

		public ItemId<Faction> AbandonedStarbaseFaction = ItemId<Faction>.Empty;
		public Wrapper<ShipBuild>[] StartingShipBuilds;
		public ItemId<LootModel> StartingInventory = ItemId<LootModel>.Empty;
		public ItemId<ShipBuild> SupporterPackShip = ItemId<ShipBuild>.Empty;
		public ItemId<ShipBuild> DefaultStarbaseBuild = ItemId<ShipBuild>.Empty;
		public NumericValue<int> MaxEnemyShipsLevel = new NumericValue<int>(0, 100, 500);
		public string EnemyLevel;
		public string ShipMinSpawnDistance;
		public ItemId<QuestModel> CaptureStarbaseQuest = ItemId<QuestModel>.Empty;

		public static GalaxySettings DefaultValue { get; private set; }
	}
}
