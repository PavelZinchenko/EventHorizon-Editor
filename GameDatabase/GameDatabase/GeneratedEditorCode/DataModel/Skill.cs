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
	public partial class Skill
	{
		partial void OnDataDeserialized(SkillSerializable serializable, Database database);
		partial void OnDataSerialized(ref SkillSerializable serializable);


		public Skill(SkillSerializable serializable, Database database)
		{
			try
			{
				Id = new ItemId<Skill>(serializable.Id, serializable.FileName);
				Name = serializable.Name;
				Icon = serializable.Icon;
				Description = serializable.Description;
				BaseRequirement = new NumericValue<float>(serializable.BaseRequirement, 0f, 100f);
				RequirementPerLevel = new NumericValue<float>(serializable.RequirementPerLevel, 0f, 100f);
				BasePrice = new NumericValue<float>(serializable.BasePrice, 0f, 100f);
				PricePerLevel = new NumericValue<float>(serializable.PricePerLevel, 0f, 100f);
				MaxLevel = new NumericValue<int>(serializable.MaxLevel, 1, 1000);
			}
			catch (DatabaseException e)
			{
				throw new DatabaseException(this.GetType() + ": deserialization failed. " + serializable.FileName + " (" + serializable.Id + ")", e);
			}
			OnDataDeserialized(serializable, database);
		}

		public void Save(SkillSerializable serializable)
		{
			serializable.Name = Name;
			serializable.Icon = Icon;
			serializable.Description = Description;
			serializable.BaseRequirement = BaseRequirement.Value;
			serializable.RequirementPerLevel = RequirementPerLevel.Value;
			serializable.BasePrice = BasePrice.Value;
			serializable.PricePerLevel = PricePerLevel.Value;
			serializable.MaxLevel = MaxLevel.Value;
			OnDataSerialized(ref serializable);
		}

		public readonly ItemId<Skill> Id;

		public string Name;
		public string Icon;
		public string Description;
		public NumericValue<float> BaseRequirement = new NumericValue<float>(0, 0f, 100f);
		public NumericValue<float> RequirementPerLevel = new NumericValue<float>(0, 0f, 100f);
		public NumericValue<float> BasePrice = new NumericValue<float>(0, 0f, 100f);
		public NumericValue<float> PricePerLevel = new NumericValue<float>(0, 0f, 100f);
		public NumericValue<int> MaxLevel = new NumericValue<int>(0, 1, 1000);

		public static Skill DefaultValue { get; private set; }
	}
}
