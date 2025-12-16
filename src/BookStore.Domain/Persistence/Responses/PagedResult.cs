namespace BookStore.Domain.Persistence.Responses
{
    public class PagedResult<T>
    {
        public PagedResult(int pageNumber, int pageSize, int totalCount, IEnumerable<T> items)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pageNumber);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pageSize);

            CurrentPage = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
            Items = items;
        }

        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize); 
        public IEnumerable<T> Items { get; private set; }
    }
}
