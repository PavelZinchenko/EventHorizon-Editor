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
	public partial class PvpSettings
	{
		partial void OnDataDeserialized(PvpSettingsSerializable serializable, Database database);
		partial void OnDataSerialized(ref PvpSettingsSerializable serializable);

		public static PvpSettings Create(PvpSettingsSerializable serializable, Database database)
		{
			if (serializable == null) return DefaultValue;
			return new PvpSettings(serializable, database);
		}

		private PvpSettings(PvpSettingsSerializable serializable, Database database)
		{
			ArenaPrice = database.GetQuestItemId(serializable.ArenaPrice);
			OnDataDeserialized(serializable, database);
		}

		public void Save(PvpSettingsSerializable serializable)
		{
			serializable.ArenaPrice = ArenaPrice.Value;
			OnDataSerialized(ref serializable);
		}

		public ItemId<QuestItem> ArenaPrice = ItemId<QuestItem>.Empty;

		public static PvpSettings DefaultValue { get; private set; }
	}
}
