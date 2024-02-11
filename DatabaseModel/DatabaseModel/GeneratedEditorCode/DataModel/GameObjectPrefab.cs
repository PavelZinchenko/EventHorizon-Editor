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

	public interface IGameObjectPrefabContent
	{
		void Load(GameObjectPrefabSerializable serializable, Database database);
		void Save(ref GameObjectPrefabSerializable serializable);
	}

	public partial class GameObjectPrefab : IDataAdapter
	{
		partial void OnDataDeserialized(GameObjectPrefabSerializable serializable, Database database);
		partial void OnDataSerialized(ref GameObjectPrefabSerializable serializable);

		private static IGameObjectPrefabContent CreateContent(ObjectPrefabType type)
		{
			switch (type)
			{
				case ObjectPrefabType.Undefined:
					return new GameObjectPrefabEmptyContent();
				case ObjectPrefabType.WormTailSegment:
					return new GameObjectPrefab_WormTailSegment();
				default:
					throw new DatabaseException("GameObjectPrefab: Invalid content type - " + type);
			}
		}

		public static GameObjectPrefab Create(GameObjectPrefabSerializable serializable, Database database)
		{
			if (serializable == null) return DefaultValue;
			return new GameObjectPrefab(serializable, database);
		}

		public GameObjectPrefab()
		{
			_content = new GameObjectPrefabEmptyContent();
		}

		private GameObjectPrefab(GameObjectPrefabSerializable serializable, Database database)
		{
			Id = new ItemId<GameObjectPrefab>(serializable);

			Type = serializable.Type;
			_content = CreateContent(serializable.Type);
			_content.Load(serializable, database);

			OnDataDeserialized(serializable, database);
		}

		public void Save(GameObjectPrefabSerializable serializable)
		{
			serializable.Image1 = string.Empty;
			serializable.Image2 = string.Empty;
			_content.Save(ref serializable);
			serializable.Type = Type;
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

		public readonly ItemId<GameObjectPrefab> Id;

		private IGameObjectPrefabContent _content;
		public ObjectPrefabType Type;

		public static GameObjectPrefab DefaultValue { get; private set; }
	}

	public class GameObjectPrefabEmptyContent : IGameObjectPrefabContent
	{
		public void Load(GameObjectPrefabSerializable serializable, Database database) {}
		public void Save(ref GameObjectPrefabSerializable serializable) {}
	}

	public partial class GameObjectPrefab_WormTailSegment : IGameObjectPrefabContent
	{
		partial void OnDataDeserialized(GameObjectPrefabSerializable serializable, Database database);
		partial void OnDataSerialized(ref GameObjectPrefabSerializable serializable);

		public void Load(GameObjectPrefabSerializable serializable, Database database)
		{
			BodyImage = serializable.Image1;
			JointImage = serializable.Image2;

			OnDataDeserialized(serializable, database);
		}

		public void Save(ref GameObjectPrefabSerializable serializable)
		{
			serializable.Image1 = BodyImage;
			serializable.Image2 = JointImage;
			OnDataSerialized(ref serializable);
		}

		public string BodyImage;
		public string JointImage;
	}

}

