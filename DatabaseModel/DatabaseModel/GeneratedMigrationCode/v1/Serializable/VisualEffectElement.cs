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
	public class VisualEffectElementSerializable
	{
		public VisualEffectType Type;
		[DefaultValue("")]
		public string Image;
		public ColorMode ColorMode;
		[DefaultValue("")]
		public string Color;
		public float Size;
		public float GrowthRate;
		public float TurnRate;
		public float StartTime;
		public float Lifetime;
	}
}
