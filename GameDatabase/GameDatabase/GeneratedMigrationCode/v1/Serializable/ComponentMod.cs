//-------------------------------------------------------------------------------
//                                                                               
//    This code was automatically generated.                                     
//    Changes to this file may cause incorrect behavior and will be lost if      
//    the code is regenerated.                                                   
//                                                                               
//-------------------------------------------------------------------------------

using System;
using EditorDatabase.Model;
using DatabaseMigration.v1.Enums;

namespace DatabaseMigration.v1.Serializable
{
	[Serializable]
	public class ComponentModSerializable : SerializableItem
	{
		public ComponentModSerializable()
		{
			ItemType = ItemType.ComponentMod;
			FileName = $"{ItemType}.json";
		}

		public ComponentModType Type;
	}
}
