using GameDatabase.Enums;
using GameDatabase.Enums.Weapon;
using GameDatabase.GameDatabase.Helpers;
using GameDatabase.Model;
using GameDatabase.Serializable;
using System;

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
            Enum.TryParse(weapon.ShotEffectPrefab, out ShotEffectPrefab);
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
            serializable.ShotEffectPrefab = ShotEffectPrefab == EffectObsolete.None ? string.Empty : ShotEffectPrefab.ToString();
            serializable.ControlButtonIcon = ControlButtonIcon;
        }

        public readonly ItemId<Weapon> ItemId;

        public WeaponClass WeaponClass;
        public NumericValue<float> FireRate;
        public NumericValue<float> Spread;
        public NumericValue<int> Magazine;

        public ActivationType ActivationType;

        [AutoCompleteAtribute(AutoCompleteAtribute.Type.SoundObsolete)]
        public string ShotSound;

        [AutoCompleteAtribute(AutoCompleteAtribute.Type.SoundObsolete)]
        public string ChargeSound;

        public EffectObsolete ShotEffectPrefab;

        [AutoCompleteAtribute(AutoCompleteAtribute.Type.Controls)]
        public string ControlButtonIcon;
    }
}
