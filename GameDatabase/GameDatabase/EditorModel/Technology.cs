using System.Linq;
using GameDatabase.Enums;
using GameDatabase.Model;
using GameDatabase.Serializable;

namespace GameDatabase.EditorModel
{
    public class Technology
    {
        public Technology(SerializableTechnology technology, Database database)
        {
            ItemId = new ItemId<Technology>(technology.Id, technology.FileName);

            Type = technology.Type;
            FactionId = database.GetFaction(technology.Faction).Id;
            Price = new NumericValue<int>(technology.Price, 0, 1000);
            Hidden = technology.Hidden;
            Dependencies = technology.Dependencies.Select(item => new TechWrapper { Technology = database.GetTechnologyId(item) }).ToArray();

            if (Type == TechType.Component)
                LinkedItem = database.GetComponent(technology.ItemId).ItemId;
            else if (Type == TechType.Satellite)
                LinkedItem = database.GetSatellite(technology.ItemId).ItemId;
            else if (Type == TechType.Ship)
                LinkedItem = database.GetShip(technology.ItemId).ItemId;
        }

        public void Save(SerializableTechnology serializable)
        {
            serializable.Type = Type;
            serializable.ItemId = LinkedItem.Id;
            serializable.Faction = FactionId.Id;
            serializable.Price = Price.Value;
            serializable.Hidden = Hidden;
            serializable.Dependencies = Dependencies.Select(item => item.Technology.Id).ToArray();
        }

        public ItemId<Technology> ItemId;

        public TechType Type;
        public IItemId LinkedItem;

        public ItemId<Faction> FactionId;
        public NumericValue<int> Price;
        public bool Hidden;

        public TechWrapper[] Dependencies;
    }

    public class TechWrapper
    {
        public ItemId<Technology> Technology = ItemId<Technology>.Empty;

        public override string ToString()
        {
            return Technology.ToString();
        }
    }
}
