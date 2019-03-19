using System;
using System.ComponentModel;

namespace GameDatabase.Serializable
{
    [Serializable]
    public class SerializableFaction : SerializableItem
    {
        public string Name;
        [DefaultValue("")]
        public string Color;
        public int HomeStarDistance;
        public int WanderingShipsDistance;
        public bool Hidden;
    }
}
