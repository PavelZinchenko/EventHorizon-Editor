using System;
using System.ComponentModel;

namespace GameDatabase.Serializable
{
    [Serializable]
    public class SerializableNode
    {
        public int Id;
        public int Type;
        public int RequiredView;
        [DefaultValue("")]
        public string Message;
        public int DefaultTransition;
        public int FailureTransition;
        public int Enemy;
        public int Loot;
        public int Quest;
        public int Character;
        public int Value;
        public Action[] Actions;
        public Transition[] Transitions;

        [Serializable]
        public struct Action
        {
            public int TargetNode;
            public SerializableRequirement Requirement;
            [DefaultValue("")]
            public string ButtonText;
        }

        [Serializable]
        public struct Transition
        {
            public int TargetNode;
            public SerializableRequirement Requirement;
            public float Weight;
        }
    }
}
