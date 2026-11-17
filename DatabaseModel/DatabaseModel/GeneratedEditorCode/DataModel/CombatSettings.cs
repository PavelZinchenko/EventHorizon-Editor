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
	public partial class CombatSettings
	{
		partial void OnDataDeserialized(CombatSettingsSerializable serializable, Database database);
		partial void OnDataSerialized(ref CombatSettingsSerializable serializable);

		public static CombatSettings Create(CombatSettingsSerializable serializable, Database database)
		{
			if (serializable == null) return DefaultValue;
			return new CombatSettings(serializable, database);
		}

		private CombatSettings(CombatSettingsSerializable serializable, Database database)
		{
			EnemyAI = database.GetBehaviorTreeId(serializable.EnemyAI);
			EnemyInvaderAI = database.GetBehaviorTreeId(serializable.EnemyInvaderAI);
			ArenaCombatantAI = database.GetBehaviorTreeId(serializable.ArenaCombatantAI);
			AutopilotAI = database.GetBehaviorTreeId(serializable.AutopilotAI);
			AssistantAI = database.GetBehaviorTreeId(serializable.AssistantAI);
			CloneAI = database.GetBehaviorTreeId(serializable.CloneAI);
			DefensiveDroneAI = database.GetBehaviorTreeId(serializable.DefensiveDroneAI);
			OffensiveDroneAI = database.GetBehaviorTreeId(serializable.OffensiveDroneAI);
			StarbaseAI = database.GetBehaviorTreeId(serializable.StarbaseAI);
			StarbasePassiveAI = database.GetBehaviorTreeId(serializable.StarbasePassiveAI);
			DefensiveMercenaryAI = database.GetBehaviorTreeId(serializable.DefensiveMercenaryAI);
			OffensiveMercenaryAI = database.GetBehaviorTreeId(serializable.OffensiveMercenaryAI);
			ProvocatorAssistantAI = database.GetBehaviorTreeId(serializable.ProvocatorAssistantAI);
			DefaultCombatRules = database.GetCombatRulesId(serializable.DefaultCombatRules);
			UseNewCombatSimulator = serializable.UseNewCombatSimulator;
			OnDataDeserialized(serializable, database);
		}

		public void Save(CombatSettingsSerializable serializable)
		{
			serializable.EnemyAI = EnemyAI.Value;
			serializable.EnemyInvaderAI = EnemyInvaderAI.Value;
			serializable.ArenaCombatantAI = ArenaCombatantAI.Value;
			serializable.AutopilotAI = AutopilotAI.Value;
			serializable.AssistantAI = AssistantAI.Value;
			serializable.CloneAI = CloneAI.Value;
			serializable.DefensiveDroneAI = DefensiveDroneAI.Value;
			serializable.OffensiveDroneAI = OffensiveDroneAI.Value;
			serializable.StarbaseAI = StarbaseAI.Value;
			serializable.StarbasePassiveAI = StarbasePassiveAI.Value;
			serializable.DefensiveMercenaryAI = DefensiveMercenaryAI.Value;
			serializable.OffensiveMercenaryAI = OffensiveMercenaryAI.Value;
			serializable.ProvocatorAssistantAI = ProvocatorAssistantAI.Value;
			serializable.DefaultCombatRules = DefaultCombatRules.Value;
			serializable.UseNewCombatSimulator = UseNewCombatSimulator;
			OnDataSerialized(ref serializable);
		}

		public ItemId<BehaviorTreeModel> EnemyAI = ItemId<BehaviorTreeModel>.Empty;
		public ItemId<BehaviorTreeModel> EnemyInvaderAI = ItemId<BehaviorTreeModel>.Empty;
		public ItemId<BehaviorTreeModel> ArenaCombatantAI = ItemId<BehaviorTreeModel>.Empty;
		public ItemId<BehaviorTreeModel> AutopilotAI = ItemId<BehaviorTreeModel>.Empty;
		public ItemId<BehaviorTreeModel> AssistantAI = ItemId<BehaviorTreeModel>.Empty;
		public ItemId<BehaviorTreeModel> CloneAI = ItemId<BehaviorTreeModel>.Empty;
		public ItemId<BehaviorTreeModel> DefensiveDroneAI = ItemId<BehaviorTreeModel>.Empty;
		public ItemId<BehaviorTreeModel> OffensiveDroneAI = ItemId<BehaviorTreeModel>.Empty;
		public ItemId<BehaviorTreeModel> StarbaseAI = ItemId<BehaviorTreeModel>.Empty;
		public ItemId<BehaviorTreeModel> StarbasePassiveAI = ItemId<BehaviorTreeModel>.Empty;
		public ItemId<BehaviorTreeModel> DefensiveMercenaryAI = ItemId<BehaviorTreeModel>.Empty;
		public ItemId<BehaviorTreeModel> OffensiveMercenaryAI = ItemId<BehaviorTreeModel>.Empty;
		public ItemId<BehaviorTreeModel> ProvocatorAssistantAI = ItemId<BehaviorTreeModel>.Empty;
		public ItemId<CombatRules> DefaultCombatRules = ItemId<CombatRules>.Empty;
		public bool UseNewCombatSimulator;

		public static CombatSettings DefaultValue { get; private set; }
	}
}
