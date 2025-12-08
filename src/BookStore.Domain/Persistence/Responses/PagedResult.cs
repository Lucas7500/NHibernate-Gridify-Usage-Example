namespace BookStore.Domain.Persistence.Responses
{
    public class PagedResult<T>
    {
        public PagedResult(int pageNumber, int pageSize, int totalCount, IEnumerable<T> currentItems)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pageNumber);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pageSize);

            CurrentPage = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
            CurrentItems = currentItems;
        }

        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public IEnumerable<T> CurrentItems { get; private set; }
        
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public bool PreviousPageExists => CurrentPage > 1;
        public bool NextPageExists => CurrentPage < TotalPages;
    }
}
