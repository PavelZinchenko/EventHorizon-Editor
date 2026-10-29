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
			ExcludedSatellites = serializable.ExcludedSatellites?.Select(id => new Wrapper<Satellite> { Item = database.GetSatelliteId(id) }).ToArray();
			MaxSatelliteCells = serializable.MaxSatelliteCells;
			OnDataDeserialized(serializable, database);
		}

		public void Save(RuinsSettingsSerializable serializable)
		{
			if (ExcludedSatellites == null || ExcludedSatellites.Length == 0)
			    serializable.ExcludedSatellites = null;
			else
			    serializable.ExcludedSatellites = ExcludedSatellites.Select(wrapper => wrapper.Item.Value).ToArray();
			serializable.MaxSatelliteCells = MaxSatelliteCells;
			OnDataSerialized(ref serializable);
		}

		public Wrapper<Satellite>[] ExcludedSatellites;
		public string MaxSatelliteCells;

		public static RuinsSettings DefaultValue { get; private set; }
	}
}
