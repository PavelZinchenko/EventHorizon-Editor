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
	public partial class QuestModel
	{
		partial void OnDataDeserialized(QuestSerializable serializable, Database database);


		public QuestModel(QuestSerializable serializable, Database database)
		{
			Id = new ItemId<QuestModel>(serializable.Id, serializable.FileName);
			Name = serializable.Name;
			QuestType = serializable.QuestType;
			StartCondition = serializable.StartCondition;
			Weight = new NumericValue<float>(serializable.Weight, 0f, 1000f);
			Requirement = new Requirement(serializable.Requirement, database);
			Level = new NumericValue<int>(serializable.Level, 0, 1000);
			Nodes = serializable.Nodes?.Select(item => new Node(item, database)).ToArray();

			OnDataDeserialized(serializable, database);
		}

		public void Save(QuestSerializable serializable)
		{
			serializable.Name = Name;
			serializable.QuestType = QuestType;
			serializable.StartCondition = StartCondition;
			serializable.Weight = Weight.Value;
			serializable.Requirement = Requirement.Serialize();
			serializable.Level = Level.Value;
			if (Nodes == null || Nodes.Length == 0)
			    serializable.Nodes = null;
			else
			    serializable.Nodes = Nodes.Select(item => item.Serialize()).ToArray();
		}

		public readonly ItemId<QuestModel> Id;

		public string Name;
		public QuestType QuestType;
		public StartCondition StartCondition;
		public NumericValue<float> Weight = new NumericValue<float>(0, 0f, 1000f);
		public Requirement Requirement = new Requirement();
		public NumericValue<int> Level = new NumericValue<int>(0, 0, 1000);
		public Node[] Nodes;

		public static QuestModel DefaultValue { get; private set; }
	}
}
