using System;

namespace GameDatabase.Serializable
{
    [Serializable]
    public class SerializableFleet : SerializableItem
    {
        public int LevelBonus;
        public bool NoRandomShips;

        public SerializableFactionFilter Factions;

        public int CombatTimeLimit;
        public int LootCondition;
        public int ExpCondition;
        public int[] SpecificShips;
    }
}
