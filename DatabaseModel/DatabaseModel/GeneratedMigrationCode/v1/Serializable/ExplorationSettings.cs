//-------------------------------------------------------------------------------
//                                                                               
//    This code was automatically generated.                                     
//    Changes to this file may cause incorrect behavior and will be lost if      
//    the code is regenerated.                                                   
//                                                                               
//-------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using EditorDatabase.Model;
using DatabaseMigration.v1.Enums;

namespace DatabaseMigration.v1.Serializable
{
	[Serializable]
	public class ExplorationSettingsSerializable : SerializableItem
	{
		public ExplorationSettingsSerializable()
		{
			ItemType = ItemType.ExplorationSettings;
			FileName = "ExplorationSettings.json";
		}

		public int OutpostShip;
		public int TurretShip;
		public int InfectedPlanetFaction;
		public int HiveShipBuild;
		[DefaultValue("MIN(level*2,500)")]
		public string GasCloudDPS = "MIN(level*2,500)";
	}
}
