using BookStore.Domain.Persistence.Requests;
using Gridify;

namespace BookStore.Infra.Mappers
{
    internal static class GridifyMappers
    {
        public static IGridifyQuery ToGridifyQuery(this QueryRequest request)
        {
            return new GridifyQuery
            {
                Page = request.Page,
                PageSize = request.PageSize,
                Filter = request.Filter,
                OrderBy = request.OrderBy
            };
        }
    }
}
