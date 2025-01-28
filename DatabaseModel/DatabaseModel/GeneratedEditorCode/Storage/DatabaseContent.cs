//-------------------------------------------------------------------------------
//                                                                               
//    This code was automatically generated.                                     
//    Changes to this file may cause incorrect behavior and will be lost if      
//    the code is regenerated.                                                   
//                                                                               
//-------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using EditorDatabase.Enums;
using EditorDatabase.Serializable;
using EditorDatabase.Model;

namespace EditorDatabase.Storage
{
    public class DatabaseContent : IContentLoader
    {
        public DatabaseContent(IJsonSerializer jsonSerializer, IDataStorage storage)
        {
            _serializer = jsonSerializer;
            storage?.LoadContent(this);
        }
  
        public void Save(IDataStorage storage, IJsonSerializer jsonSerializer)
        {
            foreach (var item in _ammunitionObsoleteMap.Values)
                storage.SaveJson(item.FileName, jsonSerializer.ToJson(item));
            foreach (var item in _componentMap.Values)
                storage.SaveJson(item.FileName, jsonSerializer.ToJson(item));
            foreach (var item in _componentGroupTagMap.Values)
                storage.SaveJson(item.FileName, jsonSerializer.ToJson(item));
            foreach (var item in _componentModMap.Values)
                storage.SaveJson(item.FileName, jsonSerializer.ToJson(item));
            foreach (var item in _componentStatsMap.Values)
                storage.SaveJson(item.FileName, jsonSerializer.ToJson(item));
            foreach (var item in _componentStatUpgradeMap.Values)
                storage.SaveJson(item.FileName, jsonSerializer.ToJson(item));
            foreach (var item in _deviceMap.Values)
                storage.SaveJson(item.FileName, jsonSerializer.ToJson(item));
            foreach (var item in _droneBayMap.Values)
                storage.SaveJson(item.FileName, jsonSerializer.ToJson(item));
            foreach (var item in _factionMap.Values)
                storage.SaveJson(item.FileName, jsonSerializer.ToJson(item));
            foreach (var item in _gameObjectPrefabMap.Values)
                storage.SaveJson(item.FileName, jsonSerializer.ToJson(item));
            foreach (var item in _satelliteMap.Values)
                storage.SaveJson(item.FileName, jsonSerializer.ToJson(item));
            foreach (var item in _satelliteBuildMap.Values)
                storage.SaveJson(item.FileName, jsonSerializer.ToJson(item));
            foreach (var item in _shipMap.Values)
                storage.SaveJson(item.FileName, jsonSerializer.ToJson(item));
            foreach (var item in _shipBuildMap.Values)
                storage.SaveJson(item.FileName, jsonSerializer.ToJson(item));
            foreach (var item in _statUpgradeTemplateMap.Values)
                storage.SaveJson(item.FileName, jsonSerializer.ToJson(item));
            foreach (var item in _technologyMap.Values)
                storage.SaveJson(item.FileName, jsonSerializer.ToJson(item));
            foreach (var item in _behaviorTreeMap.Values)
                storage.SaveJson(item.FileName, jsonSerializer.ToJson(item));
            foreach (var item in _characterMap.Values)
                storage.SaveJson(item.FileName, jsonSerializer.ToJson(item));
            foreach (var item in _combatRulesMap.Values)
                storage.SaveJson(item.FileName, jsonSerializer.ToJson(item));
            foreach (var item in _fleetMap.Values)
                storage.SaveJson(item.FileName, jsonSerializer.ToJson(item));
            foreach (var item in _lootMap.Values)
                storage.SaveJson(item.FileName, jsonSerializer.ToJson(item));
            foreach (var item in _questMap.Values)
                storage.SaveJson(item.FileName, jsonSerializer.ToJson(item));
            foreach (var item in _questItemMap.Values)
                storage.SaveJson(item.FileName, jsonSerializer.ToJson(item));
            foreach (var item in _ammunitionMap.Values)
                storage.SaveJson(item.FileName, jsonSerializer.ToJson(item));
            foreach (var item in _bulletPrefabMap.Values)
                storage.SaveJson(item.FileName, jsonSerializer.ToJson(item));
            foreach (var item in _visualEffectMap.Values)
                storage.SaveJson(item.FileName, jsonSerializer.ToJson(item));
            foreach (var item in _weaponMap.Values)
                storage.SaveJson(item.FileName, jsonSerializer.ToJson(item));
            if (CombatSettings != null)
                storage.SaveJson(CombatSettings.FileName, jsonSerializer.ToJson(CombatSettings));            
            if (DatabaseSettings != null)
                storage.SaveJson(DatabaseSettings.FileName, jsonSerializer.ToJson(DatabaseSettings));            
            if (DebugSettings != null)
                storage.SaveJson(DebugSettings.FileName, jsonSerializer.ToJson(DebugSettings));            
            if (ExplorationSettings != null)
                storage.SaveJson(ExplorationSettings.FileName, jsonSerializer.ToJson(ExplorationSettings));            
            if (FactionsSettings != null)
                storage.SaveJson(FactionsSettings.FileName, jsonSerializer.ToJson(FactionsSettings));            
            if (GalaxySettings != null)
                storage.SaveJson(GalaxySettings.FileName, jsonSerializer.ToJson(GalaxySettings));            
            if (LocalizationSettings != null)
                storage.SaveJson(LocalizationSettings.FileName, jsonSerializer.ToJson(LocalizationSettings));            
            if (MusicPlaylist != null)
                storage.SaveJson(MusicPlaylist.FileName, jsonSerializer.ToJson(MusicPlaylist));            
            if (ShipModSettings != null)
                storage.SaveJson(ShipModSettings.FileName, jsonSerializer.ToJson(ShipModSettings));            
            if (ShipSettings != null)
                storage.SaveJson(ShipSettings.FileName, jsonSerializer.ToJson(ShipSettings));            
            if (SkillSettings != null)
                storage.SaveJson(SkillSettings.FileName, jsonSerializer.ToJson(SkillSettings));            
            if (SpecialEventSettings != null)
                storage.SaveJson(SpecialEventSettings.FileName, jsonSerializer.ToJson(SpecialEventSettings));            
            if (UiSettings != null)
                storage.SaveJson(UiSettings.FileName, jsonSerializer.ToJson(UiSettings));            
            if (WeaponSlots != null)
                storage.SaveJson(WeaponSlots.FileName, jsonSerializer.ToJson(WeaponSlots));            
        }

        public void LoadJson(string name, string content)
        {
            var item = _serializer.FromJson<SerializableItem>(content);
            var type = item.ItemType;

            if (type == ItemType.AmmunitionObsolete)
            {
			    if (_ammunitionObsoleteMap.ContainsKey(item.Id)) throw new DatabaseException("Duplicate AmmunitionObsolete ID - " + item.Id + " (" + name + ")");
                var data = _serializer.FromJson<AmmunitionObsoleteSerializable>(content);
                data.FileName = name;
                _ammunitionObsoleteMap.Add(data.Id, data);
            }
            else if (type == ItemType.Component)
            {
			    if (_componentMap.ContainsKey(item.Id)) throw new DatabaseException("Duplicate Component ID - " + item.Id + " (" + name + ")");
                var data = _serializer.FromJson<ComponentSerializable>(content);
                data.FileName = name;
                _componentMap.Add(data.Id, data);
            }
            else if (type == ItemType.ComponentGroupTag)
            {
			    if (_componentGroupTagMap.ContainsKey(item.Id)) throw new DatabaseException("Duplicate ComponentGroupTag ID - " + item.Id + " (" + name + ")");
                var data = _serializer.FromJson<ComponentGroupTagSerializable>(content);
                data.FileName = name;
                _componentGroupTagMap.Add(data.Id, data);
            }
            else if (type == ItemType.ComponentMod)
            {
			    if (_componentModMap.ContainsKey(item.Id)) throw new DatabaseException("Duplicate ComponentMod ID - " + item.Id + " (" + name + ")");
                var data = _serializer.FromJson<ComponentModSerializable>(content);
                data.FileName = name;
                _componentModMap.Add(data.Id, data);
            }
            else if (type == ItemType.ComponentStats)
            {
			    if (_componentStatsMap.ContainsKey(item.Id)) throw new DatabaseException("Duplicate ComponentStats ID - " + item.Id + " (" + name + ")");
                var data = _serializer.FromJson<ComponentStatsSerializable>(content);
                data.FileName = name;
                _componentStatsMap.Add(data.Id, data);
            }
            else if (type == ItemType.ComponentStatUpgrade)
            {
			    if (_componentStatUpgradeMap.ContainsKey(item.Id)) throw new DatabaseException("Duplicate ComponentStatUpgrade ID - " + item.Id + " (" + name + ")");
                var data = _serializer.FromJson<ComponentStatUpgradeSerializable>(content);
                data.FileName = name;
                _componentStatUpgradeMap.Add(data.Id, data);
            }
            else if (type == ItemType.Device)
            {
			    if (_deviceMap.ContainsKey(item.Id)) throw new DatabaseException("Duplicate Device ID - " + item.Id + " (" + name + ")");
                var data = _serializer.FromJson<DeviceSerializable>(content);
                data.FileName = name;
                _deviceMap.Add(data.Id, data);
            }
            else if (type == ItemType.DroneBay)
            {
			    if (_droneBayMap.ContainsKey(item.Id)) throw new DatabaseException("Duplicate DroneBay ID - " + item.Id + " (" + name + ")");
                var data = _serializer.FromJson<DroneBaySerializable>(content);
                data.FileName = name;
                _droneBayMap.Add(data.Id, data);
            }
            else if (type == ItemType.Faction)
            {
			    if (_factionMap.ContainsKey(item.Id)) throw new DatabaseException("Duplicate Faction ID - " + item.Id + " (" + name + ")");
                var data = _serializer.FromJson<FactionSerializable>(content);
                data.FileName = name;
                _factionMap.Add(data.Id, data);
            }
            else if (type == ItemType.GameObjectPrefab)
            {
			    if (_gameObjectPrefabMap.ContainsKey(item.Id)) throw new DatabaseException("Duplicate GameObjectPrefab ID - " + item.Id + " (" + name + ")");
                var data = _serializer.FromJson<GameObjectPrefabSerializable>(content);
                data.FileName = name;
                _gameObjectPrefabMap.Add(data.Id, data);
            }
            else if (type == ItemType.Satellite)
            {
			    if (_satelliteMap.ContainsKey(item.Id)) throw new DatabaseException("Duplicate Satellite ID - " + item.Id + " (" + name + ")");
                var data = _serializer.FromJson<SatelliteSerializable>(content);
                data.FileName = name;
                _satelliteMap.Add(data.Id, data);
            }
            else if (type == ItemType.SatelliteBuild)
            {
			    if (_satelliteBuildMap.ContainsKey(item.Id)) throw new DatabaseException("Duplicate SatelliteBuild ID - " + item.Id + " (" + name + ")");
                var data = _serializer.FromJson<SatelliteBuildSerializable>(content);
                data.FileName = name;
                _satelliteBuildMap.Add(data.Id, data);
            }
            else if (type == ItemType.Ship)
            {
			    if (_shipMap.ContainsKey(item.Id)) throw new DatabaseException("Duplicate Ship ID - " + item.Id + " (" + name + ")");
                var data = _serializer.FromJson<ShipSerializable>(content);
                data.FileName = name;
                _shipMap.Add(data.Id, data);
            }
            else if (type == ItemType.ShipBuild)
            {
			    if (_shipBuildMap.ContainsKey(item.Id)) throw new DatabaseException("Duplicate ShipBuild ID - " + item.Id + " (" + name + ")");
                var data = _serializer.FromJson<ShipBuildSerializable>(content);
                data.FileName = name;
                _shipBuildMap.Add(data.Id, data);
            }
            else if (type == ItemType.StatUpgradeTemplate)
            {
			    if (_statUpgradeTemplateMap.ContainsKey(item.Id)) throw new DatabaseException("Duplicate StatUpgradeTemplate ID - " + item.Id + " (" + name + ")");
                var data = _serializer.FromJson<StatUpgradeTemplateSerializable>(content);
                data.FileName = name;
                _statUpgradeTemplateMap.Add(data.Id, data);
            }
            else if (type == ItemType.Technology)
            {
			    if (_technologyMap.ContainsKey(item.Id)) throw new DatabaseException("Duplicate Technology ID - " + item.Id + " (" + name + ")");
                var data = _serializer.FromJson<TechnologySerializable>(content);
                data.FileName = name;
                _technologyMap.Add(data.Id, data);
            }
            else if (type == ItemType.BehaviorTree)
            {
			    if (_behaviorTreeMap.ContainsKey(item.Id)) throw new DatabaseException("Duplicate BehaviorTree ID - " + item.Id + " (" + name + ")");
                var data = _serializer.FromJson<BehaviorTreeSerializable>(content);
                data.FileName = name;
                _behaviorTreeMap.Add(data.Id, data);
            }
            else if (type == ItemType.Character)
            {
			    if (_characterMap.ContainsKey(item.Id)) throw new DatabaseException("Duplicate Character ID - " + item.Id + " (" + name + ")");
                var data = _serializer.FromJson<CharacterSerializable>(content);
                data.FileName = name;
                _characterMap.Add(data.Id, data);
            }
            else if (type == ItemType.CombatRules)
            {
			    if (_combatRulesMap.ContainsKey(item.Id)) throw new DatabaseException("Duplicate CombatRules ID - " + item.Id + " (" + name + ")");
                var data = _serializer.FromJson<CombatRulesSerializable>(content);
                data.FileName = name;
                _combatRulesMap.Add(data.Id, data);
            }
            else if (type == ItemType.Fleet)
            {
			    if (_fleetMap.ContainsKey(item.Id)) throw new DatabaseException("Duplicate Fleet ID - " + item.Id + " (" + name + ")");
                var data = _serializer.FromJson<FleetSerializable>(content);
                data.FileName = name;
                _fleetMap.Add(data.Id, data);
            }
            else if (type == ItemType.Loot)
            {
			    if (_lootMap.ContainsKey(item.Id)) throw new DatabaseException("Duplicate Loot ID - " + item.Id + " (" + name + ")");
                var data = _serializer.FromJson<LootSerializable>(content);
                data.FileName = name;
                _lootMap.Add(data.Id, data);
            }
            else if (type == ItemType.Quest)
            {
			    if (_questMap.ContainsKey(item.Id)) throw new DatabaseException("Duplicate Quest ID - " + item.Id + " (" + name + ")");
                var data = _serializer.FromJson<QuestSerializable>(content);
                data.FileName = name;
                _questMap.Add(data.Id, data);
            }
            else if (type == ItemType.QuestItem)
            {
			    if (_questItemMap.ContainsKey(item.Id)) throw new DatabaseException("Duplicate QuestItem ID - " + item.Id + " (" + name + ")");
                var data = _serializer.FromJson<QuestItemSerializable>(content);
                data.FileName = name;
                _questItemMap.Add(data.Id, data);
            }
            else if (type == ItemType.Ammunition)
            {
			    if (_ammunitionMap.ContainsKey(item.Id)) throw new DatabaseException("Duplicate Ammunition ID - " + item.Id + " (" + name + ")");
                var data = _serializer.FromJson<AmmunitionSerializable>(content);
                data.FileName = name;
                _ammunitionMap.Add(data.Id, data);
            }
            else if (type == ItemType.BulletPrefab)
            {
			    if (_bulletPrefabMap.ContainsKey(item.Id)) throw new DatabaseException("Duplicate BulletPrefab ID - " + item.Id + " (" + name + ")");
                var data = _serializer.FromJson<BulletPrefabSerializable>(content);
                data.FileName = name;
                _bulletPrefabMap.Add(data.Id, data);
            }
            else if (type == ItemType.VisualEffect)
            {
			    if (_visualEffectMap.ContainsKey(item.Id)) throw new DatabaseException("Duplicate VisualEffect ID - " + item.Id + " (" + name + ")");
                var data = _serializer.FromJson<VisualEffectSerializable>(content);
                data.FileName = name;
                _visualEffectMap.Add(data.Id, data);
            }
            else if (type == ItemType.Weapon)
            {
			    if (_weaponMap.ContainsKey(item.Id)) throw new DatabaseException("Duplicate Weapon ID - " + item.Id + " (" + name + ")");
                var data = _serializer.FromJson<WeaponSerializable>(content);
                data.FileName = name;
                _weaponMap.Add(data.Id, data);
            }
            else if (type == ItemType.CombatSettings)
            {
                var data = _serializer.FromJson<CombatSettingsSerializable>(content);
                data.FileName = name;

				if (CombatSettings != null)
                    throw new DatabaseException("Duplicate CombatSettings file found - " + name);
                CombatSettings = data;
            }
            else if (type == ItemType.DatabaseSettings)
            {
                var data = _serializer.FromJson<DatabaseSettingsSerializable>(content);
                data.FileName = name;

				if (DatabaseSettings != null)
                    throw new DatabaseException("Duplicate DatabaseSettings file found - " + name);
                DatabaseSettings = data;
            }
            else if (type == ItemType.DebugSettings)
            {
                var data = _serializer.FromJson<DebugSettingsSerializable>(content);
                data.FileName = name;

				if (DebugSettings != null)
                    throw new DatabaseException("Duplicate DebugSettings file found - " + name);
                DebugSettings = data;
            }
            else if (type == ItemType.ExplorationSettings)
            {
                var data = _serializer.FromJson<ExplorationSettingsSerializable>(content);
                data.FileName = name;

				if (ExplorationSettings != null)
                    throw new DatabaseException("Duplicate ExplorationSettings file found - " + name);
                ExplorationSettings = data;
            }
            else if (type == ItemType.FactionsSettings)
            {
                var data = _serializer.FromJson<FactionsSettingsSerializable>(content);
                data.FileName = name;

				if (FactionsSettings != null)
                    throw new DatabaseException("Duplicate FactionsSettings file found - " + name);
                FactionsSettings = data;
            }
            else if (type == ItemType.GalaxySettings)
            {
                var data = _serializer.FromJson<GalaxySettingsSerializable>(content);
                data.FileName = name;

				if (GalaxySettings != null)
                    throw new DatabaseException("Duplicate GalaxySettings file found - " + name);
                GalaxySettings = data;
            }
            else if (type == ItemType.LocalizationSettings)
            {
                var data = _serializer.FromJson<LocalizationSettingsSerializable>(content);
                data.FileName = name;

				if (LocalizationSettings != null)
                    throw new DatabaseException("Duplicate LocalizationSettings file found - " + name);
                LocalizationSettings = data;
            }
            else if (type == ItemType.MusicPlaylist)
            {
                var data = _serializer.FromJson<MusicPlaylistSerializable>(content);
                data.FileName = name;

				if (MusicPlaylist != null)
                    throw new DatabaseException("Duplicate MusicPlaylist file found - " + name);
                MusicPlaylist = data;
            }
            else if (type == ItemType.ShipModSettings)
            {
                var data = _serializer.FromJson<ShipModSettingsSerializable>(content);
                data.FileName = name;

				if (ShipModSettings != null)
                    throw new DatabaseException("Duplicate ShipModSettings file found - " + name);
                ShipModSettings = data;
            }
            else if (type == ItemType.ShipSettings)
            {
                var data = _serializer.FromJson<ShipSettingsSerializable>(content);
                data.FileName = name;

				if (ShipSettings != null)
                    throw new DatabaseException("Duplicate ShipSettings file found - " + name);
                ShipSettings = data;
            }
            else if (type == ItemType.SkillSettings)
            {
                var data = _serializer.FromJson<SkillSettingsSerializable>(content);
                data.FileName = name;

				if (SkillSettings != null)
                    throw new DatabaseException("Duplicate SkillSettings file found - " + name);
                SkillSettings = data;
            }
            else if (type == ItemType.SpecialEventSettings)
            {
                var data = _serializer.FromJson<SpecialEventSettingsSerializable>(content);
                data.FileName = name;

				if (SpecialEventSettings != null)
                    throw new DatabaseException("Duplicate SpecialEventSettings file found - " + name);
                SpecialEventSettings = data;
            }
            else if (type == ItemType.UiSettings)
            {
                var data = _serializer.FromJson<UiSettingsSerializable>(content);
                data.FileName = name;

				if (UiSettings != null)
                    throw new DatabaseException("Duplicate UiSettings file found - " + name);
                UiSettings = data;
            }
            else if (type == ItemType.WeaponSlots)
            {
                var data = _serializer.FromJson<WeaponSlotsSerializable>(content);
                data.FileName = name;

				if (WeaponSlots != null)
                    throw new DatabaseException("Duplicate WeaponSlots file found - " + name);
                WeaponSlots = data;
            }
            else
            {
                throw new DatabaseException("Unknown file type - " + type + "(" + name + ")");
            }
        }

		public void LoadLocalization(string name, string data)
        {
            _localizations.Add(name, data);
        }

        public void LoadImage(string name, IImageData image)
        {
            _images.Add(name, image);
        }
        
        public void LoadAudioClip(string name, IAudioClipData audioClip)
        {
            _audioClips.Add(name, audioClip);
        }
		public CombatSettingsSerializable CombatSettings { get; private set; }
		public DatabaseSettingsSerializable DatabaseSettings { get; private set; }
		public DebugSettingsSerializable DebugSettings { get; private set; }
		public ExplorationSettingsSerializable ExplorationSettings { get; private set; }
		public FactionsSettingsSerializable FactionsSettings { get; private set; }
		public GalaxySettingsSerializable GalaxySettings { get; private set; }
		public LocalizationSettingsSerializable LocalizationSettings { get; private set; }
		public MusicPlaylistSerializable MusicPlaylist { get; private set; }
		public ShipModSettingsSerializable ShipModSettings { get; private set; }
		public ShipSettingsSerializable ShipSettings { get; private set; }
		public SkillSettingsSerializable SkillSettings { get; private set; }
		public SpecialEventSettingsSerializable SpecialEventSettings { get; private set; }
		public UiSettingsSerializable UiSettings { get; private set; }
		public WeaponSlotsSerializable WeaponSlots { get; private set; }

		public IEnumerable<AmmunitionObsoleteSerializable> AmmunitionObsoleteList => _ammunitionObsoleteMap.Values;
		public IEnumerable<ComponentSerializable> ComponentList => _componentMap.Values;
		public IEnumerable<ComponentGroupTagSerializable> ComponentGroupTagList => _componentGroupTagMap.Values;
		public IEnumerable<ComponentModSerializable> ComponentModList => _componentModMap.Values;
		public IEnumerable<ComponentStatsSerializable> ComponentStatsList => _componentStatsMap.Values;
		public IEnumerable<ComponentStatUpgradeSerializable> ComponentStatUpgradeList => _componentStatUpgradeMap.Values;
		public IEnumerable<DeviceSerializable> DeviceList => _deviceMap.Values;
		public IEnumerable<DroneBaySerializable> DroneBayList => _droneBayMap.Values;
		public IEnumerable<FactionSerializable> FactionList => _factionMap.Values;
		public IEnumerable<GameObjectPrefabSerializable> GameObjectPrefabList => _gameObjectPrefabMap.Values;
		public IEnumerable<SatelliteSerializable> SatelliteList => _satelliteMap.Values;
		public IEnumerable<SatelliteBuildSerializable> SatelliteBuildList => _satelliteBuildMap.Values;
		public IEnumerable<ShipSerializable> ShipList => _shipMap.Values;
		public IEnumerable<ShipBuildSerializable> ShipBuildList => _shipBuildMap.Values;
		public IEnumerable<StatUpgradeTemplateSerializable> StatUpgradeTemplateList => _statUpgradeTemplateMap.Values;
		public IEnumerable<TechnologySerializable> TechnologyList => _technologyMap.Values;
		public IEnumerable<BehaviorTreeSerializable> BehaviorTreeList => _behaviorTreeMap.Values;
		public IEnumerable<CharacterSerializable> CharacterList => _characterMap.Values;
		public IEnumerable<CombatRulesSerializable> CombatRulesList => _combatRulesMap.Values;
		public IEnumerable<FleetSerializable> FleetList => _fleetMap.Values;
		public IEnumerable<LootSerializable> LootList => _lootMap.Values;
		public IEnumerable<QuestSerializable> QuestList => _questMap.Values;
		public IEnumerable<QuestItemSerializable> QuestItemList => _questItemMap.Values;
		public IEnumerable<AmmunitionSerializable> AmmunitionList => _ammunitionMap.Values;
		public IEnumerable<BulletPrefabSerializable> BulletPrefabList => _bulletPrefabMap.Values;
		public IEnumerable<VisualEffectSerializable> VisualEffectList => _visualEffectMap.Values;
		public IEnumerable<WeaponSerializable> WeaponList => _weaponMap.Values;

		public AmmunitionObsoleteSerializable GetAmmunitionObsolete(int id) { return _ammunitionObsoleteMap.TryGetValue(id, out var item) ? item : null; }
		public ComponentSerializable GetComponent(int id) { return _componentMap.TryGetValue(id, out var item) ? item : null; }
		public ComponentGroupTagSerializable GetComponentGroupTag(int id) { return _componentGroupTagMap.TryGetValue(id, out var item) ? item : null; }
		public ComponentModSerializable GetComponentMod(int id) { return _componentModMap.TryGetValue(id, out var item) ? item : null; }
		public ComponentStatsSerializable GetComponentStats(int id) { return _componentStatsMap.TryGetValue(id, out var item) ? item : null; }
		public ComponentStatUpgradeSerializable GetComponentStatUpgrade(int id) { return _componentStatUpgradeMap.TryGetValue(id, out var item) ? item : null; }
		public DeviceSerializable GetDevice(int id) { return _deviceMap.TryGetValue(id, out var item) ? item : null; }
		public DroneBaySerializable GetDroneBay(int id) { return _droneBayMap.TryGetValue(id, out var item) ? item : null; }
		public FactionSerializable GetFaction(int id) { return _factionMap.TryGetValue(id, out var item) ? item : null; }
		public GameObjectPrefabSerializable GetGameObjectPrefab(int id) { return _gameObjectPrefabMap.TryGetValue(id, out var item) ? item : null; }
		public SatelliteSerializable GetSatellite(int id) { return _satelliteMap.TryGetValue(id, out var item) ? item : null; }
		public SatelliteBuildSerializable GetSatelliteBuild(int id) { return _satelliteBuildMap.TryGetValue(id, out var item) ? item : null; }
		public ShipSerializable GetShip(int id) { return _shipMap.TryGetValue(id, out var item) ? item : null; }
		public ShipBuildSerializable GetShipBuild(int id) { return _shipBuildMap.TryGetValue(id, out var item) ? item : null; }
		public StatUpgradeTemplateSerializable GetStatUpgradeTemplate(int id) { return _statUpgradeTemplateMap.TryGetValue(id, out var item) ? item : null; }
		public TechnologySerializable GetTechnology(int id) { return _technologyMap.TryGetValue(id, out var item) ? item : null; }
		public BehaviorTreeSerializable GetBehaviorTree(int id) { return _behaviorTreeMap.TryGetValue(id, out var item) ? item : null; }
		public CharacterSerializable GetCharacter(int id) { return _characterMap.TryGetValue(id, out var item) ? item : null; }
		public CombatRulesSerializable GetCombatRules(int id) { return _combatRulesMap.TryGetValue(id, out var item) ? item : null; }
		public FleetSerializable GetFleet(int id) { return _fleetMap.TryGetValue(id, out var item) ? item : null; }
		public LootSerializable GetLoot(int id) { return _lootMap.TryGetValue(id, out var item) ? item : null; }
		public QuestSerializable GetQuest(int id) { return _questMap.TryGetValue(id, out var item) ? item : null; }
		public QuestItemSerializable GetQuestItem(int id) { return _questItemMap.TryGetValue(id, out var item) ? item : null; }
		public AmmunitionSerializable GetAmmunition(int id) { return _ammunitionMap.TryGetValue(id, out var item) ? item : null; }
		public BulletPrefabSerializable GetBulletPrefab(int id) { return _bulletPrefabMap.TryGetValue(id, out var item) ? item : null; }
		public VisualEffectSerializable GetVisualEffect(int id) { return _visualEffectMap.TryGetValue(id, out var item) ? item : null; }
		public WeaponSerializable GetWeapon(int id) { return _weaponMap.TryGetValue(id, out var item) ? item : null; }

        public IImageData GetImage(string name)
        {
            return _images.TryGetValue(name, out var image) ? image : ImageData.Empty;
        }

        public IAudioClipData GetAudioClip(string name)
        {
            return _audioClips.TryGetValue(name, out var audioClip) ? audioClip : AudioClipData.Empty;
        }

        private IEnumerable<KeyValuePair<string, IImageData>> Images => _images;
        private IEnumerable<KeyValuePair<string, string>> Localizations => _localizations;

        private readonly IJsonSerializer _serializer;

		private readonly Dictionary<int, AmmunitionObsoleteSerializable> _ammunitionObsoleteMap = new Dictionary<int, AmmunitionObsoleteSerializable>();
		private readonly Dictionary<int, ComponentSerializable> _componentMap = new Dictionary<int, ComponentSerializable>();
		private readonly Dictionary<int, ComponentGroupTagSerializable> _componentGroupTagMap = new Dictionary<int, ComponentGroupTagSerializable>();
		private readonly Dictionary<int, ComponentModSerializable> _componentModMap = new Dictionary<int, ComponentModSerializable>();
		private readonly Dictionary<int, ComponentStatsSerializable> _componentStatsMap = new Dictionary<int, ComponentStatsSerializable>();
		private readonly Dictionary<int, ComponentStatUpgradeSerializable> _componentStatUpgradeMap = new Dictionary<int, ComponentStatUpgradeSerializable>();
		private readonly Dictionary<int, DeviceSerializable> _deviceMap = new Dictionary<int, DeviceSerializable>();
		private readonly Dictionary<int, DroneBaySerializable> _droneBayMap = new Dictionary<int, DroneBaySerializable>();
		private readonly Dictionary<int, FactionSerializable> _factionMap = new Dictionary<int, FactionSerializable>();
		private readonly Dictionary<int, GameObjectPrefabSerializable> _gameObjectPrefabMap = new Dictionary<int, GameObjectPrefabSerializable>();
		private readonly Dictionary<int, SatelliteSerializable> _satelliteMap = new Dictionary<int, SatelliteSerializable>();
		private readonly Dictionary<int, SatelliteBuildSerializable> _satelliteBuildMap = new Dictionary<int, SatelliteBuildSerializable>();
		private readonly Dictionary<int, ShipSerializable> _shipMap = new Dictionary<int, ShipSerializable>();
		private readonly Dictionary<int, ShipBuildSerializable> _shipBuildMap = new Dictionary<int, ShipBuildSerializable>();
		private readonly Dictionary<int, StatUpgradeTemplateSerializable> _statUpgradeTemplateMap = new Dictionary<int, StatUpgradeTemplateSerializable>();
		private readonly Dictionary<int, TechnologySerializable> _technologyMap = new Dictionary<int, TechnologySerializable>();
		private readonly Dictionary<int, BehaviorTreeSerializable> _behaviorTreeMap = new Dictionary<int, BehaviorTreeSerializable>();
		private readonly Dictionary<int, CharacterSerializable> _characterMap = new Dictionary<int, CharacterSerializable>();
		private readonly Dictionary<int, CombatRulesSerializable> _combatRulesMap = new Dictionary<int, CombatRulesSerializable>();
		private readonly Dictionary<int, FleetSerializable> _fleetMap = new Dictionary<int, FleetSerializable>();
		private readonly Dictionary<int, LootSerializable> _lootMap = new Dictionary<int, LootSerializable>();
		private readonly Dictionary<int, QuestSerializable> _questMap = new Dictionary<int, QuestSerializable>();
		private readonly Dictionary<int, QuestItemSerializable> _questItemMap = new Dictionary<int, QuestItemSerializable>();
		private readonly Dictionary<int, AmmunitionSerializable> _ammunitionMap = new Dictionary<int, AmmunitionSerializable>();
		private readonly Dictionary<int, BulletPrefabSerializable> _bulletPrefabMap = new Dictionary<int, BulletPrefabSerializable>();
		private readonly Dictionary<int, VisualEffectSerializable> _visualEffectMap = new Dictionary<int, VisualEffectSerializable>();
		private readonly Dictionary<int, WeaponSerializable> _weaponMap = new Dictionary<int, WeaponSerializable>();

        private readonly Dictionary<string, IImageData> _images = new Dictionary<string, IImageData>(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, IAudioClipData> _audioClips = new Dictionary<string, IAudioClipData>(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, string> _localizations = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
	}
}

