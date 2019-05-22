using System;
using System.ComponentModel;
using GameDatabase.Enums;

namespace GameDatabase.Serializable
{
    [Serializable]
    public class SerializableVisualEffect : SerializableItem
    {
        public EffectData[] Elements;

        [Serializable]
        public struct EffectData
        {
            public VisualEffectType Type;
            [DefaultValue("")]
            public string Image;

            public ColorMode ColorMode;
            [DefaultValue("")]
            public string Color;

            public float Size;

            public float StartTime;
            public float Lifetime;
        }
    }
}
