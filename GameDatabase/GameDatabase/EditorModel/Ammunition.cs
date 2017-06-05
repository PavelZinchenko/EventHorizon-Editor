using System.Drawing;
using GameDatabase.Enums;
using GameDatabase.Model;
using GameDatabase.Serializable;

namespace GameDatabase.EditorModel
{
    public class Ammunition
    {
        public Ammunition(SerializableAmmunition ammunition, Database database)
        {
            ItemId = new ItemId<Ammunition>(ammunition.Id, ammunition.FileName);
            AmmunitionClass = ammunition.AmmunitionClass;
            DamageType = ammunition.DamageType;
            Impulse = new NumericValue<float>(ammunition.Impulse, 0, 10);
            Recoil = new NumericValue<float>(ammunition.Recoil, 0, 10);
            Size = new NumericValue<float>(ammunition.Size, 0, 1000);
            InitialPosition = ammunition.InitialPosition;
            AreaOfEffect = new NumericValue<float>(ammunition.AreaOfEffect, 0, 1000);
            Range = new NumericValue<float>(ammunition.Range, 0, 1000);
            Damage = new NumericValue<float>(ammunition.Damage, 0, 10000);
            Velocity = new NumericValue<float>(ammunition.Velocity, 0, 1000);
            LifeTime = new NumericValue<float>(ammunition.LifeTime, 0, 1000);
            IgnoresShipSpeed = ammunition.IgnoresShipVelocity;
            HitPoints = new NumericValue<int>(ammunition.HitPoints, 0, 1000);
            EnergyCost = new NumericValue<float>(ammunition.EnergyCost, 0, 1000);

            var coupledAmmo = database.GetAmmunition(ammunition.CoupledAmmunitionId);
            if (coupledAmmo != null)
                CoupledAmmunitionId = coupledAmmo.ItemId;

            Color = Helpers.ColorFromString(ammunition.Color);
            FireSound = ammunition.FireSound;
            HitSound = ammunition.HitSound;
            HitEffectPrefab = ammunition.HitEffectPrefab;
            BulletPrefab = ammunition.BulletPrefab;
        }

        public void Save(SerializableAmmunition serializable)
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
            serializable.HitEffectPrefab = HitEffectPrefab;
            serializable.BulletPrefab = BulletPrefab;
        }

        public ItemId<Ammunition> ItemId;

        public AmmunitionClass AmmunitionClass;
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
        public ItemId<Ammunition> CoupledAmmunitionId = ItemId<Ammunition>.Empty;

        public Color Color;
        public string FireSound;
        public string HitSound;
        public string HitEffectPrefab;
        public string BulletPrefab;
    }
}
