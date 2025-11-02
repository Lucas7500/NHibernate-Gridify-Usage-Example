using BookStore.Domain.Models.BookModel;
using BookStore.Infra.Constants;
using FluentNHibernate.Mapping;

namespace BookStore.Infra.NHibernate.Mapping
{
    public class BookMap : ClassMap<Book>
    {
        public BookMap()
        {
            Table("books");

            Id(b => b.IdValue)
                .GeneratedBy.Identity()
                .Column(BookTableConstants.Columns.Id);

            Map(b => b.Title)
                .Not.Nullable()
                .Length(200)
                .Column(BookTableConstants.Columns.Title);

            Map(b => b.Price)
                .Not.Nullable()
                .Precision(5)
                .Scale(2)
                .Column(BookTableConstants.Columns.Price);
            
            Map(b => b.IsAvailable)
                .Not.Nullable()
                .Column(BookTableConstants.Columns.IsAvailable);

            References(b => b.Author)
                .Column(BookTableConstants.Columns.Author)
                .Not.Nullable()
                .Cascade.None();
        }
    }
}
