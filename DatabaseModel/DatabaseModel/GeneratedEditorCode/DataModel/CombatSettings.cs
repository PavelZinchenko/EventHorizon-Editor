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
			AutopilotAI = database.GetBehaviorTreeId(serializable.AutopilotAI);
			CloneAI = database.GetBehaviorTreeId(serializable.CloneAI);
			DefensiveDroneAI = database.GetBehaviorTreeId(serializable.DefensiveDroneAI);
			ImprovedDefensiveDroneAI = database.GetBehaviorTreeId(serializable.ImprovedDefensiveDroneAI);
			OffensiveDroneAI = database.GetBehaviorTreeId(serializable.OffensiveDroneAI);
			ImprovedOffensiveDroneAI = database.GetBehaviorTreeId(serializable.ImprovedOffensiveDroneAI);
			OnDataDeserialized(serializable, database);
		}

		public void Save(CombatSettingsSerializable serializable)
		{
			serializable.EnemyAI = EnemyAI.Value;
			serializable.AutopilotAI = AutopilotAI.Value;
			serializable.CloneAI = CloneAI.Value;
			serializable.DefensiveDroneAI = DefensiveDroneAI.Value;
			serializable.ImprovedDefensiveDroneAI = ImprovedDefensiveDroneAI.Value;
			serializable.OffensiveDroneAI = OffensiveDroneAI.Value;
			serializable.ImprovedOffensiveDroneAI = ImprovedOffensiveDroneAI.Value;
			OnDataSerialized(ref serializable);
		}

		public ItemId<BehaviorTreeModel> EnemyAI = ItemId<BehaviorTreeModel>.Empty;
		public ItemId<BehaviorTreeModel> AutopilotAI = ItemId<BehaviorTreeModel>.Empty;
		public ItemId<BehaviorTreeModel> CloneAI = ItemId<BehaviorTreeModel>.Empty;
		public ItemId<BehaviorTreeModel> DefensiveDroneAI = ItemId<BehaviorTreeModel>.Empty;
		public ItemId<BehaviorTreeModel> ImprovedDefensiveDroneAI = ItemId<BehaviorTreeModel>.Empty;
		public ItemId<BehaviorTreeModel> OffensiveDroneAI = ItemId<BehaviorTreeModel>.Empty;
		public ItemId<BehaviorTreeModel> ImprovedOffensiveDroneAI = ItemId<BehaviorTreeModel>.Empty;

		public static CombatSettings DefaultValue { get; private set; }
	}
}
