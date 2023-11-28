using System;

namespace DatabaseMigration.v1
{
    public partial class DatabaseUpgrader
    {
        partial void Migrate_1_2()
        {
            Console.WriteLine("Database migration: v1.1 -> v1.2");

            Content.CreateGalaxySettings().EnemyLevel = "MIN(3*distance/5 - 5, MaxEnemyShipsLevel)";

            const string commonFormula = "0.1*level";
            Content.CreateSkillSettings().AttackBonus = commonFormula;
            Content.SkillSettings.DefenseBonus = commonFormula;
            Content.SkillSettings.ShieldStrengthBonus = commonFormula;
            Content.SkillSettings.ShieldRechargeBonus = commonFormula;
            Content.SkillSettings.ExperienceBonus = commonFormula;
            Content.SkillSettings.ExplorationLootBonus = commonFormula;
            Content.SkillSettings.HeatResistance = commonFormula;
            Content.SkillSettings.KineticResistance = commonFormula;
            Content.SkillSettings.EnergyResistance = commonFormula;
            Content.SkillSettings.CraftingLevelReduction = "5*level";
            Content.SkillSettings.FuelTankCapacity = "BaseFuelCapacity + 50*level";
            Content.SkillSettings.FlightSpeed = "BaseFlightSpeed + 0.4*level";
            Content.SkillSettings.FlightRange = "BaseFlightRange + 0.09*level";
            Content.SkillSettings.MerchantPriceFactor = Content.SkillSettings.CraftingPriceFactor = "1 - 0.05*level";
        }
    }
}
