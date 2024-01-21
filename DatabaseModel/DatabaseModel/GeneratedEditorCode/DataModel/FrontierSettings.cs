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
	public partial class FrontierSettings
	{
		partial void OnDataDeserialized(FrontierSettingsSerializable serializable, Database database);
		partial void OnDataSerialized(ref FrontierSettingsSerializable serializable);

		public static FrontierSettings Create(FrontierSettingsSerializable serializable, Database database)
		{
			if (serializable == null) return DefaultValue;
			return new FrontierSettings(serializable, database);
		}

		private FrontierSettings(FrontierSettingsSerializable serializable, Database database)
		{
			BaseCommandPoints = new NumericValue<int>(serializable.BaseCommandPoints, 0, 2147483647);
			MaxExtraCommandPoints = new NumericValue<int>(serializable.MaxExtraCommandPoints, 0, 2147483647);
			SupporterPackShip = database.GetShipId(serializable.SupporterPackShip);
			FalconPackShip = database.GetShipId(serializable.FalconPackShip);
			BigBossEasyBuild = database.GetShipBuildId(serializable.BigBossEasyBuild);
			BigBossNormalBuild = database.GetShipBuildId(serializable.BigBossNormalBuild);
			BigBossHardBuild = database.GetShipBuildId(serializable.BigBossHardBuild);
			DemoSceneStarbaseBuild = database.GetShipBuildId(serializable.DemoSceneStarbaseBuild);
			TutorialStarbaseBuild = database.GetShipBuildId(serializable.TutorialStarbaseBuild);
			DefaultStarbaseBuild = database.GetShipBuildId(serializable.DefaultStarbaseBuild);
			ExplorationStarbase = database.GetShipId(serializable.ExplorationStarbase);
			MerchantShipBuild = database.GetShipBuildId(serializable.MerchantShipBuild);
			SmugglerShipBuild = database.GetShipBuildId(serializable.SmugglerShipBuild);
			EngineerShipBuild = database.GetShipBuildId(serializable.EngineerShipBuild);
			MercenaryShipBuild = database.GetShipBuildId(serializable.MercenaryShipBuild);
			ShipyardShipBuild = database.GetShipBuildId(serializable.ShipyardShipBuild);
			SantaShipBuild = database.GetShipBuildId(serializable.SantaShipBuild);
			SalvageDroneBuild = database.GetShipBuildId(serializable.SalvageDroneBuild);
			CustomShipLevels = serializable.CustomShipLevels?.Select(item => ShipToValue.Create(item, database)).ToArray();
			CustomShipPrices = serializable.CustomShipPrices?.Select(item => ShipToValue.Create(item, database)).ToArray();
			ExplorationShips = serializable.ExplorationShips?.Select(id => new Wrapper<Ship> { Item = database.GetShipId(id) }).ToArray();
			OnDataDeserialized(serializable, database);
		}

		public void Save(FrontierSettingsSerializable serializable)
		{
			serializable.BaseCommandPoints = BaseCommandPoints.Value;
			serializable.MaxExtraCommandPoints = MaxExtraCommandPoints.Value;
			serializable.SupporterPackShip = SupporterPackShip.Value;
			serializable.FalconPackShip = FalconPackShip.Value;
			serializable.BigBossEasyBuild = BigBossEasyBuild.Value;
			serializable.BigBossNormalBuild = BigBossNormalBuild.Value;
			serializable.BigBossHardBuild = BigBossHardBuild.Value;
			serializable.DemoSceneStarbaseBuild = DemoSceneStarbaseBuild.Value;
			serializable.TutorialStarbaseBuild = TutorialStarbaseBuild.Value;
			serializable.DefaultStarbaseBuild = DefaultStarbaseBuild.Value;
			serializable.ExplorationStarbase = ExplorationStarbase.Value;
			serializable.MerchantShipBuild = MerchantShipBuild.Value;
			serializable.SmugglerShipBuild = SmugglerShipBuild.Value;
			serializable.EngineerShipBuild = EngineerShipBuild.Value;
			serializable.MercenaryShipBuild = MercenaryShipBuild.Value;
			serializable.ShipyardShipBuild = ShipyardShipBuild.Value;
			serializable.SantaShipBuild = SantaShipBuild.Value;
			serializable.SalvageDroneBuild = SalvageDroneBuild.Value;
			if (CustomShipLevels == null || CustomShipLevels.Length == 0)
			    serializable.CustomShipLevels = null;
			else
			    serializable.CustomShipLevels = CustomShipLevels.Select(item => item.Serialize()).ToArray();
			if (CustomShipPrices == null || CustomShipPrices.Length == 0)
			    serializable.CustomShipPrices = null;
			else
			    serializable.CustomShipPrices = CustomShipPrices.Select(item => item.Serialize()).ToArray();
			if (ExplorationShips == null || ExplorationShips.Length == 0)
			    serializable.ExplorationShips = null;
			else
			    serializable.ExplorationShips = ExplorationShips.Select(wrapper => wrapper.Item.Value).ToArray();
			OnDataSerialized(ref serializable);
		}

		public NumericValue<int> BaseCommandPoints = new NumericValue<int>(0, 0, 2147483647);
		public NumericValue<int> MaxExtraCommandPoints = new NumericValue<int>(0, 0, 2147483647);
		public ItemId<Ship> SupporterPackShip = ItemId<Ship>.Empty;
		public ItemId<Ship> FalconPackShip = ItemId<Ship>.Empty;
		public ItemId<ShipBuild> BigBossEasyBuild = ItemId<ShipBuild>.Empty;
		public ItemId<ShipBuild> BigBossNormalBuild = ItemId<ShipBuild>.Empty;
		public ItemId<ShipBuild> BigBossHardBuild = ItemId<ShipBuild>.Empty;
		public ItemId<ShipBuild> DemoSceneStarbaseBuild = ItemId<ShipBuild>.Empty;
		public ItemId<ShipBuild> TutorialStarbaseBuild = ItemId<ShipBuild>.Empty;
		public ItemId<ShipBuild> DefaultStarbaseBuild = ItemId<ShipBuild>.Empty;
		public ItemId<Ship> ExplorationStarbase = ItemId<Ship>.Empty;
		public ItemId<ShipBuild> MerchantShipBuild = ItemId<ShipBuild>.Empty;
		public ItemId<ShipBuild> SmugglerShipBuild = ItemId<ShipBuild>.Empty;
		public ItemId<ShipBuild> EngineerShipBuild = ItemId<ShipBuild>.Empty;
		public ItemId<ShipBuild> MercenaryShipBuild = ItemId<ShipBuild>.Empty;
		public ItemId<ShipBuild> ShipyardShipBuild = ItemId<ShipBuild>.Empty;
		public ItemId<ShipBuild> SantaShipBuild = ItemId<ShipBuild>.Empty;
		public ItemId<ShipBuild> SalvageDroneBuild = ItemId<ShipBuild>.Empty;
		public ShipToValue[] CustomShipLevels;
		public ShipToValue[] CustomShipPrices;
		public Wrapper<Ship>[] ExplorationShips;

		public static FrontierSettings DefaultValue { get; private set; }
	}
}
