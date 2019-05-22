using System;
using System.ComponentModel;
using GameDatabase.Enums;

namespace GameDatabase.Serializable
{
    [Serializable]
    public class SerializableBulletPrefab : SerializableItem
    {
        public BulletShape Shape;
        [DefaultValue("")]
        public string Image;
        public float Size;
        public float Margins;
        [DefaultValue("")]
        public string MainColor;
        public ColorMode MainColorMode;
        [DefaultValue("")]
        public string SecondColor;
        public ColorMode SecondColorMode;
    }
}
