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
            if (settings.StartingShipBuilds != null)
                StartingShipBuilds = settings.StartingShipBuilds.Select(item => new ShipBuildWrapper { ShipBuild = database.GetShipBuildId(item) }).ToArray();
        }

        public void Save(SerializableGalaxySettings serializable)
        {
            serializable.AbandonedStarbaseFaction = AbandonedStarbaseFaction.Id;
            serializable.AlienLifeformFaction = AlienLifeformFaction.Id;
            if (StartingShipBuilds != null)
                serializable.StartingShipBuilds = StartingShipBuilds.Select(item => item.ShipBuild.Id).ToArray();
        }

        public ItemId<Faction> AbandonedStarbaseFaction;
        public ItemId<Faction> AlienLifeformFaction;
        public ShipBuildWrapper[] StartingShipBuilds;
    }

    public class ShipBuildWrapper
    {
        public ItemId<ShipBuild> ShipBuild = ItemId<ShipBuild>.Empty;

        public override string ToString()
        {
            return ShipBuild.ToString();
        }
    }
}
