using System;
using System.ComponentModel;
using GameDatabase.Enums;
using GameDatabase.Model;

namespace GameDatabase.Serializable
{
    [Serializable]
    public class SerializableAmmunition : SerializableItem
    {
        public AmmunitionClass AmmunitionClass;
        public DamageType DamageType;
        public float Weight;
        public float Size;
        public Vector2 InitialPosition;
        public float AreaOfEffect;
        public float Damage;
        public float Range;
        public float Velocity;
        public float LifeTime;
        public int HitPoints;
        public bool IgnoresShipVelocity;
        public float EnergyCost;
        public int CoupledAmmunitionId;

        [DefaultValue("")]
        public string Color;

        [DefaultValue("")]
        public string FireSound;
        [DefaultValue("")]
        public string HitSound;
        [DefaultValue("")]
        public string HitEffectPrefab;
        [DefaultValue("")]
        public string BulletPrefab;
    }
}
