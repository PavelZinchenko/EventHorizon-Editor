using GameDatabase.GameDatabase.Helpers;
using GameDatabase.Model;
using GameDatabase.Serializable;

namespace GameDatabase.EditorModel.Quests
{
    public class Character
    {
        public Character(SerializableCharacter character, Database database)
        {
            ItemId = new ItemId<Character>(character.Id, character.FileName);

            Name = character.Name;
            AvatarIcon = character.AvatarIcon;
            Faction = database.GetFactionId(character.Faction);
            Relations = new NumericValue<int>(character.Relations, -100, 100);
        }

        public void Save(SerializableCharacter serializable)
        {
            serializable.Name = Name;
            serializable.AvatarIcon = AvatarIcon;
            serializable.Faction = Faction.Id;
            serializable.Relations = Relations.Value;
        }

        public readonly ItemId<Character> ItemId;

        public string Name;

        [AutoCompleteAtribute(AutoCompleteAtribute.Type.SoundObsolete)]
        public string AvatarIcon;
        public ItemId<Faction> Faction;
        public NumericValue<int> Relations;
    }
}
