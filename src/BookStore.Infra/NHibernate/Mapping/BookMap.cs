using BookStore.Domain.Models;
using FluentNHibernate.Mapping;

namespace BookStore.Infra.NHibernate.Mapping
{
    public class BookMap : ClassMap<Book>
    {
        public BookMap()
        {
            Id(b => b.Id).GeneratedBy.Increment();

            Map(b => b.Title).Not.Nullable().Length(200);
            Map(b => b.Price).Not.Nullable().Precision(2);
            Map(b => b.IsAvailable).Not.Nullable();

            References(b => b.Author)
                .Column("AuthorId")
                .Not.Nullable()
                .Cascade.None();

            Table("books");
        }
    }
}
