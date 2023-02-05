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
	public partial class QuestOrigin
	{
		partial void OnDataDeserialized(QuestOriginSerializable serializable, Database database);
		partial void OnDataSerialized(ref QuestOriginSerializable serializable);

		public QuestOrigin() {}

		public QuestOrigin(QuestOriginSerializable serializable, Database database)
		{
			Type = serializable.Type;
			Factions = new RequiredFactions(serializable.Factions, database);
			MinDistance = new NumericValue<int>(serializable.MinDistance, 0, 9999);
			MaxDistance = new NumericValue<int>(serializable.MaxDistance, 0, 9999);
			MinRelations = new NumericValue<int>(serializable.MinRelations, -100, 100);
			MaxRelations = new NumericValue<int>(serializable.MaxRelations, -100, 100);
			OnDataDeserialized(serializable, database);
		}

		public QuestOriginSerializable Serialize()
		{
			var serializable = new QuestOriginSerializable();
			serializable.Type = Type;
			serializable.Factions = Factions.Serialize();
			serializable.MinDistance = MinDistance.Value;
			serializable.MaxDistance = MaxDistance.Value;
			serializable.MinRelations = MinRelations.Value;
			serializable.MaxRelations = MaxRelations.Value;
			OnDataSerialized(ref serializable);
			return serializable;
		}

		public QuestOriginType Type;
		public RequiredFactions Factions = new RequiredFactions();
		public NumericValue<int> MinDistance = new NumericValue<int>(0, 0, 9999);
		public NumericValue<int> MaxDistance = new NumericValue<int>(0, 0, 9999);
		public NumericValue<int> MinRelations = new NumericValue<int>(0, -100, 100);
		public NumericValue<int> MaxRelations = new NumericValue<int>(0, -100, 100);

		public static QuestOrigin DefaultValue { get; private set; }
	}
}
