using Gridify;

namespace BookStore.Domain.Persistence.Requests
{
    public record QueryRequest : IGridifyQuery
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? Filter { get; set; }
        public string? OrderBy { get; set; }
    }
}
