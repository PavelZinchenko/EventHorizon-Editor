using System.Drawing;
using GameDatabase.Model;
using GameDatabase.Serializable;

namespace GameDatabase.EditorModel.Quests
{
    public class Artifact
    {
        public Artifact(SerializableArtifact artifact, Database database)
        {
            ItemId = new ItemId<Artifact>(artifact.Id, artifact.FileName);

            Name = artifact.Name;
            Description = artifact.Description;
            Icon = artifact.Icon;
            Color = Helpers.ColorFromString(artifact.Color);
            Price = new NumericValue<int>(artifact.Price, 0, 1000000);
        }

        public void Save(SerializableArtifact serializable)
        {
            serializable.Name = Name;
            serializable.Color = Helpers.ColorToString(Color);
            serializable.Description = Description;
            serializable.Icon = Icon;
            serializable.Price = Price.Value;
        }

        public readonly ItemId<Artifact> ItemId;
        public string Name;
        public string Description;
        public string Icon;
        public Color Color;
        public NumericValue<int> Price;
    }
}
