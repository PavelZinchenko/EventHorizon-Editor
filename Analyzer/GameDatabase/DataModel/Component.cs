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
