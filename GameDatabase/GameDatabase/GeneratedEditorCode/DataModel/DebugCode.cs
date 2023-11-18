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
	public partial class DebugCode
	{
		partial void OnDataDeserialized(DebugCodeSerializable serializable, Database database);
		partial void OnDataSerialized(ref DebugCodeSerializable serializable);

		public DebugCode() {}

		public DebugCode(DebugCodeSerializable serializable, Database database)
		{
			Code = new NumericValue<int>(serializable.Code, 0, 999999);
			Loot = new LootContent(serializable.Loot, database);
			OnDataDeserialized(serializable, database);
		}

		public DebugCodeSerializable Serialize()
		{
			var serializable = new DebugCodeSerializable();
			serializable.Code = Code.Value;
			serializable.Loot = Loot.Serialize();
			OnDataSerialized(ref serializable);
			return serializable;
		}

		public NumericValue<int> Code = new NumericValue<int>(0, 0, 999999);
		public LootContent Loot = new LootContent();

		public static DebugCode DefaultValue { get; private set; }
	}
}
