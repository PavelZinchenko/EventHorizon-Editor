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
	public class GalaxySettingsSerializable : SerializableItem
	{
		public int AbandonedStarbaseFaction;
		public int[] StartingShipBuilds;
		public int StartingInvenory;
		public int SupporterPackShip;
		public int DefaultStarbaseBuild;
		[DefaultValue(300)]
		public int MaxEnemyShipsLevel = 300;
		[DefaultValue("MIN(3*distance/5 - 5, MaxEnemyShipsLevel)")]
		public string EnemyLevel = "MIN(3*distance/5 - 5, MaxEnemyShipsLevel)";
	}
}
