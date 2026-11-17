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
	public partial class FrontierLevelSettings
	{
		partial void OnDataDeserialized(FrontierLevelSettingsSerializable serializable, Database database);
		partial void OnDataSerialized(ref FrontierLevelSettingsSerializable serializable);

		public static FrontierLevelSettings Create(FrontierLevelSettingsSerializable serializable, Database database)
		{
			if (serializable == null) return DefaultValue;
			return new FrontierLevelSettings(serializable, database);
		}

		private FrontierLevelSettings(FrontierLevelSettingsSerializable serializable, Database database)
		{
			HardMinLevel = new NumericValue<int>(serializable.HardMinLevel, 0, 2147483647);
			ExtremeMinLevel = new NumericValue<int>(serializable.ExtremeMinLevel, 0, 2147483647);
			InsaneMinLevel = new NumericValue<int>(serializable.InsaneMinLevel, 0, 2147483647);
			TutorialLevel = database.GetFrontierCommonLevelId(serializable.TutorialLevel);
			EasyLevel = database.GetFrontierCommonLevelId(serializable.EasyLevel);
			MediumLevel = database.GetFrontierCommonLevelId(serializable.MediumLevel);
			NormalLevel = database.GetFrontierCommonLevelId(serializable.NormalLevel);
			HardLevel = database.GetFrontierCommonLevelId(serializable.HardLevel);
			ExtremeLevel = database.GetFrontierCommonLevelId(serializable.ExtremeLevel);
			InsaneLevel = database.GetFrontierCommonLevelId(serializable.InsaneLevel);
			OnDataDeserialized(serializable, database);
		}

		public void Save(FrontierLevelSettingsSerializable serializable)
		{
			serializable.HardMinLevel = HardMinLevel.Value;
			serializable.ExtremeMinLevel = ExtremeMinLevel.Value;
			serializable.InsaneMinLevel = InsaneMinLevel.Value;
			serializable.TutorialLevel = TutorialLevel.Value;
			serializable.EasyLevel = EasyLevel.Value;
			serializable.MediumLevel = MediumLevel.Value;
			serializable.NormalLevel = NormalLevel.Value;
			serializable.HardLevel = HardLevel.Value;
			serializable.ExtremeLevel = ExtremeLevel.Value;
			serializable.InsaneLevel = InsaneLevel.Value;
			OnDataSerialized(ref serializable);
		}

		public NumericValue<int> HardMinLevel = new NumericValue<int>(0, 0, 2147483647);
		public NumericValue<int> ExtremeMinLevel = new NumericValue<int>(0, 0, 2147483647);
		public NumericValue<int> InsaneMinLevel = new NumericValue<int>(0, 0, 2147483647);
		public ItemId<FrontierCommonLevel> TutorialLevel = ItemId<FrontierCommonLevel>.Empty;
		public ItemId<FrontierCommonLevel> EasyLevel = ItemId<FrontierCommonLevel>.Empty;
		public ItemId<FrontierCommonLevel> MediumLevel = ItemId<FrontierCommonLevel>.Empty;
		public ItemId<FrontierCommonLevel> NormalLevel = ItemId<FrontierCommonLevel>.Empty;
		public ItemId<FrontierCommonLevel> HardLevel = ItemId<FrontierCommonLevel>.Empty;
		public ItemId<FrontierCommonLevel> ExtremeLevel = ItemId<FrontierCommonLevel>.Empty;
		public ItemId<FrontierCommonLevel> InsaneLevel = ItemId<FrontierCommonLevel>.Empty;

		public static FrontierLevelSettings DefaultValue { get; private set; }
	}
}
