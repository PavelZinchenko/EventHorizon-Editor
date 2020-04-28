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
	public partial class BulletTrigger
	{
		partial void OnDataDeserialized(BulletTriggerSerializable serializable, Database database);

		public BulletTrigger() {}

		public BulletTrigger(BulletTriggerSerializable serializable, Database database)
		{
			Condition = serializable.Condition;
			EffectType = serializable.EffectType;
			VisualEffect = database.GetVisualEffectId(serializable.VisualEffect);
			AudioClip = serializable.AudioClip;
			Ammunition = database.GetAmmunitionId(serializable.Ammunition);
			Color = Helpers.ColorFromString(serializable.Color);
			ColorMode = serializable.ColorMode;
			Quantity = new NumericValue<int>(serializable.Quantity, 0, 1000);
			Size = new NumericValue<float>(serializable.Size, 0.01f, 100f);
			Lifetime = new NumericValue<float>(serializable.Lifetime, 0f, 1000f);
			Cooldown = new NumericValue<float>(serializable.Cooldown, 0f, 1000f);
			RandomFactor = new NumericValue<float>(serializable.RandomFactor, 0f, 1f);
			PowerMultiplier = new NumericValue<float>(serializable.PowerMultiplier, 0f, 1000f);
			MaxNestingLevel = new NumericValue<int>(serializable.MaxNestingLevel, 0, 100);

			OnDataDeserialized(serializable, database);
		}

		public BulletTriggerSerializable Serialize()
		{
			var serializable = new BulletTriggerSerializable();
			serializable.Condition = Condition;
			serializable.EffectType = EffectType;
			serializable.VisualEffect = VisualEffect.Value;
			serializable.AudioClip = AudioClip;
			serializable.Ammunition = Ammunition.Value;
			serializable.Color = Helpers.ColorToString(Color);
			serializable.ColorMode = ColorMode;
			serializable.Quantity = Quantity.Value;
			serializable.Size = Size.Value;
			serializable.Lifetime = Lifetime.Value;
			serializable.Cooldown = Cooldown.Value;
			serializable.RandomFactor = RandomFactor.Value;
			serializable.PowerMultiplier = PowerMultiplier.Value;
			serializable.MaxNestingLevel = MaxNestingLevel.Value;
			return serializable;
		}

		public BulletTriggerCondition Condition;
		public BulletEffectType EffectType;
		public ItemId<VisualEffect> VisualEffect = ItemId<VisualEffect>.Empty;
		public string AudioClip;
		public ItemId<Ammunition> Ammunition = ItemId<Ammunition>.Empty;
		public System.Drawing.Color Color;
		public ColorMode ColorMode;
		public NumericValue<int> Quantity = new NumericValue<int>(0, 0, 1000);
		public NumericValue<float> Size = new NumericValue<float>(0, 0.01f, 100f);
		public NumericValue<float> Lifetime = new NumericValue<float>(0, 0f, 1000f);
		public NumericValue<float> Cooldown = new NumericValue<float>(0, 0f, 1000f);
		public NumericValue<float> RandomFactor = new NumericValue<float>(0, 0f, 1f);
		public NumericValue<float> PowerMultiplier = new NumericValue<float>(0, 0f, 1000f);
		public NumericValue<int> MaxNestingLevel = new NumericValue<int>(0, 0, 100);

		public static BulletTrigger DefaultValue { get; private set; }
	}
}
