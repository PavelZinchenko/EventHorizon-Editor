using System;

namespace GameDatabase.Serializable
{
    [Serializable]
    public class SerializableGalaxySettings : SerializableItem
    {
        public int AbandonedStarbaseFaction;
        public int AlienLifeformFaction;
        public int[] StartingShipBuilds;
    }
}
