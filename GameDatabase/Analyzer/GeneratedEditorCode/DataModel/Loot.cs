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
		partial void OnDataSerialized(ref LootSerializable serializable);


		public LootModel(LootSerializable serializable, Database database)
		{
			try
			{
				Id = new ItemId<LootModel>(serializable.Id, serializable.FileName);
				Loot = new LootContent(serializable.Loot, database);
			}
			catch (DatabaseException e)
			{
				throw new DatabaseException(this.GetType() + ": deserialization failed. " + serializable.FileName + " (" + serializable.Id + ")", e);
			}
			OnDataDeserialized(serializable, database);
		}

		public void Save(LootSerializable serializable)
		{
			serializable.Loot = Loot.Serialize();
			OnDataSerialized(ref serializable);
		}

		public readonly ItemId<LootModel> Id;

		public LootContent Loot = new LootContent();

		public static LootModel DefaultValue { get; private set; }
	}
}
