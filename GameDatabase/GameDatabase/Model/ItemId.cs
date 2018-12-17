using System;

namespace GameDatabase.Model
{
    public interface IItemId
    {
        int Id { get; }
        string Name { get; }
        bool IsNull { get; }
    }

    public class ItemId<T> : IItemId
    {
        public ItemId(int id, string name)
        {
            _id = id;
            _name = name;
        }

        public int Id { get { return _id; } }
        public string Name { get { return _name; } }
        public bool IsNull { get { return Id <= 0; } }

        public override int GetHashCode() { return Id; }

        public override bool Equals(object obj)
        {
            if (obj is ItemId<T>)
            {
                return _id == ((ItemId<T>)obj)._id;
            }

            if (obj is int)
            {
                return _id == (int)obj;
            }

            return false;
        }

        public override string ToString() { return string.IsNullOrEmpty(_name) ? "[EMPTY]" : _name; }

        private readonly int _id;
        private readonly string _name;

        public static readonly ItemId<T> Empty = new ItemId<T>(0, string.Empty);
    }
}
