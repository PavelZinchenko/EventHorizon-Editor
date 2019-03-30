using System.Linq;
using GameDatabase.Enums.Quests;
using GameDatabase.Model;
using GameDatabase.Serializable;

namespace GameDatabase.EditorModel.Quests
{
    public interface ILootContent
    {
        void Load(SerializableLootContent serializable, Database database);
        void Save(SerializableLootContent serializable);
    }

    public static class LootFactory
    {
        public static ILootContent CreateLoot(LootItemType type)
        {
            switch (type)
            {
                case LootItemType.None:
                case LootItemType.StarMap:
                    return new EmptyLootContent();
                case LootItemType.SomeMoney:
                    return new ResourceByLevelLootContent();
                case LootItemType.Money:
                case LootItemType.Fuel:
                case LootItemType.Stars:
                    return new ResourceLootContent();
                case LootItemType.RandomComponents:
                    return new RandomComponentsLootContent();
                case LootItemType.ItemsWithChance:
                    return new ItemsWithChanceLootContent();
                case LootItemType.RandomItems:
                    return new RandomItemsLootContent();
                case LootItemType.AllItems:
                    return new ItemGroupLootContent();
                case LootItemType.QuestItem:
                    return new QuestItemLootContent();
                case LootItemType.Ship:
                    return new ShipBuildLootContent();
                case LootItemType.EmptyShip:
                    return new ShipLootContent();
                case LootItemType.Component:
                    return new ComponentLootContent();
                default:
                    throw new System.ArgumentException("Unknown loot item type - " + type);
            }
        }
    }

    public class ShipBuildLootContent : ILootContent
    {
        public void Load(SerializableLootContent serializable, Database database)
        {
            Build = database.GetShipBuildId(serializable.ItemId);
        }

        public void Save(SerializableLootContent serializable)
        {
            serializable.ItemId = Build.Id;
        }

        public ItemId<ShipBuild> Build;
    }

    public class ShipLootContent : ILootContent
    {
        public void Load(SerializableLootContent serializable, Database database)
        {
            Ship = database.GetShipId(serializable.ItemId);
        }

        public void Save(SerializableLootContent serializable)
        {
            serializable.ItemId = Ship.Id;
        }

        public ItemId<Ship> Ship;
    }

    public class ComponentLootContent : ILootContent
    {
        public void Load(SerializableLootContent serializable, Database database)
        {
            Component = database.GetComponentId(serializable.ItemId);
            MinAmount = new NumericValue<int>(serializable.MinAmount, 0, 1000);
            MaxAmount = new NumericValue<int>(serializable.MaxAmount, 0, 1000);
        }

        public void Save(SerializableLootContent serializable)
        {
            serializable.ItemId = Component.Id;
            serializable.MinAmount = MinAmount.Value;
            serializable.MaxAmount = MaxAmount.Value;
        }

        public ItemId<Component> Component;
        public NumericValue<int> MinAmount = new NumericValue<int>(0, 0, 1000);
        public NumericValue<int> MaxAmount = new NumericValue<int>(0, 0, 1000);
    }

    public class EmptyLootContent : ILootContent
    {
        public void Load(SerializableLootContent serializable, Database database) {}
        public void Save(SerializableLootContent serializable) {}
    }

    public class ResourceByLevelLootContent : ILootContent
    {
        public void Load(SerializableLootContent serializable, Database database)
        {
            ValueRatio = new NumericValue<float>(serializable.ValueRatio, 0, 100);
        }

        public void Save(SerializableLootContent serializable)
        {
            serializable.ValueRatio = ValueRatio.Value;
        }

        public NumericValue<float> ValueRatio = new NumericValue<float>(0, 0, 100);
    }

    public class ResourceLootContent : ILootContent
    {
        public void Load(SerializableLootContent serializable, Database database)
        {
            MinAmount = new NumericValue<int>(serializable.MinAmount, 0, 1000000);
            MaxAmount = new NumericValue<int>(serializable.MaxAmount, 0, 1000000);
        }

        public void Save(SerializableLootContent serializable)
        {
            serializable.MinAmount = MinAmount.Value;
            serializable.MaxAmount = MaxAmount.Value;
        }

        public NumericValue<int> MinAmount = new NumericValue<int>(0, 0, 1000000);
        public NumericValue<int> MaxAmount = new NumericValue<int>(0, 0, 1000000);
    }

    public class QuestItemLootContent : ILootContent
    {
        public void Load(SerializableLootContent serializable, Database database)
        {
            Item = database.GetQuestItemId(serializable.ItemId);
            MinAmount = new NumericValue<int>(serializable.MinAmount, 0, 1000000);
            MaxAmount = new NumericValue<int>(serializable.MaxAmount, 0, 1000000);
        }

        public void Save(SerializableLootContent serializable)
        {
            serializable.ItemId = Item.Id;
            serializable.MinAmount = MinAmount.Value;
            serializable.MaxAmount = MaxAmount.Value;
        }

        public ItemId<QuestItem> Item = ItemId<QuestItem>.Empty;
        public NumericValue<int> MinAmount = new NumericValue<int>(0, 0, 1000000);
        public NumericValue<int> MaxAmount = new NumericValue<int>(0, 0, 1000000);
    }

    public class RandomComponentsLootContent : ILootContent
    {
        public void Load(SerializableLootContent serializable, Database database)
        {
            ValueRatio = new NumericValue<float>(serializable.ValueRatio, 0, 100);
            MinAmount = new NumericValue<int>(serializable.MinAmount, 0, 1000);
            MaxAmount = new NumericValue<int>(serializable.MaxAmount, 0, 1000);
            Factions = new FactionFilter(serializable.Factions, database);
        }

        public void Save(SerializableLootContent serializable)
        {
            serializable.ValueRatio = ValueRatio.Value;
            serializable.MinAmount = MinAmount.Value;
            serializable.MaxAmount = MaxAmount.Value;
            serializable.Factions = Factions.Serialize();
        }

        public NumericValue<float> ValueRatio = new NumericValue<float>(0, 0, 100);
        public NumericValue<int> MinAmount = new NumericValue<int>(0, 0, 1000);
        public NumericValue<int> MaxAmount = new NumericValue<int>(0, 0, 1000);
        public FactionFilter Factions = new FactionFilter(null, null);
    }

    public class ItemGroupLootContent : ILootContent
    {
        public void Load(SerializableLootContent serializable, Database database)
        {
            Items = serializable.Items?.Select(item => new Loot(item.Loot, database)).ToArray();
        }

        public void Save(SerializableLootContent serializable)
        {
            serializable.Items = Items?.Select(item =>
            {
                var loot = new SerializableLootContent();
                item.Save(loot);
                return new SerializableLootContent.LootItem { Loot = loot };
            }).ToArray();
        }

        public Loot[] Items;
    }

    public class ItemsWithChanceLootContent : ILootContent
    {
        public void Load(SerializableLootContent serializable, Database database)
        {
            Items = serializable.Items?.Select(item => new LootContentItem
            {
                Loot = new Loot(item.Loot, database),
                Chance = new NumericValue<float>(item.Weight, 0, 1)
            }).ToArray();
        }

        public void Save(SerializableLootContent serializable)
        {
            serializable.Items = Items?.Select(item =>
            {
                var loot = new SerializableLootContent();
                item.Loot.Save(loot);
                return new SerializableLootContent.LootItem { Weight = item.Chance.Value, Loot = loot };
            }).ToArray();
        }

        public LootContentItem[] Items;

        public class LootContentItem
        {
            public Loot Loot = new Loot();
            public NumericValue<float> Chance = new NumericValue<float>(0, 0, 1);
        }
    }

    public class RandomItemsLootContent : ILootContent
    {
        public void Load(SerializableLootContent serializable, Database database)
        {
            MinAmount = new NumericValue<int>(serializable.MinAmount, 0, 1000);
            MaxAmount = new NumericValue<int>(serializable.MaxAmount, 0, 1000);

            Items = serializable.Items?.Select(item => new LootContentItem
            {
                Loot = new Loot(item.Loot, database),
                Weight = new NumericValue<float>(item.Weight, 0, 100)
            }).ToArray();
        }

        public void Save(SerializableLootContent serializable)
        {
            serializable.MinAmount = MinAmount.Value;
            serializable.MaxAmount = MaxAmount.Value;
            serializable.Items = Items?.Select(item =>
            {
                var loot = new SerializableLootContent();
                item.Loot.Save(loot);
                return new SerializableLootContent.LootItem { Weight = item.Weight.Value, Loot = loot };
            }).ToArray();
        }

        public NumericValue<int> MinAmount = new NumericValue<int>(0, 0, 1000);
        public NumericValue<int> MaxAmount = new NumericValue<int>(0, 0, 1000);
        public LootContentItem[] Items;

        public class LootContentItem
        {
            public Loot Loot = new Loot();
            public NumericValue<float> Weight = new NumericValue<float>(0, 0, 100);
        }
    }
}
