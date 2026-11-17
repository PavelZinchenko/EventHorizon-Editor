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
	public class ComponentStatUpgradeSerializable : SerializableItem
	{
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
