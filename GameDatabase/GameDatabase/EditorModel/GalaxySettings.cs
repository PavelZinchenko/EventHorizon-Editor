using System.Linq;
using GameDatabase.Model;
using GameDatabase.Serializable;

namespace GameDatabase.EditorModel
{
    public class GalaxySettings
    {
        public GalaxySettings(SerializableGalaxySettings settings, Database database)
        {
            AbandonedStarbaseFaction = database.GetFaction(settings.AbandonedStarbaseFaction).Id;
            AlienLifeformFaction = database.GetFaction(settings.AlienLifeformFaction).Id;
            StartingShipBuilds = settings.StartingShipBuilds.Select(item => new ItemId<ShipBuild>(item, string.Empty)).ToArray();
        }

        public void Save(SerializableGalaxySettings serializable)
        {
            serializable.AbandonedStarbaseFaction = AbandonedStarbaseFaction.Id;
            serializable.AlienLifeformFaction = AlienLifeformFaction.Id;
            serializable.StartingShipBuilds = StartingShipBuilds.Select(item => item.Id).ToArray();
        }

        public ItemId<Faction> AbandonedStarbaseFaction;
        public ItemId<Faction> AlienLifeformFaction;
        public readonly ItemId<ShipBuild>[] StartingShipBuilds;
    }
}
