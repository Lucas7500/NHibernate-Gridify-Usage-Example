using BookStore.Domain.Models;
using FluentNHibernate.Mapping;

namespace BookStore.Infra.NHibernate.Mapping
{
    public class AuthorMap : ClassMap<Author>
    {
        public AuthorMap()
        {
            Id(a => a.Id).GeneratedBy.Increment();
     
            Map(a => a.Name).Not.Nullable();

            Table("authors");
        }
    }
}
