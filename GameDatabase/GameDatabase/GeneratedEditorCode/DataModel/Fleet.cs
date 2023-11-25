//-------------------------------------------------------------------------------
//                                                                               
//    This code was automatically generated.                                     
//    Changes to this file may cause incorrect behavior and will be lost if      
//    the code is regenerated.                                                   
//                                                                               
//-------------------------------------------------------------------------------

using System.Linq;
using EditorDatabase.Enums;
using EditorDatabase.Serializable;
using EditorDatabase.Model;

namespace EditorDatabase.DataModel
{
	public partial class Fleet
	{
		partial void OnDataDeserialized(FleetSerializable serializable, Database database);
		partial void OnDataSerialized(ref FleetSerializable serializable);


		public Fleet(FleetSerializable serializable, Database database)
		{
			try
			{
				Id = new ItemId<Fleet>(serializable.Id, serializable.FileName);
				Factions = new RequiredFactions(serializable.Factions, database);
				LevelBonus = new NumericValue<int>(serializable.LevelBonus, -10000, 10000);
				NoRandomShips = serializable.NoRandomShips;
				CombatTimeLimit = new NumericValue<int>(serializable.CombatTimeLimit, 0, 999);
				LootCondition = serializable.LootCondition;
				ExpCondition = serializable.ExpCondition;
				SpecificShips = serializable.SpecificShips?.Select(id => new Wrapper<ShipBuild> { Item = database.GetShipBuildId(id) }).ToArray();
				NoShipChanging = serializable.NoShipChanging;
				PlayerHasOneShip = serializable.PlayerHasOneShip;
			}
			catch (DatabaseException e)
			{
				throw new DatabaseException(this.GetType() + ": deserialization failed. " + serializable.FileName + " (" + serializable.Id + ")", e);
			}
			OnDataDeserialized(serializable, database);
		}

		public void Save(FleetSerializable serializable)
		{
			serializable.Factions = Factions.Serialize();
			serializable.LevelBonus = LevelBonus.Value;
			serializable.NoRandomShips = NoRandomShips;
			serializable.CombatTimeLimit = CombatTimeLimit.Value;
			serializable.LootCondition = LootCondition;
			serializable.ExpCondition = ExpCondition;
			if (SpecificShips == null || SpecificShips.Length == 0)
			    serializable.SpecificShips = null;
			else
			    serializable.SpecificShips = SpecificShips.Select(wrapper => wrapper.Item.Value).ToArray();
			serializable.NoShipChanging = NoShipChanging;
			serializable.PlayerHasOneShip = PlayerHasOneShip;
			OnDataSerialized(ref serializable);
		}

		public readonly ItemId<Fleet> Id;

		public RequiredFactions Factions = new RequiredFactions();
		public NumericValue<int> LevelBonus = new NumericValue<int>(0, -10000, 10000);
		public bool NoRandomShips;
		public NumericValue<int> CombatTimeLimit = new NumericValue<int>(0, 0, 999);
		public RewardCondition LootCondition;
		public RewardCondition ExpCondition;
		public Wrapper<ShipBuild>[] SpecificShips;
		public bool NoShipChanging;
		public bool PlayerHasOneShip;

		public static Fleet DefaultValue { get; private set; }
	}
}
