using System;

namespace GameDatabase.Serializable
{
    [Serializable]
    public class SerializableFaction : SerializableItem
    {
        public string Name;
        public string Color;
        public int HomeStarDistance;
        public int WanderingShipsDistance;
    }
}
