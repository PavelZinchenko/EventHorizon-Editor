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
	public partial class NodeAction
	{
		partial void OnDataDeserialized(NodeActionSerializable serializable, Database database);
		partial void OnDataSerialized(ref NodeActionSerializable serializable);

		public NodeAction() {}

		public NodeAction(NodeActionSerializable serializable, Database database)
		{
			TargetNode = new NumericValue<int>(serializable.TargetNode, 1, 1000);
			Requirement = new Requirement(serializable.Requirement, database);
			ButtonText = serializable.ButtonText;
			OnDataDeserialized(serializable, database);
		}

		public NodeActionSerializable Serialize()
		{
			var serializable = new NodeActionSerializable();
			serializable.TargetNode = TargetNode.Value;
			serializable.Requirement = Requirement.Serialize();
			serializable.ButtonText = ButtonText;
			OnDataSerialized(ref serializable);
			return serializable;
		}

		public NumericValue<int> TargetNode = new NumericValue<int>(0, 1, 1000);
		public Requirement Requirement = new Requirement();
		public string ButtonText;

		public static NodeAction DefaultValue { get; private set; }
	}
}
