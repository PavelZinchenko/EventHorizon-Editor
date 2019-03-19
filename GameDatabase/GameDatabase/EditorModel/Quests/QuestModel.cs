using System.Linq;
using GameDatabase.Enums.Quests;
using GameDatabase.Model;
using GameDatabase.Serializable;

namespace GameDatabase.EditorModel.Quests
{
    public class QuestModel
    {
        public QuestModel(SerializableQuest quest, Database database)
        {
            ItemId = new ItemId<QuestModel>(quest.Id, quest.FileName);
            Name = quest.Name;
            StartCondition = (StartCondition)quest.StartCondition;
            Weight = new NumericValue<float>(quest.Weight, 0, 100);
            Requirement = new Requirement(quest.Requirement ?? new SerializableRequirement(), database);
            QuestType = (QuestType)quest.QuestType;
            Level = new NumericValue<int>(quest.Level, 0, 1000);

            Nodes = quest.Nodes?.Select(item => new QuestNode(item, database)).ToArray();
        }

        public void Save(SerializableQuest serializable)
        {
            serializable.Name = Name;
            serializable.StartCondition = (int)StartCondition;
            serializable.Weight = Weight.Value;
            serializable.Requirement = Requirement.Save();
            serializable.QuestType = (int)QuestType;
            serializable.Level = Level.Value;
            serializable.Nodes = Nodes?.Select(item => item.Save()).ToArray();
        }

        public readonly ItemId<QuestModel> ItemId;
        public string Name;
        public QuestType QuestType;
        public StartCondition StartCondition;
        public NumericValue<float> Weight;
        public Requirement Requirement;
        public NumericValue<int> Level;
        public QuestNode[] Nodes;
    }
}
