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
	public partial class BlackMarketSettings
	{
		partial void OnDataDeserialized(BlackMarketSettingsSerializable serializable, Database database);
		partial void OnDataSerialized(ref BlackMarketSettingsSerializable serializable);

		public static BlackMarketSettings Create(BlackMarketSettingsSerializable serializable, Database database)
		{
			if (serializable == null) return DefaultValue;
			return new BlackMarketSettings(serializable, database);
		}

		private BlackMarketSettings(BlackMarketSettingsSerializable serializable, Database database)
		{
			ExcludedSatellites = serializable.ExcludedSatellites?.Select(id => new Wrapper<Satellite> { Item = database.GetSatelliteId(id) }).ToArray();
			OnDataDeserialized(serializable, database);
		}

		public void Save(BlackMarketSettingsSerializable serializable)
		{
			if (ExcludedSatellites == null || ExcludedSatellites.Length == 0)
			    serializable.ExcludedSatellites = null;
			else
			    serializable.ExcludedSatellites = ExcludedSatellites.Select(wrapper => wrapper.Item.Value).ToArray();
			OnDataSerialized(ref serializable);
		}

		public Wrapper<Satellite>[] ExcludedSatellites;

		public static BlackMarketSettings DefaultValue { get; private set; }
	}
}
