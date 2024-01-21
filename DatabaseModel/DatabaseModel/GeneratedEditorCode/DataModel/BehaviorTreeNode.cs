//-------------------------------------------------------------------------------
//                                                                               
//    This code was automatically generated.                                     
//    Changes to this file may cause incorrect behavior and will be lost if      
//    the code is regenerated.                                                   
//                                                                               
//-------------------------------------------------------------------------------

using System.Linq;
using System.Collections.Generic;
using EditorDatabase.Enums;
using EditorDatabase.Serializable;
using EditorDatabase.Model;

namespace EditorDatabase.DataModel
{

	public interface IBehaviorTreeNodeContent
	{
		void Load(BehaviorTreeNodeSerializable serializable, Database database);
		void Save(ref BehaviorTreeNodeSerializable serializable);
	}

	public partial class BehaviorTreeNode : IDataAdapter
	{
		partial void OnDataDeserialized(BehaviorTreeNodeSerializable serializable, Database database);
		partial void OnDataSerialized(ref BehaviorTreeNodeSerializable serializable);

		private static IBehaviorTreeNodeContent CreateContent(BehaviorNodeType type)
		{
			switch (type)
			{
				case BehaviorNodeType.Undefined:
					return new BehaviorTreeNodeEmptyContent();
				case BehaviorNodeType.SubTree:
					return new BehaviorTreeNode_SubTree();
				case BehaviorNodeType.Selector:
					return new BehaviorTreeNode_Selector();
				case BehaviorNodeType.Sequence:
					return new BehaviorTreeNode_Sequence();
				case BehaviorNodeType.Parallel:
					return new BehaviorTreeNode_Parallel();
				case BehaviorNodeType.RandomSelector:
					return new BehaviorTreeNode_RandomSelector();
				case BehaviorNodeType.Invertor:
					return new BehaviorTreeNode_Invertor();
				case BehaviorNodeType.HaveEnoughEnergy:
					return new BehaviorTreeNode_HaveEnoughEnergy();
				case BehaviorNodeType.HaveEnoughHp:
					return new BehaviorTreeNode_HaveEnoughHp();
				case BehaviorNodeType.IsPlayerControled:
					return new BehaviorTreeNodeEmptyContent();
				case BehaviorNodeType.FindEnemy:
					return new BehaviorTreeNode_FindEnemy();
				case BehaviorNodeType.MoveToAttackRange:
					return new BehaviorTreeNode_MoveToAttackRange();
				case BehaviorNodeType.Attack:
					return new BehaviorTreeNodeEmptyContent();
				case BehaviorNodeType.SelectWeapon:
					return new BehaviorTreeNode_SelectWeapon();
				case BehaviorNodeType.SpawnDrones:
					return new BehaviorTreeNodeEmptyContent();
				case BehaviorNodeType.Ram:
					return new BehaviorTreeNode_Ram();
				case BehaviorNodeType.DetonateShip:
					return new BehaviorTreeNode_DetonateShip();
				case BehaviorNodeType.Vanish:
					return new BehaviorTreeNodeEmptyContent();
				case BehaviorNodeType.MaintainAttackRange:
					return new BehaviorTreeNode_MaintainAttackRange();
				case BehaviorNodeType.Wait:
					return new BehaviorTreeNode_Wait();
				case BehaviorNodeType.LookAtTarget:
					return new BehaviorTreeNodeEmptyContent();
				case BehaviorNodeType.LookForSecondaryTargets:
					return new BehaviorTreeNode_LookForSecondaryTargets();
				case BehaviorNodeType.LookForThreats:
					return new BehaviorTreeNode_LookForThreats();
				case BehaviorNodeType.AttackSecondaryTargets:
					return new BehaviorTreeNodeEmptyContent();
				case BehaviorNodeType.ActivateDevice:
					return new BehaviorTreeNode_ActivateDevice();
				case BehaviorNodeType.RechargeEnergy:
					return new BehaviorTreeNode_RechargeEnergy();
				case BehaviorNodeType.SustainAim:
					return new BehaviorTreeNodeEmptyContent();
				case BehaviorNodeType.ChargeWeapons:
					return new BehaviorTreeNodeEmptyContent();
				case BehaviorNodeType.IsWithinAttackRange:
					return new BehaviorTreeNode_IsWithinAttackRange();
				case BehaviorNodeType.MotherShipRetreated:
					return new BehaviorTreeNodeEmptyContent();
				case BehaviorNodeType.MotherShipDestroyed:
					return new BehaviorTreeNodeEmptyContent();
				case BehaviorNodeType.FlyAroundMothership:
					return new BehaviorTreeNode_FlyAroundMothership();
				case BehaviorNodeType.GoBerserk:
					return new BehaviorTreeNodeEmptyContent();
				case BehaviorNodeType.TargetMothership:
					return new BehaviorTreeNodeEmptyContent();
				case BehaviorNodeType.MothershipHp:
					return new BehaviorTreeNode_MothershipHp();
				case BehaviorNodeType.ShowMessage:
					return new BehaviorTreeNode_ShowMessage();
				default:
					throw new DatabaseException("BehaviorTreeNode: Invalid content type - " + type);
			}
		}

		public static BehaviorTreeNode Create(BehaviorTreeNodeSerializable serializable, Database database)
		{
			if (serializable == null) return DefaultValue;
			return new BehaviorTreeNode(serializable, database);
		}

		public BehaviorTreeNode()
		{
			_content = new BehaviorTreeNodeEmptyContent();
		}

		private BehaviorTreeNode(BehaviorTreeNodeSerializable serializable, Database database)
		{
			Type = serializable.Type;
			Requirement.Value = DataModel.BehaviorNodeRequirement.Create(serializable.Requirement, database);
			InverseResult = serializable.InverseResult;
			_content = CreateContent(serializable.Type);
			_content.Load(serializable, database);

			OnDataDeserialized(serializable, database);
		}

		public BehaviorTreeNodeSerializable Serialize()
		{
			var serializable = new BehaviorTreeNodeSerializable();
			serializable.Nodes = null;
			serializable.Node = null;
			serializable.ItemId = 0;
			serializable.WeaponType = 0;
			serializable.MinValue = 0f;
			serializable.MaxValue = 0f;
			serializable.Cooldown = 0f;
			serializable.Cooldown = 0f;
			serializable.MinValue = 0f;
			serializable.MaxValue = 0f;
			serializable.InRange = false;
			serializable.InRange = false;
			serializable.NoDrones = false;
			serializable.UseSystems = false;
			serializable.MinValue = 0f;
			serializable.MaxValue = 0f;
			serializable.MaxValue = 0f;
			serializable.MinValue = 0f;
			serializable.MaxValue = 0f;
			serializable.DeviceClass = 0;
			serializable.Text = string.Empty;
			serializable.Cooldown = 0f;
			_content.Save(ref serializable);
			serializable.Type = Type;
			serializable.Requirement = Requirement.Value?.Serialize();
			serializable.InverseResult = InverseResult;
			OnDataSerialized(ref serializable);
			return serializable;
		}

		public event System.Action LayoutChangedEvent;
		public event System.Action DataChangedEvent;

		public IEnumerable<IProperty> Properties
		{
			get
			{
				var type = GetType();

				yield return new Property(this, type.GetField("Type"), OnTypeChanged);
				yield return new Property(this, type.GetField("Requirement"), DataChangedEvent);
				yield return new Property(this, type.GetField("InverseResult"), DataChangedEvent);

				foreach (var item in _content.GetType().GetFields().Where(f => f.IsPublic && !f.IsStatic))
					yield return new Property(_content, item, DataChangedEvent);
			}
		}

		private void OnTypeChanged()
		{
			_content = CreateContent(Type);
			DataChangedEvent?.Invoke();
			LayoutChangedEvent?.Invoke();
		}

		private IBehaviorTreeNodeContent _content;
		public BehaviorNodeType Type;
		public ObjectWrapper<BehaviorNodeRequirement> Requirement = new(DataModel.BehaviorNodeRequirement.DefaultValue);
		public bool InverseResult;

		public static BehaviorTreeNode DefaultValue { get; private set; }
	}

	public class BehaviorTreeNodeEmptyContent : IBehaviorTreeNodeContent
	{
		public void Load(BehaviorTreeNodeSerializable serializable, Database database) {}
		public void Save(ref BehaviorTreeNodeSerializable serializable) {}
	}

	public partial class BehaviorTreeNode_SubTree : IBehaviorTreeNodeContent
	{
		partial void OnDataDeserialized(BehaviorTreeNodeSerializable serializable, Database database);
		partial void OnDataSerialized(ref BehaviorTreeNodeSerializable serializable);

		public void Load(BehaviorTreeNodeSerializable serializable, Database database)
		{
			BehaviourTree = database.GetBehaviorTreeId(serializable.ItemId);

			OnDataDeserialized(serializable, database);
		}

		public void Save(ref BehaviorTreeNodeSerializable serializable)
		{
			serializable.ItemId = BehaviourTree.Value;
			OnDataSerialized(ref serializable);
		}

		public ItemId<BehaviorTreeModel> BehaviourTree = ItemId<BehaviorTreeModel>.Empty;
	}

	public partial class BehaviorTreeNode_Selector : IBehaviorTreeNodeContent
	{
		partial void OnDataDeserialized(BehaviorTreeNodeSerializable serializable, Database database);
		partial void OnDataSerialized(ref BehaviorTreeNodeSerializable serializable);

		public void Load(BehaviorTreeNodeSerializable serializable, Database database)
		{
			Nodes = serializable.Nodes?.Select(item => BehaviorTreeNode.Create(item, database)).ToArray();

			OnDataDeserialized(serializable, database);
		}

		public void Save(ref BehaviorTreeNodeSerializable serializable)
		{
			if (Nodes == null || Nodes.Length == 0)
			    serializable.Nodes = null;
			else
			    serializable.Nodes = Nodes.Select(item => item.Serialize()).ToArray();
			OnDataSerialized(ref serializable);
		}

		public BehaviorTreeNode[] Nodes;
	}

	public partial class BehaviorTreeNode_Sequence : IBehaviorTreeNodeContent
	{
		partial void OnDataDeserialized(BehaviorTreeNodeSerializable serializable, Database database);
		partial void OnDataSerialized(ref BehaviorTreeNodeSerializable serializable);

		public void Load(BehaviorTreeNodeSerializable serializable, Database database)
		{
			Nodes = serializable.Nodes?.Select(item => BehaviorTreeNode.Create(item, database)).ToArray();

			OnDataDeserialized(serializable, database);
		}

		public void Save(ref BehaviorTreeNodeSerializable serializable)
		{
			if (Nodes == null || Nodes.Length == 0)
			    serializable.Nodes = null;
			else
			    serializable.Nodes = Nodes.Select(item => item.Serialize()).ToArray();
			OnDataSerialized(ref serializable);
		}

		public BehaviorTreeNode[] Nodes;
	}

	public partial class BehaviorTreeNode_Parallel : IBehaviorTreeNodeContent
	{
		partial void OnDataDeserialized(BehaviorTreeNodeSerializable serializable, Database database);
		partial void OnDataSerialized(ref BehaviorTreeNodeSerializable serializable);

		public void Load(BehaviorTreeNodeSerializable serializable, Database database)
		{
			Nodes = serializable.Nodes?.Select(item => BehaviorTreeNode.Create(item, database)).ToArray();

			OnDataDeserialized(serializable, database);
		}

		public void Save(ref BehaviorTreeNodeSerializable serializable)
		{
			if (Nodes == null || Nodes.Length == 0)
			    serializable.Nodes = null;
			else
			    serializable.Nodes = Nodes.Select(item => item.Serialize()).ToArray();
			OnDataSerialized(ref serializable);
		}

		public BehaviorTreeNode[] Nodes;
	}

	public partial class BehaviorTreeNode_RandomSelector : IBehaviorTreeNodeContent
	{
		partial void OnDataDeserialized(BehaviorTreeNodeSerializable serializable, Database database);
		partial void OnDataSerialized(ref BehaviorTreeNodeSerializable serializable);

		public void Load(BehaviorTreeNodeSerializable serializable, Database database)
		{
			Nodes = serializable.Nodes?.Select(item => BehaviorTreeNode.Create(item, database)).ToArray();
			Cooldown = new NumericValue<float>(serializable.Cooldown, 0f, 3.402823E+38f);

			OnDataDeserialized(serializable, database);
		}

		public void Save(ref BehaviorTreeNodeSerializable serializable)
		{
			if (Nodes == null || Nodes.Length == 0)
			    serializable.Nodes = null;
			else
			    serializable.Nodes = Nodes.Select(item => item.Serialize()).ToArray();
			serializable.Cooldown = Cooldown.Value;
			OnDataSerialized(ref serializable);
		}

		public BehaviorTreeNode[] Nodes;
		public NumericValue<float> Cooldown = new NumericValue<float>(0, 0f, 3.402823E+38f);
	}

	public partial class BehaviorTreeNode_Invertor : IBehaviorTreeNodeContent
	{
		partial void OnDataDeserialized(BehaviorTreeNodeSerializable serializable, Database database);
		partial void OnDataSerialized(ref BehaviorTreeNodeSerializable serializable);

		public void Load(BehaviorTreeNodeSerializable serializable, Database database)
		{
			Node.Value = DataModel.BehaviorTreeNode.Create(serializable.Node, database);

			OnDataDeserialized(serializable, database);
		}

		public void Save(ref BehaviorTreeNodeSerializable serializable)
		{
			serializable.Node = Node.Value?.Serialize();
			OnDataSerialized(ref serializable);
		}

		public ObjectWrapper<BehaviorTreeNode> Node = new(DataModel.BehaviorTreeNode.DefaultValue);
	}

	public partial class BehaviorTreeNode_HaveEnoughEnergy : IBehaviorTreeNodeContent
	{
		partial void OnDataDeserialized(BehaviorTreeNodeSerializable serializable, Database database);
		partial void OnDataSerialized(ref BehaviorTreeNodeSerializable serializable);

		public void Load(BehaviorTreeNodeSerializable serializable, Database database)
		{
			FailIfLess = new NumericValue<float>(serializable.MinValue, 0f, 1f);

			OnDataDeserialized(serializable, database);
		}

		public void Save(ref BehaviorTreeNodeSerializable serializable)
		{
			serializable.MinValue = FailIfLess.Value;
			OnDataSerialized(ref serializable);
		}

		public NumericValue<float> FailIfLess = new NumericValue<float>(0, 0f, 1f);
	}

	public partial class BehaviorTreeNode_HaveEnoughHp : IBehaviorTreeNodeContent
	{
		partial void OnDataDeserialized(BehaviorTreeNodeSerializable serializable, Database database);
		partial void OnDataSerialized(ref BehaviorTreeNodeSerializable serializable);

		public void Load(BehaviorTreeNodeSerializable serializable, Database database)
		{
			FailIfLess = new NumericValue<float>(serializable.MinValue, 0f, 1f);

			OnDataDeserialized(serializable, database);
		}

		public void Save(ref BehaviorTreeNodeSerializable serializable)
		{
			serializable.MinValue = FailIfLess.Value;
			OnDataSerialized(ref serializable);
		}

		public NumericValue<float> FailIfLess = new NumericValue<float>(0, 0f, 1f);
	}

	public partial class BehaviorTreeNode_FindEnemy : IBehaviorTreeNodeContent
	{
		partial void OnDataDeserialized(BehaviorTreeNodeSerializable serializable, Database database);
		partial void OnDataSerialized(ref BehaviorTreeNodeSerializable serializable);

		public void Load(BehaviorTreeNodeSerializable serializable, Database database)
		{
			MinCooldown = new NumericValue<float>(serializable.MinValue, 0.5f, 3.402823E+38f);
			MaxCooldown = new NumericValue<float>(serializable.MaxValue, 0f, 3.402823E+38f);
			InAttackRange = serializable.InRange;
			IgnoreDrones = serializable.NoDrones;

			OnDataDeserialized(serializable, database);
		}

		public void Save(ref BehaviorTreeNodeSerializable serializable)
		{
			serializable.MinValue = MinCooldown.Value;
			serializable.MaxValue = MaxCooldown.Value;
			serializable.InRange = InAttackRange;
			serializable.NoDrones = IgnoreDrones;
			OnDataSerialized(ref serializable);
		}

		public NumericValue<float> MinCooldown = new NumericValue<float>(0, 0.5f, 3.402823E+38f);
		public NumericValue<float> MaxCooldown = new NumericValue<float>(0, 0f, 3.402823E+38f);
		public bool InAttackRange;
		public bool IgnoreDrones;
	}

	public partial class BehaviorTreeNode_MoveToAttackRange : IBehaviorTreeNodeContent
	{
		partial void OnDataDeserialized(BehaviorTreeNodeSerializable serializable, Database database);
		partial void OnDataSerialized(ref BehaviorTreeNodeSerializable serializable);

		public void Load(BehaviorTreeNodeSerializable serializable, Database database)
		{
			MinMaxLerp = new NumericValue<float>(serializable.MinValue, 0f, 1f);
			Multiplier = new NumericValue<float>(serializable.MaxValue, 0f, 1f);

			OnDataDeserialized(serializable, database);
		}

		public void Save(ref BehaviorTreeNodeSerializable serializable)
		{
			serializable.MinValue = MinMaxLerp.Value;
			serializable.MaxValue = Multiplier.Value;
			OnDataSerialized(ref serializable);
		}

		public NumericValue<float> MinMaxLerp = new NumericValue<float>(0, 0f, 1f);
		public NumericValue<float> Multiplier = new NumericValue<float>(0, 0f, 1f);
	}

	public partial class BehaviorTreeNode_SelectWeapon : IBehaviorTreeNodeContent
	{
		partial void OnDataDeserialized(BehaviorTreeNodeSerializable serializable, Database database);
		partial void OnDataSerialized(ref BehaviorTreeNodeSerializable serializable);

		public void Load(BehaviorTreeNodeSerializable serializable, Database database)
		{
			WeaponType = serializable.WeaponType;

			OnDataDeserialized(serializable, database);
		}

		public void Save(ref BehaviorTreeNodeSerializable serializable)
		{
			serializable.WeaponType = WeaponType;
			OnDataSerialized(ref serializable);
		}

		public AiWeaponCategory WeaponType;
	}

	public partial class BehaviorTreeNode_Ram : IBehaviorTreeNodeContent
	{
		partial void OnDataDeserialized(BehaviorTreeNodeSerializable serializable, Database database);
		partial void OnDataSerialized(ref BehaviorTreeNodeSerializable serializable);

		public void Load(BehaviorTreeNodeSerializable serializable, Database database)
		{
			UseShipSystems = serializable.UseSystems;

			OnDataDeserialized(serializable, database);
		}

		public void Save(ref BehaviorTreeNodeSerializable serializable)
		{
			serializable.UseSystems = UseShipSystems;
			OnDataSerialized(ref serializable);
		}

		public bool UseShipSystems;
	}

	public partial class BehaviorTreeNode_DetonateShip : IBehaviorTreeNodeContent
	{
		partial void OnDataDeserialized(BehaviorTreeNodeSerializable serializable, Database database);
		partial void OnDataSerialized(ref BehaviorTreeNodeSerializable serializable);

		public void Load(BehaviorTreeNodeSerializable serializable, Database database)
		{
			InAttackRange = serializable.InRange;

			OnDataDeserialized(serializable, database);
		}

		public void Save(ref BehaviorTreeNodeSerializable serializable)
		{
			serializable.InRange = InAttackRange;
			OnDataSerialized(ref serializable);
		}

		public bool InAttackRange;
	}

	public partial class BehaviorTreeNode_MaintainAttackRange : IBehaviorTreeNodeContent
	{
		partial void OnDataDeserialized(BehaviorTreeNodeSerializable serializable, Database database);
		partial void OnDataSerialized(ref BehaviorTreeNodeSerializable serializable);

		public void Load(BehaviorTreeNodeSerializable serializable, Database database)
		{
			MinMaxLerp = new NumericValue<float>(serializable.MinValue, 0f, 1f);
			Tolerance = new NumericValue<float>(serializable.MaxValue, 0f, 1f);

			OnDataDeserialized(serializable, database);
		}

		public void Save(ref BehaviorTreeNodeSerializable serializable)
		{
			serializable.MinValue = MinMaxLerp.Value;
			serializable.MaxValue = Tolerance.Value;
			OnDataSerialized(ref serializable);
		}

		public NumericValue<float> MinMaxLerp = new NumericValue<float>(0, 0f, 1f);
		public NumericValue<float> Tolerance = new NumericValue<float>(0, 0f, 1f);
	}

	public partial class BehaviorTreeNode_Wait : IBehaviorTreeNodeContent
	{
		partial void OnDataDeserialized(BehaviorTreeNodeSerializable serializable, Database database);
		partial void OnDataSerialized(ref BehaviorTreeNodeSerializable serializable);

		public void Load(BehaviorTreeNodeSerializable serializable, Database database)
		{
			Cooldown = new NumericValue<float>(serializable.Cooldown, 0f, 3.402823E+38f);
			ResetIfInterrupted = serializable.InRange;

			OnDataDeserialized(serializable, database);
		}

		public void Save(ref BehaviorTreeNodeSerializable serializable)
		{
			serializable.Cooldown = Cooldown.Value;
			serializable.InRange = ResetIfInterrupted;
			OnDataSerialized(ref serializable);
		}

		public NumericValue<float> Cooldown = new NumericValue<float>(0, 0f, 3.402823E+38f);
		public bool ResetIfInterrupted;
	}

	public partial class BehaviorTreeNode_LookForSecondaryTargets : IBehaviorTreeNodeContent
	{
		partial void OnDataDeserialized(BehaviorTreeNodeSerializable serializable, Database database);
		partial void OnDataSerialized(ref BehaviorTreeNodeSerializable serializable);

		public void Load(BehaviorTreeNodeSerializable serializable, Database database)
		{
			Cooldown = new NumericValue<float>(serializable.Cooldown, 0.1f, 3.402823E+38f);

			OnDataDeserialized(serializable, database);
		}

		public void Save(ref BehaviorTreeNodeSerializable serializable)
		{
			serializable.Cooldown = Cooldown.Value;
			OnDataSerialized(ref serializable);
		}

		public NumericValue<float> Cooldown = new NumericValue<float>(0, 0.1f, 3.402823E+38f);
	}

	public partial class BehaviorTreeNode_LookForThreats : IBehaviorTreeNodeContent
	{
		partial void OnDataDeserialized(BehaviorTreeNodeSerializable serializable, Database database);
		partial void OnDataSerialized(ref BehaviorTreeNodeSerializable serializable);

		public void Load(BehaviorTreeNodeSerializable serializable, Database database)
		{
			Cooldown = new NumericValue<float>(serializable.Cooldown, 0.1f, 3.402823E+38f);

			OnDataDeserialized(serializable, database);
		}

		public void Save(ref BehaviorTreeNodeSerializable serializable)
		{
			serializable.Cooldown = Cooldown.Value;
			OnDataSerialized(ref serializable);
		}

		public NumericValue<float> Cooldown = new NumericValue<float>(0, 0.1f, 3.402823E+38f);
	}

	public partial class BehaviorTreeNode_ActivateDevice : IBehaviorTreeNodeContent
	{
		partial void OnDataDeserialized(BehaviorTreeNodeSerializable serializable, Database database);
		partial void OnDataSerialized(ref BehaviorTreeNodeSerializable serializable);

		public void Load(BehaviorTreeNodeSerializable serializable, Database database)
		{
			DeviceClass = serializable.DeviceClass;

			OnDataDeserialized(serializable, database);
		}

		public void Save(ref BehaviorTreeNodeSerializable serializable)
		{
			serializable.DeviceClass = DeviceClass;
			OnDataSerialized(ref serializable);
		}

		public DeviceClass DeviceClass;
	}

	public partial class BehaviorTreeNode_RechargeEnergy : IBehaviorTreeNodeContent
	{
		partial void OnDataDeserialized(BehaviorTreeNodeSerializable serializable, Database database);
		partial void OnDataSerialized(ref BehaviorTreeNodeSerializable serializable);

		public void Load(BehaviorTreeNodeSerializable serializable, Database database)
		{
			FailIfLess = new NumericValue<float>(serializable.MinValue, 0f, 1f);
			RestoreUntil = new NumericValue<float>(serializable.MaxValue, 0f, 1f);

			OnDataDeserialized(serializable, database);
		}

		public void Save(ref BehaviorTreeNodeSerializable serializable)
		{
			serializable.MinValue = FailIfLess.Value;
			serializable.MaxValue = RestoreUntil.Value;
			OnDataSerialized(ref serializable);
		}

		public NumericValue<float> FailIfLess = new NumericValue<float>(0, 0f, 1f);
		public NumericValue<float> RestoreUntil = new NumericValue<float>(0, 0f, 1f);
	}

	public partial class BehaviorTreeNode_IsWithinAttackRange : IBehaviorTreeNodeContent
	{
		partial void OnDataDeserialized(BehaviorTreeNodeSerializable serializable, Database database);
		partial void OnDataSerialized(ref BehaviorTreeNodeSerializable serializable);

		public void Load(BehaviorTreeNodeSerializable serializable, Database database)
		{
			MinMaxLerp = new NumericValue<float>(serializable.MinValue, 0f, 1f);

			OnDataDeserialized(serializable, database);
		}

		public void Save(ref BehaviorTreeNodeSerializable serializable)
		{
			serializable.MinValue = MinMaxLerp.Value;
			OnDataSerialized(ref serializable);
		}

		public NumericValue<float> MinMaxLerp = new NumericValue<float>(0, 0f, 1f);
	}

	public partial class BehaviorTreeNode_FlyAroundMothership : IBehaviorTreeNodeContent
	{
		partial void OnDataDeserialized(BehaviorTreeNodeSerializable serializable, Database database);
		partial void OnDataSerialized(ref BehaviorTreeNodeSerializable serializable);

		public void Load(BehaviorTreeNodeSerializable serializable, Database database)
		{
			MinDistance = new NumericValue<float>(serializable.MinValue, 0f, 10f);
			MaxDistance = new NumericValue<float>(serializable.MaxValue, 0f, 10f);

			OnDataDeserialized(serializable, database);
		}

		public void Save(ref BehaviorTreeNodeSerializable serializable)
		{
			serializable.MinValue = MinDistance.Value;
			serializable.MaxValue = MaxDistance.Value;
			OnDataSerialized(ref serializable);
		}

		public NumericValue<float> MinDistance = new NumericValue<float>(0, 0f, 10f);
		public NumericValue<float> MaxDistance = new NumericValue<float>(0, 0f, 10f);
	}

	public partial class BehaviorTreeNode_MothershipHp : IBehaviorTreeNodeContent
	{
		partial void OnDataDeserialized(BehaviorTreeNodeSerializable serializable, Database database);
		partial void OnDataSerialized(ref BehaviorTreeNodeSerializable serializable);

		public void Load(BehaviorTreeNodeSerializable serializable, Database database)
		{
			FailIfLess = new NumericValue<float>(serializable.MinValue, 0f, 1f);

			OnDataDeserialized(serializable, database);
		}

		public void Save(ref BehaviorTreeNodeSerializable serializable)
		{
			serializable.MinValue = FailIfLess.Value;
			OnDataSerialized(ref serializable);
		}

		public NumericValue<float> FailIfLess = new NumericValue<float>(0, 0f, 1f);
	}

	public partial class BehaviorTreeNode_ShowMessage : IBehaviorTreeNodeContent
	{
		partial void OnDataDeserialized(BehaviorTreeNodeSerializable serializable, Database database);
		partial void OnDataSerialized(ref BehaviorTreeNodeSerializable serializable);

		public void Load(BehaviorTreeNodeSerializable serializable, Database database)
		{
			Text = serializable.Text;
			Cooldown = new NumericValue<float>(serializable.Cooldown, 0.2f, 3.402823E+38f);

			OnDataDeserialized(serializable, database);
		}

		public void Save(ref BehaviorTreeNodeSerializable serializable)
		{
			serializable.Text = Text;
			serializable.Cooldown = Cooldown.Value;
			OnDataSerialized(ref serializable);
		}

		public string Text;
		public NumericValue<float> Cooldown = new NumericValue<float>(0, 0.2f, 3.402823E+38f);
	}

}

