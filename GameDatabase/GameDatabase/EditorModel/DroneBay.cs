﻿using GameDatabase.Enums;
using GameDatabase.Enums.Weapon;
using GameDatabase.GameDatabase.Helpers;
using GameDatabase.Model;
using GameDatabase.Serializable;
using System;

namespace GameDatabase.EditorModel
{
    public class DroneBay
    {
        public DroneBay(SerializableDroneBay droneBay, Database database)
        {
            ItemId = new ItemId<DroneBay>(droneBay.Id, droneBay.FileName);
            EnergyConsumption = new NumericValue<float>(droneBay.EnergyConsumption, 0, 1000);
            PassiveEnergyConsumption = new NumericValue<float>(droneBay.PassiveEnergyConsumption, 0, 1000);
            Range = new NumericValue<float>(droneBay.Range, 1, 100);
            DamageMultiplier = new NumericValue<float>(droneBay.DamageMultiplier, 0.01f, 100);
            DefenseMultiplier = new NumericValue<float>(droneBay.DefenseMultiplier, 0.01f, 100);
            SpeedMultiplier = new NumericValue<float>(droneBay.SpeedMultiplier, 0.01f, 100);
            Capacity = new NumericValue<int>(droneBay.Capacity, 1, 100);
            ActivationType = droneBay.ActivationType;
            LaunchSound = droneBay.LaunchSound;
            Enum.TryParse(droneBay.LaunchEffectPrefab, out LaunchEffectPrefab);
            ControlButtonIcon = droneBay.ControlButtonIcon;
            ImprovedAi = droneBay.ImprovedAi;
        }

        public void Save(SerializableDroneBay serializable)
        {
            serializable.EnergyConsumption = EnergyConsumption.Value;
            serializable.PassiveEnergyConsumption = PassiveEnergyConsumption.Value;
            serializable.Range = Range.Value;
            serializable.DamageMultiplier = DamageMultiplier.Value;
            serializable.DefenseMultiplier = DefenseMultiplier.Value;
            serializable.SpeedMultiplier = SpeedMultiplier.Value;
            serializable.Capacity = Capacity.Value;
            serializable.ActivationType = ActivationType;
            serializable.LaunchSound = LaunchSound;
            serializable.LaunchEffectPrefab = LaunchEffectPrefab == EffectObsolete.None ? string.Empty : LaunchEffectPrefab.ToString();
            serializable.ControlButtonIcon = ControlButtonIcon;
            serializable.ImprovedAi = ImprovedAi;
        }

        public readonly ItemId<DroneBay> ItemId;

        public NumericValue<float> EnergyConsumption;
        public NumericValue<float> PassiveEnergyConsumption;
        public NumericValue<float> Range;
        public NumericValue<float> DamageMultiplier;
        public NumericValue<float> DefenseMultiplier;
        public NumericValue<float> SpeedMultiplier;
        public NumericValue<int> Capacity;
        public bool ImprovedAi;

        public ActivationType ActivationType;

        [AutoCompleteAtribute(AutoCompleteAtribute.Type.SoundObsolete)]
        public string LaunchSound;
        
        public EffectObsolete LaunchEffectPrefab;

        [AutoCompleteAtribute(AutoCompleteAtribute.Type.Controls)]
        public string ControlButtonIcon;
    }
}
