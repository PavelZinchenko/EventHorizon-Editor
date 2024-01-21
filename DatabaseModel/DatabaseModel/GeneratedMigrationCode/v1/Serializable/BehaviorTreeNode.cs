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
	public class BehaviorTreeNodeSerializable
	{
		public BehaviorNodeType Type;
		public BehaviorNodeRequirementSerializable Requirement;
		public bool InverseResult;
		public BehaviorTreeNodeSerializable[] Nodes;
		public BehaviorTreeNodeSerializable Node;
		public int ItemId;
		public AiWeaponCategory WeaponType;
		public float MinValue;
		public float MaxValue;
		public float Cooldown;
		public bool InRange;
		public bool NoDrones;
		public bool UseSystems;
		public DeviceClass DeviceClass;
		[DefaultValue("")]
		public string Text;
	}
}
