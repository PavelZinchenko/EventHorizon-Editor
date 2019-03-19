using System.Linq;
using GameDatabase.Enums.Quests;
using GameDatabase.Model;
using GameDatabase.Serializable;

namespace GameDatabase.EditorModel.Quests
{
    public class Fleet
    {
        public Fleet(SerializableFleet fleet, Database database)
        {
            ItemId = new ItemId<Fleet>(fleet.Id, fleet.FileName);

            Factions = new FactionFilter(fleet.Factions, database);

            CombatTimeLimit = new NumericValue<int>(fleet.CombatTimeLimit, 0, 999);
            LevelBonus = new NumericValue<int>(fleet.LevelBonus, -100, 100);
            NoRandomShips = fleet.NoRandomShips;
            LootCondition = (RewardCondition)fleet.LootCondition;
            ExpCondition = (RewardCondition)fleet.ExpCondition;
            Factions = new FactionFilter(fleet.Factions, database);
            SpecificShips = fleet.SpecificShips?.Select(item => new Wrapper<ShipBuild> { Item = database.GetShipBuildId(item) }).ToArray();
        }

        public void Save(SerializableFleet serializable)
        {
            serializable.Factions = Factions.Serialize();
            serializable.CombatTimeLimit = CombatTimeLimit.Value;
            serializable.LevelBonus = LevelBonus.Value;
            serializable.NoRandomShips = NoRandomShips;
            serializable.LootCondition = (int)LootCondition;
            serializable.ExpCondition = (int)ExpCondition;
            serializable.SpecificShips = SpecificShips?.Select(item => item.Item.Id).ToArray();
        }

        public readonly ItemId<Fleet> ItemId;

        public FactionFilter Factions;

        public NumericValue<int> LevelBonus;
        public bool NoRandomShips;
        public NumericValue<int> CombatTimeLimit;
        public RewardCondition LootCondition;
        public RewardCondition ExpCondition;
        public Wrapper<ShipBuild>[] SpecificShips;
    }
}
