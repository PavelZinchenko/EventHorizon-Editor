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
	public partial class LootItem
	{
		partial void OnDataDeserialized(LootItemSerializable serializable, Database database);
		partial void OnDataSerialized(ref LootItemSerializable serializable);

		public static LootItem Create(LootItemSerializable serializable, Database database)
		{
			if (serializable == null) return DefaultValue;
			return new LootItem(serializable, database);
		}

		public LootItem() {}

		private LootItem(LootItemSerializable serializable, Database database)
		{
			Weight = new NumericValue<float>(serializable.Weight, -3.402823E+38f, 3.402823E+38f);
			Requirement.Value = DataModel.Requirement.Create(serializable.Requirement, database);
			Loot.Value = DataModel.LootContent.Create(serializable.Loot, database);
			OnDataDeserialized(serializable, database);
		}

		public LootItemSerializable Serialize()
		{
			var serializable = new LootItemSerializable();
			serializable.Weight = Weight.Value;
			serializable.Requirement = Requirement.Value?.Serialize();
			serializable.Loot = Loot.Value?.Serialize();
			OnDataSerialized(ref serializable);
			return serializable;
		}

		[TooltipText("Weight of the item. Items with lower weights are less likely to get picked. Items with zero weight only get picked if the amount requested is larger than the amount of other active items")]
		public NumericValue<float> Weight = new NumericValue<float>(0, -3.402823E+38f, 3.402823E+38f);
		public ObjectWrapper<Requirement> Requirement = new(DataModel.Requirement.DefaultValue);
		public ObjectWrapper<LootContent> Loot = new(DataModel.LootContent.DefaultValue);

		public static LootItem DefaultValue { get; private set; }
	}
}
