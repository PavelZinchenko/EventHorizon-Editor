using System;
using GameDatabase.Enums;

namespace GameDatabase.Serializable
{
    [Serializable]
    public class SerializableTechnology : SerializableItem
    {
        public TechType Type;
        public int ItemId;

        public Faction Faction;
        public int Price;
        public bool Hidden;

        public int[] Dependencies;
    }
}
