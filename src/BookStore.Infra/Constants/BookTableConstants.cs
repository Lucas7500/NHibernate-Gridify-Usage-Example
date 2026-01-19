namespace BookStore.Infra.Constants
{
    internal static class BookTableConstants
    {
        internal const string TableName = "books";

        internal static class Columns
        {
            internal const string Id = "id";
            internal const string Title = "title";
            internal const string Author = "author_id";
            internal const string Price = "price";
            internal const string IsAvailable = "is_available";
        }
    }
}
