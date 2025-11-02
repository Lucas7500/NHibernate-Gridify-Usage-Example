namespace BookStore.Infra.Constants
{
    internal static class AuthorTableConstants
    {
        public const string TableName = "authors";

        internal static class Columns
        {
            public const string Id = "id";
            public const string Name = "name";
            public const string Books = "books";
        }
    }
}
