namespace BookStore.Domain.Persistence.Requests
{
    public record QueryRequest
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string? Filter { get; set; }
        public string? OrderBy { get; set; }
    }
}
