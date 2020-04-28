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
	public partial class LootModel
	{
		partial void OnDataDeserialized(LootSerializable serializable, Database database);


		public LootModel(LootSerializable serializable, Database database)
		{
			Id = new ItemId<LootModel>(serializable.Id, serializable.FileName);
			Loot = new LootContent(serializable.Loot, database);

			OnDataDeserialized(serializable, database);
		}

		public void Save(LootSerializable serializable)
		{
			serializable.Loot = Loot.Serialize();
		}

		public readonly ItemId<LootModel> Id;

		public LootContent Loot = new LootContent();

		public static LootModel DefaultValue { get; private set; }
	}
}
