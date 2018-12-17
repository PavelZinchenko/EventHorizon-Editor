using System;
using System.ComponentModel;
using GameDatabase.Enums;

namespace GameDatabase.Serializable
{
    [Serializable]
    public class SerializableDroneBay : SerializableItem
    {
        public float EnergyConsumption;
        public float PassiveEnergyConsumption;
        public float Range;
        public float DamageMultiplier;
        public float DefenseMultiplier;
        public float SpeedMultiplier;
        public bool ImprovedAi;
        public int Capacity;

        public ActivationType ActivationType;

        [DefaultValue("")]
        public string LaunchSound;
        [DefaultValue("")]
        public string LaunchEffectPrefab;
        [DefaultValue("")]
        public string ControlButtonIcon;
    }
}
