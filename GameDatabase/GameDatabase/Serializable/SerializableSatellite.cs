using System;
using System.ComponentModel;
using GameDatabase.Enums;

namespace GameDatabase.Serializable
{
    [Serializable]
    public class SerializableSatellite : SerializableItem
    {
        public string Name;
        public string ModelImage;
        public float ModelScale;
        public SizeClass SizeClass;

        [DefaultValue("")]
        public string Layout;
        public SerializableBarrel[] Barrels;
    }
}
