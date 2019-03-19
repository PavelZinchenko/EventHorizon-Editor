using System;
using System.Collections.Generic;
using System.Linq;
using GameDatabase.Enums.Quests;
using GameDatabase.GameDatabase;
using GameDatabase.Serializable;

namespace GameDatabase.EditorModel.Quests
{
    public class Requirement : IDataAdapter
    {
        public Requirement()
        {
            Type = RequirementType.Empty;
            Content = RequirementFactory.CreateRequirement(Type);
        }

        public Requirement(SerializableRequirement serializable, Database database)
        {
            Type = (RequirementType)serializable.Type;

            Content = RequirementFactory.CreateRequirement(Type);
            Content.Load(serializable, database);
        }

        public SerializableRequirement Save()
        {
            if (Type == RequirementType.Empty) return null;
            var serializable = new SerializableRequirement { Type = (int)Type };
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
                yield return new Property(this, type.GetField("Type"), OnTypeChanged);

                foreach (var item in Content.GetType().GetFields().Where(f => f.IsPublic && !f.IsStatic))
                    yield return new Property(Content, item, DataChangedEvent);
            }
        }

        private void OnTypeChanged()
        {
            Content = RequirementFactory.CreateRequirement(Type);
            DataChangedEvent?.Invoke();
            LayoutChangedEvent?.Invoke();
        }

        public RequirementType Type;
        public IRequirementContent Content;
    }
}
