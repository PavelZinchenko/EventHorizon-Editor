using System.Drawing;
using System.Linq;
using GameDatabase.Enums;
using GameDatabase.GameDatabase.Helpers;
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
            FactionId = database.GetFaction(ship.Faction).ItemId;
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

            Layout = new Layout(ship.Layout);
            BuiltinDevices = ship.BuiltinDevices?.Select(id => new Wrapper<Device> { Item = database.GetDevice(id).ItemId }).ToArray();
            Barrels = ship.Barrels?.Select(item => new Barrel(item)).ToArray();
            Engines = ship.Engines?.Select(item => new Engine { Position = item.Position, Size = new NumericValue<float>(item.Size, 0, 1) }).ToArray();
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
            serializable.Layout = Layout.Data;

            serializable.BuiltinDevices = BuiltinDevices?.Select(device => device.Item.Id).ToArray();
            serializable.Barrels = Barrels?.Select(item => item.Serialize()).ToArray();
            serializable.Engines = Engines?.Select(item => new SerializableShip.Engine { Position = item.Position, Size = item.Size.Value } ).ToArray();
        }

        public readonly ItemId<Ship> ItemId;

        public ShipCategory ShipCategory;

        public string Name;
        public ItemId<Faction> FactionId;
        public SizeClass SizeClass;

        [AutoCompleteAtribute(AutoCompleteAtribute.Type.ShipIcon)]
        public string IconImage;
        public NumericValue<float> IconScale;

        [AutoCompleteAtribute(AutoCompleteAtribute.Type.ShipSprite)]
        public string ModelImage;
        public NumericValue<float> ModelScale;
        public Color EngineColor;

        public Vector2 EnginePosition;
        public NumericValue<float> EngineSize;

        public Engine[] Engines;

        public NumericValue<float> EnergyResistance;
        public NumericValue<float> KineticResistance;
        public NumericValue<float> HeatResistance;
        public bool Regeneration;
        public NumericValue<float> BaseWeightModifier;
        public Wrapper<Device>[] BuiltinDevices;

        public Layout Layout;
        public Barrel[] Barrels;

        public class Engine
        {
            public Vector2 Position;
            public NumericValue<float> Size = new NumericValue<float>(0.5f,0,1);
        }
    }
}
