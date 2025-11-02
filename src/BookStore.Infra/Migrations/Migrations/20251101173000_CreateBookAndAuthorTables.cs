using BookStore.Infra.Constants;
using FluentMigrator;

namespace BookStore.Infra.Migrations.Migrations
{
    [Migration(20251101173000)]
    public class _20251101173000_CreateBookAndAuthorTables : Migration
    {
        public override void Up()
        {
            Create.Table(AuthorTableConstants.TableName)
                .WithColumn(AuthorTableConstants.Columns.Id).AsGuid().PrimaryKey()
                .WithColumn(AuthorTableConstants.Columns.Name).AsString(200).NotNullable();

            Create.Table(BookTableConstants.TableName)
                .WithColumn(BookTableConstants.Columns.Id).AsInt32().PrimaryKey().Identity()
                .WithColumn(BookTableConstants.Columns.Title).AsString(200).NotNullable()
                .WithColumn(BookTableConstants.Columns.Price).AsDecimal(10, 2).NotNullable()
                .WithColumn(BookTableConstants.Columns.IsAvailable).AsBoolean().NotNullable()
                .WithColumn(BookTableConstants.Columns.Author).AsGuid().NotNullable();

            Create.ForeignKey("fk_books_author_id")
                .FromTable(BookTableConstants.TableName).ForeignColumn(BookTableConstants.Columns.Author)
                .ToTable(AuthorTableConstants.TableName).PrimaryColumn(AuthorTableConstants.Columns.Id);
        }

        public override void Down()
        {
            Delete.ForeignKey("fk_books_author_id");
            Delete.Table(BookTableConstants.TableName);
            Delete.Table(AuthorTableConstants.TableName);
        }
    }
}
