using System;
using System.ComponentModel;
using GameDatabase.Enums;

namespace GameDatabase.Serializable
{
    [Serializable]
    public class SerializableAmmunition : SerializableItem
    {
        public BulletBody Body;
        public BulletTrigger[] Triggers;

        public BulletImpactType ImpactType;
        public ImpactEffect[] Effects;

        [Serializable]
        public struct BulletBody
        {
            public BulletType Type;
            public float Size;
            public float Velocity;
            public float Range;
            public float Lifetime;
            public float Weight;
            public int HitPoints;
            [DefaultValue("")]
            public string Color;
            public int BulletPrefab;
            public float EnergyCost;
            public bool CanBeDisarmed;
            public bool FriendlyFire;
        }

        [Serializable]
        public struct BulletTrigger
        {
            public BulletTriggerCondition Condition;
            public BulletEffectType EffectType;

            public int VisualEffect;
            [DefaultValue("")]
            public string AudioClip;
            public int Ammunition;

            [DefaultValue("")]
            public string Color;
            public ColorMode ColorMode;

            public int Quantity;
            public float Size;
            public float Lifetime;
            public float Cooldown;
            public float RandomFactor;
            public float PowerMultiplier;
            public int MaxNestingLevel;
        }

        [Serializable]
        public struct ImpactEffect
        {
            public ImpactEffectType Type;
            public DamageType DamageType;
            public float Power;
            public float Factor;
        }
    }
}
