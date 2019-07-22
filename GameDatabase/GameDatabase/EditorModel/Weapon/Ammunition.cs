using System;
using System.Drawing;
using System.Linq;
using GameDatabase.Enums;
using GameDatabase.Model;
using GameDatabase.Serializable;

namespace GameDatabase.EditorModel
{
    public class Ammunition
    {
        public Ammunition(SerializableAmmunition serializable, Database database)
        {
            ItemId = new ItemId<Ammunition>(serializable.Id, serializable.FileName);

            Body = new BulletBody
            {
                Type = serializable.Body.Type,
                Size = new NumericValue<float>(serializable.Body.Size, 0, 1000),
                Velocity = new NumericValue<float>(serializable.Body.Velocity, 0, 1000),
                Range = new NumericValue<float>(serializable.Body.Range, 0, 1000),
                Lifetime = new NumericValue<float>(serializable.Body.Lifetime, 0, 1000),
                Weight = new NumericValue<float>(serializable.Body.Weight, 0, 1000),
                HitPoints = new NumericValue<int>(serializable.Body.HitPoints, 0, 1000),
                EnergyCost = new NumericValue<float>(serializable.Body.EnergyCost, 0, 1000),
                Color = Helpers.ColorFromString(serializable.Body.Color),
                BulletPrefab = database.GetBulletPrefabId(serializable.Body.BulletPrefab),
                CanBeDisarmed = serializable.Body.CanBeDisarmed,
                FriendlyFire = serializable.Body.FriendlyFire,
            };

            Triggers = serializable.Triggers?.Select(item => new Trigger
            {
                Condition = item.Condition,
                EffectType = item.EffectType,
                VisualEffect = database.GetVisualEffectId(item.VisualEffect),
                Ammunition = database.GetAmmunitionId(item.Ammunition),
                AudioClip = item.AudioClip,
                Quantity = new NumericValue<int>(item.Quantity, 0, 100),
                ColorMode = item.ColorMode,
                Color = Helpers.ColorFromString(item.Color),
                Size = new NumericValue<float>(item.Size, 0, 1000),
                Lifetime = new NumericValue<float>(item.Lifetime, 0, 1000),
                Cooldown = new NumericValue<float>(item.Cooldown, 0, 1000),
                RandomFactor = new NumericValue<float>(item.RandomFactor, 0, 1),
                PowerMultiplier = new NumericValue<float>(item.PowerMultiplier, 0, 100),
                MaxNestingLevel = new NumericValue<int>(item.MaxNestingLevel, 0, 100),
            }).ToArray();

            ImpactType = serializable.ImpactType;

            ImpactEffects = serializable.Effects?.Select(item => new ImpactEffect
            {
                Type = item.Type,
                DamageType = item.DamageType,
                Power = new NumericValue<float>(item.Power, 0, 1000),
                Factor = new NumericValue<float>(item.Factor, 0, 100),
            }).ToArray();
        }

        public void Save(SerializableAmmunition serializable)
        {
            serializable.Body = new SerializableAmmunition.BulletBody
            {
                Type = Body.Type,
                Size = Body.Size.Value,
                Velocity = Body.Velocity.Value,
                Range = Body.Range.Value,
                Lifetime = Body.Lifetime.Value,
                Weight = Body.Weight.Value,
                HitPoints = Body.HitPoints.Value,
                Color = Helpers.ColorToString(Body.Color),
                BulletPrefab = Body.BulletPrefab.Id,
                EnergyCost = Body.EnergyCost.Value,
                CanBeDisarmed = Body.CanBeDisarmed,
                FriendlyFire = Body.FriendlyFire,
            };

            serializable.Triggers = Triggers?.Select(item => new SerializableAmmunition.BulletTrigger
            {
                Condition = item.Condition,
                EffectType = item.EffectType,
                VisualEffect = item.VisualEffect.Id,
                Ammunition = item.Ammunition.Id,
                AudioClip = item.AudioClip,
                Quantity = item.Quantity.Value,
                Color = Helpers.ColorToString(item.Color),
                ColorMode = item.ColorMode,
                Size = item.Size.Value,
                Lifetime = item.Lifetime.Value,
                Cooldown = item.Cooldown.Value,
                RandomFactor = item.RandomFactor.Value,
                PowerMultiplier = item.PowerMultiplier.Value,
                MaxNestingLevel = item.MaxNestingLevel.Value,
            }).ToArray();

            serializable.ImpactType = ImpactType;

            serializable.Effects = ImpactEffects?.Select(item => new SerializableAmmunition.ImpactEffect
            {
                Type = item.Type,
                DamageType = item.DamageType,
                Power = item.Power.Value,
                Factor = item.Factor.Value,
            }).ToArray();
        }

        public readonly ItemId<Ammunition> ItemId;

        public BulletBody Body;
        public Trigger[] Triggers;
        public BulletImpactType ImpactType;
        public ImpactEffect[] ImpactEffects;

        public class BulletBody
        {
            public BulletType Type;
            public NumericValue<float> Size = new NumericValue<float>(0, 0, 1000);
            public NumericValue<float> Velocity = new NumericValue<float>(0, 0, 1000);
            public NumericValue<float> Range = new NumericValue<float>(0, 0, 1000);
            public NumericValue<float> Lifetime = new NumericValue<float>(0, 0, 1000);
            public NumericValue<float> Weight = new NumericValue<float>(0, 0, 1000);
            public NumericValue<int> HitPoints = new NumericValue<int>(0, 0, 1000);
            public Color Color;
            public ItemId<BulletPrefab> BulletPrefab = ItemId<BulletPrefab>.Empty;
            public NumericValue<float> EnergyCost = new NumericValue<float>(0, 0, 1000);
            public bool CanBeDisarmed;
            public bool FriendlyFire;
        }

        public class ImpactEffect
        {
            public ImpactEffectType Type;
            public DamageType DamageType;
            public NumericValue<float> Power = new NumericValue<float>(0, 0, 1000);
            public NumericValue<float> Factor = new NumericValue<float>(0, 0, 100);
        }

        public class Trigger
        {
            public BulletTriggerCondition Condition;
            public BulletEffectType EffectType;

            public ItemId<VisualEffect> VisualEffect = ItemId<VisualEffect>.Empty;
            public ItemId<Ammunition> Ammunition = ItemId<Ammunition>.Empty;
            public string AudioClip;

            public ColorMode ColorMode;
            public Color Color = Color.White;

            public NumericValue<int> Quantity = new NumericValue<int>(0, 0, 100);
            public NumericValue<float> Size = new NumericValue<float>(0, 0, 1000);
            public NumericValue<float> Lifetime = new NumericValue<float>(0, 0, 1000);
            public NumericValue<float> Cooldown = new NumericValue<float>(0, 0, 1000);
            public NumericValue<float> RandomFactor = new NumericValue<float>(0, 0, 1);
            public NumericValue<float> PowerMultiplier = new NumericValue<float>(0, 0, 100);

            public NumericValue<int> MaxNestingLevel = new NumericValue<int>(0, 0, 100);
        }
    }
}
