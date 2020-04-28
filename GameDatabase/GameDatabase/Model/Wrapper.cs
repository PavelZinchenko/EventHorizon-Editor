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
}
