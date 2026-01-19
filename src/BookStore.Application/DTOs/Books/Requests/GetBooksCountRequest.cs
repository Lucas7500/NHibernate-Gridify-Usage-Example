namespace BookStore.Application.DTOs.Books.Requests
{
    public sealed record GetBooksCountRequest
    {
        public static GetBooksCountRequest Instance => new();
    }
}
