using System;
using System.ComponentModel;
using GameDatabase.Enums;

namespace GameDatabase.Serializable
{
    [Serializable]
    public class SerializableComponent : SerializableItem
    {
        public string Name;
        [DefaultValue("")]
        public string Description;

        public ComponentCategory DisplayCategory;

        public Availability Availability;

        public int ComponentStatsId;

        public Faction Faction;
        public int Level;

        [DefaultValue("")]
        public string Icon;
        [DefaultValue("")]
        public string Color;
        [DefaultValue("")]
        public string Layout;

        public string CellType;

        public int DeviceId;

        public int WeaponId;
        public int AmmunitionId;
        //public string WeaponType;
        public string WeaponSlotType;

        public int DroneBayId;
        public int DroneId;

        public int[] PossibleModifications;
    }
}
