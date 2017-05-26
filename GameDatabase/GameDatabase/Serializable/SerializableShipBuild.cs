using System;
using GameDatabase.Enums;

namespace GameDatabase.Serializable
{
    [Serializable]
    public class SerializableShipBuild : SerializableItem
    {
        public int ShipId;
        public bool NotAvailableInGame;
        public DifficultyClass DifficultyClass;
        public Faction BuildFaction;
        public SerializableInstalledComponent[] Components;
    }
}
