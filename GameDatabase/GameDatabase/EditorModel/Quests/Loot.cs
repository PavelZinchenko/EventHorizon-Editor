using System;
using System.Collections.Generic;
using System.Linq;
using GameDatabase.Enums.Quests;
using GameDatabase.GameDatabase;
using GameDatabase.Serializable;

namespace GameDatabase.EditorModel.Quests
{
    public class Loot : IDataAdapter
    {
        public Loot(SerializableLoot serializable, Database database)
            : this(serializable.Loot, database)
        {
            //ItemId = new ItemId<Loot>(loot.Id, loot.FileName);
        }

        public Loot(SerializableLootContent serializable, Database database)
            : this()
        {
            if (serializable == null)
                return;

            Type = (LootItemType)serializable.Type;
            Content = LootFactory.CreateLoot(Type);
            Content.Load(serializable, database);
        }

        public Loot()
        {
            Type = LootItemType.None;
            Content = LootFactory.CreateLoot(Type);
        }

        public void Save(SerializableLootContent serializable)
        {
            serializable.Type = (int)Type;
            Content.Save(serializable);
        }

        public void Save(SerializableLoot serializable)
        {
            if (serializable.Loot == null)
                serializable.Loot = new SerializableLootContent();

            serializable.Loot.Type = (int)Type;
            Content.Save(serializable.Loot);
        }

        public event Action LayoutChangedEvent;
        public event Action DataChangedEvent;

        public IEnumerable<IProperty> Properties
        {
            get
            {
                var type = GetType();
                //yield return new Property(this, type.GetField("ItemId"), DataChangedEvent);
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

        //public readonly ItemId<Loot> ItemId = ItemId<Loot>.Empty;
        public LootItemType Type;
        public ILootContent Content;
    }
}
