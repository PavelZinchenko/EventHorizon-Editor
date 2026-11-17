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
	public partial class ResearchSetting
	{
		partial void OnDataDeserialized(ResearchSettingSerializable serializable, Database database);
		partial void OnDataSerialized(ref ResearchSettingSerializable serializable);

		public static ResearchSetting Create(ResearchSettingSerializable serializable, Database database)
		{
			if (serializable == null) return DefaultValue;
			return new ResearchSetting(serializable, database);
		}

		private ResearchSetting(ResearchSettingSerializable serializable, Database database)
		{
			ResearchResource1 = database.GetQuestItemId(serializable.ResearchResource1);
			ResearchResource2 = database.GetQuestItemId(serializable.ResearchResource2);
			ResearchResource3 = database.GetQuestItemId(serializable.ResearchResource3);
			ResearchResource4 = database.GetQuestItemId(serializable.ResearchResource4);
			ResearchResource5 = database.GetQuestItemId(serializable.ResearchResource5);
			OnDataDeserialized(serializable, database);
		}

		public void Save(ResearchSettingSerializable serializable)
		{
			serializable.ResearchResource1 = ResearchResource1.Value;
			serializable.ResearchResource2 = ResearchResource2.Value;
			serializable.ResearchResource3 = ResearchResource3.Value;
			serializable.ResearchResource4 = ResearchResource4.Value;
			serializable.ResearchResource5 = ResearchResource5.Value;
			OnDataSerialized(ref serializable);
		}

		public ItemId<QuestItem> ResearchResource1 = ItemId<QuestItem>.Empty;
		public ItemId<QuestItem> ResearchResource2 = ItemId<QuestItem>.Empty;
		public ItemId<QuestItem> ResearchResource3 = ItemId<QuestItem>.Empty;
		public ItemId<QuestItem> ResearchResource4 = ItemId<QuestItem>.Empty;
		public ItemId<QuestItem> ResearchResource5 = ItemId<QuestItem>.Empty;

		public static ResearchSetting DefaultValue { get; private set; }
	}
}
