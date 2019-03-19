using System;
using System.Linq;
using GameDatabase.Enums.Quests;
using GameDatabase.Model;
using GameDatabase.Serializable;

namespace GameDatabase.EditorModel.Quests
{
    public interface IRequirementContent
    {
        void Load(SerializableRequirement serializable, Database database);
        void Save(SerializableRequirement serializable);
    }

    public static class RequirementFactory
    {
        public static IRequirementContent CreateRequirement(RequirementType type)
        {
            switch (type)
            {
                case RequirementType.Empty:
                case RequirementType.AggressiveOccupants:
                case RequirementType.ComeBack:
                    return new EmptyContent();
                case RequirementType.All:
                case RequirementType.Any:
                case RequirementType.None:
                    return new BooleanContent();
                case RequirementType.HaveItem:
                    return new RequiredItemsContent();
                case RequirementType.HaveArtifact:
                    return new RequiredArtifactContent();
                case RequirementType.PlayerPosition:
                case RequirementType.RandomStarSystem:
                    return new RandomStarContent();
                case RequirementType.QuestCompleted:
                case RequirementType.QuestActive:
                    return new QuestContent();
                case RequirementType.CharacterRelations:
                    return new CharacterRelationsContent();
                case RequirementType.FactionRelations:
                    return new FactionRelationsContent();
                case RequirementType.Faction:
                    return new FactionContent();
                default:
                    throw new ArgumentException("Unknown requirement type - " + type);
            }
        }
    }

    public class EmptyContent : IRequirementContent
    {
        public void Load(SerializableRequirement serializable, Database database) { }
        public void Save(SerializableRequirement serializable) { }
    }

    public class BooleanContent : IRequirementContent
    {
        public void Load(SerializableRequirement serializable, Database database)
        {
            Requirements = serializable.Requirements?.Select(item => new Requirement(item, database)).ToArray() ?? new Requirement[] {};
        }

        public void Save(SerializableRequirement serializable)
        {
            if (Requirements.Length == 0)
            {
                serializable.Requirements = null;
                return;
            }

            serializable.Requirements = Requirements.Select(item => item.Save()).ToArray();
        }

        public Requirement[] Requirements = {};
    }

    public class RequiredItemsContent : IRequirementContent
    {
        public void Load(SerializableRequirement serializable, Database database)
        {
            LootId = database.GetLootId(serializable.ItemId);
        }

        public void Save(SerializableRequirement serializable)
        {
            serializable.ItemId = LootId.Id;
        }

        public ItemId<Loot> LootId;
    }

    public class RequiredArtifactContent : IRequirementContent
    {
        public void Load(SerializableRequirement serializable, Database database)
        {
            Item = database.GetArtifactId(serializable.ItemId);
            Amount = new NumericValue<int>(serializable.MinValue, 0, 1000000);
        }

        public void Save(SerializableRequirement serializable)
        {
            serializable.ItemId = Item.Id;
            serializable.MinValue = Amount.Value;
        }

        public ItemId<Artifact> Item;
        public NumericValue<int> Amount = new NumericValue<int>(0, 0, 1000000);
    }

    public class QuestContent : IRequirementContent
    {
        public void Load(SerializableRequirement serializable, Database database)
        {
            QuestId = database.GetQuest(serializable.ItemId)?.ItemId ?? ItemId<QuestModel>.Empty;
        }

        public void Save(SerializableRequirement serializable)
        {
            serializable.ItemId = QuestId.Id;
        }

        public ItemId<QuestModel> QuestId;
    }

    public class RandomStarContent : IRequirementContent
    {
        public void Load(SerializableRequirement serializable, Database database)
        {
            MinDistance.Value = serializable.MinValue;
            MaxDistance.Value = serializable.MaxValue;
        }

        public void Save(SerializableRequirement serializable)
        {
            serializable.MinValue = MinDistance.Value;
            serializable.MaxValue = MaxDistance.Value;
        }

        public NumericValue<int> MinDistance = new NumericValue<int>(0, 0, 1000);
        public NumericValue<int> MaxDistance = new NumericValue<int>(0, 0, 1000);
    }

    public class CharacterRelationsContent : IRequirementContent
    {
        public void Load(SerializableRequirement serializable, Database database)
        {
            MinValue.Value = serializable.MinValue;
            MaxValue.Value = serializable.MaxValue;
            Character = database.GetCharacterId(serializable.Character);
        }

        public void Save(SerializableRequirement serializable)
        {
            serializable.MinValue = MinValue.Value;
            serializable.MaxValue = MaxValue.Value;
            serializable.Character = Character.Id;
        }

        public ItemId<Character> Character = ItemId<Character>.Empty;
        public NumericValue<int> MinValue = new NumericValue<int>(0, -100, 100);
        public NumericValue<int> MaxValue = new NumericValue<int>(0, -100, 100);
    }

    public class FactionRelationsContent : IRequirementContent
    {
        public void Load(SerializableRequirement serializable, Database database)
        {
            MinValue.Value = serializable.MinValue;
            MaxValue.Value = serializable.MaxValue;
        }

        public void Save(SerializableRequirement serializable)
        {
            serializable.MinValue = MinValue.Value;
            serializable.MaxValue = MaxValue.Value;
        }

        public NumericValue<int> MinValue = new NumericValue<int>(0, -100, 100);
        public NumericValue<int> MaxValue = new NumericValue<int>(0, -100, 100);
    }

    public class FactionContent : IRequirementContent
    {
        public void Load(SerializableRequirement serializable, Database database)
        {
            Faction = database.GetFactionId(serializable.Faction);
        }

        public void Save(SerializableRequirement serializable)
        {
            serializable.Faction = Faction.Id;
        }

        public ItemId<Faction> Faction = ItemId<Faction>.Empty;
    }
}
