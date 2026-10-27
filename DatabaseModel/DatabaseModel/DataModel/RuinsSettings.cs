using EditorDatabase.Serializable;

namespace EditorDatabase.DataModel
{
	public partial class RuinsSettings
	{
		partial void OnDataDeserialized(RuinsSettingsSerializable serializable, Database database)
		{
			ExcludedSatellites = SatelliteExclusions.Resolve(database, serializable.ExcludedSatellites);
		}
	}
}
