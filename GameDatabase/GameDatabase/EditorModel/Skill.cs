using System.Linq;
using GameDatabase.Enums;
using GameDatabase.Model;
using GameDatabase.Serializable;

namespace GameDatabase.EditorModel
{
    public class Skill
    {
        public void Save(SerializableSkill serializable)
        {
            serializable.Type = (int)Type;
            serializable.Detail = Detail.Value;
            serializable.Price = Price.Value;
            serializable.Position = Position;
            serializable.Dependencies = Dependencies.Select(item => item.Skill.Id).ToArray();
        }

        public Skill(SerializableSkill skill, Database database)
        {
            Type = (SkillType)skill.Type;
            Price = new NumericValue<int>(skill.Price, 0, 100);
            Detail = new NumericValue<int>(skill.Detail, 0, 1000);
            Position = new HexPosition(skill.Position);
            Dependencies = skill.Dependencies.Select(item => new SkillWrapper { Skill = database.GetSkillId(item) }).ToArray();
        }

        public SkillType Type;
        public NumericValue<int> Detail;
        public NumericValue<int> Price;
        public HexPosition Position;
        public SkillWrapper[] Dependencies;
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
