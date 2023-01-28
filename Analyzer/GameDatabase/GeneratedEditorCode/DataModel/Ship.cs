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
	public partial class Ship
	{
		partial void OnDataDeserialized(ShipSerializable serializable, Database database);
		partial void OnDataSerialized(ref ShipSerializable serializable);


		public Ship(ShipSerializable serializable, Database database)
		{
			try
			{
				Id = new ItemId<Ship>(serializable.Id, serializable.FileName);
				ShipCategory = serializable.ShipCategory;
				Name = serializable.Name;
				Faction = database.GetFactionId(serializable.Faction);
				SizeClass = serializable.SizeClass;
				IconImage = serializable.IconImage;
				IconScale = new NumericValue<float>(serializable.IconScale, 0.1f, 100f);
				ModelImage = serializable.ModelImage;
				ModelScale = new NumericValue<float>(serializable.ModelScale, 0.1f, 100f);
				EnginePosition = serializable.EnginePosition;
				EngineColor = Helpers.ColorFromString(serializable.EngineColor);
				EngineSize = new NumericValue<float>(serializable.EngineSize, 0f, 1f);
				Engines = serializable.Engines?.Select(item => new Engine(item, database)).ToArray();
				EnergyResistance = new NumericValue<float>(serializable.EnergyResistance, -100f, 100f);
				KineticResistance = new NumericValue<float>(serializable.KineticResistance, -100f, 100f);
				HeatResistance = new NumericValue<float>(serializable.HeatResistance, -100f, 100f);
				Regeneration = serializable.Regeneration;
				WeightModifier = new NumericValue<float>(serializable.WeightModifier, -0.99f, 10f);
				VelocityModifier = new NumericValue<float>(serializable.VelocityModifier, -0.99f, 10f);
				TurnRateModifier = new NumericValue<float>(serializable.TurnRateModifier, -0.99f, 10f);
				BuiltinDevices = serializable.BuiltinDevices?.Select(id => new Wrapper<Device> { Item = database.GetDeviceId(id) }).ToArray();
				Layout = new Layout(serializable.Layout);
				Barrels = serializable.Barrels?.Select(item => new Barrel(item, database)).ToArray();
			}
			catch (DatabaseException e)
			{
				throw new DatabaseException(this.GetType() + ": deserialization failed. " + serializable.FileName + " (" + serializable.Id + ")", e);
			}
			OnDataDeserialized(serializable, database);
		}

		public void Save(ShipSerializable serializable)
		{
			serializable.ShipCategory = ShipCategory;
			serializable.Name = Name;
			serializable.Faction = Faction.Value;
			serializable.SizeClass = SizeClass;
			serializable.IconImage = IconImage;
			serializable.IconScale = IconScale.Value;
			serializable.ModelImage = ModelImage;
			serializable.ModelScale = ModelScale.Value;
			serializable.EnginePosition = EnginePosition;
			serializable.EngineColor = Helpers.ColorToString(EngineColor);
			serializable.EngineSize = EngineSize.Value;
			if (Engines == null || Engines.Length == 0)
			    serializable.Engines = null;
			else
			    serializable.Engines = Engines.Select(item => item.Serialize()).ToArray();
			serializable.EnergyResistance = EnergyResistance.Value;
			serializable.KineticResistance = KineticResistance.Value;
			serializable.HeatResistance = HeatResistance.Value;
			serializable.Regeneration = Regeneration;
			serializable.WeightModifier = WeightModifier.Value;
			serializable.VelocityModifier = VelocityModifier.Value;
			serializable.TurnRateModifier = TurnRateModifier.Value;
			if (BuiltinDevices == null || BuiltinDevices.Length == 0)
			    serializable.BuiltinDevices = null;
			else
			    serializable.BuiltinDevices = BuiltinDevices.Select(wrapper => wrapper.Item.Value).ToArray();
			serializable.Layout = Layout.Data;
			if (Barrels == null || Barrels.Length == 0)
			    serializable.Barrels = null;
			else
			    serializable.Barrels = Barrels.Select(item => item.Serialize()).ToArray();
			OnDataSerialized(ref serializable);
		}

		public readonly ItemId<Ship> Id;

		public ShipCategory ShipCategory;
		public string Name;
		public ItemId<Faction> Faction = ItemId<Faction>.Empty;
		public SizeClass SizeClass;
		public string IconImage;
		public NumericValue<float> IconScale = new NumericValue<float>(0, 0.1f, 100f);
		public string ModelImage;
		public NumericValue<float> ModelScale = new NumericValue<float>(0, 0.1f, 100f);
		public Vector2 EnginePosition;
		public System.Drawing.Color EngineColor;
		public NumericValue<float> EngineSize = new NumericValue<float>(0, 0f, 1f);
		public Engine[] Engines;
		public NumericValue<float> EnergyResistance = new NumericValue<float>(0, -100f, 100f);
		public NumericValue<float> KineticResistance = new NumericValue<float>(0, -100f, 100f);
		public NumericValue<float> HeatResistance = new NumericValue<float>(0, -100f, 100f);
		public bool Regeneration;
		public NumericValue<float> WeightModifier = new NumericValue<float>(0, -0.99f, 10f);
		public NumericValue<float> VelocityModifier = new NumericValue<float>(0, -0.99f, 10f);
		public NumericValue<float> TurnRateModifier = new NumericValue<float>(0, -0.99f, 10f);
		public Wrapper<Device>[] BuiltinDevices;
		public Layout Layout;
		public Barrel[] Barrels;

		public static Ship DefaultValue { get; private set; }
	}
}
