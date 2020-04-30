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
	public partial class Weapon
	{
		partial void OnDataDeserialized(WeaponSerializable serializable, Database database);
		partial void OnDataSerialized(ref WeaponSerializable serializable);


		public Weapon(WeaponSerializable serializable, Database database)
		{
			Id = new ItemId<Weapon>(serializable.Id, serializable.FileName);
			WeaponClass = serializable.WeaponClass;
			FireRate = new NumericValue<float>(serializable.FireRate, 0f, 100f);
			Spread = new NumericValue<float>(serializable.Spread, 0f, 180f);
			Magazine = new NumericValue<int>(serializable.Magazine, 0, 1000);
			ActivationType = serializable.ActivationType;
			ShotSound = serializable.ShotSound;
			ChargeSound = serializable.ChargeSound;
			ShotEffectPrefab = serializable.ShotEffectPrefab;
			ControlButtonIcon = serializable.ControlButtonIcon;

			OnDataDeserialized(serializable, database);
		}

		public void Save(WeaponSerializable serializable)
		{
			serializable.WeaponClass = WeaponClass;
			serializable.FireRate = FireRate.Value;
			serializable.Spread = Spread.Value;
			serializable.Magazine = Magazine.Value;
			serializable.ActivationType = ActivationType;
			serializable.ShotSound = ShotSound;
			serializable.ChargeSound = ChargeSound;
			serializable.ShotEffectPrefab = ShotEffectPrefab;
			serializable.ControlButtonIcon = ControlButtonIcon;
			OnDataSerialized(ref serializable);
		}

		public readonly ItemId<Weapon> Id;

		public WeaponClass WeaponClass;
		public NumericValue<float> FireRate = new NumericValue<float>(0, 0f, 100f);
		public NumericValue<float> Spread = new NumericValue<float>(0, 0f, 180f);
		public NumericValue<int> Magazine = new NumericValue<int>(0, 0, 1000);
		public ActivationType ActivationType;
		public string ShotSound;
		public string ChargeSound;
		public string ShotEffectPrefab;
		public string ControlButtonIcon;

		public static Weapon DefaultValue { get; private set; }
	}
}
