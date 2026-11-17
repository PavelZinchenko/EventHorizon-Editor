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
	public partial class FrontierPredefinedLevel
	{
		partial void OnDataDeserialized(FrontierPredefinedLevelSerializable serializable, Database database);
		partial void OnDataSerialized(ref FrontierPredefinedLevelSerializable serializable);

		public static FrontierPredefinedLevel Create(FrontierPredefinedLevelSerializable serializable, Database database)
		{
			if (serializable == null) return DefaultValue;
			return new FrontierPredefinedLevel(serializable, database);
		}

		private FrontierPredefinedLevel(FrontierPredefinedLevelSerializable serializable, Database database)
		{
			try
			{
				Id = new ItemId<FrontierPredefinedLevel>(serializable.Id, serializable.FileName);
				LeveSettings = database.GetFrontierCommonLevelId(serializable.LeveSettings);
				Factions = serializable.Factions?.Select(id => new Wrapper<Faction> { Item = database.GetFactionId(id) }).ToArray();
			}
			catch (DatabaseException e)
			{
				throw new DatabaseException(this.GetType() + ": deserialization failed. " + serializable.FileName + " (" + serializable.Id + ")", e);
			}
			OnDataDeserialized(serializable, database);
		}

		public void Save(FrontierPredefinedLevelSerializable serializable)
		{
			serializable.LeveSettings = LeveSettings.Value;
			if (Factions == null || Factions.Length == 0)
			    serializable.Factions = null;
			else
			    serializable.Factions = Factions.Select(wrapper => wrapper.Item.Value).ToArray();
			OnDataSerialized(ref serializable);
		}

		public readonly ItemId<FrontierPredefinedLevel> Id;

		public ItemId<FrontierCommonLevel> LeveSettings = ItemId<FrontierCommonLevel>.Empty;
		public Wrapper<Faction>[] Factions;

		public static FrontierPredefinedLevel DefaultValue { get; private set; }
	}
}
