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
	public partial class LootItem
	{
		partial void OnDataDeserialized(LootItemSerializable serializable, Database database);
		partial void OnDataSerialized(ref LootItemSerializable serializable);

		public LootItem() {}

		public LootItem(LootItemSerializable serializable, Database database)
		{
			Weight = new NumericValue<float>(serializable.Weight, -3.402823E+38f, 3.402823E+38f);
			Loot = new LootContent(serializable.Loot, database);
			OnDataDeserialized(serializable, database);
		}

		public LootItemSerializable Serialize()
		{
			var serializable = new LootItemSerializable();
			serializable.Weight = Weight.Value;
			serializable.Loot = Loot.Serialize();
			OnDataSerialized(ref serializable);
			return serializable;
		}

		public NumericValue<float> Weight = new NumericValue<float>(0, -3.402823E+38f, 3.402823E+38f);
		public LootContent Loot = new LootContent();

		public static LootItem DefaultValue { get; private set; }
	}
}
