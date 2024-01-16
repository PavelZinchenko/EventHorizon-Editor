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
	public class ExplorationSettingsSerializable : SerializableItem
	{
		public int OutpostShip;
		public int TurretShip;
		public int InfectedPlanetFaction;
		public int HiveShipBuild;
		[DefaultValue("level*2")]
		public string GasCloudDPS = "level*2";
	}
}
