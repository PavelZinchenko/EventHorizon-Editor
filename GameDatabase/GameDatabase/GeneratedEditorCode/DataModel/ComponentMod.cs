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
	public partial class ComponentMod
	{
		partial void OnDataDeserialized(ComponentModSerializable serializable, Database database);
		partial void OnDataSerialized(ref ComponentModSerializable serializable);


		public ComponentMod(ComponentModSerializable serializable, Database database)
		{
			try
			{
				Id = new ItemId<ComponentMod>(serializable.Id, serializable.FileName);
				Type = serializable.Type;
			}
			catch (DatabaseException e)
			{
				throw new DatabaseException(this.GetType() + ": deserialization failed. " + serializable.FileName + " (" + serializable.Id + ")", e);
			}
			OnDataDeserialized(serializable, database);
		}

		public void Save(ComponentModSerializable serializable)
		{
			serializable.Type = Type;
			OnDataSerialized(ref serializable);
		}

		public readonly ItemId<ComponentMod> Id;

		public ComponentModType Type;

		public static ComponentMod DefaultValue { get; private set; }
	}
}
