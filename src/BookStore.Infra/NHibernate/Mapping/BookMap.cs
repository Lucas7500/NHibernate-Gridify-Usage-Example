using BookStore.Domain.Models.BookModel;
using FluentNHibernate.Mapping;

using static BookStore.Infra.Constants.BookTableConstants;

namespace BookStore.Infra.NHibernate.Mapping
{
    public class BookMap : ClassMap<Book>
    {
        public BookMap()
        {
            Table("books");

            Id(b => b.IdValue)
                .GeneratedBy.Identity()
                .Column(Columns.Id);

            Map(b => b.Title)
                .Not.Nullable()
                .Length(200)
                .Column(Columns.Title);

            Map(b => b.Price)
                .Not.Nullable()
                .Precision(5)
                .Scale(2)
                .Column(Columns.Price);
            
            Map(b => b.IsAvailable)
                .Not.Nullable()
                .Column(Columns.IsAvailable);

            References(b => b.Author)
                .Column(Columns.Author)
                .Not.Nullable()
                .Cascade.None();
        }
    }
}
