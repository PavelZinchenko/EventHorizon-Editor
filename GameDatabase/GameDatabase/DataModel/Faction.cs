using EditorDatabase.Serializable;

namespace EditorDatabase.DataModel
{
    public partial class Faction
    {
        partial void OnDataDeserialized(FactionSerializable serializable, Database database)
        {
            if (serializable.Hidden)
            {
                NoTerritories = true;
                NoWanderingShips = true;
                HideFromMerchants = true;
                HideResearchTree = true;
            }

            if (serializable.Hostile)
            {
                NoMissions = true;
            }
        }

        partial void OnDataSerialized(ref FactionSerializable serializable)
        {
            serializable.Hostile = false;
            serializable.Hidden = false;
        }
    }
}
