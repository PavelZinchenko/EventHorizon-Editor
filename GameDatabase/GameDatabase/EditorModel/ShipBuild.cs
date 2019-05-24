using System;
using System.Linq;
using System.Windows.Forms;
using GameDatabase.Enums;
using GameDatabase.GameDatabase.Model;
using GameDatabase.Model;
using GameDatabase.Serializable;

namespace GameDatabase.EditorModel
{
    public class ShipBuild
    {
        public ShipBuild(SerializableShipBuild shipBuild, Database database)
        {
            ItemId = new ItemId<ShipBuild>(shipBuild.Id, shipBuild.FileName);

            try
            {
                ShipId = database.GetShip(shipBuild.ShipId).ItemId;
            } catch (NullReferenceException err)
            {
                throw new EditorException("Unknown ship ID - " + shipBuild.ShipId + " in " + shipBuild.FilePath);
            }
            NotAvailableInGame = shipBuild.NotAvailableInGame;
            DifficultyClass = shipBuild.DifficultyClass;
            BuildFactionId = database.GetFaction(shipBuild.BuildFaction).ItemId;
            try
            {
                Components = shipBuild.Components.Select(item => new InstalledComponent(item, database)).ToArray();
            } catch(System.ArgumentException e)
            {
                throw new EditorException(e.Message+ " in " + shipBuild.FilePath);
            }
        }

        public void Save(SerializableShipBuild serializable)
        {
            serializable.ShipId = ShipId.Id;
            serializable.NotAvailableInGame = NotAvailableInGame;
            serializable.DifficultyClass = DifficultyClass;
            serializable.BuildFaction = BuildFactionId.Id;
            serializable.Components = Components.Select(item => item.Serialize()).ToArray();
        }

        public readonly ItemId<ShipBuild> ItemId;

        public ItemId<Ship> ShipId;
        public bool NotAvailableInGame;
        public DifficultyClass DifficultyClass;
        public ItemId<Faction> BuildFactionId;
        public InstalledComponent[] Components;
    }
}
