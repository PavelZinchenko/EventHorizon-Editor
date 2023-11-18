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
	public partial class DebugSettings
	{
		partial void OnDataDeserialized(DebugSettingsSerializable serializable, Database database);
		partial void OnDataSerialized(ref DebugSettingsSerializable serializable);


		public DebugSettings(DebugSettingsSerializable serializable, Database database)
		{
			Codes = serializable.Codes?.Select(item => new DebugCode(item, database)).ToArray();
			OnDataDeserialized(serializable, database);
		}

		public void Save(DebugSettingsSerializable serializable)
		{
			if (Codes == null || Codes.Length == 0)
			    serializable.Codes = null;
			else
			    serializable.Codes = Codes.Select(item => item.Serialize()).ToArray();
			OnDataSerialized(ref serializable);
		}

		public DebugCode[] Codes;

		public static DebugSettings DefaultValue { get; private set; }
	}
}
