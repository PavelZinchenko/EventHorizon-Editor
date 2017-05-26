using GameDatabase.Enums;
using GameDatabase.Model;
using GameDatabase.Serializable;

namespace GameDatabase.EditorModel
{
    public class InstalledComponent
    {
        public InstalledComponent()
        {
            ComponentId = ItemId<Component>.Empty;
        }

        public InstalledComponent(SerializableInstalledComponent data, Database database)
        {
            ComponentId = database.GetComponent(data.ComponentId).ItemId;

            Modification = data.Modification;
            Quality = data.Quality;
            Locked = data.Locked;
            X = data.X;
            Y = data.Y;
            BarrelId = data.BarrelId;
            Behaviour = data.Behaviour;
            KeyBinding = data.KeyBinding;
        }

        public SerializableInstalledComponent Serialize()
        {
            return new SerializableInstalledComponent
            {
                ComponentId = ComponentId.Id,
                Modification = Modification,
                Locked = Locked,
                X = X,
                Y = Y,
                BarrelId = BarrelId,
                Behaviour = Behaviour,
                KeyBinding = KeyBinding,
            };
        }

        public readonly ItemId<Component> ComponentId;
        public readonly ComponentModType Modification;
        public readonly ModificationQuality Quality;

        public readonly bool Locked;
        public readonly int X;
        public readonly int Y;
        public readonly int BarrelId;
        public readonly int Behaviour;
        public readonly int KeyBinding;

        public override string ToString()
        {
            return ComponentId.ToString();
        }
    }
}
