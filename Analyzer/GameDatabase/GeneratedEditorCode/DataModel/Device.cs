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
	public partial class Device
	{
		partial void OnDataDeserialized(DeviceSerializable serializable, Database database);
		partial void OnDataSerialized(ref DeviceSerializable serializable);


		public Device(DeviceSerializable serializable, Database database)
		{
			try
			{
				Id = new ItemId<Device>(serializable.Id, serializable.FileName);
				DeviceClass = serializable.DeviceClass;
				EnergyConsumption = new NumericValue<float>(serializable.EnergyConsumption, 0f, 1E+09f);
				PassiveEnergyConsumption = new NumericValue<float>(serializable.PassiveEnergyConsumption, 0f, 1E+09f);
				Power = new NumericValue<float>(serializable.Power, 0f, 1000f);
				Range = new NumericValue<float>(serializable.Range, 0f, 1000f);
				Size = new NumericValue<float>(serializable.Size, 0f, 1000f);
				Cooldown = new NumericValue<float>(serializable.Cooldown, 0f, 1000f);
				Lifetime = new NumericValue<float>(serializable.Lifetime, 0f, 1000f);
				Offset = serializable.Offset;
				ActivationType = serializable.ActivationType;
				Color = Helpers.ColorFromString(serializable.Color);
				Sound = serializable.Sound;
				EffectPrefab = serializable.EffectPrefab;
				ObjectPrefab = serializable.ObjectPrefab;
				ControlButtonIcon = serializable.ControlButtonIcon;
			}
			catch (DatabaseException e)
			{
				throw new DatabaseException(this.GetType() + ": deserialization failed. " + serializable.FileName + " (" + serializable.Id + ")", e);
			}
			OnDataDeserialized(serializable, database);
		}

		public void Save(DeviceSerializable serializable)
		{
			serializable.DeviceClass = DeviceClass;
			serializable.EnergyConsumption = EnergyConsumption.Value;
			serializable.PassiveEnergyConsumption = PassiveEnergyConsumption.Value;
			serializable.Power = Power.Value;
			serializable.Range = Range.Value;
			serializable.Size = Size.Value;
			serializable.Cooldown = Cooldown.Value;
			serializable.Lifetime = Lifetime.Value;
			serializable.Offset = Offset;
			serializable.ActivationType = ActivationType;
			serializable.Color = Helpers.ColorToString(Color);
			serializable.Sound = Sound;
			serializable.EffectPrefab = EffectPrefab;
			serializable.ObjectPrefab = ObjectPrefab;
			serializable.ControlButtonIcon = ControlButtonIcon;
			OnDataSerialized(ref serializable);
		}

		public readonly ItemId<Device> Id;

		public DeviceClass DeviceClass;
		public NumericValue<float> EnergyConsumption = new NumericValue<float>(0, 0f, 1E+09f);
		public NumericValue<float> PassiveEnergyConsumption = new NumericValue<float>(0, 0f, 1E+09f);
		public NumericValue<float> Power = new NumericValue<float>(0, 0f, 1000f);
		public NumericValue<float> Range = new NumericValue<float>(0, 0f, 1000f);
		public NumericValue<float> Size = new NumericValue<float>(0, 0f, 1000f);
		public NumericValue<float> Cooldown = new NumericValue<float>(0, 0f, 1000f);
		public NumericValue<float> Lifetime = new NumericValue<float>(0, 0f, 1000f);
		public Vector2 Offset;
		public ActivationType ActivationType;
		public System.Drawing.Color Color;
		public string Sound;
		public string EffectPrefab;
		public string ObjectPrefab;
		public string ControlButtonIcon;

		public static Device DefaultValue { get; private set; }
	}
}
