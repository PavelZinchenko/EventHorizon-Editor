using EditorDatabase.Serializable;
using EditorDatabase.Model;

namespace EditorDatabase.DataModel
{
	public partial class Barrel
	{
        partial void OnDataDeserialized(BarrelSerializable serializable, Database database)
        {
            Position = new Vector2(serializable.Position.y, serializable.Position.x);
            if (serializable.PlatformType > 0)
                AutoAimingArc.Value = PlatformTypeToAngle(serializable.PlatformType);
        }

		partial void OnDataSerialized(ref BarrelSerializable serializable)
		{
            serializable.Position = new Vector2(Position.y, Position.x);
            serializable.PlatformType = 0;
		}

        public static float PlatformTypeToAngle(int platformType)
        {
            switch (platformType)
            {
                case 1: //AutoTarget
                    return 360;
                case 2:// AutoTargetFrontal
                    return 80;
                case 3:// TargetingUnit
                    return 20;
                default:
                    return 0;
            }
        }
    }
}
