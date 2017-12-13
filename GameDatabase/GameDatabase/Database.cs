using System;
using System.Collections.Generic;
using System.Linq;
using GameDatabase.EditorModel;
using GameDatabase.Model;

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
                _ammunitions = _jsonDatabase.Ammunitions.ToDictionary(item => item.Id, item => new Ammunition(item, this));
                _droneBays = _jsonDatabase.DroneBays.ToDictionary(item => item.Id, item => new DroneBay(item, this));
                _ships = _jsonDatabase.Ships.ToDictionary(item => item.Id, item => new Ship(item, this));
                _shipBuilds = _jsonDatabase.ShipBuilds.ToDictionary(item => item.Id, item => new ShipBuild(item, this));
                _satellites = _jsonDatabase.Satellites.ToDictionary(item => item.Id, item => new Satellite(item, this));
                _satelliteBuilds = _jsonDatabase.SatelliteBuilds.ToDictionary(item => item.Id, item => new SatelliteBuild(item, this));
                _technologies = _jsonDatabase.Technologies.ToDictionary(item => item.Id, item => new Technology(item, this));
                _componentStats = _jsonDatabase.ComponentStats.ToDictionary(item => item.Id, item => new ComponentStats(item, this));
                _componentMods = _jsonDatabase.ComponentMods.ToDictionary(item => item.Id, item => new ComponentMod(item, this));
                _shipBuilderSettings = _jsonDatabase.ShipBuilderSettings.ToDictionary(item => item.Id, item => new ShipBuilderSettings(item, this));
            }
        }

        public void SaveAs(string path)
        {
            foreach (var item in _components)
                item.Value.Save(_jsonDatabase.GetComponent(item.Key));
            foreach (var item in _devices)
                item.Value.Save(_jsonDatabase.GetDevice(item.Key));
            foreach (var item in _weapons)
                item.Value.Save(_jsonDatabase.GetWeapon(item.Key));
            foreach (var item in _ammunitions)
                item.Value.Save(_jsonDatabase.GetAmmunition(item.Key));
            foreach (var item in _droneBays)
                item.Value.Save(_jsonDatabase.GetDroneBay(item.Key));
            foreach (var item in _ships)
                item.Value.Save(_jsonDatabase.GetShip(item.Key));
            foreach (var item in _shipBuilds)
                item.Value.Save(_jsonDatabase.GetShipBuild(item.Key));
            foreach (var item in _satellites)
                item.Value.Save(_jsonDatabase.GetSatellite(item.Key));
            foreach (var item in _satelliteBuilds)
                item.Value.Save(_jsonDatabase.GetSatelliteBuild(item.Key));
            foreach (var item in _technologies)
                item.Value.Save(_jsonDatabase.GetTechnology(item.Key));
            foreach (var item in _skills)
                item.Value.Save(_jsonDatabase.GetSkill(item.Key));
            foreach (var item in _componentStats)
                item.Value.Save(_jsonDatabase.GetComponentStats(item.Key));
            foreach (var item in _componentMods)
                item.Value.Save(_jsonDatabase.GetComponentMod(item.Key));
            foreach (var item in _shipBuilderSettings)
                item.Value.Save(_jsonDatabase.GetShipBuilderSettings(item.Key));

            _jsonDatabase.SaveData(path);
        }

        public Component GetComponent(int id) { return GetItem(id, _components, _jsonDatabase.GetComponent(id)); }
        public Device GetDevice(int id) { return GetItem(id, _devices, _jsonDatabase.GetDevice(id)); }
        public Weapon GetWeapon(int id) { return GetItem(id, _weapons, _jsonDatabase.GetWeapon(id)); }
        public Ammunition GetAmmunition(int id) { return GetItem(id, _ammunitions, _jsonDatabase.GetAmmunition(id)); }
        public DroneBay GetDroneBay(int id) { return GetItem(id, _droneBays, _jsonDatabase.GetDroneBay(id)); }
        public Ship GetShip(int id) { return GetItem(id, _ships, _jsonDatabase.GetShip(id)); }
        public ShipBuild GetShipBuild(int id) { return GetItem(id, _shipBuilds, _jsonDatabase.GetShipBuild(id)); }
        public Satellite GetSatellite(int id) { return GetItem(id, _satellites, _jsonDatabase.GetSatellite(id)); }
        public SatelliteBuild GetSatelliteBuild(int id) { return GetItem(id, _satelliteBuilds, _jsonDatabase.GetSatelliteBuild(id)); }
        public Technology GetTechnology(int id) { return GetItem(id, _technologies, _jsonDatabase.GetTechnology(id)); }
        public Skill GetSkill(int id) { return GetItem(id, _skills, _jsonDatabase.GetSkill(id)); }
        public ComponentStats GetComponentStats(int id) { return GetItem(id, _componentStats, _jsonDatabase.GetComponentStats(id)); }
        public ComponentMod GetComponentMods(int id) { return GetItem(id, _componentMods, _jsonDatabase.GetComponentMod(id)); }
        public ShipBuilderSettings GetShipBuilderSettings(int id) { return GetItem(id, _shipBuilderSettings, _jsonDatabase.GetShipBuilderSettings(id)); }

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

        public ItemId<Technology> GetTechnologyId(int id) { return new ItemId<Technology>(id, _jsonDatabase.GetTechnology(id).FileName); }
        public ItemId<Skill> GetSkillId(int id) { return new ItemId<Skill>(id, _jsonDatabase.GetSkill(id).FileName); }
        public ItemId<ComponentMod> GetComponentModId(int id) { return new ItemId<ComponentMod>(id, _jsonDatabase.GetComponentMod(id).FileName); }

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

        private readonly Dictionary<int, Component> _components = new Dictionary<int, Component>();
        private readonly Dictionary<int, Device> _devices = new Dictionary<int, Device>();
        private readonly Dictionary<int, Weapon> _weapons = new Dictionary<int, Weapon>();
        private readonly Dictionary<int, Ammunition> _ammunitions = new Dictionary<int, Ammunition>();
        private readonly Dictionary<int, DroneBay> _droneBays = new Dictionary<int, DroneBay>();
        private readonly Dictionary<int, Ship> _ships = new Dictionary<int, Ship>();
        private readonly Dictionary<int, ShipBuild> _shipBuilds = new Dictionary<int, ShipBuild>();
        private readonly Dictionary<int, Satellite> _satellites = new Dictionary<int, Satellite>();
        private readonly Dictionary<int, SatelliteBuild> _satelliteBuilds = new Dictionary<int, SatelliteBuild>();
        private readonly Dictionary<int, Technology> _technologies = new Dictionary<int, Technology>();
        private readonly Dictionary<int, Skill> _skills = new Dictionary<int, Skill>();
        private readonly Dictionary<int, ComponentStats> _componentStats = new Dictionary<int, ComponentStats>();
        private readonly Dictionary<int, ComponentMod> _componentMods = new Dictionary<int, ComponentMod>();
        private readonly Dictionary<int, ShipBuilderSettings> _shipBuilderSettings = new Dictionary<int, ShipBuilderSettings>();

        private readonly JsonDatabase _jsonDatabase;
    }
}
