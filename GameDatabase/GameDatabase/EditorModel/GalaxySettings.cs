using System.Linq;
using GameDatabase.Model;
using GameDatabase.Serializable;

namespace GameDatabase.EditorModel
{
    public class GalaxySettings
    {
        public GalaxySettings(SerializableGalaxySettings settings, Database database)
        {
            AbandonedStarbaseFaction = database.GetFaction(settings.AbandonedStarbaseFaction).ItemId;
            AlienLifeformFaction = database.GetFaction(settings.AlienLifeformFaction).ItemId;
            if (settings.StartingShipBuilds != null)
                StartingShipBuilds = settings.StartingShipBuilds.Select(item => new Wrapper<ShipBuild> { Item = database.GetShipBuildId(item) }).ToArray();
        }

        public void Save(SerializableGalaxySettings serializable)
        {
            serializable.AbandonedStarbaseFaction = AbandonedStarbaseFaction.Id;
            serializable.AlienLifeformFaction = AlienLifeformFaction.Id;
            if (StartingShipBuilds != null)
                serializable.StartingShipBuilds = StartingShipBuilds.Select(item => item.Item.Id).ToArray();
        }

        public ItemId<Faction> AbandonedStarbaseFaction;
        public ItemId<Faction> AlienLifeformFaction;
        public Wrapper<ShipBuild>[] StartingShipBuilds;
    }
}
