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
	public class GameObjectPrefabSerializable : SerializableItem
	{
		public GameObjectPrefabSerializable()
		{
			ItemType = ItemType.GameObjectPrefab;
			FileName = "GameObjectPrefab.json";
		}

		public ObjectPrefabType Type;
		[DefaultValue("")]
		public string Image1;
		[DefaultValue("")]
		public string Image2;
	}
}
