using System;
using GameDatabase.Enums;

namespace GameDatabase.Serializable
{
    [Serializable]
    public class SerializableInstalledComponent
    {
        public int ComponentId;
        public ComponentModType Modification;
        public ModificationQuality Quality;

        public bool Locked;
        public int X;
        public int Y;
        public int BarrelId;
        public int Behaviour;
        public int KeyBinding;
    }
}
