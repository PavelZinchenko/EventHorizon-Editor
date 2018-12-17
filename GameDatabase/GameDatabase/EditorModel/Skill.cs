using GameDatabase.Model;
using GameDatabase.Serializable;

namespace GameDatabase.EditorModel
{
    public class Skill
    {
        public void Save(SerializableSkill serializable)
        {
            serializable.Name = Name;
            serializable.Description = Description;
            serializable.Icon = Icon;
            serializable.BaseRequirement = BaseRequirement.Value;
            serializable.RequirementPerLevel = RequirementPerLevel.Value;
            serializable.BasePrice = BasePrice.Value;
            serializable.PricePerLevel = PricePerLevel.Value;
            serializable.MaxLevel = MaxLevel.Value;
        }

        public Skill(SerializableSkill skill, Database database)
        {
            ItemId = new ItemId<Skill>(skill.Id, skill.FileName);
            Name = skill.Name;
            Description = skill.Description;
            Icon = skill.Icon;
            BaseRequirement = new NumericValue<float>(skill.BaseRequirement, 0, 100);
            RequirementPerLevel = new NumericValue<float>(skill.RequirementPerLevel, 0, 100);
            BasePrice = new NumericValue<float>(skill.BasePrice, 0, 100);
            PricePerLevel = new NumericValue<float>(skill.PricePerLevel, 0, 100);
            MaxLevel = new NumericValue<int>(skill.MaxLevel, 1, 1000);
        }

        public readonly ItemId<Skill> ItemId;
        public string Name;
        public string Icon;
        public string Description;
        public NumericValue<float> BaseRequirement;
        public NumericValue<float> RequirementPerLevel;
        public NumericValue<float> BasePrice;
        public NumericValue<float> PricePerLevel;
        public NumericValue<int> MaxLevel;
    }

    public class SkillWrapper
    {
        public ItemId<Skill> Skill = ItemId<Skill>.Empty;

        public override string ToString()
        {
            return Skill.ToString();
        }
    }
}
