using System.Linq;
using EditorDatabase.Enums;
using EditorDatabase.Serializable;

namespace EditorDatabase.DataModel
{
	public partial class Component
	{
        partial void OnDataDeserialized(ComponentSerializable serializable, Database database)
        {
            if (!string.IsNullOrEmpty(serializable.WeaponSlotType))
                WeaponSlotType = (WeaponSlotType)serializable.WeaponSlotType.First();
            if (!string.IsNullOrEmpty(serializable.CellType))
                CellType = (CellType)serializable.CellType.First();
        }

		partial void OnDataSerialized(ref ComponentSerializable serializable)
        {
            serializable.AmmunitionId = Ammunition.IsNull ? AmmunitionObsolete.Value : Ammunition.Value;
            serializable.WeaponSlotType = ((char)WeaponSlotType).ToString();
            serializable.CellType = ((char)CellType).ToString();
        }

        public WeaponSlotType WeaponSlotType;
        public CellType CellType;
	}
}
