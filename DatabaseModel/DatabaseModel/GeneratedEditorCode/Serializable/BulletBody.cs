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
	public class BulletBodySerializable
	{
		public BulletType Type;
		public float Size;
		public float Length;
		public float Velocity;
		public float Range;
		public float Lifetime;
		public float Weight;
		public int HitPoints;
		[DefaultValue("")]
		public string Color;
		public int BulletPrefab;
		public float EnergyCost;
		public bool CanBeDisarmed;
		public bool FriendlyFire;
	}
}
