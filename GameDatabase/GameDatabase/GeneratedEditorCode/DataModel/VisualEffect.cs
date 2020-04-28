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
	public partial class VisualEffect
	{
		partial void OnDataDeserialized(VisualEffectSerializable serializable, Database database);


		public VisualEffect(VisualEffectSerializable serializable, Database database)
		{
			Id = new ItemId<VisualEffect>(serializable.Id, serializable.FileName);
			Elements = serializable.Elements?.Select(item => new VisualEffectElement(item, database)).ToArray();

			OnDataDeserialized(serializable, database);
		}

		public void Save(VisualEffectSerializable serializable)
		{
			if (Elements == null || Elements.Length == 0)
			    serializable.Elements = null;
			else
			    serializable.Elements = Elements.Select(item => item.Serialize()).ToArray();
		}

		public readonly ItemId<VisualEffect> Id;

		public VisualEffectElement[] Elements;

		public static VisualEffect DefaultValue { get; private set; }
	}
}
