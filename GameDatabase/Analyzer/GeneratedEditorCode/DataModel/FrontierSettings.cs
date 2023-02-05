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
	public partial class FrontierSettings
	{
		partial void OnDataDeserialized(FrontierSettingsSerializable serializable, Database database);
		partial void OnDataSerialized(ref FrontierSettingsSerializable serializable);


		public FrontierSettings(FrontierSettingsSerializable serializable, Database database)
		{
			CustomShipLevels = serializable.CustomShipLevels?.Select(item => new ShipToValue(item, database)).ToArray();
			ExplorationShips = serializable.ExplorationShips?.Select(id => new Wrapper<Ship> { Item = database.GetShipId(id) }).ToArray();
			OnDataDeserialized(serializable, database);
		}

		public void Save(FrontierSettingsSerializable serializable)
		{
			if (CustomShipLevels == null || CustomShipLevels.Length == 0)
			    serializable.CustomShipLevels = null;
			else
			    serializable.CustomShipLevels = CustomShipLevels.Select(item => item.Serialize()).ToArray();
			if (ExplorationShips == null || ExplorationShips.Length == 0)
			    serializable.ExplorationShips = null;
			else
			    serializable.ExplorationShips = ExplorationShips.Select(wrapper => wrapper.Item.Value).ToArray();
			OnDataSerialized(ref serializable);
		}

		public ShipToValue[] CustomShipLevels;
		public Wrapper<Ship>[] ExplorationShips;

		public static FrontierSettings DefaultValue { get; private set; }
	}
}
