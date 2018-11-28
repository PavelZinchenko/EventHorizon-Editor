using System;

namespace GameDatabase.Serializable
{
    [Serializable]
    public class SerializableSkill : SerializableItem
    {
        public string Name;
        public string Icon;
        public string Description;
        public float BaseRequirement;
        public float RequirementPerLevel;
        public float BasePrice;
        public float PricePerLevel;
        public int MaxLevel;
    }
}
