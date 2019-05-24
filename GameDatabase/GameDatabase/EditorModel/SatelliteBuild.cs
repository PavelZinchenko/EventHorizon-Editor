using System;
using System.Linq;
using GameDatabase.Enums;
using GameDatabase.GameDatabase.Model;
using GameDatabase.Model;
using GameDatabase.Serializable;

namespace GameDatabase.EditorModel
{
    public class SatelliteBuild
    {
        public SatelliteBuild(SerializableSatelliteBuild satelliteBuild, Database database)
        {
            ItemId = new ItemId<SatelliteBuild>(satelliteBuild.Id, satelliteBuild.FileName);

            try
            {
                SatelliteId = database.GetSatellite(satelliteBuild.SatelliteId).ItemId;
            }
            catch (NullReferenceException err)
            {
                throw new EditorException("Unknown satelite ID - " + satelliteBuild.SatelliteId + " in " + satelliteBuild.FilePath);
            }
            NotAvailableInGame = satelliteBuild.NotAvailableInGame;
            DifficultyClass = satelliteBuild.DifficultyClass;
            try { 
                Components = satelliteBuild.Components.Select(item => new InstalledComponent(item, database)).ToArray();
            }
            catch (System.ArgumentException e)
            {
                throw new EditorException(e.Message + " in " + satelliteBuild.FilePath);
            }

        }

        public void Save(SerializableSatelliteBuild serializable)
        {
            serializable.SatelliteId = SatelliteId.Id;
            serializable.NotAvailableInGame = NotAvailableInGame;
            serializable.DifficultyClass = DifficultyClass;
            serializable.Components = Components.Select(item => item.Serialize()).ToArray();
        }

        public readonly ItemId<SatelliteBuild> ItemId;

        public ItemId<Satellite> SatelliteId;
        public bool NotAvailableInGame;
        public DifficultyClass DifficultyClass;
        public InstalledComponent[] Components;
    }
}
