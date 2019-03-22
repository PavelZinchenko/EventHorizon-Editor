using System.Drawing;
using GameDatabase.Model;
using GameDatabase.Serializable;

namespace GameDatabase.EditorModel.Quests
{
    public class QuestItem
    {
        public QuestItem(SerializableQuestItem questItem, Database database)
        {
            ItemId = new ItemId<QuestItem>(questItem.Id, questItem.FileName);

            Name = questItem.Name;
            Description = questItem.Description;
            Icon = questItem.Icon;
            Color = Helpers.ColorFromString(questItem.Color);
            Price = new NumericValue<int>(questItem.Price, 0, 1000000);
        }

        public void Save(SerializableQuestItem serializable)
        {
            serializable.Name = Name;
            serializable.Color = Helpers.ColorToString(Color);
            serializable.Description = Description;
            serializable.Icon = Icon;
            serializable.Price = Price.Value;
        }

        public readonly ItemId<QuestItem> ItemId;
        public string Name;
        public string Description;
        public string Icon;
        public Color Color;
        public NumericValue<int> Price;
    }
}
