using NHibernate;

namespace BookStore.Infra.NHibernate
{
    internal class NHibernateContext(ISessionFactory sessionFactory) : IDisposable
    {
        private readonly Lazy<ISession> _session = new(sessionFactory.OpenSession);

        public ISession Session => _session.Value;

        public void Dispose()
        {
            if (_session is { IsValueCreated: true, Value: not null })
            {
                _session.Value.Dispose();
            }
        }
    }
}
