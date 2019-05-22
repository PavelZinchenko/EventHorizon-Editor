using System.Drawing;
using System.Linq;
using GameDatabase.Enums;
using GameDatabase.Model;
using GameDatabase.Serializable;

namespace GameDatabase.EditorModel
{
    public class VisualEffect
    {
        public VisualEffect(SerializableVisualEffect serializable, Database database)
        {
            ItemId = new ItemId<VisualEffect>(serializable.Id, serializable.FileName);
            Elements = serializable.Elements?.Select(item => new EffectData
            {
                Type = item.Type,
                Image = item.Image,
                Size = new NumericValue<float>(item.Size, 0, 100),
                ColorMode = item.ColorMode,
                Color = Helpers.ColorFromString(item.Color),
                StartTime = new NumericValue<float>(item.StartTime, 0, 1000),
                Lifetime = new NumericValue<float>(item.Lifetime, 0, 1000),
            }).ToArray();
        }

        public void Save(SerializableVisualEffect serializable)
        {
            serializable.Elements = Elements?.Select(item => new SerializableVisualEffect.EffectData
            {
                Type = item.Type,
                Image = item.Image,
                Size = item.Size.Value,
                ColorMode = item.ColorMode,
                Color = Helpers.ColorToString(item.Color),
                StartTime = item.StartTime.Value,
                Lifetime = item.Lifetime.Value,
            }).ToArray();
        }

        public readonly ItemId<VisualEffect> ItemId;
        public EffectData[] Elements;

        public class EffectData
        {
            public VisualEffectType Type;
            public ColorMode ColorMode;
            public string Image;
            public Color Color = Color.White;
            public NumericValue<float> Size = new NumericValue<float>(0, 0, 100);
            public NumericValue<float> StartTime = new NumericValue<float>(0, 0, 1000);
            public NumericValue<float> Lifetime = new NumericValue<float>(0, 0, 1000);
        }
    }
}
