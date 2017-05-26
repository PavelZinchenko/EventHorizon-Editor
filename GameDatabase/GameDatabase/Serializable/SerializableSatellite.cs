using System;
using System.ComponentModel;

namespace GameDatabase.Serializable
{
    [Serializable]
    public class SerializableSatellite : SerializableItem
    {
        public string Name;
        public string ModelImage;
        public float ModelScale;

        [DefaultValue("")]
        public string Layout;
        public SerializableBarrel[] Barrels;
    }
}
