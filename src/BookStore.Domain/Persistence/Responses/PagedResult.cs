namespace BookStore.Domain.Persistence.Responses
{
    public class PagedResult<T> : List<T>
    {
        public PagedResult(int pageNumber, int pageSize, int count, IEnumerable<T> currentItems)
        {
            CurrentPage = pageNumber;
            PageSize = pageSize;
            TotalCount = count;

            AddRange(currentItems);
        }

        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public bool PreviousPageExists => CurrentPage > 1;
        public bool NextPageExists => CurrentPage < TotalPages;
    }
}
