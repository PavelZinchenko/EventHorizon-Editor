using System;
using GameDatabase.Enums;
using Newtonsoft.Json;

namespace GameDatabase.GameDatabase.Serializable
{
    [Serializable]
    public class SerializableTemplateItem
    {
        public string Filename;
        public string Type;
        public Newtonsoft.Json.Linq.JObject Content;
        public SerializableTemplateItem[] Items;
    }
}
