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
	public class BulletControllerSerializable
	{
		public BulletControllerType Type;
		[DefaultValue(0.1f)]
		public float StartingVelocityModifier = 0.1f;
		public bool IgnoreRotation;
		public bool SmartAim;
	}
}
