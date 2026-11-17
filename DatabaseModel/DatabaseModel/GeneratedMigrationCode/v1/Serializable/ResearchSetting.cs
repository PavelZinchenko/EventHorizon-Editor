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
	public class ResearchSettingSerializable : SerializableItem
	{
		public ResearchSettingSerializable()
		{
			ItemType = ItemType.ResearchSetting;
			FileName = "ResearchSetting.json";
		}

		public int ResearchResource1;
		public int ResearchResource2;
		public int ResearchResource3;
		public int ResearchResource4;
		public int ResearchResource5;
	}
}
