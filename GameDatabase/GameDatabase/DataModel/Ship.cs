using EditorDatabase.Model;
using EditorDatabase.Serializable;
using System;

namespace EditorDatabase.DataModel
{
    public partial class Ship
    {
        partial void OnDataDeserialized(ShipSerializable serializable, Database database)
        {
            if (serializable.EnginePosition.y != 0 || serializable.EnginePosition.y != 0 || serializable.EngineSize != 0)
            {
                var engine = new EngineSerializable { Position = serializable.EnginePosition, Size = serializable.EngineSize };
                var length = Engines != null ? Engines.Length : 0;
                Array.Resize(ref Engines, length + 1);
                Engines[length] = new Engine(engine, database);
            }

            switch (serializable.ShipCategory)
            {
                case 1: ShipType = Enums.ShipType.Rare; break;
                case 3: ShipType = Enums.ShipType.Special; break;
                case 5: ShipType = Enums.ShipType.Hidden; break;
            }

            if (serializable.EnergyResistance != 0 ||
                serializable.HeatResistance != 0 ||
                serializable.KineticResistance != 0 ||
                serializable.BaseWeightModifier != 0 ||
                serializable.Regeneration ||
                (serializable.BuiltinDevices != null && serializable.BuiltinDevices.Length > 0))
            {
                var data = new ShipFeaturesSerializable
                {
                    HeatResistance = serializable.HeatResistance,
                    EnergyResistance = serializable.EnergyResistance,
                    KineticResistance = serializable.KineticResistance,
                    Regeneration = serializable.Regeneration,
                    BuiltinDevices = serializable.BuiltinDevices,
                    WeightBonus = serializable.BaseWeightModifier,
                };

                Features = new ShipFeatures(data, database);
            }
        }

        partial void OnDataSerialized(ref ShipSerializable serializable)
        {
            serializable.EnginePosition = Vector2.Zero;
            serializable.EngineSize = 0;
            serializable.ShipCategory = 0;
            serializable.EnergyResistance = 0;
            serializable.KineticResistance = 0;
            serializable.HeatResistance = 0;
            serializable.Regeneration = false;
            serializable.BuiltinDevices = null;
            serializable.BaseWeightModifier = 0;
        }
    }
}
