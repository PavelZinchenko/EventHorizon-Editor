using System;

namespace GameDatabase.Serializable
{
    [Serializable]
    public class SerializableShipSettings : SerializableItem
    {
        public float DefaultWeightPerCell;
        public float MinimumWeightPerCell;

        public float BaseArmorPoints;
        public float ArmorPointsPerCell;
        public float ArmorRepairCooldown;

        public float BaseEnergyPoints;
        public float BaseEnergyRechargeRate;
        public float EnergyRechargeCooldown;

        public float BaseShieldRechargeRate;
        public float ShieldRechargeCooldown;

        public float BaseDroneReconstructionSpeed;

        public float MaxVelocity;
        public float MaxTurnRate;
    }
}
