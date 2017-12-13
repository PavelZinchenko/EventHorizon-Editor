using System;

namespace GameDatabase.Serializable
{
    [Serializable]
    public class SerializableSkill : SerializableItem
    {
        public int Type;
        public int Detail;
        public int Price;
        public int Position;

        public int[] Dependencies;
    }
}
