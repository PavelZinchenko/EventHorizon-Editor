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
	public partial class SatelliteMountPoint
	{
		partial void OnDataDeserialized(SatelliteMountPointSerializable serializable, Database database);
		partial void OnDataSerialized(ref SatelliteMountPointSerializable serializable);

		public static SatelliteMountPoint Create(SatelliteMountPointSerializable serializable, Database database)
		{
			if (serializable == null) return DefaultValue;
			return new SatelliteMountPoint(serializable, database);
		}

		public SatelliteMountPoint() {}

		private SatelliteMountPoint(SatelliteMountPointSerializable serializable, Database database)
		{
			Position = serializable.Position;
			Direction = new NumericValue<float>(serializable.Direction, -180f, 180f);
			RotationLimit = new NumericValue<float>(serializable.RotationLimit, 0f, 180f);
			MinSizeClass = serializable.MinSizeClass;
			MaxSizeClass = serializable.MaxSizeClass;
			OnDataDeserialized(serializable, database);
		}

		public SatelliteMountPointSerializable Serialize()
		{
			var serializable = new SatelliteMountPointSerializable();
			serializable.Position = Position;
			serializable.Direction = Direction.Value;
			serializable.RotationLimit = RotationLimit.Value;
			serializable.MinSizeClass = MinSizeClass;
			serializable.MaxSizeClass = MaxSizeClass;
			OnDataSerialized(ref serializable);
			return serializable;
		}

		public Vector2 Position;
		public NumericValue<float> Direction = new NumericValue<float>(0, -180f, 180f);
		public NumericValue<float> RotationLimit = new NumericValue<float>(0, 0f, 180f);
		public SizeClass MinSizeClass;
		public SizeClass MaxSizeClass;

		public static SatelliteMountPoint DefaultValue { get; private set; }
	}
}
