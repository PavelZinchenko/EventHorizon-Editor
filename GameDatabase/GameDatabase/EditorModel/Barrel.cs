using GameDatabase.Enums;
using GameDatabase.Model;
using GameDatabase.Serializable;

namespace GameDatabase.EditorModel
{
    public class Barrel
    {
        public Barrel()
        {
            Position = Vector2.Zero;
            Rotation = new NumericValue<float>(0, -360, 360);
            Offset = new NumericValue<float>(0, 0f, 1f);
            WeaponClass = string.Empty;
        }

        public Barrel(SerializableBarrel barrel)
        {
            Position = new Vector2 { x = barrel.Position.y, y = barrel.Position.x };
            Rotation = new NumericValue<float>(barrel.Rotation, -360, 360);
            Offset = new NumericValue<float>(barrel.Offset, 0f, 1f);
            PlatformType = barrel.PlatformType;
            WeaponClass = barrel.WeaponClass;
            ImageId = new IconId(barrel.Image);
            Size = new NumericValue<float>(barrel.Size, 0, 1f);
        }

        public SerializableBarrel Serialize()
        {
            return new SerializableBarrel
            {
                Position = new Vector2 {x = Position.y, y = Position.x},
                Rotation = Rotation.Value,
                Offset = Offset.Value,
                PlatformType = PlatformType,
                WeaponClass = WeaponClass,
                Image = ImageId.Id,
                Size = Size.Value
            };
        }

        public Vector2 Position;
        public NumericValue<float> Rotation;
        public NumericValue<float> Offset;
        public PlatformType PlatformType;
        public string WeaponClass;
        public IconId ImageId;
        public NumericValue<float> Size;

        public override string ToString()
        {
            return Position.ToString();
        }
    }
}
