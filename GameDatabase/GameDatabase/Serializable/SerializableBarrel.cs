using System;
using System.ComponentModel;
using GameDatabase.Enums;
using GameDatabase.Model;

namespace GameDatabase.Serializable
{
    [Serializable]
    public struct SerializableBarrel
    {
        public Vector2 Position;
        public float Rotation;
        public float Offset;
        public PlatformType PlatformType;
        [DefaultValue("")]
        public string WeaponClass;
        [DefaultValue("")]
        public string Image;
        public float Size;
    }
}
