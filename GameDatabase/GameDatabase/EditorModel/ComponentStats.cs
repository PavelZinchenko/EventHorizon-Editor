using GameDatabase.Enums;
using GameDatabase.Model;
using GameDatabase.Serializable;

namespace GameDatabase.EditorModel
{
    public class ComponentStats
    {
        public ComponentStats(SerializableComponentStats stats, Database database)
        {
            ItemId = new ItemId<ComponentStats>(stats.Id, stats.FileName);

            Type = stats.Type;

            ArmorPoints = new NumericValue<float>(stats.ArmorPoints, -1000, 1000);
            ArmorRepairRate = new NumericValue<float>(stats.ArmorRepairRate, -1000, 1000);
            ArmorRepairCooldownModifier = new NumericValue<float>(stats.ArmorRepairCooldownModifier, -1f, 1f);

            HullPoints = new NumericValue<float>(stats.HullPoints, -1000, 1000);
            HullRepairRate = new NumericValue<float>(stats.HullRepairRate, -1000, 1000);
            HullRepairCooldownModifier = new NumericValue<float>(stats.HullRepairCooldownModifier, -1f, 1f);

            EnergyPoints = new NumericValue<float>(stats.EnergyPoints, -1000, 1000);
            EnergyRechargeRate = new NumericValue<float>(stats.EnergyRechargeRate, -1000, 1000);
            EnergyRechargeCooldownModifier = new NumericValue<float>(stats.EnergyRechargeCooldownModifier, -1f, 1f);

            ShieldPoints = new NumericValue<float>(stats.ShieldPoints, -1000, 1000);
            ShieldRechargeRate = new NumericValue<float>(stats.ShieldRechargeRate, -1000, 1000);
            ShieldRechargeCooldownModifier = new NumericValue<float>(stats.ShieldRechargeCooldownModifier, -1f, 1f);

            Weight = new NumericValue<float>(stats.Weight, -10000, 10000);

            KineticResistance = new NumericValue<float>(stats.KineticResistance, -1000, 1000);
            EnergyResistance = new NumericValue<float>(stats.EnergyResistance, -1000, 1000);
            ThermalResistance = new NumericValue<float>(stats.ThermalResistance, -1000, 1000);

            RammingDamage = new NumericValue<float>(stats.RammingDamage, 0, 1000);
            EnergyAbsorption = new NumericValue<float>(stats.EnergyAbsorption, 0, 1000);

            EnginePower = new NumericValue<float>(stats.EnginePower, 0, 20);
            TurnRate = new NumericValue<float>(stats.TurnRate, 0, 20);

            Autopilot = stats.Autopilot;

            DroneDamageModifier = new NumericValue<float>(stats.DroneDamageModifier, -5, 5);
            DroneRangeModifier = new NumericValue<float>(stats.DroneRangeModifier, -5, 5);
            DroneDefenseModifier = new NumericValue<float>(stats.DroneDefenseModifier, -5, 5);
            DroneSpeedModifier = new NumericValue<float>(stats.DroneSpeedModifier, -5, 5);
            DronesBuiltPerSecond = new NumericValue<float>(stats.DronesBuiltPerSecond, 0, 10);
            DroneBuildTimeModifier = new NumericValue<float>(stats.DroneBuildTimeModifier, 0, 10);

            WeaponFireRateModifier = new NumericValue<float>(stats.WeaponFireRateModifier, -1, 1);
            WeaponDamageModifier = new NumericValue<float>(stats.WeaponDamageModifier, -1, 1);
            WeaponRangeModifier = new NumericValue<float>(stats.WeaponRangeModifier, -1, 1);
            WeaponEnergyCostModifier = new NumericValue<float>(stats.WeaponEnergyCostModifier, -1, 1);

            AlterWeaponPlatform = stats.AlterWeaponPlatform;
        }

        public void Save(SerializableComponentStats serializable)
        {
            serializable.Type = Type;

            serializable.ArmorPoints = ArmorPoints.Value;
            serializable.ArmorRepairRate = ArmorRepairRate.Value;
            serializable.ArmorRepairCooldownModifier = ArmorRepairCooldownModifier.Value;

            serializable.HullPoints = HullPoints.Value;
            serializable.HullRepairRate = HullRepairRate.Value;
            serializable.HullRepairCooldownModifier = HullRepairCooldownModifier.Value;

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

        public readonly ItemId<ComponentStats> ItemId;

        public ComponentStatsType Type;

        public NumericValue<float> ArmorPoints;
        public NumericValue<float> ArmorRepairRate;
        public NumericValue<float> ArmorRepairCooldownModifier;

        public NumericValue<float> HullPoints;
        public NumericValue<float> HullRepairRate;
        public NumericValue<float> HullRepairCooldownModifier;

        public NumericValue<float> EnergyPoints;
        public NumericValue<float> EnergyRechargeRate;
        public NumericValue<float> EnergyRechargeCooldownModifier;

        public NumericValue<float> ShieldPoints;
        public NumericValue<float> ShieldRechargeRate;
        public NumericValue<float> ShieldRechargeCooldownModifier;

        public NumericValue<float> Weight;

        public NumericValue<float> RammingDamage;
        public NumericValue<float> EnergyAbsorption;

        public NumericValue<float> KineticResistance;
        public NumericValue<float> EnergyResistance;
        public NumericValue<float> ThermalResistance;

        public NumericValue<float> EnginePower;
        public NumericValue<float> TurnRate;

        public bool Autopilot;
        
        public NumericValue<float> DroneRangeModifier;
        public NumericValue<float> DroneDamageModifier;
        public NumericValue<float> DroneDefenseModifier;
        public NumericValue<float> DroneSpeedModifier;

        public NumericValue<float> DronesBuiltPerSecond;
        public NumericValue<float> DroneBuildTimeModifier;

        public NumericValue<float> WeaponFireRateModifier;
        public NumericValue<float> WeaponDamageModifier;
        public NumericValue<float> WeaponRangeModifier;
        public NumericValue<float> WeaponEnergyCostModifier;

        public PlatformType AlterWeaponPlatform;
    }
}
