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
	public partial class SkillSettings
	{
		partial void OnDataDeserialized(SkillSettingsSerializable serializable, Database database);
		partial void OnDataSerialized(ref SkillSettingsSerializable serializable);


		public SkillSettings(SkillSettingsSerializable serializable, Database database)
		{
			BeatAllEnemiesFactionList = serializable.BeatAllEnemiesFactionList?.Select(id => new Wrapper<Faction> { Item = database.GetFactionId(id) }).ToArray();
			DisableExceedTheLimits = serializable.DisableExceedTheLimits;
			FuelTankCapacity = new NumericValue<int>(serializable.FuelTankCapacity, 0, 100);
			MapFlightRange = new NumericValue<int>(serializable.MapFlightRange, 0, 100);
			MapFlightSpeed = new NumericValue<int>(serializable.MapFlightSpeed, 0, 100);
			AttackBonus = new NumericValue<int>(serializable.AttackBonus, 0, 100);
			DefenseBonus = new NumericValue<int>(serializable.DefenseBonus, 0, 100);
			ExperienceBonus = new NumericValue<int>(serializable.ExperienceBonus, 0, 100);
			ExplorationLootBonus = new NumericValue<int>(serializable.ExplorationLootBonus, 0, 100);
			HeatDefenseBonus = new NumericValue<int>(serializable.HeatDefenseBonus, 0, 100);
			KineticDefenseBonus = new NumericValue<int>(serializable.KineticDefenseBonus, 0, 100);
			EnergyDefenseBonus = new NumericValue<int>(serializable.EnergyDefenseBonus, 0, 100);
			MerchantPriceReduction = new NumericValue<int>(serializable.MerchantPriceReduction, 0, 100);
			CraftPriceReduction = new NumericValue<int>(serializable.CraftPriceReduction, 0, 100);
			CraftLevelReduction = new NumericValue<int>(serializable.CraftLevelReduction, 0, 100);
			ShieldStrengthBonus = new NumericValue<int>(serializable.ShieldStrengthBonus, 0, 100);
			ShieldRechargeBonus = new NumericValue<int>(serializable.ShieldRechargeBonus, 0, 100);
			IncreasedLevelLimit = new NumericValue<int>(serializable.IncreasedLevelLimit, 100, 1000);
			OnDataDeserialized(serializable, database);
		}

		public void Save(SkillSettingsSerializable serializable)
		{
			if (BeatAllEnemiesFactionList == null || BeatAllEnemiesFactionList.Length == 0)
			    serializable.BeatAllEnemiesFactionList = null;
			else
			    serializable.BeatAllEnemiesFactionList = BeatAllEnemiesFactionList.Select(wrapper => wrapper.Item.Value).ToArray();
			serializable.DisableExceedTheLimits = DisableExceedTheLimits;
			serializable.FuelTankCapacity = FuelTankCapacity.Value;
			serializable.MapFlightRange = MapFlightRange.Value;
			serializable.MapFlightSpeed = MapFlightSpeed.Value;
			serializable.AttackBonus = AttackBonus.Value;
			serializable.DefenseBonus = DefenseBonus.Value;
			serializable.ExperienceBonus = ExperienceBonus.Value;
			serializable.ExplorationLootBonus = ExplorationLootBonus.Value;
			serializable.HeatDefenseBonus = HeatDefenseBonus.Value;
			serializable.KineticDefenseBonus = KineticDefenseBonus.Value;
			serializable.EnergyDefenseBonus = EnergyDefenseBonus.Value;
			serializable.MerchantPriceReduction = MerchantPriceReduction.Value;
			serializable.CraftPriceReduction = CraftPriceReduction.Value;
			serializable.CraftLevelReduction = CraftLevelReduction.Value;
			serializable.ShieldStrengthBonus = ShieldStrengthBonus.Value;
			serializable.ShieldRechargeBonus = ShieldRechargeBonus.Value;
			serializable.IncreasedLevelLimit = IncreasedLevelLimit.Value;
			OnDataSerialized(ref serializable);
		}

		public Wrapper<Faction>[] BeatAllEnemiesFactionList;
		public bool DisableExceedTheLimits;
		public NumericValue<int> FuelTankCapacity = new NumericValue<int>(0, 0, 100);
		public NumericValue<int> MapFlightRange = new NumericValue<int>(0, 0, 100);
		public NumericValue<int> MapFlightSpeed = new NumericValue<int>(0, 0, 100);
		public NumericValue<int> AttackBonus = new NumericValue<int>(0, 0, 100);
		public NumericValue<int> DefenseBonus = new NumericValue<int>(0, 0, 100);
		public NumericValue<int> ExperienceBonus = new NumericValue<int>(0, 0, 100);
		public NumericValue<int> ExplorationLootBonus = new NumericValue<int>(0, 0, 100);
		public NumericValue<int> HeatDefenseBonus = new NumericValue<int>(0, 0, 100);
		public NumericValue<int> KineticDefenseBonus = new NumericValue<int>(0, 0, 100);
		public NumericValue<int> EnergyDefenseBonus = new NumericValue<int>(0, 0, 100);
		public NumericValue<int> MerchantPriceReduction = new NumericValue<int>(0, 0, 100);
		public NumericValue<int> CraftPriceReduction = new NumericValue<int>(0, 0, 100);
		public NumericValue<int> CraftLevelReduction = new NumericValue<int>(0, 0, 100);
		public NumericValue<int> ShieldStrengthBonus = new NumericValue<int>(0, 0, 100);
		public NumericValue<int> ShieldRechargeBonus = new NumericValue<int>(0, 0, 100);
		public NumericValue<int> IncreasedLevelLimit = new NumericValue<int>(0, 100, 1000);

		public static SkillSettings DefaultValue { get; private set; }
	}
}
