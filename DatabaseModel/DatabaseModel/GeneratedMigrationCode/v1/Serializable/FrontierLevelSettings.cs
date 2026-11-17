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
	public class FrontierLevelSettingsSerializable : SerializableItem
	{
		public FrontierLevelSettingsSerializable()
		{
			ItemType = ItemType.FrontierLevelSettings;
			FileName = "FrontierLevelSettings.json";
		}

		[DefaultValue(40)]
		public int HardMinLevel = 40;
		[DefaultValue(100)]
		public int ExtremeMinLevel = 100;
		[DefaultValue(200)]
		public int InsaneMinLevel = 200;
		public int TutorialLevel;
		public int EasyLevel;
		public int MediumLevel;
		public int NormalLevel;
		public int HardLevel;
		public int ExtremeLevel;
		public int InsaneLevel;
	}
}
