using System;
using System.Linq;
using GameDatabase.Enums.Quests;
using GameDatabase.Model;
using GameDatabase.Serializable;

namespace GameDatabase.EditorModel.Quests
{
    public interface INodeContent
    {
        void Load(SerializableNode serializable, Database database);
        void Save(SerializableNode serializable);
    }

    public static class NodeFactory
    {
        public static INodeContent CreateNode(NodeType type)
        {
            switch (type)
            {
                case NodeType.ShowDialog:
                    return new TextNodeContent();
                case NodeType.Switch:
                case NodeType.Random:
                    return new SwitchNodeContent();
                case NodeType.CompleteQuest:
                case NodeType.FailQuest:
                case NodeType.Undefined:
                case NodeType.ComingSoon:
                    return new EmptyNodeContent();
                case NodeType.Retreat:
                case NodeType.DestroyOccupants:
                case NodeType.SuppressOccupants:
                    return new SimpleNodeContent();
                case NodeType.AttackFleet:
                    return new CombatNodeContent();
                case NodeType.AttackOccupants:
                    return new LocalCombatNodeContent();
                case NodeType.ReceiveItem:
                case NodeType.RemoveItem:
                case NodeType.Trade:
                    return new LootNodeContent();
                case NodeType.StartQuest:
                    return new QuestNodeContent();
                case NodeType.ChangeFactionRelations:
                case NodeType.SetFactionRelations:
                    return new FactionRelationsNodeContent();
                case NodeType.ChangeCharacterRelations:
                case NodeType.SetCharacterRelations:
                    return new CharacterRelationsNodeContent();
                default:
                    throw new ArgumentException("Unknown node type - " + type);
            }
        }
    }

    public class EmptyNodeContent : INodeContent
    {
        public void Load(SerializableNode serializable, Database database) { }
        public void Save(SerializableNode serializable) { }
    }

    public class SimpleNodeContent : INodeContent
    {
        public void Load(SerializableNode serializable, Database database)
        {
            Transition = new NumericValue<int>(serializable.DefaultTransition, 1, 1000);
        }

        public void Save(SerializableNode serializable)
        {
            serializable.DefaultTransition = Transition.Value;
        }

        public NumericValue<int> Transition = new NumericValue<int>(1,1,1000);
    }

    public class QuestNodeContent : INodeContent
    {
        public void Load(SerializableNode serializable, Database database)
        {
            Transition = new NumericValue<int>(serializable.DefaultTransition, 1, 1000);
            Quest = database.GetQuest(serializable.Quest)?.ItemId ?? ItemId<QuestModel>.Empty;
        }

        public void Save(SerializableNode serializable)
        {
            serializable.DefaultTransition = Transition.Value;
            serializable.Quest = Quest.Id;
        }

        public ItemId<QuestModel> Quest = ItemId<QuestModel>.Empty;
        public NumericValue<int> Transition = new NumericValue<int>(1, 1, 1000);
    }

    public class CombatNodeContent : INodeContent
    {
        public void Load(SerializableNode serializable, Database database)
        {
            VictoryTransition = new NumericValue<int>(serializable.DefaultTransition, 1, 1000);
            DefeatTransition = new NumericValue<int>(serializable.FailureTransition, 1, 1000);
            Enemy = database.GetFleetId(serializable.Enemy);
            Loot = database.GetLootId(serializable.Loot);
        }

        public void Save(SerializableNode serializable)
        {
            serializable.DefaultTransition = VictoryTransition.Value;
            serializable.FailureTransition = DefeatTransition.Value;
            serializable.Enemy = Enemy.Id;
            serializable.Loot = Loot.Id;
        }

        public NumericValue<int> VictoryTransition = new NumericValue<int>(1, 1, 1000);
        public NumericValue<int> DefeatTransition = new NumericValue<int>(1, 1, 1000);
        public ItemId<Fleet> Enemy = ItemId<Fleet>.Empty;
        public ItemId<Loot> Loot = ItemId<Loot>.Empty;
    }

    public class LocalCombatNodeContent : INodeContent
    {
        public void Load(SerializableNode serializable, Database database)
        {
            VictoryTransition = new NumericValue<int>(serializable.DefaultTransition, 1, 1000);
            DefeatTransition = new NumericValue<int>(serializable.FailureTransition, 1, 1000);
        }

        public void Save(SerializableNode serializable)
        {
            serializable.DefaultTransition = VictoryTransition.Value;
            serializable.FailureTransition = DefeatTransition.Value;
        }

        public NumericValue<int> VictoryTransition = new NumericValue<int>(1, 1, 1000);
        public NumericValue<int> DefeatTransition = new NumericValue<int>(1, 1, 1000);
    }

    public class LootNodeContent : INodeContent
    {
        public void Load(SerializableNode serializable, Database database)
        {
            Loot = database.GetLootId(serializable.Loot);
            Transition = new NumericValue<int>(serializable.DefaultTransition, 1, 1000);
        }

        public void Save(SerializableNode serializable)
        {
            serializable.DefaultTransition = Transition.Value;
            serializable.Loot = Loot.Id;
        }

        public ItemId<Loot> Loot = ItemId<Loot>.Empty;
        public NumericValue<int> Transition = new NumericValue<int>(1, 1, 1000);
    }


    public class TextNodeContent : INodeContent
    {
        public void Load(SerializableNode serializable, Database database)
        {
            Message = serializable.Message;
            Character = database.GetCharacterId(serializable.Character);
            Enemy = database.GetFleetId(serializable.Enemy);
            Loot = database.GetLootId(serializable.Loot);
            RequiredView = (RequiredViewMode)serializable.RequiredView;

            Actions = serializable.Actions?.Select(action =>
            {
                var requirement = new Requirement(action.Requirement ?? new SerializableRequirement(), database);
                return new QuestAction
                {
                    ButtonText = action.ButtonText,
                    Requirement = requirement,
                    TargetNode = new NumericValue<int>(action.TargetNode, 1, 1000)
                };
            }).ToArray();
        }

        public void Save(SerializableNode serializable)
        {
            serializable.Message = Message;
            serializable.RequiredView = (int)RequiredView;
            serializable.Character = Character.Id;
            serializable.Enemy = Enemy.Id;
            serializable.Loot = Loot.Id;

            if (Actions == null || Actions.Length == 0)
                serializable.Actions = null;
            else
            {
                serializable.Actions = Actions.Select(action => new SerializableNode.Action
                {
                    ButtonText = action.ButtonText,
                    TargetNode = action.TargetNode.Value,
                    Requirement = action.Requirement.Save(),
                }).ToArray();
            }
        }

        public string Message;
        public ItemId<Character> Character = ItemId<Character>.Empty;
        public ItemId<Fleet> Enemy = ItemId<Fleet>.Empty;
        public ItemId<Loot> Loot = ItemId<Loot>.Empty;
        public RequiredViewMode RequiredView;
        public QuestAction[] Actions;

        public class QuestAction
        {
            public NumericValue<int> TargetNode = new NumericValue<int>(1, 1, 1000);
            public Requirement Requirement = new Requirement();
            public string ButtonText;
        }
    }

    public class SwitchNodeContent : INodeContent
    {
        public void Load(SerializableNode serializable, Database database)
        {
            DefaultTransition = new NumericValue<int>(serializable.DefaultTransition, 0, 1000);
            QuestLogText = serializable.Message;

            Transitions = serializable.Transitions?.Select(transition =>
            {
                var requirement = new Requirement(transition.Requirement ?? new SerializableRequirement(), database);
                return new QuestTransition
                {
                    TargetNode = new NumericValue<int>(transition.TargetNode, 1, 1000),
                    Requirement = requirement,
                    Weight = new NumericValue<float>(transition.Weight, 0, 100),
                };
            }).ToArray();
        }

        public void Save(SerializableNode serializable)
        {
            serializable.DefaultTransition = DefaultTransition.Value;
            serializable.Message = QuestLogText;

            if (Transitions == null || Transitions.Length == 0)
                serializable.Actions = null;
            else
            {
                serializable.Transitions = Transitions.Select(transition => new SerializableNode.Transition
                {
                    TargetNode = transition.TargetNode.Value,
                    Requirement = transition.Requirement.Save(),
                    Weight = transition.Weight.Value,
                }).ToArray();
            }
        }

        public string QuestLogText;
        public NumericValue<int> DefaultTransition = new NumericValue<int>(1, 0, 1000);
        public QuestTransition[] Transitions;

        public class QuestTransition
        {
            public NumericValue<int> TargetNode = new NumericValue<int>(1, 1, 1000);
            public Requirement Requirement = new Requirement();
            public NumericValue<float> Weight = new NumericValue<float>(0, 0, 100);
        }
    }

    public class FactionRelationsNodeContent : INodeContent
    {
        public void Load(SerializableNode serializable, Database database)
        {
            Transition = new NumericValue<int>(serializable.DefaultTransition, 1, 1000);
            Value = new NumericValue<int>(serializable.Value, -100, 100);
        }

        public void Save(SerializableNode serializable)
        {
            serializable.DefaultTransition = Transition.Value;
            serializable.Value = Value.Value;
        }

        public NumericValue<int> Value = new NumericValue<int>(0, -100, 100);
        public NumericValue<int> Transition = new NumericValue<int>(1, 1, 1000);
    }

    public class CharacterRelationsNodeContent : INodeContent
    {
        public void Load(SerializableNode serializable, Database database)
        {
            Transition = new NumericValue<int>(serializable.DefaultTransition, 1, 1000);
            Character = database.GetCharacterId(serializable.Character);
            Value = new NumericValue<int>(serializable.Value, -100, 100);
        }

        public void Save(SerializableNode serializable)
        {
            serializable.DefaultTransition = Transition.Value;
            serializable.Character = Character.Id;
            serializable.Value = Value.Value;
        }

        public ItemId<Character> Character = ItemId<Character>.Empty;
        public NumericValue<int> Value = new NumericValue<int>(0, -100, 100);
        public NumericValue<int> Transition = new NumericValue<int>(1, 1, 1000);
    }
}
