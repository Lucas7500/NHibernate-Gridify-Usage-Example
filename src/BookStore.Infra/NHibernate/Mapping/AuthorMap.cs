using BookStore.Domain.Models.AuthorModel;
using BookStore.Infra.Constants;
using FluentNHibernate.Mapping;

namespace BookStore.Infra.NHibernate.Mapping
{
    public class AuthorMap : ClassMap<Author>
    {
        public AuthorMap()
        {
            Table(AuthorTableConstants.TableName);

            Id(a => a.IdValue)
                .GeneratedBy.Assigned()
                .Column(AuthorTableConstants.Columns.Id);
     
            Map(a => a.Name)
                .Not.Nullable()
                .Column(AuthorTableConstants.Columns.Name);

            HasMany(x => x.Books)
                .KeyColumn(BookTableConstants.Columns.Author)
                .Inverse()
                .Cascade.None();

        }
    }
}
