//-------------------------------------------------------------------------------
//                                                                               
//    This code was automatically generated.                                     
//    Changes to this file may cause incorrect behavior and will be lost if      
//    the code is regenerated.                                                   
//                                                                               
//-------------------------------------------------------------------------------

using System.Linq;
using System.Collections.Generic;
using EditorDatabase.Enums;
using EditorDatabase.Serializable;
using EditorDatabase.Model;

namespace EditorDatabase.DataModel
{

	public interface ITechnologyContent
	{
		void Load(TechnologySerializable serializable, Database database);
		void Save(ref TechnologySerializable serializable);
	}

	public partial class Technology : IDataAdapter
	{
		partial void OnDataDeserialized(TechnologySerializable serializable, Database database);
		partial void OnDataSerialized(ref TechnologySerializable serializable);

		private static ITechnologyContent CreateContent(TechType type)
		{
			switch (type)
			{
				case TechType.Component:
					return new Technology_Component();
				case TechType.Ship:
					return new Technology_Ship();
				case TechType.Satellite:
					return new Technology_Satellite();
				default:
					throw new DatabaseException("Technology: Invalid content type - " + type);
			}
		}

		public Technology()
		{
			_content = new TechnologyEmptyContent();
		}

		public Technology(TechnologySerializable serializable, Database database)
		{
			Id = new ItemId<Technology>(serializable);

			Type = serializable.Type;
			Price = new NumericValue<int>(serializable.Price, 0, 10000);
			Hidden = serializable.Hidden;
			Special = serializable.Special;
			Dependencies = serializable.Dependencies?.Select(id => new Wrapper<Technology> { Item = database.GetTechnologyId(id) }).ToArray();
			_content = CreateContent(serializable.Type);
			_content.Load(serializable, database);

			OnDataDeserialized(serializable, database);
		}

		public void Save(TechnologySerializable serializable)
		{
			serializable.ItemId = 0;
			serializable.ItemId = 0;
			serializable.ItemId = 0;
			serializable.Faction = 0;
			_content.Save(ref serializable);
			serializable.Type = Type;
			serializable.Price = Price.Value;
			serializable.Hidden = Hidden;
			serializable.Special = Special;
			if (Dependencies == null || Dependencies.Length == 0)
			    serializable.Dependencies = null;
			else
			    serializable.Dependencies = Dependencies.Select(wrapper => wrapper.Item.Value).ToArray();
			OnDataSerialized(ref serializable);
		}

		public event System.Action LayoutChangedEvent;
		public event System.Action DataChangedEvent;

		public IEnumerable<IProperty> Properties
		{
			get
			{
				var type = GetType();

				yield return new Property(this, type.GetField("Type"), OnTypeChanged);
				yield return new Property(this, type.GetField("Price"), DataChangedEvent);
				yield return new Property(this, type.GetField("Hidden"), DataChangedEvent);
				yield return new Property(this, type.GetField("Special"), DataChangedEvent);
				yield return new Property(this, type.GetField("Dependencies"), DataChangedEvent);

				foreach (var item in _content.GetType().GetFields().Where(f => f.IsPublic && !f.IsStatic))
					yield return new Property(_content, item, DataChangedEvent);
			}
		}

		private void OnTypeChanged()
		{
			_content = CreateContent(Type);
			DataChangedEvent?.Invoke();
			LayoutChangedEvent?.Invoke();
		}

		public readonly ItemId<Technology> Id;

		private ITechnologyContent _content;
		public TechType Type;
		public NumericValue<int> Price = new NumericValue<int>(0, 0, 10000);
		public bool Hidden;
		public bool Special;
		public Wrapper<Technology>[] Dependencies;

		public static Technology DefaultValue { get; private set; }
	}

	public class TechnologyEmptyContent : ITechnologyContent
	{
		public void Load(TechnologySerializable serializable, Database database) {}
		public void Save(ref TechnologySerializable serializable) {}
	}

	public partial class Technology_Component : ITechnologyContent
	{
		partial void OnDataDeserialized(TechnologySerializable serializable, Database database);
		partial void OnDataSerialized(ref TechnologySerializable serializable);

		public void Load(TechnologySerializable serializable, Database database)
		{
			Component = database.GetComponentId(serializable.ItemId);
			if (Component.IsNull)
			    throw new DatabaseException(this.GetType().Name + ": Component cannot be null");
			Faction = database.GetFactionId(serializable.Faction);

			OnDataDeserialized(serializable, database);
		}

		public void Save(ref TechnologySerializable serializable)
		{
			serializable.ItemId = Component.Value;
			serializable.Faction = Faction.Value;
			OnDataSerialized(ref serializable);
		}

		public ItemId<Component> Component = ItemId<Component>.Empty;
		public ItemId<Faction> Faction = ItemId<Faction>.Empty;
	}

	public partial class Technology_Ship : ITechnologyContent
	{
		partial void OnDataDeserialized(TechnologySerializable serializable, Database database);
		partial void OnDataSerialized(ref TechnologySerializable serializable);

		public void Load(TechnologySerializable serializable, Database database)
		{
			Ship = database.GetShipId(serializable.ItemId);
			if (Ship.IsNull)
			    throw new DatabaseException(this.GetType().Name + ": Ship cannot be null");

			OnDataDeserialized(serializable, database);
		}

		public void Save(ref TechnologySerializable serializable)
		{
			serializable.ItemId = Ship.Value;
			OnDataSerialized(ref serializable);
		}

		public ItemId<Ship> Ship = ItemId<Ship>.Empty;
	}

	public partial class Technology_Satellite : ITechnologyContent
	{
		partial void OnDataDeserialized(TechnologySerializable serializable, Database database);
		partial void OnDataSerialized(ref TechnologySerializable serializable);

		public void Load(TechnologySerializable serializable, Database database)
		{
			Satellite = database.GetSatelliteId(serializable.ItemId);
			if (Satellite.IsNull)
			    throw new DatabaseException(this.GetType().Name + ": Satellite cannot be null");
			Faction = database.GetFactionId(serializable.Faction);

			OnDataDeserialized(serializable, database);
		}

		public void Save(ref TechnologySerializable serializable)
		{
			serializable.ItemId = Satellite.Value;
			serializable.Faction = Faction.Value;
			OnDataSerialized(ref serializable);
		}

		public ItemId<Satellite> Satellite = ItemId<Satellite>.Empty;
		public ItemId<Faction> Faction = ItemId<Faction>.Empty;
	}

}

