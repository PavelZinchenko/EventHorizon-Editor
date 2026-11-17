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
	public class FrontierNpcSettingsSerializable : SerializableItem
	{
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
