using BookStore.Domain.Persistence;
using BookStore.Infra.NHibernate;

namespace BookStore.Infra.Repositories
{
    internal class BooksRepositoryHQL(NHibernateContext context) : IBooksRepository
    {
        public async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            var query = context.Session.CreateQuery("select count(*) from Book b");
            var result = await query.UniqueResultAsync<int>(cancellationToken);
            
            return result;
        }
    }
}
