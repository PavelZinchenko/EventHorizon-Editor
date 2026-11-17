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
	public class FrontierLevelSerializable : SerializableItem
	{
		public FrontierLevelType Type;
		public int Faction;
		public int Difficulty;
		public int MinimumMapLevel;
		public int Reward;
	}
}
