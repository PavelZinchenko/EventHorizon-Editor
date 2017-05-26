using GameDatabase.Enums;
using GameDatabase.Model;
using GameDatabase.Serializable;

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
            LaunchEffectPrefab = droneBay.LaunchEffectPrefab;
            ControlButtonIcon = droneBay.ControlButtonIcon;
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
            serializable.LaunchEffectPrefab = LaunchEffectPrefab;
            serializable.ControlButtonIcon = ControlButtonIcon;
        }

        public ItemId<DroneBay> ItemId;

        public NumericValue<float> EnergyConsumption;
        public NumericValue<float> PassiveEnergyConsumption;
        public NumericValue<float> Range;
        public NumericValue<float> DamageMultiplier;
        public NumericValue<float> DefenseMultiplier;
        public NumericValue<float> SpeedMultiplier;
        public NumericValue<int> Capacity;

        public ActivationType ActivationType;

        public string LaunchSound;
        public string LaunchEffectPrefab;
        public string ControlButtonIcon;
    }
}
