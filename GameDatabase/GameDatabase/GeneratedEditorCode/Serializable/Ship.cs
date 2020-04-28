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
	public class ShipSerializable : SerializableItem
	{
		public ShipCategory ShipCategory;
		[DefaultValue("")]
		public string Name;
		public int Faction;
		public SizeClass SizeClass;
		[DefaultValue("")]
		public string IconImage;
		public float IconScale;
		[DefaultValue("")]
		public string ModelImage;
		public float ModelScale;
		public Vector2 EnginePosition;
		[DefaultValue("")]
		public string EngineColor;
		public float EngineSize;
		public EngineSerializable[] Engines;
		public float EnergyResistance;
		public float KineticResistance;
		public float HeatResistance;
		public bool Regeneration;
		public float BaseWeightModifier;
		public int[] BuiltinDevices;
		[DefaultValue("")]
		public string Layout;
		public BarrelSerializable[] Barrels;
	}
}
