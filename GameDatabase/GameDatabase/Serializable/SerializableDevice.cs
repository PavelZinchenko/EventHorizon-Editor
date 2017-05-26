using System;
using System.ComponentModel;
using GameDatabase.Enums;
using GameDatabase.Model;

namespace GameDatabase.Serializable
{
    [Serializable]
    public class SerializableDevice : SerializableItem
    {
        public DeviceClass DeviceClass;
        public float EnergyConsumption;
        public float PassiveEnergyConsumption;
        public float Power;
        public float Range;
        public float Size;
        public float Cooldown;
        public float Lifetime;
        public Vector2 Offset;

        public ActivationType ActivationType;

        [DefaultValue("")]
        public string Color;
        [DefaultValue("")]
        public string Sound;
        [DefaultValue("")]
        public string EffectPrefab;
        [DefaultValue("")]
        public string ObjectPrefab;
        [DefaultValue("")]
        public string ControlButtonIcon;
    }
}
