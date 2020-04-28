//-------------------------------------------------------------------------------
//                                                                               
//    This code was automatically generated.                                     
//    Changes to this file may cause incorrect behavior and will be lost if      
//    the code is regenerated.                                                   
//                                                                               
//-------------------------------------------------------------------------------

using System.Linq;
using EditorDatabase.Enums;
using EditorDatabase.Serializable;
using EditorDatabase.Model;

namespace EditorDatabase.DataModel
{
	public partial class ComponentStats
	{
		partial void OnDataDeserialized(ComponentStatsSerializable serializable, Database database);


		public ComponentStats(ComponentStatsSerializable serializable, Database database)
		{
			Id = new ItemId<ComponentStats>(serializable.Id, serializable.FileName);
			Type = serializable.Type;
			ArmorPoints = new NumericValue<float>(serializable.ArmorPoints, -1000f, 1000f);
			ArmorRepairRate = new NumericValue<float>(serializable.ArmorRepairRate, -1000f, 1000f);
			ArmorRepairCooldownModifier = new NumericValue<float>(serializable.ArmorRepairCooldownModifier, -1f, 1f);
			EnergyPoints = new NumericValue<float>(serializable.EnergyPoints, -1000f, 1000f);
			EnergyRechargeRate = new NumericValue<float>(serializable.EnergyRechargeRate, -1000f, 1000f);
			EnergyRechargeCooldownModifier = new NumericValue<float>(serializable.EnergyRechargeCooldownModifier, -1f, 1f);
			ShieldPoints = new NumericValue<float>(serializable.ShieldPoints, -1000f, 1000f);
			ShieldRechargeRate = new NumericValue<float>(serializable.ShieldRechargeRate, -1000f, 1000f);
			ShieldRechargeCooldownModifier = new NumericValue<float>(serializable.ShieldRechargeCooldownModifier, -1f, 1f);
			Weight = new NumericValue<float>(serializable.Weight, -10000f, 10000f);
			RammingDamage = new NumericValue<float>(serializable.RammingDamage, 0f, 1000f);
			EnergyAbsorption = new NumericValue<float>(serializable.EnergyAbsorption, 0f, 1000f);
			KineticResistance = new NumericValue<float>(serializable.KineticResistance, -1000f, 1000f);
			EnergyResistance = new NumericValue<float>(serializable.EnergyResistance, -1000f, 1000f);
			ThermalResistance = new NumericValue<float>(serializable.ThermalResistance, -1000f, 1000f);
			EnginePower = new NumericValue<float>(serializable.EnginePower, 0f, 20f);
			TurnRate = new NumericValue<float>(serializable.TurnRate, 0f, 20f);
			Autopilot = serializable.Autopilot;
			DroneRangeModifier = new NumericValue<float>(serializable.DroneRangeModifier, -5f, 5f);
			DroneDamageModifier = new NumericValue<float>(serializable.DroneDamageModifier, -5f, 5f);
			DroneDefenseModifier = new NumericValue<float>(serializable.DroneDefenseModifier, -5f, 5f);
			DroneSpeedModifier = new NumericValue<float>(serializable.DroneSpeedModifier, -5f, 5f);
			DronesBuiltPerSecond = new NumericValue<float>(serializable.DronesBuiltPerSecond, 0f, 10f);
			DroneBuildTimeModifier = new NumericValue<float>(serializable.DroneBuildTimeModifier, 0f, 10f);
			WeaponFireRateModifier = new NumericValue<float>(serializable.WeaponFireRateModifier, -1f, 1f);
			WeaponDamageModifier = new NumericValue<float>(serializable.WeaponDamageModifier, -1f, 1f);
			WeaponRangeModifier = new NumericValue<float>(serializable.WeaponRangeModifier, -1f, 1f);
			WeaponEnergyCostModifier = new NumericValue<float>(serializable.WeaponEnergyCostModifier, -1f, 1f);
			AlterWeaponPlatform = serializable.AlterWeaponPlatform;

			OnDataDeserialized(serializable, database);
		}

		public void Save(ComponentStatsSerializable serializable)
		{
			serializable.Type = Type;
			serializable.ArmorPoints = ArmorPoints.Value;
			serializable.ArmorRepairRate = ArmorRepairRate.Value;
			serializable.ArmorRepairCooldownModifier = ArmorRepairCooldownModifier.Value;
			serializable.EnergyPoints = EnergyPoints.Value;
			serializable.EnergyRechargeRate = EnergyRechargeRate.Value;
			serializable.EnergyRechargeCooldownModifier = EnergyRechargeCooldownModifier.Value;
			serializable.ShieldPoints = ShieldPoints.Value;
			serializable.ShieldRechargeRate = ShieldRechargeRate.Value;
			serializable.ShieldRechargeCooldownModifier = ShieldRechargeCooldownModifier.Value;
			serializable.Weight = Weight.Value;
			serializable.RammingDamage = RammingDamage.Value;
			serializable.EnergyAbsorption = EnergyAbsorption.Value;
			serializable.KineticResistance = KineticResistance.Value;
			serializable.EnergyResistance = EnergyResistance.Value;
			serializable.ThermalResistance = ThermalResistance.Value;
			serializable.EnginePower = EnginePower.Value;
			serializable.TurnRate = TurnRate.Value;
			serializable.Autopilot = Autopilot;
			serializable.DroneRangeModifier = DroneRangeModifier.Value;
			serializable.DroneDamageModifier = DroneDamageModifier.Value;
			serializable.DroneDefenseModifier = DroneDefenseModifier.Value;
			serializable.DroneSpeedModifier = DroneSpeedModifier.Value;
			serializable.DronesBuiltPerSecond = DronesBuiltPerSecond.Value;
			serializable.DroneBuildTimeModifier = DroneBuildTimeModifier.Value;
			serializable.WeaponFireRateModifier = WeaponFireRateModifier.Value;
			serializable.WeaponDamageModifier = WeaponDamageModifier.Value;
			serializable.WeaponRangeModifier = WeaponRangeModifier.Value;
			serializable.WeaponEnergyCostModifier = WeaponEnergyCostModifier.Value;
			serializable.AlterWeaponPlatform = AlterWeaponPlatform;
		}

		public readonly ItemId<ComponentStats> Id;

		public ComponentStatsType Type;
		public NumericValue<float> ArmorPoints = new NumericValue<float>(0, -1000f, 1000f);
		public NumericValue<float> ArmorRepairRate = new NumericValue<float>(0, -1000f, 1000f);
		public NumericValue<float> ArmorRepairCooldownModifier = new NumericValue<float>(0, -1f, 1f);
		public NumericValue<float> EnergyPoints = new NumericValue<float>(0, -1000f, 1000f);
		public NumericValue<float> EnergyRechargeRate = new NumericValue<float>(0, -1000f, 1000f);
		public NumericValue<float> EnergyRechargeCooldownModifier = new NumericValue<float>(0, -1f, 1f);
		public NumericValue<float> ShieldPoints = new NumericValue<float>(0, -1000f, 1000f);
		public NumericValue<float> ShieldRechargeRate = new NumericValue<float>(0, -1000f, 1000f);
		public NumericValue<float> ShieldRechargeCooldownModifier = new NumericValue<float>(0, -1f, 1f);
		public NumericValue<float> Weight = new NumericValue<float>(0, -10000f, 10000f);
		public NumericValue<float> RammingDamage = new NumericValue<float>(0, 0f, 1000f);
		public NumericValue<float> EnergyAbsorption = new NumericValue<float>(0, 0f, 1000f);
		public NumericValue<float> KineticResistance = new NumericValue<float>(0, -1000f, 1000f);
		public NumericValue<float> EnergyResistance = new NumericValue<float>(0, -1000f, 1000f);
		public NumericValue<float> ThermalResistance = new NumericValue<float>(0, -1000f, 1000f);
		public NumericValue<float> EnginePower = new NumericValue<float>(0, 0f, 20f);
		public NumericValue<float> TurnRate = new NumericValue<float>(0, 0f, 20f);
		public bool Autopilot;
		public NumericValue<float> DroneRangeModifier = new NumericValue<float>(0, -5f, 5f);
		public NumericValue<float> DroneDamageModifier = new NumericValue<float>(0, -5f, 5f);
		public NumericValue<float> DroneDefenseModifier = new NumericValue<float>(0, -5f, 5f);
		public NumericValue<float> DroneSpeedModifier = new NumericValue<float>(0, -5f, 5f);
		public NumericValue<float> DronesBuiltPerSecond = new NumericValue<float>(0, 0f, 10f);
		public NumericValue<float> DroneBuildTimeModifier = new NumericValue<float>(0, 0f, 10f);
		public NumericValue<float> WeaponFireRateModifier = new NumericValue<float>(0, -1f, 1f);
		public NumericValue<float> WeaponDamageModifier = new NumericValue<float>(0, -1f, 1f);
		public NumericValue<float> WeaponRangeModifier = new NumericValue<float>(0, -1f, 1f);
		public NumericValue<float> WeaponEnergyCostModifier = new NumericValue<float>(0, -1f, 1f);
		public PlatformType AlterWeaponPlatform;

		public static ComponentStats DefaultValue { get; private set; }
	}
}
