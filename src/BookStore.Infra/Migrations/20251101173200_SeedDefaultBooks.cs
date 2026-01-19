using BookStore.Domain.ValueObjects;
using FluentMigrator;

using static BookStore.Infra.Constants.BookTableConstants;

namespace BookStore.Infra.Migrations
{
    [Migration(20251101173200)]
    public sealed class SeedDefaultBooks : Migration
    {
        private static readonly AuthorId author1Id = new("11111111-1111-1111-1111-111111111111");
        private static readonly AuthorId author2Id = new("22222222-2222-2222-2222-222222222222");

        public override void Up()
        {
            Insert.IntoTable(TableName)
                .Row(new
                {
                    title = "1984",
                    price = 15.99m,
                    is_available = true,
                    author_id = author1Id.Value
                })
                .Row(new
                {
                    title = "Animal Farm",
                    price = 12.99m,
                    is_available = true,
                    author_id = author1Id.Value
                })
                .Row(new
                {
                    title = "The Hobbit",
                    price = 14.99m,
                    is_available = true,
                    author_id = author2Id.Value
                });
        }

        public override void Down()
        {
            Delete.FromTable(TableName)
                .Row(new { title = "1984" })
                .Row(new { title = "Animal Farm" })
                .Row(new { title = "The Hobbit" });
        }
    }
}
