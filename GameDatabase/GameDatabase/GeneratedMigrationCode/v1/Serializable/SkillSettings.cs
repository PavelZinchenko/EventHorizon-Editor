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
	public class SkillSettingsSerializable : SerializableItem
	{
		public SkillSettingsSerializable()
		{
			ItemType = ItemType.SkillSettings;
			FileName = "SkillSettings.json";
		}

		public int[] BeatAllEnemiesFactionList;
		public bool DisableExceedTheLimits;
		[DefaultValue("")]
		public string FuelTankCapacity;
		[DefaultValue("")]
		public string AttackBonus;
		[DefaultValue("")]
		public string DefenseBonus;
		[DefaultValue("")]
		public string ShieldStrengthBonus;
		[DefaultValue("")]
		public string ShieldRechargeBonus;
		[DefaultValue("")]
		public string ExperienceBonus;
		[DefaultValue("")]
		public string FlightSpeed;
		[DefaultValue("")]
		public string FlightRange;
		[DefaultValue("")]
		public string ExplorationLootBonus;
		[DefaultValue("")]
		public string HeatResistance;
		[DefaultValue("")]
		public string KineticResistance;
		[DefaultValue("")]
		public string EnergyResistance;
		[DefaultValue("")]
		public string MerchantPriceFactor;
		[DefaultValue("")]
		public string CraftingPriceFactor;
		[DefaultValue("")]
		public string CraftingLevelReduction;
		[DefaultValue(200)]
		public int IncreasedLevelLimit = 200;
		[DefaultValue(100)]
		public int BaseFuelCapacity = 100;
		[DefaultValue(1.5f)]
		public float BaseFlightRange = 1.5f;
		[DefaultValue(1f)]
		public float BaseFlightSpeed = 1f;
	}
}
