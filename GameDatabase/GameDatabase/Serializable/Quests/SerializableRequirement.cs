using System;

namespace GameDatabase.Serializable
{
    [Serializable]
    public class SerializableRequirement
    {
        public int Type;
        public int ItemId;
        public int[] ItemIds;
        public int MinValue;
        public int MaxValue;
        public int Character;
        public int Faction;
        public SerializableLootContent Loot;
        public SerializableRequirement[] Requirements;
    }
}
