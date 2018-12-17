using GameDatabase.Enums;
using GameDatabase.Model;
using GameDatabase.Serializable;

namespace GameDatabase.EditorModel
{
    public class ComponentMod
    {
        public ComponentMod(SerializableComponentMod mod, Database database)
        {
            ItemId = new ItemId<ComponentMod>(mod.Id, mod.FileName);
            Type = mod.Type;
        }

        public void Save(SerializableComponentMod serializable)
        {
            serializable.Type = Type;
        }

        public readonly ItemId<ComponentMod> ItemId;

        public ComponentModType Type;
    }
}
