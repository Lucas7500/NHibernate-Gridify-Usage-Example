namespace BookStore.Domain.Persistence
{
    public interface IBooksRepository : IQueryableBooksRepository, IWriteableBooksRepository;
}
