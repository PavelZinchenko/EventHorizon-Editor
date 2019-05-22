using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using GameDatabase.EditorModel;
using GameDatabase.EditorModel.Quests;
using GameDatabase.Model;
using GameDatabase.Serializable;

namespace GameDatabase
{
    public class Database
    {
        public Database(string path, bool loadAllData)
        {
            _jsonDatabase = new JsonDatabase();
            _jsonDatabase.LoadData(path);

            if (loadAllData)
            {
                _components = _jsonDatabase.Components.ToDictionary(item => item.Id, item => new Component(item, this));
                _devices = _jsonDatabase.Devices.ToDictionary(item => item.Id, item => new Device(item, this));
                _weapons = _jsonDatabase.Weapons.ToDictionary(item => item.Id, item => new Weapon(item, this));
                _ammunition = _jsonDatabase.Ammunitions.ToDictionary(item => item.Id, item => new Ammunition(item, this));
                _droneBays = _jsonDatabase.DroneBays.ToDictionary(item => item.Id, item => new DroneBay(item, this));
                _ships = _jsonDatabase.Ships.ToDictionary(item => item.Id, item => new Ship(item, this));
                _shipBuilds = _jsonDatabase.ShipBuilds.ToDictionary(item => item.Id, item => new ShipBuild(item, this));
                _satellites = _jsonDatabase.Satellites.ToDictionary(item => item.Id, item => new Satellite(item, this));
                _satelliteBuilds = _jsonDatabase.SatelliteBuilds.ToDictionary(item => item.Id, item => new SatelliteBuild(item, this));
                _technologies = _jsonDatabase.Technologies.ToDictionary(item => item.Id, item => new Technology(item, this));
                _skills = _jsonDatabase.Skills.ToDictionary(item => item.Id, item => new Skill(item, this));
                _componentStats = _jsonDatabase.ComponentStats.ToDictionary(item => item.Id, item => new ComponentStats(item, this));
                _componentMods = _jsonDatabase.ComponentMods.ToDictionary(item => item.Id, item => new ComponentMod(item, this));
                _factions = _jsonDatabase.Factions.ToDictionary(item => item.Id, item => new Faction(item, this));
                _characters = _jsonDatabase.Characters.ToDictionary(item => item.Id, item => new Character(item, this));
                _questItems = _jsonDatabase.QuestItems.ToDictionary(item => item.Id, item => new QuestItem(item, this));
                _ammunitionObsolete = _jsonDatabase.AmmunitionObsolete.ToDictionary(item => item.Id, item => new AmmunitionObsolete(item, this));
                _bulletPrefabs = _jsonDatabase.BulletPrefabs.ToDictionary(item => item.Id, item => new BulletPrefab(item, this));
                _visualEffects = _jsonDatabase.VisualEffects.ToDictionary(item => item.Id, item => new VisualEffect(item, this));
            }
        }

        private static T Cleanup<T>(T serializable) where T : SerializableItem
        {
            var id = serializable.Id;
            var name = serializable.FileName;
            var type = serializable.ItemType;

            foreach (var item in serializable.GetType().GetFields().Where(item => item.IsPublic && !item.IsStatic))
                item.SetValue(serializable, null);

            serializable.Id = id;
            serializable.FileName = name;
            serializable.ItemType = type;
            return serializable;
        }

        public void SaveAs(string path)
        {
            foreach (var item in _components)
                item.Value.Save(Cleanup(_jsonDatabase.GetComponent(item.Key)));
            foreach (var item in _devices)
                item.Value.Save(Cleanup(_jsonDatabase.GetDevice(item.Key)));
            foreach (var item in _weapons)
                item.Value.Save(Cleanup(_jsonDatabase.GetWeapon(item.Key)));
            foreach (var item in _ammunition)
                item.Value.Save(Cleanup(_jsonDatabase.GetAmmunition(item.Key)));
            foreach (var item in _ammunitionObsolete)
                item.Value.Save(Cleanup(_jsonDatabase.GetAmmunitionObsolete(item.Key)));
            foreach (var item in _droneBays)
                item.Value.Save(Cleanup(_jsonDatabase.GetDroneBay(item.Key)));
            foreach (var item in _ships)
                item.Value.Save(Cleanup(_jsonDatabase.GetShip(item.Key)));
            foreach (var item in _shipBuilds)
                item.Value.Save(Cleanup(_jsonDatabase.GetShipBuild(item.Key)));
            foreach (var item in _satellites)
                item.Value.Save(Cleanup(_jsonDatabase.GetSatellite(item.Key)));
            foreach (var item in _satelliteBuilds)
                item.Value.Save(Cleanup(_jsonDatabase.GetSatelliteBuild(item.Key)));
            foreach (var item in _technologies)
                item.Value.Save(Cleanup(_jsonDatabase.GetTechnology(item.Key)));
            foreach (var item in _skills)
                item.Value.Save(Cleanup(_jsonDatabase.GetSkill(item.Key)));
            foreach (var item in _componentStats)
                item.Value.Save(Cleanup(_jsonDatabase.GetComponentStats(item.Key)));
            foreach (var item in _componentMods)
                item.Value.Save(Cleanup(_jsonDatabase.GetComponentMod(item.Key)));
            foreach (var item in _factions)
                item.Value.Save(Cleanup(_jsonDatabase.GetFaction(item.Key)));
            foreach (var item in _loot)
                item.Value.Save(Cleanup(_jsonDatabase.GetLoot(item.Key)));
            foreach (var item in _quests)
                item.Value.Save(Cleanup(_jsonDatabase.GetQuest(item.Key)));
            foreach (var item in _fleets)
                item.Value.Save(Cleanup(_jsonDatabase.GetFleet(item.Key)));
            foreach (var item in _characters)
                item.Value.Save(Cleanup(_jsonDatabase.GetCharacter(item.Key)));
            foreach (var item in _questItems)
                item.Value.Save(Cleanup(_jsonDatabase.GetQuestItem(item.Key)));
            foreach (var item in _bulletPrefabs)
                item.Value.Save(Cleanup(_jsonDatabase.GetBulletPrefab(item.Key)));
            foreach (var item in _visualEffects)
                item.Value.Save(Cleanup(_jsonDatabase.GetVisualEffect(item.Key)));

            ShipSettings.Save(Cleanup(_jsonDatabase.ShipSettings));
            GalaxySettings.Save(Cleanup(_jsonDatabase.GalaxySettings));

            _jsonDatabase.SaveData(path);
        }

        public ShipSettings ShipSettings => _shipSettings ?? (_shipSettings = new ShipSettings(_jsonDatabase.ShipSettings, this));
        public GalaxySettings GalaxySettings => _galaxySettings ?? (_galaxySettings = new GalaxySettings(_jsonDatabase.GalaxySettings, this));

        public Component GetComponent(int id) { return GetItem(id, _components, _jsonDatabase.GetComponent(id)); }
        public Device GetDevice(int id) { return GetItem(id, _devices, _jsonDatabase.GetDevice(id)); }
        public Weapon GetWeapon(int id) { return GetItem(id, _weapons, _jsonDatabase.GetWeapon(id)); }
        public DroneBay GetDroneBay(int id) { return GetItem(id, _droneBays, _jsonDatabase.GetDroneBay(id)); }
        public Ship GetShip(int id) { return GetItem(id, _ships, _jsonDatabase.GetShip(id)); }
        public ShipBuild GetShipBuild(int id) { return GetItem(id, _shipBuilds, _jsonDatabase.GetShipBuild(id)); }
        public Satellite GetSatellite(int id) { return GetItem(id, _satellites, _jsonDatabase.GetSatellite(id)); }
        public SatelliteBuild GetSatelliteBuild(int id) { return GetItem(id, _satelliteBuilds, _jsonDatabase.GetSatelliteBuild(id)); }
        public Technology GetTechnology(int id) { return GetItem(id, _technologies, _jsonDatabase.GetTechnology(id)); }
        public Skill GetSkill(int id) { return GetItem(id, _skills, _jsonDatabase.GetSkill(id)); }
        public ComponentStats GetComponentStats(int id) { return GetItem(id, _componentStats, _jsonDatabase.GetComponentStats(id)); }
        public ComponentMod GetComponentMods(int id) { return GetItem(id, _componentMods, _jsonDatabase.GetComponentMod(id)); }
        public Faction GetFaction(int id) { return id == Faction.Neutral.ItemId.Id ? Faction.Neutral : GetItem(id, _factions, _jsonDatabase.GetFaction(id)) ?? Faction.Undefined; }
        public Loot GetLoot(int id) { return GetItem(id, _loot, _jsonDatabase.GetLoot(id)); }
        public QuestModel GetQuest(int id) { return GetItem(id, _quests, _jsonDatabase.GetQuest(id)); }
        public Fleet GetFleet(int id) { return GetItem(id, _fleets, _jsonDatabase.GetFleet(id)); }
        public Character GetCharacter(int id) { return GetItem(id, _characters, _jsonDatabase.GetCharacter(id)); }
        public QuestItem GetQuestItem(int id) { return GetItem(id, _questItems, _jsonDatabase.GetQuestItem(id)); }
        public VisualEffect GetVisualEffect(int id) { return GetItem(id, _visualEffects, _jsonDatabase.GetVisualEffect(id)); }
        public BulletPrefab GetBulletPrefab(int id) { return GetItem(id, _bulletPrefabs, _jsonDatabase.GetBulletPrefab(id)); }
        public AmmunitionObsolete GetAmmunitionObsolete(int id) { return GetItem(id, _ammunitionObsolete, _jsonDatabase.GetAmmunitionObsolete(id)); }
        public Ammunition GetAmmunition(int id) { return GetItem(id, _ammunition, _jsonDatabase.GetAmmunition(id)); }

        public IEnumerable<ItemId<Component>> ComponentIds { get { return _jsonDatabase.Components.Select(item => new ItemId<Component>(item.Id, item.FileName)); } }
        public IEnumerable<ItemId<Device>> DeviceIds { get { return _jsonDatabase.Devices.Select(item => new ItemId<Device>(item.Id, item.FileName)); } }
        public IEnumerable<ItemId<Weapon>> WeaponIds { get { return _jsonDatabase.Weapons.Select(item => new ItemId<Weapon>(item.Id, item.FileName)); } }
        public IEnumerable<ItemId<Ammunition>> AmmunitionIds { get { return _jsonDatabase.Ammunitions.Select(item => new ItemId<Ammunition>(item.Id, item.FileName)); } }
        public IEnumerable<ItemId<DroneBay>> DroneBayIds { get { return _jsonDatabase.DroneBays.Select(item => new ItemId<DroneBay>(item.Id, item.FileName)); } }
        public IEnumerable<ItemId<Ship>> ShipIds { get { return _jsonDatabase.Ships.Select(item => new ItemId<Ship>(item.Id, item.FileName)); } }
        public IEnumerable<ItemId<ShipBuild>> ShipBuildIds { get { return _jsonDatabase.ShipBuilds.Select(item => new ItemId<ShipBuild>(item.Id, item.FileName)); } }
        public IEnumerable<ItemId<Satellite>> SatelliteIds { get { return _jsonDatabase.Satellites.Select(item => new ItemId<Satellite>(item.Id, item.FileName)); } }
        public IEnumerable<ItemId<SatelliteBuild>> SatelliteBuildIds { get { return _jsonDatabase.SatelliteBuilds.Select(item => new ItemId<SatelliteBuild>(item.Id, item.FileName)); } }
        public IEnumerable<ItemId<Technology>> TechnologyIds { get { return _jsonDatabase.Technologies.Select(item => new ItemId<Technology>(item.Id, item.FileName)); } }
        public IEnumerable<ItemId<Skill>> SkillIds { get { return _jsonDatabase.Skills.Select(item => new ItemId<Skill>(item.Id, item.FileName)); } }
        public IEnumerable<ItemId<ComponentStats>> ComponentStatsIds { get { return _jsonDatabase.ComponentStats.Select(item => new ItemId<ComponentStats>(item.Id, item.FileName)); } }
        public IEnumerable<ItemId<ComponentMod>> ComponentModIds { get { return _jsonDatabase.ComponentMods.Select(item => new ItemId<ComponentMod>(item.Id, item.FileName)); } }
        public IEnumerable<ItemId<Faction>> FactionIds { get { return new[] { Faction.Undefined.ItemId, Faction.Neutral.ItemId }.Concat(_jsonDatabase.Factions.Select(item => new ItemId<Faction>(item.Id, item.FileName))); } }
        public IEnumerable<ItemId<Loot>> LootIds { get { return _jsonDatabase.Loot.Select(item => new ItemId<Loot>(item.Id, item.FileName)); } }
        public IEnumerable<ItemId<QuestModel>> QuestIds { get { return _jsonDatabase.Quests.Select(item => new ItemId<QuestModel>(item.Id, item.FileName)); } }
        public IEnumerable<ItemId<Fleet>> FleetIds { get { return _jsonDatabase.Fleets.Select(item => new ItemId<Fleet>(item.Id, item.FileName)); } }
        public IEnumerable<ItemId<Character>> CharacterIds { get { return _jsonDatabase.Characters.Select(item => new ItemId<Character>(item.Id, item.FileName)); } }
        public IEnumerable<ItemId<QuestItem>> QuestItemIds { get { return _jsonDatabase.QuestItems.Select(item => new ItemId<QuestItem>(item.Id, item.FileName)); } }
        public IEnumerable<ItemId<AmmunitionObsolete>> AmmunitionObsoleteIds { get { return _jsonDatabase.AmmunitionObsolete.Select(item => new ItemId<AmmunitionObsolete>(item.Id, item.FileName)); } }
        public IEnumerable<ItemId<BulletPrefab>> BulletPerfabIds { get { return _jsonDatabase.BulletPrefabs.Select(item => new ItemId<BulletPrefab>(item.Id, item.FileName)); } }
        public IEnumerable<ItemId<VisualEffect>> VisualEffectIds { get { return _jsonDatabase.VisualEffects.Select(item => new ItemId<VisualEffect>(item.Id, item.FileName)); } }

        public Image GetImage(string name) { return _jsonDatabase.GetImage(name); }
        public string GetLocalization(string language) { return _jsonDatabase.GetLocalization(language); }

        public ItemId<Technology> GetTechnologyId(int id) { return new ItemId<Technology>(_jsonDatabase.GetTechnology(id)); }
        public ItemId<Skill> GetSkillId(int id) { return new ItemId<Skill>(_jsonDatabase.GetSkill(id)); }
        public ItemId<ComponentMod> GetComponentModId(int id) { return new ItemId<ComponentMod>(_jsonDatabase.GetComponentMod(id)); }
        public ItemId<ShipBuild> GetShipBuildId(int id) { return new ItemId<ShipBuild>(_jsonDatabase.GetShipBuild(id)); }
        public ItemId<Ship> GetShipId(int id) { return new ItemId<Ship>(_jsonDatabase.GetShip(id)); }
        public ItemId<Faction> GetFactionId(int id) { return new ItemId<Faction>(_jsonDatabase.GetFaction(id)); }
        public ItemId<Loot> GetLootId(int id) { return new ItemId<Loot>(_jsonDatabase.GetLoot(id)); }
        public ItemId<Fleet> GetFleetId(int id) { return new ItemId<Fleet>(_jsonDatabase.GetFleet(id)); }
        public ItemId<Character> GetCharacterId(int id) { return new ItemId<Character>(_jsonDatabase.GetCharacter(id)); }
        public ItemId<QuestItem> GetQuestItemId(int id) { return new ItemId<QuestItem>(_jsonDatabase.GetQuestItem(id)); }
        public ItemId<Component> GetComponentId(int id) { return new ItemId<Component>(_jsonDatabase.GetComponent(id)); }
        public ItemId<BulletPrefab> GetBulletPrefabId(int id) { return new ItemId<BulletPrefab>(_jsonDatabase.GetBulletPrefab(id)); }
        public ItemId<VisualEffect> GetVisualEffectId(int id) { return new ItemId<VisualEffect>(_jsonDatabase.GetVisualEffect(id)); }
        public ItemId<Ammunition> GetAmmunitionId(int id) { return new ItemId<Ammunition>(_jsonDatabase.GetAmmunition(id)); }
        public ItemId<AmmunitionObsolete> GetAmmunitionObsoleteId(int id) { return new ItemId<AmmunitionObsolete>(_jsonDatabase.GetAmmunitionObsolete(id)); }

        private T GetItem<T,U>(int id, Dictionary<int, T> cache, U source) where T : class
        {
            T item;
            if (!cache.TryGetValue(id, out item) && source != null)
            {
                item = (T)Activator.CreateInstance(typeof(T), source, this);
                cache.Add(id, item);
            }

            return item;
        }

        private ShipSettings _shipSettings;
        private GalaxySettings _galaxySettings;

        private readonly Dictionary<int, Component> _components = new Dictionary<int, Component>();
        private readonly Dictionary<int, Device> _devices = new Dictionary<int, Device>();
        private readonly Dictionary<int, Weapon> _weapons = new Dictionary<int, Weapon>();
        private readonly Dictionary<int, AmmunitionObsolete> _ammunitionObsolete = new Dictionary<int, AmmunitionObsolete>();
        private readonly Dictionary<int, Ammunition> _ammunition = new Dictionary<int, Ammunition>();
        private readonly Dictionary<int, DroneBay> _droneBays = new Dictionary<int, DroneBay>();
        private readonly Dictionary<int, Ship> _ships = new Dictionary<int, Ship>();
        private readonly Dictionary<int, ShipBuild> _shipBuilds = new Dictionary<int, ShipBuild>();
        private readonly Dictionary<int, Satellite> _satellites = new Dictionary<int, Satellite>();
        private readonly Dictionary<int, SatelliteBuild> _satelliteBuilds = new Dictionary<int, SatelliteBuild>();
        private readonly Dictionary<int, Technology> _technologies = new Dictionary<int, Technology>();
        private readonly Dictionary<int, Skill> _skills = new Dictionary<int, Skill>();
        private readonly Dictionary<int, ComponentStats> _componentStats = new Dictionary<int, ComponentStats>();
        private readonly Dictionary<int, ComponentMod> _componentMods = new Dictionary<int, ComponentMod>();
        private readonly Dictionary<int, Faction> _factions = new Dictionary<int, Faction>();
        private readonly Dictionary<int, Loot> _loot = new Dictionary<int, Loot>();
        private readonly Dictionary<int, QuestModel> _quests = new Dictionary<int, QuestModel>();
        private readonly Dictionary<int, Fleet> _fleets = new Dictionary<int, Fleet>();
        private readonly Dictionary<int, Character> _characters = new Dictionary<int, Character>();
        private readonly Dictionary<int, QuestItem> _questItems = new Dictionary<int, QuestItem>();
        private readonly Dictionary<int, VisualEffect> _visualEffects = new Dictionary<int, VisualEffect>();
        private readonly Dictionary<int, BulletPrefab> _bulletPrefabs = new Dictionary<int, BulletPrefab>();

        private readonly JsonDatabase _jsonDatabase;
    }
}
