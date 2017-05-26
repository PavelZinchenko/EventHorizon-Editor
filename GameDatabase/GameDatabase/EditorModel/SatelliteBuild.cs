using System.Linq;
using GameDatabase.Enums;
using GameDatabase.Model;
using GameDatabase.Serializable;

namespace GameDatabase.EditorModel
{
    public class SatelliteBuild
    {
        public SatelliteBuild(SerializableSatelliteBuild satelliteBuild, Database database)
        {
            ItemId = new ItemId<SatelliteBuild>(satelliteBuild.Id, satelliteBuild.FileName);

            SatelliteId = database.GetSatellite(satelliteBuild.SatelliteId).ItemId;
            NotAvailableInGame = satelliteBuild.NotAvailableInGame;
            DifficultyClass = satelliteBuild.DifficultyClass;
            Components = satelliteBuild.Components.Select(item => new InstalledComponent(item, database)).ToArray();
        }

        public void Save(SerializableSatelliteBuild serializable)
        {
            serializable.SatelliteId = SatelliteId.Id;
            serializable.NotAvailableInGame = NotAvailableInGame;
            serializable.DifficultyClass = DifficultyClass;
            serializable.Components = Components.Select(item => item.Serialize()).ToArray();
        }

        public ItemId<SatelliteBuild> ItemId;

        public ItemId<Satellite> SatelliteId;
        public bool NotAvailableInGame;
        public DifficultyClass DifficultyClass;
        public InstalledComponent[] Components;
    }
}
