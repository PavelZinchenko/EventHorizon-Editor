using System.Linq;
using GameDatabase.Enums;
using GameDatabase.GameDatabase.Helpers;
using GameDatabase.Model;
using GameDatabase.Serializable;

namespace GameDatabase.EditorModel
{
    public class Satellite
    {
        public Satellite(SerializableSatellite satellite, Database database)
        {
            ItemId = new ItemId<Satellite>(satellite.Id, satellite.FileName);

            Name = satellite.Name;
            ModelImage = satellite.ModelImage;
            ModelScale = new NumericValue<float>(satellite.ModelScale, 0.1f, 100);
            Layout = new Layout(satellite.Layout);
            Barrels = satellite.Barrels.Select(item => new Barrel(item)).ToArray();
            SizeClass = satellite.SizeClass;
        }

        public void Save(SerializableSatellite serializable)
        {
            serializable.Name = Name;
            serializable.ModelImage = ModelImage;
            serializable.ModelScale = ModelScale.Value;
            serializable.Layout = Layout.Data;
            serializable.Barrels = Barrels.Select(item => item.Serialize()).ToArray();
            serializable.SizeClass = SizeClass;
        }

        public readonly ItemId<Satellite> ItemId;

        public string Name;

        [AutoCompleteAtribute(AutoCompleteAtribute.Type.SateliteSprite)]
        public string ModelImage;
        public NumericValue<float> ModelScale;
        public SizeClass SizeClass;

        public Layout Layout;
        public Barrel[] Barrels;
    }
}
