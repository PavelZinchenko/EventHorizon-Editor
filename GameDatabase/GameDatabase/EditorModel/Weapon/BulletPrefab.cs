using System.Drawing;
using GameDatabase.Enums;
using GameDatabase.GameDatabase.Helpers;
using GameDatabase.Model;
using GameDatabase.Serializable;

namespace GameDatabase.EditorModel
{
    public class BulletPrefab
    {
        public BulletPrefab(SerializableBulletPrefab serializable, Database database)
        {
            ItemId = new ItemId<BulletPrefab>(serializable.Id, serializable.FileName);

            Shape = serializable.Shape;
            Size = new NumericValue<float>(serializable.Size, 0.1f, 10);
            Margins = new NumericValue<float>(serializable.Margins, 0, 1);
            MainColor = Helpers.ColorFromString(serializable.MainColor);
            MainColorMode = serializable.MainColorMode;
            SecondColor = Helpers.ColorFromString(serializable.SecondColor);
            SecondColorMode = serializable.SecondColorMode;
            Image = serializable.Image;
        }

        public void Save(SerializableBulletPrefab serializable)
        {
            serializable.MainColor = Helpers.ColorToString(MainColor);
            serializable.MainColorMode = MainColorMode;
            serializable.SecondColor = Helpers.ColorToString(SecondColor);
            serializable.SecondColorMode = SecondColorMode;
            serializable.Image = Image;
            serializable.Size = Size.Value;
            serializable.Margins = Margins.Value;
            serializable.Shape = Shape;
        }

        public readonly ItemId<BulletPrefab> ItemId;

        public BulletShape Shape;
        public NumericValue<float> Size;
        public NumericValue<float> Margins;
        public Color MainColor;
        public ColorMode MainColorMode;
        public Color SecondColor;
        public ColorMode SecondColorMode;

        [AutoCompleteAtribute(AutoCompleteAtribute.Type.BulletSprite)]
        public string Image;
    }
}
