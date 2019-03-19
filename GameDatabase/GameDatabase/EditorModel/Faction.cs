using System.Drawing;
using GameDatabase.Model;
using GameDatabase.Serializable;

namespace GameDatabase.EditorModel
{
    public class Faction
    {
        public Faction(SerializableFaction faction, Database database)
        {
            ItemId = new ItemId<Faction>(faction.Id, faction.FileName);
            Color = Helpers.ColorFromString(faction.Color);
            Name = faction.Name;
            HomeStarDistance = new NumericValue<int>(faction.HomeStarDistance, 0, 1000);
            WanderingShipsDistance = new NumericValue<int>(faction.WanderingShipsDistance, 0, 1000);
            Hidden = faction.Hidden;
        }

        public void Save(SerializableFaction serializable)
        {
            if (serializable == null)
                return;

            serializable.Name = Name;
            serializable.Color = Helpers.ColorToString(Color);
            serializable.HomeStarDistance = HomeStarDistance.Value;
            serializable.WanderingShipsDistance = WanderingShipsDistance.Value;
            serializable.Hidden = Hidden;
        }

        private Faction(int id, Color color, string name, int level, int shipLevel)
        {
            ItemId = new ItemId<Faction>(id, name);
            Color = color;
            Name = name;
            HomeStarDistance = new NumericValue<int>(level, 0, 0);
            WanderingShipsDistance = new NumericValue<int>(shipLevel, 0, 0);
        }

        public readonly ItemId<Faction> ItemId;
        public string Name;
        public Color Color;
        public NumericValue<int> HomeStarDistance;
        public NumericValue<int> WanderingShipsDistance;
        public bool Hidden;

        public static readonly Faction Neutral = new Faction(0, Color.Gray, "$NeutralFaction", 0, 0);
        public static readonly Faction Undefined = new Faction(-1, Color.Black, "UNDEFINED", 0, 0);
    }
}
