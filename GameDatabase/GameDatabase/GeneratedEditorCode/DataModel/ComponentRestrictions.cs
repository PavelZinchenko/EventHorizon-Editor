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
	public partial class ComponentRestrictions
	{
		partial void OnDataDeserialized(ComponentRestrictionsSerializable serializable, Database database);

		public ComponentRestrictions() {}

		public ComponentRestrictions(ComponentRestrictionsSerializable serializable, Database database)
		{
			ShipSizes = (SizeClass[])serializable.ShipSizes?.Clone();
			NotForOrganicShips = serializable.NotForOrganicShips;
			NotForMechanicShips = serializable.NotForMechanicShips;
			UniqueId = serializable.UniqueId;

			OnDataDeserialized(serializable, database);
		}

		public ComponentRestrictionsSerializable Serialize()
		{
			var serializable = new ComponentRestrictionsSerializable();
			if (ShipSizes == null || ShipSizes.Length == 0)
			    serializable.ShipSizes = null;
			else
			    serializable.ShipSizes = (SizeClass[])ShipSizes.Clone();
			serializable.NotForOrganicShips = NotForOrganicShips;
			serializable.NotForMechanicShips = NotForMechanicShips;
			serializable.UniqueId = UniqueId;
			return serializable;
		}

		public SizeClass[] ShipSizes;
		public bool NotForOrganicShips;
		public bool NotForMechanicShips;
		public string UniqueId;

		public static ComponentRestrictions DefaultValue { get; private set; }
	}
}
