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
	public class FrontierPredefinedLevelSerializable : SerializableItem
	{
		public FrontierPredefinedLevelSerializable()
		{
			ItemType = ItemType.FrontierPredefinedLevel;
			FileName = "FrontierPredefinedLevel.json";
		}

		public int LeveSettings;
		public int[] Factions;
	}
}
