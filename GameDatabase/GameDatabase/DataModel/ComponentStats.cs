using EditorDatabase.Serializable;

namespace EditorDatabase.DataModel
{
    public partial class ComponentStats
    {
        partial void OnDataDeserialized(ComponentStatsSerializable serializable, Database database)
        {
            if (serializable.AlterWeaponPlatform > 0)
                AutoAimingArc.Value = Barrel.PlatformTypeToAngle(serializable.AlterWeaponPlatform);
        }

        partial void OnDataSerialized(ref ComponentStatsSerializable serializable)
        {
            serializable.AlterWeaponPlatform = 0;
        }
    }
}
