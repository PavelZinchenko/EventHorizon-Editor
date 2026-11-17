//-------------------------------------------------------------------------------
//                                                                               
//    This code was automatically generated.                                     
//    Changes to this file may cause incorrect behavior and will be lost if      
//    the code is regenerated.                                                   
//                                                                               
//-------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using EditorDatabase.Enums;
using EditorDatabase.Model;

namespace EditorDatabase.Serializable
{
	[Serializable]
	public class FrontierSettingsSerializable : SerializableItem
	{
		[DefaultValue(5)]
		public int BaseCommandPoints = 5;
		[DefaultValue(25)]
		public int MaxExtraCommandPoints = 25;
		[DefaultValue(30)]
		public int MaxMercenaryCount = 30;
		public int SupporterPackShip;
		public int FalconPackShip;
		public int BigBossEasyBuild;
		public int BigBossNormalBuild;
		public int BigBossHardBuild;
		public int DemoSceneStarbaseBuild;
		public int TutorialStarbaseBuild;
		public int DefaultStarbaseBuild;
		public int ExplorationStarbase;
		public int MerchantShipBuild;
		public int SmugglerShipBuild;
		public int EngineerShipBuild;
		public int MercenaryShipBuild;
		public int ShipyardShipBuild;
		public int SantaShipBuild;
		public int ScientistShipBuild;
		public int SalvageDroneBuild;
		public ShipToValueSerializable[] CustomShipLevels;
		public ShipToValueSerializable[] CustomShipPrices;
		public int[] ExplorationShips;
	}
}
