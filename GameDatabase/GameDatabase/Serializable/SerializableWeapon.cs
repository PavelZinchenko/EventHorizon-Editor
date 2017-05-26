using System;
using System.ComponentModel;
using GameDatabase.Enums;

namespace GameDatabase.Serializable
{
    [Serializable]
    public class SerializableWeapon : SerializableItem
    {
        public WeaponClass WeaponClass;
        public float FireRate;
        public float Spread;
        public int Magazine;

        public ActivationType ActivationType;

        [DefaultValue("")]
        public string ChargeSound;
        [DefaultValue("")]
        public string ShotEffectPrefab;
        [DefaultValue("")]
        public string ControlButtonIcon;
    }
}
