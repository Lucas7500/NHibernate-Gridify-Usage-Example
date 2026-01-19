namespace BookStore.Application.DTOs.Authors.Requests
{
    public sealed record GetAuthorsCountRequest
    {
        public static GetAuthorsCountRequest Instance => new();
    }
}
