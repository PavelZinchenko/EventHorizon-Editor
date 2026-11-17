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
	public partial class FrontierNpcSettings
	{
		partial void OnDataDeserialized(FrontierNpcSettingsSerializable serializable, Database database);
		partial void OnDataSerialized(ref FrontierNpcSettingsSerializable serializable);

		public static FrontierNpcSettings Create(FrontierNpcSettingsSerializable serializable, Database database)
		{
			if (serializable == null) return DefaultValue;
			return new FrontierNpcSettings(serializable, database);
		}

		private FrontierNpcSettings(FrontierNpcSettingsSerializable serializable, Database database)
		{
			MerchantAppearLevel = new NumericValue<int>(serializable.MerchantAppearLevel, 0, 2147483647);
			SmugglerAppearLevel = new NumericValue<int>(serializable.SmugglerAppearLevel, 0, 2147483647);
			ShipyardAppearLevel = new NumericValue<int>(serializable.ShipyardAppearLevel, 0, 2147483647);
			EngineerAppearLevel = new NumericValue<int>(serializable.EngineerAppearLevel, 0, 2147483647);
			OnDataDeserialized(serializable, database);
		}

		public void Save(FrontierNpcSettingsSerializable serializable)
		{
			serializable.MerchantAppearLevel = MerchantAppearLevel.Value;
			serializable.SmugglerAppearLevel = SmugglerAppearLevel.Value;
			serializable.ShipyardAppearLevel = ShipyardAppearLevel.Value;
			serializable.EngineerAppearLevel = EngineerAppearLevel.Value;
			OnDataSerialized(ref serializable);
		}

		public NumericValue<int> MerchantAppearLevel = new NumericValue<int>(0, 0, 2147483647);
		public NumericValue<int> SmugglerAppearLevel = new NumericValue<int>(0, 0, 2147483647);
		public NumericValue<int> ShipyardAppearLevel = new NumericValue<int>(0, 0, 2147483647);
		public NumericValue<int> EngineerAppearLevel = new NumericValue<int>(0, 0, 2147483647);

		public static FrontierNpcSettings DefaultValue { get; private set; }
	}
}
