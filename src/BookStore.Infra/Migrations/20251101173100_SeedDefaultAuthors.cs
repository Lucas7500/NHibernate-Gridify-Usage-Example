using BookStore.Domain.ValueObjects;
using FluentMigrator;

using static BookStore.Infra.Constants.AuthorTableConstants;

namespace BookStore.Infra.Migrations
{
    [Migration(20251101173100)]
    public class _20251101173100_SeedDefaultAuthors : Migration
    {
        private static readonly AuthorId author1Id = new("11111111-1111-1111-1111-111111111111");
        private static readonly AuthorId author2Id = new("22222222-2222-2222-2222-222222222222");

        public override void Up()
        {
            Insert.IntoTable(TableName)
                .Row(new { id = author1Id.Value, name = "George Orwell" })
                .Row(new { id = author2Id.Value, name = "J.R.R. Tolkien" });
        }

        public override void Down()
        {
            Delete.FromTable(TableName)
                .Row(new { id = author1Id.Value })
                .Row(new { id = author2Id.Value });
        }
    }
}
