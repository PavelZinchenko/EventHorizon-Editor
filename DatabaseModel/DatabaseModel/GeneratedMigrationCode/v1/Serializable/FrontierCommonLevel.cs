//-------------------------------------------------------------------------------
//                                                                               
//    This code was automatically generated.                                     
//    Changes to this file may cause incorrect behavior and will be lost if      
//    the code is regenerated.                                                   
//                                                                               
//-------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using EditorDatabase.Model;
using DatabaseMigration.v1.Enums;

namespace DatabaseMigration.v1.Serializable
{
	[Serializable]
	public class FrontierCommonLevelSerializable : SerializableItem
	{
		public FrontierCommonLevelSerializable()
		{
			ItemType = ItemType.FrontierCommonLevel;
			FileName = "FrontierCommonLevel.json";
		}

		[DefaultValue("")]
		public string NormalEnemies;
		[DefaultValue("")]
		public string RandomEnemies;
		[DefaultValue("")]
		public string CrimsonEnemies;
		[DefaultValue("")]
		public string ExtraFlagships;
		public bool RandomShipOrder;
		[DefaultValue("1")]
		public string EnemyPowerMultiplier = "1";
		[DefaultValue("")]
		public string AllowRareShips;
		[DefaultValue("")]
		public string AllowHiddenShips;
		[DefaultValue("3 + wave")]
		public string MinActiveEnemies = "3 + wave";
		[DefaultValue("10")]
		public string MaxActiveEnemies = "10";
		[DefaultValue(20)]
		public int BoostedEnemyCount = 20;
		[DefaultValue(30)]
		public int ExtraEnemySpawnCooldown = 30;
		public DifficultyClass MinEnemyDifficultyClass;
		public DifficultyClass MaxEnemyDifficultyClass;
		[DefaultValue("")]
		public string MaxEnemySizeClass;
	}
}
