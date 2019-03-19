using System;
using GameDatabase.Enums;

namespace GameDatabase.Serializable
{
    [Serializable]
    public class SerializableTechnology : SerializableItem
    {
        public TechType Type;
        public int ItemId;

        public int Faction;
        public int Price;
        public bool Hidden;
        public bool Special;

        public int[] Dependencies;
    }
}
