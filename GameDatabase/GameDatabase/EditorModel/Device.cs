using System.Drawing;
using GameDatabase.Enums;
using GameDatabase.Model;
using GameDatabase.Serializable;

namespace GameDatabase.EditorModel
{
    public class Device
    {
        public Device(SerializableDevice device, Database database)
        {
            ItemId = new ItemId<Device>(device.Id, device.FileName);
            DeviceClass = device.DeviceClass;
            EnergyConsumption = new NumericValue<float>(device.EnergyConsumption, 0, 1000);
            PassiveEnergyConsumption = new NumericValue<float>(device.PassiveEnergyConsumption, 0, 1000);
            Power = new NumericValue<float>(device.Power, 0, 1000);
            Range = new NumericValue<float>(device.Range, 0, 1000);
            Size = new NumericValue<float>(device.Size, 0, 1000);
            Cooldown = new NumericValue<float>(device.Cooldown, 0, 1000);
            Lifetime = new NumericValue<float>(device.Lifetime, 0, 1000);
            Offset = device.Offset;
            ActivationType = device.ActivationType;
            Color = Helpers.ColorFromString(device.Color);
            Sound = device.Sound;
            EffectPrefab = device.EffectPrefab;
            ObjectPrefab = device.ObjectPrefab;
            ControlButtonIcon = device.ControlButtonIcon;
        }

        public void Save(SerializableDevice serializable)
        {
            serializable.DeviceClass = DeviceClass;
            serializable.EnergyConsumption = EnergyConsumption.Value;
            serializable.PassiveEnergyConsumption = PassiveEnergyConsumption.Value;
            serializable.Power = Power.Value;
            serializable.Range = Range.Value;
            serializable.Size = Size.Value;
            serializable.Cooldown = Cooldown.Value;
            serializable.Lifetime = Lifetime.Value;
            serializable.Offset = Offset;
            serializable.ActivationType = ActivationType;
            serializable.Color = Helpers.ColorToString(Color);
            serializable.Sound = Sound;
            serializable.EffectPrefab = EffectPrefab;
            serializable.ObjectPrefab = ObjectPrefab;
            serializable.ControlButtonIcon = ControlButtonIcon;
        }

        public readonly ItemId<Device> ItemId;

        public DeviceClass DeviceClass;
        public NumericValue<float> EnergyConsumption;
        public NumericValue<float> PassiveEnergyConsumption;
        public NumericValue<float> Power;
        public NumericValue<float> Range;
        public NumericValue<float> Size;
        public NumericValue<float> Cooldown;
        public NumericValue<float> Lifetime;
        public Vector2 Offset;

        public ActivationType ActivationType;

        public Color Color;
        public string Sound;
        public string EffectPrefab;
        public string ObjectPrefab;
        public string ControlButtonIcon;
    }
}
