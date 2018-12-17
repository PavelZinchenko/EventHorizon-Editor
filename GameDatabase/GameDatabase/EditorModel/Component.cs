using System.Drawing;
using System.Linq;
using GameDatabase.Enums;
using GameDatabase.Model;
using GameDatabase.Serializable;

namespace GameDatabase.EditorModel
{
    public class Component
    {
        public Component(SerializableComponent component, Database database)
        {
            ItemId = new ItemId<Component>(component.Id, component.FileName);
            Name = component.Name;
            Description = component.Description;
            DisplayCategory = component.DisplayCategory;
            Availability = component.Availability;
            FactionId = database.GetFaction(component.Faction).Id;
            Level = new NumericValue<int>(component.Level,0,500);
            Color = Helpers.ColorFromString(component.Color);
            Layout = new Layout(component.Layout);
            CellType = string.IsNullOrEmpty(component.CellType) ? CellType.Empty : (CellType)component.CellType.First();
            IconId = component.Icon;

            var stats = database.GetComponentStats(component.ComponentStatsId);
            if (stats != null)
                StatsId = stats.ItemId;

            var device = database.GetDevice(component.DeviceId);
            if (device != null)
                DeviceId = device.ItemId;

            var weapon = database.GetWeapon(component.WeaponId);
            if (weapon != null)
                WeaponId = weapon.ItemId;

            //WeaponSlotType = string.IsNullOrEmpty(component.WeaponType) ? WeaponSlotType.Default : (WeaponSlotType)component.WeaponType.First();
            WeaponSlotType = string.IsNullOrEmpty(component.WeaponSlotType) ? WeaponSlotType.Default : (WeaponSlotType)component.WeaponSlotType.First();

            var ammunition = database.GetAmmunition(component.AmmunitionId);
            if (ammunition != null)
                AmmunitionId = ammunition.ItemId;

            var dronebay = database.GetDroneBay(component.DroneBayId);
            if (dronebay != null)
                DroneBayId = dronebay.ItemId;

            var drone = database.GetShipBuild(component.DroneId);
            if (drone != null)
                DroneId = drone.ItemId;

            if (component.PossibleModifications != null)
                PossibleModification = component.PossibleModifications.Select(item => new ComponentModWrapper { Modification = database.GetComponentModId(item) }).ToArray();
            else
                PossibleModification = new ComponentModWrapper[] {};
        }

        public void Save(SerializableComponent serializable)
        {
            serializable.Name = Name;
            serializable.Description = Description;
            serializable.DisplayCategory = DisplayCategory;
            serializable.Availability = Availability;
            serializable.ComponentStatsId = StatsId.Id;
            serializable.Faction = FactionId.Id;
            serializable.Level = Level.Value;
            serializable.Icon = IconId;
            serializable.Color = Helpers.ColorToString(Color);
            serializable.Layout = Layout.Data;
            serializable.CellType = CellType == CellType.Empty ? string.Empty : ((char)CellType).ToString();
            serializable.DeviceId = DeviceId.Id;
            serializable.WeaponId = WeaponId.Id;
            serializable.AmmunitionId = AmmunitionId.Id;
            serializable.WeaponSlotType = WeaponSlotType == WeaponSlotType.Default ? string.Empty : ((char)WeaponSlotType).ToString();
            serializable.DroneBayId = DroneBayId.Id;
            serializable.DroneId = DroneId.Id;

            serializable.PossibleModifications = PossibleModification.Select(item => item.Modification.Id).ToArray();
        }

        public ItemId<Component> ItemId;
        public string Name;
        public string Description;
        public ItemId<Faction> FactionId;
        public NumericValue<int> Level;
        public Availability Availability;

        public ComponentCategory DisplayCategory;

        public ItemId<ComponentStats> StatsId = ItemId<ComponentStats>.Empty;

        public Layout Layout;
        public CellType CellType;

        public ItemId<Device> DeviceId = ItemId<Device>.Empty;

        public ItemId<Weapon> WeaponId = ItemId<Weapon>.Empty;
        public ItemId<Ammunition> AmmunitionId = ItemId<Ammunition>.Empty;
        public WeaponSlotType WeaponSlotType;

        public ItemId<DroneBay> DroneBayId = ItemId<DroneBay>.Empty;
        public ItemId<ShipBuild> DroneId = ItemId<ShipBuild>.Empty;

        public string IconId;
        public Color Color;

        public ComponentModWrapper[] PossibleModification;
    }

    public class ComponentModWrapper
    {
        public ItemId<ComponentMod> Modification = ItemId<ComponentMod>.Empty;

        public override string ToString()
        {
            return Modification.ToString();
        }
    }
}
