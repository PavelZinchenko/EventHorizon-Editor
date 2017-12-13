using GameDatabase.Model;
using GameDatabase.Serializable;

namespace GameDatabase.EditorModel
{
    public class ShipBuilderSettings
    {
        public ShipBuilderSettings(SerializableShipBuilderSettings settings, Database database)
        {
            DefaultWeightPerCell = new NumericValue<float>(settings.DefaultWeightPerCell, 1, 1000);
            MinimumWeightPerCell = new NumericValue<float>(settings.MinimumWeightPerCell, 1, 1000);

            BaseHullPoints = new NumericValue<float>(settings.BaseHullPoints, 0, 10000);
            HullPointsPerCell = new NumericValue<float>(settings.HullPointsPerCell, 0, 10000);
            HullRepairCooldown = new NumericValue<float>(settings.HullRepairCooldown, 0, 60);

            BaseArmorPoints = new NumericValue<float>(settings.BaseArmorPoints, 0, 10000);
            ArmorPointsPerCell = new NumericValue<float>(settings.ArmorPointsPerCell, 0, 10000);
            ArmorRepairCooldown = new NumericValue<float>(settings.ArmorRepairCooldown, 0, 60);

            BaseEnergyPoints = new NumericValue<float>(settings.BaseEnergyPoints, 0, 1000);
            BaseEnergyRechargeRate = new NumericValue<float>(settings.BaseEnergyRechargeRate, 0, 100);
            EnergyRechargeCooldown = new NumericValue<float>(settings.EnergyRechargeCooldown, 0, 60);

            BaseShieldRechargeRate = new NumericValue<float>(settings.BaseShieldRechargeRate, 0, 100);
            ShieldRechargeCooldown = new NumericValue<float>(settings.ShieldRechargeCooldown, 0, 60);

            MaxVelocity = new NumericValue<float>(settings.MaxVelocity, 5, 30);
            MaxTurnRate = new NumericValue<float>(settings.MaxTurnRate, 5, 30);
        }

        public void Save(SerializableShipBuilderSettings serializable)
        {
            serializable.DefaultWeightPerCell = DefaultWeightPerCell.Value;
            serializable.MinimumWeightPerCell = MinimumWeightPerCell.Value;

            serializable.BaseHullPoints = BaseHullPoints.Value;
            serializable.HullPointsPerCell = HullPointsPerCell.Value;
            serializable.HullRepairCooldown = HullRepairCooldown.Value;

            serializable.BaseArmorPoints = BaseArmorPoints.Value;
            serializable.ArmorPointsPerCell = ArmorPointsPerCell.Value;
            serializable.ArmorRepairCooldown = ArmorRepairCooldown.Value;

            serializable.BaseEnergyPoints = BaseEnergyPoints.Value;
            serializable.BaseEnergyRechargeRate = BaseEnergyRechargeRate.Value;
            serializable.EnergyRechargeCooldown = EnergyRechargeCooldown.Value;

            serializable.BaseShieldRechargeRate = BaseShieldRechargeRate.Value;
            serializable.ShieldRechargeCooldown = ShieldRechargeCooldown.Value;

            serializable.MaxVelocity = MaxVelocity.Value;
            serializable.MaxTurnRate = MaxTurnRate.Value;
        }

        public NumericValue<float> DefaultWeightPerCell;
        public NumericValue<float> MinimumWeightPerCell;

        public NumericValue<float> BaseHullPoints;
        public NumericValue<float> HullPointsPerCell;
        public NumericValue<float> HullRepairCooldown;

        public NumericValue<float> BaseArmorPoints;
        public NumericValue<float> ArmorPointsPerCell;
        public NumericValue<float> ArmorRepairCooldown;

        public NumericValue<float> BaseEnergyPoints;
        public NumericValue<float> BaseEnergyRechargeRate;
        public NumericValue<float> EnergyRechargeCooldown;

        public NumericValue<float> BaseShieldRechargeRate;
        public NumericValue<float> ShieldRechargeCooldown;

        public NumericValue<float> MaxVelocity;
        public NumericValue<float> MaxTurnRate;
    }
}
