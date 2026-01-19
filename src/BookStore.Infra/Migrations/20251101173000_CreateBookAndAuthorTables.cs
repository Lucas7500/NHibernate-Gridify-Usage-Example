using FluentMigrator;

using Author = BookStore.Infra.Constants.AuthorTableConstants;
using Book = BookStore.Infra.Constants.BookTableConstants;

namespace BookStore.Infra.Migrations
{
    [Migration(20251101173000)]
    public sealed class CreateBookAndAuthorTables : Migration
    {
        public override void Up()
        {
            Create.Table(Author.TableName)
                .WithColumn(Author.Columns.Id).AsGuid().PrimaryKey()
                .WithColumn(Author.Columns.Name).AsString(200).NotNullable();

            Create.Table(Book.TableName)
                .WithColumn(Book.Columns.Id).AsInt32().PrimaryKey().Identity()
                .WithColumn(Book.Columns.Title).AsString(200).NotNullable()
                .WithColumn(Book.Columns.Price).AsDecimal(10, 2).NotNullable()
                .WithColumn(Book.Columns.IsAvailable).AsBoolean().NotNullable()
                .WithColumn(Book.Columns.Author).AsGuid().NotNullable();

            Create.ForeignKey("fk_books_author_id")
                .FromTable(Book.TableName).ForeignColumn(Book.Columns.Author)
                .ToTable(Author.TableName).PrimaryColumn(Author.Columns.Id);
        }

        public override void Down()
        {
            Delete.ForeignKey("fk_books_author_id");
            Delete.Table(Book.TableName);
            Delete.Table(Author.TableName);
        }
    }
}
