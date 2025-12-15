using BookStore.Domain.Models.AuthorModel;
using BookStore.Infra.Constants;
using FluentNHibernate.Mapping;

using static BookStore.Infra.Constants.AuthorTableConstants;

namespace BookStore.Infra.NHibernate.Mapping
{
    public class AuthorMap : ClassMap<Author>
    {
        public AuthorMap()
        {
            Table(TableName);

            Id(a => a.IdValue)
                .GeneratedBy.Assigned()
                .Column(Columns.Id);
     
            Map(a => a.Name)
                .Not.Nullable()
                .Column(Columns.Name);

            HasMany(x => x.Books)
                .KeyColumn(BookTableConstants.Columns.Author)
                .Inverse()
                .Cascade.None();
        }
    }
}
