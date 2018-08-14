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
        }

        public void Save(SerializableGalaxySettings serializable)
        {
            serializable.AbandonedStarbaseFaction = AbandonedStarbaseFaction.Id;
            serializable.AlienLifeformFaction = AlienLifeformFaction.Id;
        }

        public ItemId<Faction> AbandonedStarbaseFaction;
        public ItemId<Faction> AlienLifeformFaction;
    }
}
