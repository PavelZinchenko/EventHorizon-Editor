using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using GameDatabase.Enums;
using GameDatabase.Serializable;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GameDatabase
{
    public class JsonDatabase
    {
        public JsonDatabase()
        {
            Settings = new JsonSerializerSettings
            {
                ContractResolver = new BaseFirstContractResolver(),
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
            };
        }

        public void LoadData(string path)
        {
            Clear();

            foreach (var file in Directory.EnumerateFiles(path, "*.json", SearchOption.AllDirectories))
            {
                var data = File.ReadAllText(file);
                var item = JsonConvert.DeserializeObject<SerializableItem>(data);
                var type = item.ItemType;

                var name = Helpers.FileName(file);
                object deserializedObject;

                switch (type)
                {
                    case ItemType.Undefined:
                        continue;
                    case ItemType.Component:
                        deserializedObject = DeserializeItem(data, name, _components);
                        break;
                    case ItemType.Device:
                        deserializedObject = DeserializeItem(data, name, _devices);
                        break;
                    case ItemType.Weapon:
                        deserializedObject = DeserializeItem(data, name, _weapons);
                        break;
                    case ItemType.Ammunition:
                        deserializedObject = DeserializeItem(data, name, _ammunitions);
                        break;
                    case ItemType.DroneBay:
                        deserializedObject = DeserializeItem(data, name, _droneBays);
                        break;
                    case ItemType.Ship:
                        deserializedObject = DeserializeItem(data, name, _ships);
                        break;
                    case ItemType.Satellite:
                        deserializedObject = DeserializeItem(data, name, _satellites);
                        break;
                    case ItemType.ShipBuild:
                        deserializedObject = DeserializeItem(data, name, _shipBuilds);
                        break;
                    case ItemType.SatelliteBuild:
                        deserializedObject = DeserializeItem(data, name, _satelliteBuilds);
                        break;
                    case ItemType.Technology:
                        deserializedObject = DeserializeItem(data, name, _technologies);
                        break;
                    case ItemType.Skill:
                        deserializedObject = DeserializeItem(data, name, _skills);
                        break;
                    case ItemType.ComponentStats:
                        deserializedObject = DeserializeItem(data, name, _componentStats);
                        break;
                    case ItemType.ComponentMod:
                        deserializedObject = DeserializeItem(data, name, _componentMods);
                        break;
                    case ItemType.ShipBuilderSettings:                        deserializedObject = DeserializeItem(data, name, _shipBuilderSettings);                        break;                    default:
                        throw new ArgumentOutOfRangeException();
                }

                _fileNames.Add(deserializedObject, file.Substring(path.Length));
            }
        }

        public void SaveData(string path)
        {
            foreach (var item in _weapons.Values)
                Serialize(item, path);
            foreach (var item in _ammunitions.Values)
                Serialize(item, path);
            foreach (var item in _devices.Values)
                Serialize(item, path);
            foreach (var item in _ships.Values)
                Serialize(item, path);
            foreach (var item in _shipBuilds.Values)
                Serialize(item, path);
            foreach (var item in _satellites.Values)
                Serialize(item, path);
            foreach (var item in _satelliteBuilds.Values)
                Serialize(item, path);
            foreach (var item in _droneBays.Values)
                Serialize(item, path);
            foreach (var item in _technologies.Values)
                Serialize(item, path);
            foreach (var item in _components.Values)
                Serialize(item, path);
            foreach (var item in _componentStats.Values)
                Serialize(item, path);
            foreach (var item in _componentMods.Values)
                Serialize(item, path);
            foreach (var item in _shipBuilderSettings.Values)
                Serialize(item, path);
        }

        public void Clear()
        {
            _weapons.Clear();
            _ammunitions.Clear();
            _devices.Clear();
            _ships.Clear();
            _shipBuilds.Clear();
            _satellites.Clear();
            _satelliteBuilds.Clear();
            _droneBays.Clear();
            _technologies.Clear();
            _components.Clear();
            _componentStats.Clear();
            _componentMods.Clear();
            _shipBuilderSettings.Clear();
            _fileNames.Clear();
        }

        public IEnumerable<SerializableWeapon> Weapons { get { return _weapons.Values; } }
        public IEnumerable<SerializableAmmunition> Ammunitions { get { return _ammunitions.Values; } }
        public IEnumerable<SerializableDevice> Devices { get { return _devices.Values; } }
        public IEnumerable<SerializableShip> Ships { get { return _ships.Values; } }
        public IEnumerable<SerializableShipBuild> ShipBuilds { get { return _shipBuilds.Values; } }
        public IEnumerable<SerializableSatellite> Satellites { get { return _satellites.Values; } }
        public IEnumerable<SerializableSatelliteBuild> SatelliteBuilds { get { return _satelliteBuilds.Values; } }
        public IEnumerable<SerializableDroneBay> DroneBays { get { return _droneBays.Values; } }
        public IEnumerable<SerializableTechnology> Technologies { get { return _technologies.Values; } }
        public IEnumerable<SerializableSkill> Skills { get { return _skills.Values; } }
        public IEnumerable<SerializableComponent> Components { get { return _components.Values; } }
        public IEnumerable<SerializableComponentStats> ComponentStats { get { return _componentStats.Values; } }
        public IEnumerable<SerializableComponentMod> ComponentMods { get { return _componentMods.Values; } }
        public IEnumerable<SerializableShipBuilderSettings> ShipBuilderSettings { get { return _shipBuilderSettings.Values; } }

        public SerializableWeapon GetWeapon(int id) { return GetItem(id, _weapons); }
        public SerializableAmmunition GetAmmunition(int id) { return GetItem(id, _ammunitions); }
        public SerializableDevice GetDevice(int id) { return GetItem(id, _devices); }
        public SerializableShip GetShip(int id) { return GetItem(id, _ships); }
        public SerializableShipBuild GetShipBuild(int id) { return GetItem(id, _shipBuilds); }
        public SerializableSatellite GetSatellite(int id) { return GetItem(id, _satellites); }
        public SerializableSatelliteBuild GetSatelliteBuild(int id) { return GetItem(id, _satelliteBuilds); }
        public SerializableDroneBay GetDroneBay(int id) { return GetItem(id, _droneBays); }
        public SerializableTechnology GetTechnology(int id) { return GetItem(id, _technologies); }
        public SerializableSkill GetSkill(int id) { return GetItem(id, _skills); }
        public SerializableComponent GetComponent(int id) { return GetItem(id, _components); }
        public SerializableComponentStats GetComponentStats(int id) { return GetItem(id, _componentStats); }
        public SerializableComponentMod GetComponentMod(int id) { return GetItem(id, _componentMods); }
        public SerializableShipBuilderSettings GetShipBuilderSettings(int id) { return GetItem(id, _shipBuilderSettings); }
        private void Serialize(object item, string path)
        {
            if (item == null)
                return;

            string filename;
            if (!_fileNames.TryGetValue(item, out filename))
                return;

            File.WriteAllText(EnsurePathExists(path + filename), JsonConvert.SerializeObject(item, Settings).Replace("\r\n", "\n"));
        }

        private string EnsurePathExists(string path)
        {
            var directory = Directory.GetParent(path).FullName;
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            return path;
        }

        private static T GetItem<T>(int id, Dictionary<int, T> dictionary) where T : class
        {
            T value;
            if (dictionary.TryGetValue(id, out value))
                return value;
            return null;
        }

        private static T DeserializeItem<T>(string data, string name) where T : SerializableItem
        {
            var item = JsonConvert.DeserializeObject<T>(data);
            item.FileName = name;
            return item;
        }

        private static object DeserializeItem<T>(string data, string name, Dictionary<int, T> dictionary) where T : SerializableItem
        {
            var item = JsonConvert.DeserializeObject<T>(data);
            item.FileName = name;

            if (dictionary.ContainsKey(item.Id))
                throw new InvalidDataException("Item " + name + " has the same ID as " + dictionary[item.Id].FileName);

            dictionary.Add(item.Id, item);
            
            return item;
        }

        private JsonSerializerSettings Settings { get; set; }

        private readonly Dictionary<int, SerializableWeapon> _weapons = new Dictionary<int, SerializableWeapon>();
        private readonly Dictionary<int, SerializableAmmunition> _ammunitions = new Dictionary<int, SerializableAmmunition>();
        private readonly Dictionary<int, SerializableDevice> _devices = new Dictionary<int, SerializableDevice>();
        private readonly Dictionary<int, SerializableShip> _ships = new Dictionary<int, SerializableShip>();
        private readonly Dictionary<int, SerializableShipBuild> _shipBuilds = new Dictionary<int, SerializableShipBuild>();
        private readonly Dictionary<int, SerializableSatellite> _satellites = new Dictionary<int, SerializableSatellite>();
        private readonly Dictionary<int, SerializableSatelliteBuild> _satelliteBuilds = new Dictionary<int, SerializableSatelliteBuild>();
        private readonly Dictionary<int, SerializableDroneBay> _droneBays = new Dictionary<int, SerializableDroneBay>();
        private readonly Dictionary<int, SerializableTechnology> _technologies = new Dictionary<int, SerializableTechnology>();
        private readonly Dictionary<int, SerializableSkill> _skills = new Dictionary<int, SerializableSkill>();
        private readonly Dictionary<int, SerializableComponent> _components = new Dictionary<int, SerializableComponent>();
        private readonly Dictionary<int, SerializableComponentStats> _componentStats = new Dictionary<int, SerializableComponentStats>();
        private readonly Dictionary<int, SerializableComponentMod> _componentMods = new Dictionary<int, SerializableComponentMod>();
        private readonly Dictionary<int, SerializableShipBuilderSettings> _shipBuilderSettings = new Dictionary<int, SerializableShipBuilderSettings>();

        private readonly Dictionary<object, string> _fileNames = new Dictionary<object, string>();

        private class BaseFirstContractResolver : DefaultContractResolver
        {
            // As of 7.0.1, Json.NET suggests using a static instance for "stateless" contract resolvers, for performance reasons.
            // http://www.newtonsoft.com/json/help/html/ContractResolver.htm
            // http://www.newtonsoft.com/json/help/html/M_Newtonsoft_Json_Serialization_DefaultContractResolver__ctor_1.htm
            // "Use the parameterless constructor and cache instances of the contract resolver within your application for optimal performance."

            protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
            {
                var properties = base.CreateProperties(type, memberSerialization);
                if (properties != null)
                    return properties.OrderBy(p => p.DeclaringType.BaseTypesAndSelf().Count()).ToList();
                return properties;
            }
        }
    }

    public static class TypeExtensions
    {
        public static IEnumerable<Type> BaseTypesAndSelf(this Type type)
        {
            while (type != null)
            {
                yield return type;
                type = type.BaseType;
            }
        }
    }
}
