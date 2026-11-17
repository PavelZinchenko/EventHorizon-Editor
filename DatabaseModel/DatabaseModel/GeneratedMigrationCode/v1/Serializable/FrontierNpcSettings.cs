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
	public class FrontierNpcSettingsSerializable : SerializableItem
	{
		public FrontierNpcSettingsSerializable()
		{
			ItemType = ItemType.FrontierNpcSettings;
			FileName = "FrontierNpcSettings.json";
		}

		[DefaultValue(2)]
		public int MerchantAppearLevel = 2;
		[DefaultValue(5)]
		public int SmugglerAppearLevel = 5;
		[DefaultValue(10)]
		public int ShipyardAppearLevel = 10;
		[DefaultValue(15)]
		public int EngineerAppearLevel = 15;
	}
}
