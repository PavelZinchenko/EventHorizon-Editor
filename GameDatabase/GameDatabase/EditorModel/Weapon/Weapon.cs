using GameDatabase.Enums;
using GameDatabase.Model;
using GameDatabase.Serializable;

namespace GameDatabase.EditorModel
{
    public class Weapon
    {
        public Weapon(SerializableWeapon weapon, Database database)
        {
            ItemId = new ItemId<Weapon>(weapon.Id, weapon.FileName);
            WeaponClass = weapon.WeaponClass;
            FireRate = new NumericValue<float>(weapon.FireRate, 0, 1000);
            Spread = new NumericValue<float>(weapon.Spread, 0, 180);
            Magazine = new NumericValue<int>(weapon.Magazine, 0, 1000);
            ActivationType = weapon.ActivationType;
            ShotSound = weapon.ShotSound;
            ChargeSound = weapon.ChargeSound;
            ShotEffectPrefab = weapon.ShotEffectPrefab;
            ControlButtonIcon = weapon.ControlButtonIcon;
        }

        public void Save(SerializableWeapon serializable)
        {
            serializable.WeaponClass = WeaponClass;
            serializable.FireRate = FireRate.Value;
            serializable.Spread = Spread.Value;
            serializable.Magazine = Magazine.Value;
            serializable.ActivationType = ActivationType;

            serializable.ShotSound = ShotSound;
            serializable.ChargeSound = ChargeSound;
            serializable.ShotEffectPrefab = ShotEffectPrefab;
            serializable.ControlButtonIcon = ControlButtonIcon;
        }

        public readonly ItemId<Weapon> ItemId;

        public WeaponClass WeaponClass;
        public NumericValue<float> FireRate;
        public NumericValue<float> Spread;
        public NumericValue<int> Magazine;

        public ActivationType ActivationType;

        public string ShotSound;
        public string ChargeSound;
        public string ShotEffectPrefab;
        public string ControlButtonIcon;
    }
}
