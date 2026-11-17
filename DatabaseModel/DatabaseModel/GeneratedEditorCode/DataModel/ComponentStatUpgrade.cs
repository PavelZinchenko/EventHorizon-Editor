//-------------------------------------------------------------------------------
//                                                                               
//    This code was automatically generated.                                     
//    Changes to this file may cause incorrect behavior and will be lost if      
//    the code is regenerated.                                                   
//                                                                               
//-------------------------------------------------------------------------------

using System.Linq;
using EditorDatabase.Enums;
using EditorDatabase.Serializable;
using EditorDatabase.Model;

namespace EditorDatabase.DataModel
{
	public partial class ComponentStatUpgrade
	{
		partial void OnDataDeserialized(ComponentStatUpgradeSerializable serializable, Database database);
		partial void OnDataSerialized(ref ComponentStatUpgradeSerializable serializable);

		public static ComponentStatUpgrade Create(ComponentStatUpgradeSerializable serializable, Database database)
		{
			if (serializable == null) return DefaultValue;
			return new ComponentStatUpgrade(serializable, database);
		}

		private ComponentStatUpgrade(ComponentStatUpgradeSerializable serializable, Database database)
		{
			try
			{
				Id = new ItemId<ComponentStatUpgrade>(serializable.Id, serializable.FileName);
				Type = serializable.Type;
				Name = serializable.Name;
				Icon = serializable.Icon;
				ValueType = serializable.ValueType;
				Value = serializable.Value;
				Resource = database.GetQuestItemId(serializable.Resource);
				Template = database.GetStatUpgradeTemplateId(serializable.Template);
			}
			catch (DatabaseException e)
			{
				throw new DatabaseException(this.GetType() + ": deserialization failed. " + serializable.FileName + " (" + serializable.Id + ")", e);
			}
			OnDataDeserialized(serializable, database);
		}

		public void Save(ComponentStatUpgradeSerializable serializable)
		{
			serializable.Type = Type;
			serializable.Name = Name;
			serializable.Icon = Icon;
			serializable.ValueType = ValueType;
			serializable.Value = Value;
			serializable.Resource = Resource.Value;
			serializable.Template = Template.Value;
			OnDataSerialized(ref serializable);
		}

		public readonly ItemId<ComponentStatUpgrade> Id;

		public StatUpgradeType Type;
		public string Name;
		public string Icon;
		public StatValueType ValueType;
		public string Value;
		public ItemId<QuestItem> Resource = ItemId<QuestItem>.Empty;
		public ItemId<StatUpgradeTemplate> Template = ItemId<StatUpgradeTemplate>.Empty;

		public static ComponentStatUpgrade DefaultValue { get; private set; }
	}
}
