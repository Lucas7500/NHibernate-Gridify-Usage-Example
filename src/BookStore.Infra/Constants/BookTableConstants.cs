namespace BookStore.Infra.Constants
{
    internal static class BookTableConstants
    {
        public const string TableName = "books";

        internal static class Columns
        {
            public const string Id = "id";
            public const string Title = "title";
            public const string Author = "author_id";
            public const string Price = "price";
            public const string IsAvailable = "is_available";
        }
    }
}
