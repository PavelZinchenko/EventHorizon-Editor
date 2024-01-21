//-------------------------------------------------------------------------------
//                                                                               
//    This code was automatically generated.                                     
//    Changes to this file may cause incorrect behavior and will be lost if      
//    the code is regenerated.                                                   
//                                                                               
//-------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using EditorDatabase.Enums;
using EditorDatabase.Model;

namespace EditorDatabase.Serializable
{
	[Serializable]
	public class BehaviorTreeNodeSerializable
	{
		public BehaviorNodeType Type;
		public BehaviorNodeRequirementSerializable Requirement;
		public bool InverseResult;
		public BehaviorTreeNodeSerializable[] Nodes;
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
