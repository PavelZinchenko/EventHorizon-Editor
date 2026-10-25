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
	public class RuinsSettingsSerializable : SerializableItem
	{
		public int[] ExcludedSatellites;
		[DefaultValue("MAX(6, FLOOR(level/2))")]
		public string MaxSatelliteCells = "MAX(6, FLOOR(level/2))";
	}
}
