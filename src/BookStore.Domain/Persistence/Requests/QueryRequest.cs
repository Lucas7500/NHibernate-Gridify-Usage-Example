namespace BookStore.Domain.Persistence.Requests
{
    public record QueryRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? Filter { get; set; }
        public string? OrderBy { get; set; }
    }
}
