namespace BookStore.Infra.Constants
{
    internal static class AuthorTableConstants
    {
        internal const string TableName = "authors";

        internal static class Columns
        {
            internal const string Id = "id";
            internal const string Name = "name";
            internal const string Books = "books";
        }
    }
}
