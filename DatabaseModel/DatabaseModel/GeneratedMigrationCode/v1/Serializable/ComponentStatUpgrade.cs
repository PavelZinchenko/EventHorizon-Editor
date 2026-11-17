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
	public class ComponentStatUpgradeSerializable : SerializableItem
	{
		public ComponentStatUpgradeSerializable()
		{
			ItemType = ItemType.ComponentStatUpgrade;
			FileName = "ComponentStatUpgrade.json";
		}

		public StatUpgradeType Type;
		[DefaultValue("")]
		public string Name;
		[DefaultValue("")]
		public string Icon;
		public StatValueType ValueType;
		[DefaultValue("level*0.05")]
		public string Value = "level*0.05";
		public int Resource;
		public int Template;
	}
}
