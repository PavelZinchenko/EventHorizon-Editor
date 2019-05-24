using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using GameDatabase.Enums;
using GameDatabase.GameDatabase.Model;
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

            var info = new DirectoryInfo(path);
            foreach (var fileInfo in info.GetFiles("*", SearchOption.AllDirectories))
            {
                var file = fileInfo.FullName;
                if (fileInfo.Extension == ".png" || fileInfo.Extension == ".jpg" || fileInfo.Extension == ".jpeg")
                {
                    LoadImage(fileInfo);
                }
                else if (fileInfo.Extension == ".xml")
                {
                    var xmlData = File.ReadAllText(file);
                    _localizations.Add(Path.GetFileNameWithoutExtension(file), xmlData);
                }
                else if (fileInfo.Extension == ".json")
                {
                    var data = File.ReadAllText(file);
                    SerializableItem item;
                    try {
                        item = JsonConvert.DeserializeObject<SerializableItem>(data);
                    }
                    catch (Exception e)
                    {
                        throw new EditorException("Malformed JSON file at " + file + " \nTry using a JSON validator.");
                    }
                    var type = item.ItemType;

                    var name = Helpers.FileName(file);
                    object deserializedObject;

                    switch (type)
                    {
                        case ItemType.Undefined:
                            continue;
                        case ItemType.Component:
                            deserializedObject = DeserializeItem(data, file, name, _components);
                            break;
                        case ItemType.Device:
                            deserializedObject = DeserializeItem(data, name, file, _devices);
                            break;
                        case ItemType.Weapon:
                            deserializedObject = DeserializeItem(data, name, file, _weapons);
                            break;
                        case ItemType.Ammunition:
                            deserializedObject = DeserializeItem(data, name, file, _ammunition);
                            break;
                        case ItemType.AmmunitionObsolete:
                            deserializedObject = DeserializeItem(data, name, file, _ammunitionObsolete);
                            break;
                        case ItemType.DroneBay:
                            deserializedObject = DeserializeItem(data, name, file, _droneBays);
                            break;
                        case ItemType.Ship:
                            deserializedObject = DeserializeItem(data, name, file, _ships);
                            break;
                        case ItemType.Satellite:
                            deserializedObject = DeserializeItem(data, name, file, _satellites);
                            break;
                        case ItemType.ShipBuild:
                            deserializedObject = DeserializeItem(data, name, file, _shipBuilds);
                            break;
                        case ItemType.SatelliteBuild:
                            deserializedObject = DeserializeItem(data, name, file, _satelliteBuilds);
                            break;
                        case ItemType.Technology:
                            deserializedObject = DeserializeItem(data, name, file, _technologies);
                            break;
                        case ItemType.Skill:
                            deserializedObject = DeserializeItem(data, name, file, _skills);
                            break;
                        case ItemType.ComponentStats:
                            deserializedObject = DeserializeItem(data, name, file, _componentStats);
                            break;
                        case ItemType.ComponentMod:
                            deserializedObject = DeserializeItem(data, name, file, _componentMods);
                            break;
                        case ItemType.Faction:
                            deserializedObject = DeserializeItem(data, name, file, _factions);
                            break;
                        case ItemType.Quest:
                            deserializedObject = DeserializeItem(data, name, file, _quests);
                            break;
                        case ItemType.Loot:
                            deserializedObject = DeserializeItem(data, name, file, _loot);
                            break;
                        case ItemType.Fleet:
                            deserializedObject = DeserializeItem(data, name, file, _fleets);
                            break;
                        case ItemType.Character:
                            deserializedObject = DeserializeItem(data, name, file, _characters);
                            break;
                        case ItemType.QuestItem:
                            deserializedObject = DeserializeItem(data, name, file, _questItems);
                            break;
                        case ItemType.BulletPrefab:
                            deserializedObject = DeserializeItem(data, name, file, _bulletPrefabs);
                            break;
                        case ItemType.VisualEffect:
                            deserializedObject = DeserializeItem(data, name, file, _visualEffects);
                            break;
                        case ItemType.ShipSettings:
                            deserializedObject = ShipSettings = DeserializeItem<SerializableShipSettings>(data, name, file);
                            break;
                        case ItemType.GalaxySettings:
                            deserializedObject = GalaxySettings = DeserializeItem<SerializableGalaxySettings>(data, name, file);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    _fileNames.Add(deserializedObject, file.Substring(path.Length));
                }
            }
        }

        public void SaveData(string path)
        {
            foreach (var item in _weapons.Values)
                Serialize(item, path);
            foreach (var item in _ammunition.Values)
                Serialize(item, path);
            foreach (var item in _ammunitionObsolete.Values)
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
            foreach (var item in _skills.Values)
                Serialize(item, path);
            foreach (var item in _components.Values)
                Serialize(item, path);
            foreach (var item in _componentStats.Values)
                Serialize(item, path);
            foreach (var item in _componentMods.Values)
                Serialize(item, path);
            foreach (var item in _factions.Values)
                Serialize(item, path);
            foreach (var item in _loot.Values)
                Serialize(item, path);
            foreach (var item in _quests.Values)
                Serialize(item, path);
            foreach (var item in _fleets.Values)
                Serialize(item, path);
            foreach (var item in _characters.Values)
                Serialize(item, path);
            foreach (var item in _questItems.Values)
                Serialize(item, path);
            foreach (var item in _bulletPrefabs.Values)
                Serialize(item, path);
            foreach (var item in _visualEffects.Values)
                Serialize(item, path);

            Serialize(ShipSettings, path);
            Serialize(GalaxySettings, path);
        }

        public void Clear()
        {
            _weapons.Clear();
            _ammunition.Clear();
            _ammunitionObsolete.Clear();
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
            _factions.Clear();
            _loot.Clear();
            _quests.Clear();
            _fleets.Clear();
            _characters.Clear();
            _questItems.Clear();
            _visualEffects.Clear();
            _bulletPrefabs.Clear();

            _fileNames.Clear();
            _images.Clear();
            _localizations.Clear();

            ShipSettings = new SerializableShipSettings();
            GalaxySettings = new SerializableGalaxySettings();
        }

        public SerializableShipSettings ShipSettings { get; private set; }
        public SerializableGalaxySettings GalaxySettings { get; private set; }

        public IEnumerable<SerializableWeapon> Weapons { get { return _weapons.Values; } }
        public IEnumerable<SerializableAmmunition> Ammunitions { get { return _ammunition.Values; } }
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
        public IEnumerable<SerializableFaction> Factions { get { return _factions.Values; } }
        public IEnumerable<SerializableLoot> Loot { get { return _loot.Values; } }
        public IEnumerable<SerializableQuest> Quests { get { return _quests.Values; } }
        public IEnumerable<SerializableFleet> Fleets { get { return _fleets.Values; } }
        public IEnumerable<SerializableCharacter> Characters { get { return _characters.Values; } }
        public IEnumerable<SerializableQuestItem> QuestItems { get { return _questItems.Values; } }
        public IEnumerable<SerializableVisualEffect> VisualEffects { get { return _visualEffects.Values; } }
        public IEnumerable<SerializableBulletPrefab> BulletPrefabs { get { return _bulletPrefabs.Values; } }

        public SerializableWeapon GetWeapon(int id) { return GetItem(id, _weapons); }
        public SerializableAmmunition GetAmmunition(int id) { return GetItem(id, _ammunition); }
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
        public SerializableFaction GetFaction(int id) { return GetItem(id, _factions); }
        public SerializableLoot GetLoot(int id) { return GetItem(id, _loot); }
        public SerializableQuest GetQuest(int id) { return GetItem(id, _quests); }
        public SerializableFleet GetFleet(int id) { return GetItem(id, _fleets); }
        public SerializableCharacter GetCharacter(int id) { return GetItem(id, _characters); }
        public SerializableQuestItem GetQuestItem(int id) { return GetItem(id, _questItems); }
        public SerializableVisualEffect GetVisualEffect(int id) { return GetItem(id, _visualEffects); }
        public SerializableBulletPrefab GetBulletPrefab(int id) { return GetItem(id, _bulletPrefabs); }

        public Image GetImage(string name)
        {
            Image image;
            return _images.TryGetValue(name, out image) ? image : null;
        }

        public string GetLocalization(string language)
        {
            string data;
            return _localizations.TryGetValue(language, out data) ? data : null;
        }

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

        private static T DeserializeItem<T>(string data, string name, string path) where T : SerializableItem
        {

            T item;
            item = JsonConvert.DeserializeObject<T>(data);
            item.FileName = name;
            item.FilePath = path;
            return item;
        }

        private static object DeserializeItem<T>(string data, string name, string path, Dictionary<int, T> dictionary) where T : SerializableItem
        {
            var item = DeserializeItem<T>(data, name, path);

            if (dictionary.ContainsKey(item.Id))
                throw new EditorException("Item " + path + " has the same ID as " + dictionary[item.Id].FilePath);

            dictionary.Add(item.Id, item);
            
            return item;
        }

        private void LoadImage(FileInfo file)
        {
            Image image;
            try
            {
                image = Image.FromFile(file.FullName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }

            if (image.Width != image.Height)
                return;

            image.RotateFlip(RotateFlipType.Rotate270FlipNone);
            _images.Add(file.Name, image);
        }


        private JsonSerializerSettings Settings { get; set; }

        private readonly Dictionary<int, SerializableWeapon> _weapons = new Dictionary<int, SerializableWeapon>();
        private readonly Dictionary<int, SerializableAmmunition> _ammunition = new Dictionary<int, SerializableAmmunition>();
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
        private readonly Dictionary<int, SerializableFaction> _factions = new Dictionary<int, SerializableFaction>();
        private readonly Dictionary<int, SerializableLoot> _loot = new Dictionary<int, SerializableLoot>();
        private readonly Dictionary<int, SerializableQuest> _quests = new Dictionary<int, SerializableQuest>();
        private readonly Dictionary<int, SerializableFleet> _fleets = new Dictionary<int, SerializableFleet>();
        private readonly Dictionary<int, SerializableCharacter> _characters = new Dictionary<int, SerializableCharacter>();
        private readonly Dictionary<int, SerializableQuestItem> _questItems = new Dictionary<int, SerializableQuestItem>();
        private readonly Dictionary<int, SerializableVisualEffect> _visualEffects = new Dictionary<int, SerializableVisualEffect>();
        private readonly Dictionary<int, SerializableBulletPrefab> _bulletPrefabs = new Dictionary<int, SerializableBulletPrefab>();

        private readonly Dictionary<string, Image> _images = new Dictionary<string, Image>();
        private readonly Dictionary<string, string> _localizations = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<object, string> _fileNames = new Dictionary<object, string>();

        #region Obsolete
        public SerializableAmmunitionObsolete GetAmmunitionObsolete(int id) { return GetItem(id, _ammunitionObsolete); }
        public IEnumerable<SerializableAmmunitionObsolete> AmmunitionObsolete { get { return _ammunitionObsolete.Values; } }
        private readonly Dictionary<int, SerializableAmmunitionObsolete> _ammunitionObsolete = new Dictionary<int, SerializableAmmunitionObsolete>();
        #endregion

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
