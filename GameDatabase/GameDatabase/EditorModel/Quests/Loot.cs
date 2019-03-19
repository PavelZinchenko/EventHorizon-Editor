using System;
using System.Collections.Generic;
using System.Linq;
using GameDatabase.Enums.Quests;
using GameDatabase.GameDatabase;
using GameDatabase.Model;
using GameDatabase.Serializable;

namespace GameDatabase.EditorModel.Quests
{
    public class Loot : IDataAdapter
    {
        public Loot(SerializableLoot loot, Database database)
        {
            ItemId = new ItemId<Loot>(loot.Id, loot.FileName);
            Type = (LootItemType)loot.Type;
            Content = LootFactory.CreateLoot(Type);
            Content.Load(loot, database);
        }

        public void Save(SerializableLoot serializable)
        {
            serializable.Type = (int)Type;
            Content.Save(serializable);
        }

        public event Action LayoutChangedEvent;
        public event Action DataChangedEvent;

        public IEnumerable<IProperty> Properties
        {
            get
            {
                var type = GetType();
                yield return new Property(this, type.GetField("ItemId"), DataChangedEvent);
                yield return new Property(this, type.GetField("Type"), OnTypeChanged);

                foreach (var item in Content.GetType().GetFields().Where(f => f.IsPublic && !f.IsStatic))
                    yield return new Property(Content, item, DataChangedEvent);
            }
        }

        private void OnTypeChanged()
        {
            Content = LootFactory.CreateLoot(Type);
            DataChangedEvent?.Invoke();
            LayoutChangedEvent?.Invoke();
        }

        public ItemId<Loot> ItemId;
        public LootItemType Type;
        public ILootContent Content;
    }
}
