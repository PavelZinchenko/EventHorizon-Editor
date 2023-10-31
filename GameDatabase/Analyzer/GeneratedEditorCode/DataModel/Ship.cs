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
				ShipType = serializable.ShipType;
				ShipRarity = serializable.ShipRarity;
				SizeClass = serializable.SizeClass;
				Name = serializable.Name;
				Faction = database.GetFactionId(serializable.Faction);
				IconImage = serializable.IconImage;
				IconScale = new NumericValue<float>(serializable.IconScale, 0.1f, 100f);
				ModelImage = serializable.ModelImage;
				ModelScale = new NumericValue<float>(serializable.ModelScale, 0.1f, 100f);
				EngineColor = Helpers.ColorFromString(serializable.EngineColor);
				Engines = serializable.Engines?.Select(item => new Engine(item, database)).ToArray();
				Layout = new Layout(serializable.Layout);
				Barrels = serializable.Barrels?.Select(item => new Barrel(item, database)).ToArray();
				Features = new ShipFeatures(serializable.Features, database);
			}
			catch (DatabaseException e)
			{
				throw new DatabaseException(this.GetType() + ": deserialization failed. " + serializable.FileName + " (" + serializable.Id + ")", e);
			}
			OnDataDeserialized(serializable, database);
		}

		public void Save(ShipSerializable serializable)
		{
			serializable.ShipType = ShipType;
			serializable.ShipRarity = ShipRarity;
			serializable.SizeClass = SizeClass;
			serializable.Name = Name;
			serializable.Faction = Faction.Value;
			serializable.IconImage = IconImage;
			serializable.IconScale = IconScale.Value;
			serializable.ModelImage = ModelImage;
			serializable.ModelScale = ModelScale.Value;
			serializable.EngineColor = Helpers.ColorToString(EngineColor);
			if (Engines == null || Engines.Length == 0)
			    serializable.Engines = null;
			else
			    serializable.Engines = Engines.Select(item => item.Serialize()).ToArray();
			serializable.Layout = Layout.Data;
			if (Barrels == null || Barrels.Length == 0)
			    serializable.Barrels = null;
			else
			    serializable.Barrels = Barrels.Select(item => item.Serialize()).ToArray();
			serializable.Features = Features.Serialize();
			OnDataSerialized(ref serializable);
		}

		public readonly ItemId<Ship> Id;

		public ShipType ShipType;
		public ShipRarity ShipRarity;
		public SizeClass SizeClass;
		public string Name;
		public ItemId<Faction> Faction = ItemId<Faction>.Empty;
		public string IconImage;
		public NumericValue<float> IconScale = new NumericValue<float>(0, 0.1f, 100f);
		public string ModelImage;
		public NumericValue<float> ModelScale = new NumericValue<float>(0, 0.1f, 100f);
		public System.Drawing.Color EngineColor;
		public Engine[] Engines;
		public Layout Layout;
		public Barrel[] Barrels;
		public ShipFeatures Features = new ShipFeatures();

		public static Ship DefaultValue { get; private set; }
	}
}
