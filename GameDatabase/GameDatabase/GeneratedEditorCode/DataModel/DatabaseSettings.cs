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
	public partial class DatabaseSettings
	{
		partial void OnDataDeserialized(DatabaseSettingsSerializable serializable, Database database);


		public DatabaseSettings(DatabaseSettingsSerializable serializable, Database database)
		{
			DatabaseVersion = new NumericValue<int>(serializable.DatabaseVersion, 1, 2147483647);
			ModName = serializable.ModName;
			ModId = serializable.ModId;
			ModVersion = new NumericValue<int>(serializable.ModVersion, -2147483648, 2147483647);

			OnDataDeserialized(serializable, database);
		}

		public void Save(DatabaseSettingsSerializable serializable)
		{
			serializable.DatabaseVersion = DatabaseVersion.Value;
			serializable.ModName = ModName;
			serializable.ModId = ModId;
			serializable.ModVersion = ModVersion.Value;
		}

		public NumericValue<int> DatabaseVersion = new NumericValue<int>(0, 1, 2147483647);
		public string ModName;
		public string ModId;
		public NumericValue<int> ModVersion = new NumericValue<int>(0, -2147483648, 2147483647);

		public static DatabaseSettings DefaultValue { get; private set; }
	}
}
