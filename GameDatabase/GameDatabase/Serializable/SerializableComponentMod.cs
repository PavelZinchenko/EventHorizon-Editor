using System;
using GameDatabase.Enums;

namespace GameDatabase.Serializable
{
    [Serializable]
    public class SerializableComponentMod : SerializableItem
    {
        public ComponentModType Type;
    }
}
