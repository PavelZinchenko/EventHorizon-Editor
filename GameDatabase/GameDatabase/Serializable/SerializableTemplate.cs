using Newtonsoft.Json;
using System;

namespace GameDatabase.GameDatabase.Serializable
{
    [Serializable]
    public class SerializableTemplate
    {
        public string Name;
        public SerializableTemplateItem[] Items;
        public int Id;
        public string NameDialog;
        public string IdDialog;

        [JsonIgnore]
        public string FileName;

        [JsonIgnore]
        public string FilePath;
    }
}
