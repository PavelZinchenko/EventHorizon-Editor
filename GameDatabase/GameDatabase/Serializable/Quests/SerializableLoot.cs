using System;

namespace GameDatabase.Serializable
{
    [Serializable]
    public class SerializableLoot : SerializableItem
    {
        public int Type;
        public int ItemId;
        public int MinAmount;
        public int MaxAmount;
        public float ValueRatio;
        public SerializableFactionFilter Factions;
        public LootItem[] Items;

        [Serializable]
        public struct LootItem
        {
            public float Weight;
            public int LootId;
        }
    }
}
