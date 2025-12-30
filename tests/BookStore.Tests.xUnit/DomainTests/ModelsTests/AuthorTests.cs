using BookStore.Domain.Exceptions;
using BookStore.Domain.Models.AuthorModel;
using BookStore.Domain.ValueObjects;

namespace BookStore.Tests.xUnit.DomainTests.ModelsTests
{
    public static class AuthorTests
    {
        private static readonly Faker _faker = new();

        public sealed class UsingStandardAssertions
        {
            [Fact]
            public void PartialArgsConstructor_GivenValidName_ShouldCreateAuthor()
            {
                string name = _faker.Name.FullName();
                Author author = new(name);
                
                Assert.NotEqual(Guid.Empty, author.Id.Value);
                Assert.Equal(name, author.Name);
            }
            
            [Theory]
            [InlineData(1)]
            [InlineData(101)]
            public void PartialArgsConstructor_GivenInvalidName_ShouldThrowBusinessRuleValidationException(int invalidNameLength)
            {
                string name = _faker.Random.String(length: invalidNameLength);

                BusinessRuleValidationException exception = Assert.Throws<BusinessRuleValidationException>(() => new Author(name));

                Assert.NotNull(exception);
                Assert.Equal("businessrule.author-name-has-invalid-length", exception.Message);
            }
            
            [Fact]
            public void FullArgsConstructor_GivenValidIdAndName_ShouldCreateAuthor()
            {
                string name = _faker.Name.FullName();
                AuthorId id = AuthorId.NewId();
                Author author = new(id, name);
                
                Assert.Equal(id, author.Id);
                Assert.Equal(name, author.Name);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(101)]
            public void FullArgsConstructor_GivenInvalidName_ShouldThrowBusinessRuleValidationException(int invalidNameLength)
            {
                AuthorId id = AuthorId.NewId();
                string name = _faker.Random.String(length: invalidNameLength);

                BusinessRuleValidationException exception = Assert.Throws<BusinessRuleValidationException>(() => new Author(id, name));

                Assert.NotNull(exception);
                Assert.Equal("businessrule.author-name-has-invalid-length", exception.Message);
            }

            [Fact]
            public void ChangeName_GivenValidName_ShouldUpdateAuthorName()
            {
                string initialName = _faker.Name.FullName();
                Author author = new(initialName);
                
                string newName = _faker.Name.FullName();
                author.ChangeName(newName);
                
                Assert.Equal(newName, author.Name);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(101)]
            public void ChangeName_GivenInvalidName_ShouldThrowBusinessRuleValidationException(int invalidNameLength)
            {
                string initialName = _faker.Name.FullName();
                Author author = new(initialName);
                
                string invalidName = _faker.Random.String(length: invalidNameLength);
                BusinessRuleValidationException exception = Assert.Throws<BusinessRuleValidationException>(() => author.ChangeName(invalidName));

                Assert.NotNull(exception);
                Assert.Equal("businessrule.author-name-has-invalid-length", exception.Message);
            }
        }

        public sealed class UsingFluentAssertions
        {
            [Fact]
            public void PartialArgsConstructor_GivenValidName_ShouldCreateAuthor()
            {
                string name = _faker.Name.FullName();
                Author author = new(name);

                author.Id.Value.Should().NotBe(Guid.Empty);
                author.Name.Should().Be(name);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(101)]
            public void PartialArgsConstructor_GivenInvalidName_ShouldThrowBusinessRuleValidationException(int invalidNameLength)
            {
                string name = _faker.Random.String(length: invalidNameLength);
                Action act = () => new Author(name);

                act.Should().ThrowExactly<BusinessRuleValidationException>()
                    .WithMessage("businessrule.author-name-has-invalid-length");
            }

            [Fact]
            public void FullArgsConstructor_GivenValidIdAndName_ShouldCreateAuthor()
            {
                string name = _faker.Name.FullName();
                AuthorId id = AuthorId.NewId();
                Author author = new(id, name);

                author.Id.Should().Be(id);
                author.Name.Should().Be(name);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(101)]
            public void FullArgsConstructor_GivenInvalidName_ShouldThrowBusinessRuleValidationException(int invalidNameLength)
            {
                AuthorId id = AuthorId.NewId();
                string name = _faker.Random.String(length: invalidNameLength);
                Action act = () => new Author(id, name);
                
                act.Should().ThrowExactly<BusinessRuleValidationException>()
                    .WithMessage("businessrule.author-name-has-invalid-length");
            }

            [Fact]
            public void ChangeName_GivenValidName_ShouldUpdateAuthorName()
            {
                string initialName = _faker.Name.FullName();
                Author author = new(initialName);
                
                string newName = _faker.Name.FullName();
                author.ChangeName(newName);
                
                author.Name.Should().Be(newName);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(101)]
            public void ChangeName_GivenInvalidName_ShouldThrowBusinessRuleValidationException(int invalidNameLength)
            {
                string initialName = _faker.Name.FullName();
                Author author = new(initialName);
                
                string invalidName = _faker.Random.String(length: invalidNameLength);
                Action act = () => author.ChangeName(invalidName);
                
                act.Should().ThrowExactly<BusinessRuleValidationException>()
                    .WithMessage("businessrule.author-name-has-invalid-length");
            }
        }
    }
}
