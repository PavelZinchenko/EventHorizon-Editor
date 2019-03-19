using System;
using System.Collections.Generic;
using System.Linq;
using GameDatabase.Enums.Quests;
using GameDatabase.GameDatabase;
using GameDatabase.Model;
using GameDatabase.Serializable;

namespace GameDatabase.EditorModel.Quests
{
    public class QuestNode : IDataAdapter
    {
        public QuestNode()
        {
            Id = new NumericValue<int>(1, 1, 1000);
            Type = NodeType.CompleteQuest;
            Content = NodeFactory.CreateNode(Type);
        }

        public QuestNode(SerializableNode serializable, Database database)
        {
            Id = new NumericValue<int>(serializable.Id, 1, 1000);
            Type = (NodeType)serializable.Type;

            Content = NodeFactory.CreateNode(Type);
            Content.Load(serializable, database);
        }

        public SerializableNode Save()
        {
            var serializable = new SerializableNode { Type = (int)Type, Id = Id.Value };
            Content.Save(serializable);
            return serializable;
        }

        public event Action LayoutChangedEvent;
        public event Action DataChangedEvent;

        public IEnumerable<IProperty> Properties
        {
            get
            {
                var type = GetType();
                yield return new Property(this, type.GetField("Id"), DataChangedEvent);
                yield return new Property(this, type.GetField("Type"), OnTypeChanged);

                foreach (var item in Content.GetType().GetFields().Where(f => f.IsPublic && !f.IsStatic))
                    yield return new Property(Content, item, DataChangedEvent);
            }
        }

        private void OnTypeChanged()
        {
            Content = NodeFactory.CreateNode(Type);
            DataChangedEvent?.Invoke();
            LayoutChangedEvent?.Invoke();
        }

        public NumericValue<int> Id;
        public NodeType Type;
        public INodeContent Content;

        public override string ToString()
        {
            return Id.Value + " - " + Type;
        }
    }
}
