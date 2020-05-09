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
	public partial class Component
	{
        partial void OnDataDeserialized(ComponentSerializable serializable, Database database)
        {

        }

		partial void OnDataSerialized(ref ComponentSerializable serializable)
        {
            serializable.AmmunitionId = Ammunition.IsNull ? AmmunitionObsolete.Value : Ammunition.Value;
        }
	}
}
