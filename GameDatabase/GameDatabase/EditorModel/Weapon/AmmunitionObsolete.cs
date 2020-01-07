using System;
using System.Drawing;
using GameDatabase.Enums;
using GameDatabase.Enums.Weapon;
using GameDatabase.GameDatabase.Helpers;
using GameDatabase.Model;
using GameDatabase.Serializable;

namespace GameDatabase.EditorModel
{
    public class AmmunitionObsolete
    {
        public AmmunitionObsolete(SerializableAmmunitionObsolete ammunitionObsolete, Database database)
        {
            ItemId = new ItemId<AmmunitionObsolete>(ammunitionObsolete.Id, ammunitionObsolete.FileName);
            AmmunitionClass = ammunitionObsolete.AmmunitionClass;
            DamageType = ammunitionObsolete.DamageType;
            Impulse = new NumericValue<float>(ammunitionObsolete.Impulse, 0, 10);
            Recoil = new NumericValue<float>(ammunitionObsolete.Recoil, 0, 10);
            Size = new NumericValue<float>(ammunitionObsolete.Size, 0, 1000);
            InitialPosition = ammunitionObsolete.InitialPosition;
            AreaOfEffect = new NumericValue<float>(ammunitionObsolete.AreaOfEffect, 0, 1000);
            Range = new NumericValue<float>(ammunitionObsolete.Range, 0, 1000);
            Damage = new NumericValue<float>(ammunitionObsolete.Damage, 0, 10000);
            Velocity = new NumericValue<float>(ammunitionObsolete.Velocity, 0, 1000);
            LifeTime = new NumericValue<float>(ammunitionObsolete.LifeTime, 0, 1000);
            IgnoresShipSpeed = ammunitionObsolete.IgnoresShipVelocity;
            HitPoints = new NumericValue<int>(ammunitionObsolete.HitPoints, 0, 1000);
            EnergyCost = new NumericValue<float>(ammunitionObsolete.EnergyCost, 0, 1000);

            var coupledAmmo = database.GetAmmunitionObsolete(ammunitionObsolete.CoupledAmmunitionId);
            if (coupledAmmo != null)
                CoupledAmmunitionId = coupledAmmo.ItemId;

            Color = Helpers.ColorFromString(ammunitionObsolete.Color);
            FireSound = ammunitionObsolete.FireSound;
            HitSound = ammunitionObsolete.HitSound;

            Enum.TryParse(ammunitionObsolete.HitEffectPrefab, out HitEffectPrefab);
            Enum.TryParse(ammunitionObsolete.BulletPrefab, out BulletPrefab);
        }

        public void Save(SerializableAmmunitionObsolete serializable)
        {
            serializable.AmmunitionClass = AmmunitionClass;
            serializable.DamageType = DamageType;
            serializable.Impulse = Impulse.Value;
            serializable.Recoil = Recoil.Value;
            serializable.Size = Size.Value;
            serializable.InitialPosition = InitialPosition;
            serializable.AreaOfEffect = AreaOfEffect.Value;
            serializable.Damage = Damage.Value;
            serializable.Velocity = Velocity.Value;
            serializable.Range = Range.Value;
            serializable.LifeTime = LifeTime.Value;
            serializable.IgnoresShipVelocity = IgnoresShipSpeed;
            serializable.HitPoints = HitPoints.Value;
            serializable.EnergyCost = EnergyCost.Value;
            serializable.CoupledAmmunitionId = CoupledAmmunitionId.Id;
            serializable.Color = Helpers.ColorToString(Color);
            serializable.FireSound = FireSound;
            serializable.HitSound = HitSound;
            serializable.HitEffectPrefab = HitEffectPrefab == EffectObsolete.None ? string.Empty : HitEffectPrefab.ToString();
            serializable.BulletPrefab = BulletPrefab == BulletPrefabObsolete.None ? string.Empty : BulletPrefab.ToString();
        }

        public readonly ItemId<AmmunitionObsolete> ItemId;

        public AmmunitionClassObsolete AmmunitionClass;
        public DamageType DamageType;
        public NumericValue<float> Impulse;
        public NumericValue<float> Recoil;
        public NumericValue<float> Size;
        public Vector2 InitialPosition;
        public NumericValue<float> AreaOfEffect;
        public NumericValue<float> Damage;
        public NumericValue<float> Velocity;
        public NumericValue<float> Range;
        public NumericValue<float> LifeTime;
        public bool IgnoresShipSpeed;
        public NumericValue<int> HitPoints;
        public NumericValue<float> EnergyCost;
        public ItemId<AmmunitionObsolete> CoupledAmmunitionId = ItemId<AmmunitionObsolete>.Empty;

        public Color Color;

        [AutoCompleteAtribute(AutoCompleteAtribute.Type.SoundObsolete)]
        public string FireSound;

        [AutoCompleteAtribute(AutoCompleteAtribute.Type.SoundObsolete)]
        public string HitSound;
        public EffectObsolete HitEffectPrefab;
        public BulletPrefabObsolete BulletPrefab;
    }
}
