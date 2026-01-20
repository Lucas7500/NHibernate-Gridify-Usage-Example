namespace BookStore.Tests.xUnit.ApplicationTests.MappersTests
{
    public static class BooksMapperExtensionsTests
    {
        private static readonly Faker _faker = new();

        public sealed class UsingStandardAssertions
        {
            [Fact]
            public void ToBookOnlyBookResponse_GivenBook_ShouldReturnBookOnlyResponseCorrectly()
            {
            }
            
            [Fact]
            public void ToBookWithAuthorResponse_GivenBook_ShouldReturnBookWithAuthorCorrectly()
            {
            }
        }

        public sealed class UsingFluentAssertions
        {

        }
    }
}
