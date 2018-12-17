using System.Drawing;
using System.Linq;
using GameDatabase.Enums;
using GameDatabase.Model;
using GameDatabase.Serializable;

namespace GameDatabase.EditorModel
{
    public class Ship
    {
        public Ship(SerializableShip ship, Database database)
        {
            ItemId = new ItemId<Ship>(ship.Id, ship.FileName);
            ShipCategory = ship.ShipCategory;
            Name = ship.Name;
            FactionId = database.GetFaction(ship.Faction).Id;
            SizeClass = ship.SizeClass;
            IconImage = ship.IconImage;
            IconScale = new NumericValue<float>(ship.IconScale, 0.1f, 100);
            ModelImage = ship.ModelImage;
            ModelScale = new NumericValue<float>(ship.ModelScale, 0.1f, 100);
            EnginePosition = ship.EnginePosition;
            EngineColor = Helpers.ColorFromString(ship.EngineColor);
            EngineSize = new NumericValue<float>(ship.EngineSize, 0, 1);

            EnergyResistance = new NumericValue<float>(ship.EnergyResistance, 0, 100);
            KineticResistance = new NumericValue<float>(ship.KineticResistance, 0, 100);
            HeatResistance = new NumericValue<float>(ship.HeatResistance, 0, 100);
            Regeneration = ship.Regeneration;
            BaseWeightModifier = new NumericValue<float>(ship.BaseWeightModifier, -0.9f, 100);
            BuiltinDevices = ship.BuiltinDevices.Select(id => new DeviceWrapper { Device = database.GetDevice(id).ItemId }).ToArray();
            Layout = new Layout(ship.Layout);
            Barrels = ship.Barrels.Select(item => new Barrel(item)).ToArray();
        }

        public void Save(SerializableShip serializable)
        {
            serializable.ShipCategory = ShipCategory;
            serializable.Name = Name;
            serializable.Faction = FactionId.Id;
            serializable.SizeClass = SizeClass;
            serializable.IconImage = IconImage;
            serializable.IconScale = IconScale.Value;
            serializable.ModelImage = ModelImage;
            serializable.ModelScale = ModelScale.Value;
            serializable.EnginePosition = EnginePosition;
            serializable.EngineColor = Helpers.ColorToString(EngineColor);
            serializable.EngineSize = EngineSize.Value;

            serializable.EnergyResistance = EnergyResistance.Value;
            serializable.KineticResistance = KineticResistance.Value;
            serializable.HeatResistance = HeatResistance.Value;
            serializable.Regeneration = Regeneration;
            serializable.BaseWeightModifier = BaseWeightModifier.Value;
            serializable.BuiltinDevices = BuiltinDevices.Select(device => device.Device.Id).ToArray();
            serializable.Layout = Layout.Data;
            serializable.Barrels = Barrels.Select(item => item.Serialize()).ToArray();
        }

        public readonly ItemId<Ship> ItemId;

        public ShipCategory ShipCategory;

        public string Name;
        public ItemId<Faction> FactionId;
        public SizeClass SizeClass;
        public string IconImage;
        public NumericValue<float> IconScale;
        public string ModelImage;
        public NumericValue<float> ModelScale;
        public Vector2 EnginePosition;
        public Color EngineColor;
        public NumericValue<float> EngineSize;

        public NumericValue<float> EnergyResistance;
        public NumericValue<float> KineticResistance;
        public NumericValue<float> HeatResistance;
        public bool Regeneration;
        public NumericValue<float> BaseWeightModifier;
        public DeviceWrapper[] BuiltinDevices;

        public Layout Layout;
        public Barrel[] Barrels;
    }

    public class DeviceWrapper
    {
        public ItemId<Device> Device = ItemId<Device>.Empty;
    }
}
