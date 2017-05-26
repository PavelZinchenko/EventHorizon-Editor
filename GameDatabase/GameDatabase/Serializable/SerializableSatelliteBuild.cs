using System;
using GameDatabase.Enums;

namespace GameDatabase.Serializable
{
    [Serializable]
    public class SerializableSatelliteBuild : SerializableItem
    {
        public int SatelliteId;
        public bool NotAvailableInGame;
        public DifficultyClass DifficultyClass;
        public SerializableInstalledComponent[] Components;
    }
}
