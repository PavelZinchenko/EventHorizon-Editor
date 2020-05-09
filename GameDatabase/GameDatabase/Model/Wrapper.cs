namespace EditorDatabase.Model
{
    public class Wrapper<T> where T : class
    {
        public ItemId<T> Item = ItemId<T>.Empty;

        public override string ToString()
        {
            return Item.ToString();
        }
    }

    public class ValueWrapper<T> where T : struct
    {
        public T Value;

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
