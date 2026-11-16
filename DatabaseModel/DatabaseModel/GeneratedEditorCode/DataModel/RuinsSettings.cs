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
	public partial class RuinsSettings
	{
		partial void OnDataDeserialized(RuinsSettingsSerializable serializable, Database database);
		partial void OnDataSerialized(ref RuinsSettingsSerializable serializable);

		public static RuinsSettings Create(RuinsSettingsSerializable serializable, Database database)
		{
			if (serializable == null) return DefaultValue;
			return new RuinsSettings(serializable, database);
		}

		private RuinsSettings(RuinsSettingsSerializable serializable, Database database)
		{
			MaxSatelliteCells = serializable.MaxSatelliteCells;
			OnDataDeserialized(serializable, database);
		}

		public void Save(RuinsSettingsSerializable serializable)
		{
			serializable.MaxSatelliteCells = MaxSatelliteCells;
			OnDataSerialized(ref serializable);
		}

		public string MaxSatelliteCells;

		public static RuinsSettings DefaultValue { get; private set; }
	}
}
