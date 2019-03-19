using GameDatabase.Model;

namespace GameDatabase.EditorModel
{
    public class Wrapper<T> where T : class
    {
        public ItemId<T> Item = ItemId<T>.Empty;

        public override string ToString()
        {
            return Item.ToString();
        }
    }

    //public class DeviceWrapper
    //{
    //    public ItemId<Device> Device = ItemId<Device>.Empty;

    //    public override string ToString()
    //    {
    //        return Device.ToString();
    //    }
    //}

    //public class SkillWrapper
    //{
    //    public ItemId<Skill> Skill = ItemId<Skill>.Empty;

    //    public override string ToString()
    //    {
    //        return Skill.ToString();
    //    }
    //}

    //public class TechWrapper
    //{
    //    public ItemId<Technology> Technology = ItemId<Technology>.Empty;

    //    public override string ToString()
    //    {
    //        return Technology.ToString();
    //    }
    //}

    //public class FactionWrapper
    //{
    //    public ItemId<Faction> Faction = ItemId<Faction>.Empty;

    //    public override string ToString()
    //    {
    //        return Faction.ToString();
    //    }
    //}

    //public class RequirementWrapper
    //{
    //    public ItemId<Quests.Requirement> Requirement = ItemId<Quests.Requirement>.Empty;

    //    public override string ToString()
    //    {
    //        return Requirement.ToString();
    //    }
    //}
}
