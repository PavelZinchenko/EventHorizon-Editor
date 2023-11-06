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
	public class ShipModSettingsSerializable : SerializableItem
	{
		public bool RemoveWeaponSlotMod;
		public float HeatDefenseValue;
		public float KineticDefenseValue;
		public float EnergyDefenseValue;
		public float RegenerationValue;
		public float RegenerationArmor;
		public float WeightReduction;
		public float AttackReduction;
	}
}
