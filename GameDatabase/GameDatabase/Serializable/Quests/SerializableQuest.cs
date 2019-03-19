using System;
using System.ComponentModel;

namespace GameDatabase.Serializable
{
    [Serializable]
    public class SerializableQuest : SerializableItem
    {
        [DefaultValue("")]
        public string Name;
        public int QuestType;
        public int StartCondition;
        public float Weight;
        public SerializableRequirement Requirement;
        public int Level;

        public SerializableNode[] Nodes;
    }
}
