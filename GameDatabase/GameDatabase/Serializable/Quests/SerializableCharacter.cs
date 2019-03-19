using System;
using System.ComponentModel;

namespace GameDatabase.Serializable
{
    [Serializable]
    public class SerializableCharacter : SerializableItem
    {
        [DefaultValue("")]
        public string Name;
        [DefaultValue("")]
        public string AvatarIcon;
        public int Faction;
        public int Inventory;
        public int Fleet;
        public int Relations;
        public bool IsUnique;
    }
}
