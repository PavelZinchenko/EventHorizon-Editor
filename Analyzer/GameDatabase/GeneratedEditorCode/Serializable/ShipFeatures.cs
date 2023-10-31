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
	public struct ShipFeaturesSerializable
	{
		public float EnergyResistance;
		public float KineticResistance;
		public float HeatResistance;
		public float WeightBonus;
		public float VelocityBonus;
		public float TurnRateBonus;
		public float ArmorBonus;
		public float ShieldBonus;
		public float EnergyBonus;
		public bool Regeneration;
		public int[] BuiltinDevices;
	}
}
