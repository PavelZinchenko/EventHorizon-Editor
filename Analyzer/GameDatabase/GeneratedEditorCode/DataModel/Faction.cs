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
	public partial class Faction
	{
		partial void OnDataDeserialized(FactionSerializable serializable, Database database);
		partial void OnDataSerialized(ref FactionSerializable serializable);


		public Faction(FactionSerializable serializable, Database database)
		{
			try
			{
				Id = new ItemId<Faction>(serializable.Id, serializable.FileName);
				Name = serializable.Name;
				Color = Helpers.ColorFromString(serializable.Color);
				HomeStarDistance = new NumericValue<int>(serializable.HomeStarDistance, 0, 1000);
				WanderingShipsDistance = new NumericValue<int>(serializable.WanderingShipsDistance, 0, 1000);
				Hidden = serializable.Hidden;
				Hostile = serializable.Hostile;
			}
			catch (DatabaseException e)
			{
				throw new DatabaseException(this.GetType() + ": deserialization failed. " + serializable.FileName + " (" + serializable.Id + ")", e);
			}
			OnDataDeserialized(serializable, database);
		}

		public void Save(FactionSerializable serializable)
		{
			serializable.Name = Name;
			serializable.Color = Helpers.ColorToString(Color);
			serializable.HomeStarDistance = HomeStarDistance.Value;
			serializable.WanderingShipsDistance = WanderingShipsDistance.Value;
			serializable.Hidden = Hidden;
			serializable.Hostile = Hostile;
			OnDataSerialized(ref serializable);
		}

		public readonly ItemId<Faction> Id;

		public string Name;
		public System.Drawing.Color Color;
		public NumericValue<int> HomeStarDistance = new NumericValue<int>(0, 0, 1000);
		public NumericValue<int> WanderingShipsDistance = new NumericValue<int>(0, 0, 1000);
		public bool Hidden;
		public bool Hostile;

		public static Faction DefaultValue { get; private set; }
	}
}
