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
	public partial class ColliderShape
	{
		partial void OnDataDeserialized(ColliderShapeSerializable serializable, Database database);
		partial void OnDataSerialized(ref ColliderShapeSerializable serializable);

		public static ColliderShape Create(ColliderShapeSerializable serializable, Database database)
		{
			if (serializable == null) return DefaultValue;
			return new ColliderShape(serializable, database);
		}

		public ColliderShape() {}

		private ColliderShape(ColliderShapeSerializable serializable, Database database)
		{
			Points = serializable.Points?.Select(item => new ValueWrapper<Vector2> { Value = item }).ToArray();
			Radius = new NumericValue<float>(serializable.Radius, 0f, 100f);
			OnDataDeserialized(serializable, database);
		}

		public ColliderShapeSerializable Serialize()
		{
			var serializable = new ColliderShapeSerializable();
			if (Points == null || Points.Length == 0)
			    serializable.Points = null;
			else
			    serializable.Points = Points.Select(item => item.Value).ToArray();
			serializable.Radius = Radius.Value;
			OnDataSerialized(ref serializable);
			return serializable;
		}

		public ValueWrapper<Vector2>[] Points;
		public NumericValue<float> Radius = new NumericValue<float>(0, 0f, 100f);

		public static ColliderShape DefaultValue { get; private set; }
	}
}
