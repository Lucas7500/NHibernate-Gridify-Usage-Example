namespace BookStore.Application.DTOs.Books.Requests
{
    public record GetBooksCountRequest
    {
        public static GetBooksCountRequest Instance => new();
    }
}
