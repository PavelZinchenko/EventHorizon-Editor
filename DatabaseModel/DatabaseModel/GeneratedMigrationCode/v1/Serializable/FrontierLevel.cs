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
	public class FrontierLevelSerializable : SerializableItem
	{
		public FrontierLevelSerializable()
		{
			ItemType = ItemType.FrontierLevel;
			FileName = "FrontierLevel.json";
		}

		public FrontierLevelType Type;
		public int Faction;
		public int Difficulty;
		public int MinimumMapLevel;
		public int Reward;
	}
}
