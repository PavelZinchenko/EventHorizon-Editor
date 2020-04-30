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
		partial void OnDataSerialized(ref ComponentRestrictionsSerializable serializable);

		public ComponentRestrictions() {}

		public ComponentRestrictions(ComponentRestrictionsSerializable serializable, Database database)
		{
			ShipSizes = (SizeClass[])serializable.ShipSizes?.Clone();
			NotForOrganicShips = serializable.NotForOrganicShips;
			NotForMechanicShips = serializable.NotForMechanicShips;
			UniqueComponentTag = serializable.UniqueComponentTag;

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
			serializable.UniqueComponentTag = UniqueComponentTag;
			OnDataSerialized(ref serializable);
			return serializable;
		}

		public SizeClass[] ShipSizes;
		public bool NotForOrganicShips;
		public bool NotForMechanicShips;
		public string UniqueComponentTag;

		public static ComponentRestrictions DefaultValue { get; private set; }
	}
}
