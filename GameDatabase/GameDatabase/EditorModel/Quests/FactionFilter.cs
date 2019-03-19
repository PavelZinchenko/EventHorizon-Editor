using System.Linq;
using GameDatabase.Enums.Quests;
using GameDatabase.Serializable;

namespace GameDatabase.EditorModel.Quests
{
    public class FactionFilter
    {
        public FactionFilter(SerializableFactionFilter serializable, Database database)
        {
            if (serializable == null || database == null) return;
            Type = (FactionFilterType)serializable.Type;
            List = serializable.List?.Select(item => new Wrapper<Faction> { Item = database.GetFactionId(item) }).ToArray();
        }

        public SerializableFactionFilter Serialize()
        {
            if (List == null || List.Length == 0)
                return Type == default(FactionFilterType) ? null : new SerializableFactionFilter { Type = (int)Type };

            return new SerializableFactionFilter
            {
                Type = (int)Type,
                List = List?.Select(item => item.Item.Id).ToArray()
            };
        }

        public FactionFilterType Type;
        public Wrapper<Faction>[] List;
    }
}
