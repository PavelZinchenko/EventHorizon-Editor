using System;

namespace GameDatabase.Serializable
{
    [Serializable]
    public class SerializableFactionFilter
    {
        public int Type;
        public int[] List;
    }
}
