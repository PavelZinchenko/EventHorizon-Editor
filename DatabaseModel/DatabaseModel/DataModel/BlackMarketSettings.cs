using EditorDatabase.Serializable;

namespace EditorDatabase.DataModel
{
	public partial class BlackMarketSettings
	{
		partial void OnDataDeserialized(BlackMarketSettingsSerializable serializable, Database database)
		{
			ExcludedSatellites = SatelliteExclusions.Resolve(database, serializable.ExcludedSatellites);
		}
	}
}
