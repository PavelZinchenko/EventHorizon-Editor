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
	public partial class BehaviorTreeModel
	{
		partial void OnDataDeserialized(BehaviorTreeSerializable serializable, Database database);
		partial void OnDataSerialized(ref BehaviorTreeSerializable serializable);

		public static BehaviorTreeModel Create(BehaviorTreeSerializable serializable, Database database)
		{
			if (serializable == null) return DefaultValue;
			return new BehaviorTreeModel(serializable, database);
		}

		private BehaviorTreeModel(BehaviorTreeSerializable serializable, Database database)
		{
			try
			{
				Id = new ItemId<BehaviorTreeModel>(serializable.Id, serializable.FileName);
				RootNode = new ObjectWrapper<BehaviorTreeNode>(DataModel.BehaviorTreeNode.Create(serializable.RootNode, database), DataModel.BehaviorTreeNode.DefaultValue);
			}
			catch (DatabaseException e)
			{
				throw new DatabaseException(this.GetType() + ": deserialization failed. " + serializable.FileName + " (" + serializable.Id + ")", e);
			}
			OnDataDeserialized(serializable, database);
		}

		public void Save(BehaviorTreeSerializable serializable)
		{
			serializable.RootNode = RootNode.Value?.Serialize();
			OnDataSerialized(ref serializable);
		}

		public readonly ItemId<BehaviorTreeModel> Id;

		public ObjectWrapper<BehaviorTreeNode> RootNode;

		public static BehaviorTreeModel DefaultValue { get; private set; }
	}
}
