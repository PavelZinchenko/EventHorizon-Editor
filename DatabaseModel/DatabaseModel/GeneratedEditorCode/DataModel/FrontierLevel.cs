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
	public partial class FrontierLevel
	{
		partial void OnDataDeserialized(FrontierLevelSerializable serializable, Database database);
		partial void OnDataSerialized(ref FrontierLevelSerializable serializable);

		public static FrontierLevel Create(FrontierLevelSerializable serializable, Database database)
		{
			if (serializable == null) return DefaultValue;
			return new FrontierLevel(serializable, database);
		}

		private FrontierLevel(FrontierLevelSerializable serializable, Database database)
		{
			try
			{
				Id = new ItemId<FrontierLevel>(serializable.Id, serializable.FileName);
				Type = serializable.Type;
				Faction = database.GetFactionId(serializable.Faction);
				Difficulty = new NumericValue<int>(serializable.Difficulty, 0, 2147483647);
				MinimumMapLevel = new NumericValue<int>(serializable.MinimumMapLevel, 0, 2147483647);
				Reward = database.GetLootId(serializable.Reward);
			}
			catch (DatabaseException e)
			{
				throw new DatabaseException(this.GetType() + ": deserialization failed. " + serializable.FileName + " (" + serializable.Id + ")", e);
			}
			OnDataDeserialized(serializable, database);
		}

		public void Save(FrontierLevelSerializable serializable)
		{
			serializable.Type = Type;
			serializable.Faction = Faction.Value;
			serializable.Difficulty = Difficulty.Value;
			serializable.MinimumMapLevel = MinimumMapLevel.Value;
			serializable.Reward = Reward.Value;
			OnDataSerialized(ref serializable);
		}

		public readonly ItemId<FrontierLevel> Id;

		public FrontierLevelType Type;
		public ItemId<Faction> Faction = ItemId<Faction>.Empty;
		public NumericValue<int> Difficulty = new NumericValue<int>(0, 0, 2147483647);
		public NumericValue<int> MinimumMapLevel = new NumericValue<int>(0, 0, 2147483647);
		public ItemId<LootModel> Reward = ItemId<LootModel>.Empty;

		public static FrontierLevel DefaultValue { get; private set; }
	}
}
