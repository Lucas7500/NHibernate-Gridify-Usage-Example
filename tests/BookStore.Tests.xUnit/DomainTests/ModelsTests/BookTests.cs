using BookStore.Domain.Exceptions;
using BookStore.Domain.Models.AuthorModel;
using BookStore.Domain.Models.BookModel;
using BookStore.Domain.ValueObjects;

namespace BookStore.Tests.xUnit.DomainTests.ModelsTests
{
    public static class BookTests
    {
        public static readonly Faker _faker = new();

        public sealed class UsingStandardAssertions
        {
            [Fact]
            public void PartialArgsConstructor_GivenValidArgs_ShouldCreateBook()
            {
                string authorName = _faker.Name.FullName();
                Author author = new(authorName);

                string bookTitle = _faker.Lorem.Sentence();
                decimal bookPrice = _faker.Random.Decimal(min: decimal.Zero, max: decimal.MaxValue);
                
                Book book = new(bookTitle, bookPrice, author);

                Assert.Equal(book.Id, BookId.Empty);
                Assert.Equal(bookTitle, book.Title);
                Assert.Equal(bookPrice, book.Price);
                Assert.Equal(author, book.Author);
                Assert.True(book.IsAvailable);
            }
            
            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData(" ")]
            public void PartialArgsConstructor_GivenInvalidTitle_ShouldThrowBusinessRuleValidationException(string? invalidTitle)
            {
                const int aboveMaxLength = 201;

                string authorName = _faker.Name.FullName();
                Author author = new(authorName);

                decimal bookPrice = _faker.Random.Decimal(min: decimal.Zero, decimal.MaxValue);

                BusinessRuleValidationException nullOrWhitespaceException = Assert.Throws<BusinessRuleValidationException>(
                    () => new Book(invalidTitle!, bookPrice, author));
                
                BusinessRuleValidationException aboveMaxLengthException = Assert.Throws<BusinessRuleValidationException>(
                    () => new Book(_faker.Random.String(length: aboveMaxLength), bookPrice, author));

                Assert.NotNull(nullOrWhitespaceException);
                Assert.Equal("businessrule.book-title-has-invalid-length", nullOrWhitespaceException.Message);
                
                Assert.NotNull(aboveMaxLengthException);
                Assert.Equal("businessrule.book-title-has-invalid-length", aboveMaxLengthException.Message);
            }

            [Fact]
            public void PartialArgsConstructor_GivenInvalidPrice_ShouldThrowBusinessRuleValidationException()
            {
                string authorName = _faker.Name.FullName();
                Author author = new(authorName);

                string bookTitle = _faker.Lorem.Sentence();
                decimal invalidBookPrice = _faker.Random.Decimal(min: decimal.MinValue, max: decimal.MinusOne);

                BusinessRuleValidationException exception = Assert.Throws<BusinessRuleValidationException>(
                    () => new Book(bookTitle, invalidBookPrice, author));

                Assert.NotNull(exception);
                Assert.Equal("businessrule.book-price-must-be-positive", exception.Message);
            }
            
            [Fact]
            public void PartialArgsConstructor_GivenInvalidAuthor_ShouldThrowBusinessRuleValidationException()
            {
                string bookTitle = _faker.Lorem.Sentence();
                decimal bookPrice = _faker.Random.Decimal(min: decimal.Zero, max: decimal.MaxValue);

                BusinessRuleValidationException invalidAuthorException = Assert.Throws<BusinessRuleValidationException>(
                    () => new Book(bookTitle, bookPrice, null!));

                Assert.NotNull(invalidAuthorException);
                Assert.Equal("businessrule.book-must-have-an-author", invalidAuthorException.Message);
            }

            [Fact]
            public void FullArgsConstructor_GivenValidArgs_ShouldCreateBook()
            {
                string authorName = _faker.Name.FullName();
                Author author = new(authorName);

                BookId bookId = new(_faker.Random.Int(min: 1));
                string bookTitle = _faker.Lorem.Sentence();
                decimal bookPrice = _faker.Random.Decimal(min: decimal.Zero, max: decimal.MaxValue);
                bool isAvailable = _faker.Random.Bool();

                Book book = new(bookId, bookTitle, bookPrice, isAvailable, author);

                Assert.Equal(bookId, book.Id);
                Assert.Equal(bookTitle, book.Title);
                Assert.Equal(bookPrice, book.Price);
                Assert.Equal(author, book.Author);
                Assert.Equal(isAvailable, book.IsAvailable);
            }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData(" ")]
            public void FullArgsConstructor_GivenInvalidTitle_ShouldThrowBusinessRuleValidationException(string? invalidTitle)
            {
                const int aboveMaxLength = 201;

                string authorName = _faker.Name.FullName();
                Author author = new(authorName);

                BookId bookId = new(_faker.Random.Int(min: 1));
                bool isAvailable = _faker.Random.Bool();
                decimal bookPrice = _faker.Random.Decimal(min: decimal.Zero, decimal.MaxValue);

                BusinessRuleValidationException nullOrWhitespaceException = Assert.Throws<BusinessRuleValidationException>(
                    () => new Book(bookId, invalidTitle!, bookPrice, isAvailable, author));

                BusinessRuleValidationException aboveMaxLengthException = Assert.Throws<BusinessRuleValidationException>(
                    () => new Book(bookId, _faker.Random.String(length: aboveMaxLength), bookPrice, isAvailable, author));

                Assert.NotNull(nullOrWhitespaceException);
                Assert.Equal("businessrule.book-title-has-invalid-length", nullOrWhitespaceException.Message);

                Assert.NotNull(aboveMaxLengthException);
                Assert.Equal("businessrule.book-title-has-invalid-length", aboveMaxLengthException.Message);
            }

            [Fact]
            public void FullArgsConstructor_GivenInvalidPrice_ShouldThrowBusinessRuleValidationException()
            {
                string authorName = _faker.Name.FullName();
                Author author = new(authorName);

                BookId bookId = new(_faker.Random.Int(min: 1));
                bool isAvailable = _faker.Random.Bool();
                string bookTitle = _faker.Lorem.Sentence();
                decimal invalidBookPrice = _faker.Random.Decimal(min: decimal.MinValue, max: decimal.MinusOne);

                BusinessRuleValidationException exception = Assert.Throws<BusinessRuleValidationException>(
                    () => new Book(bookId, bookTitle, invalidBookPrice, isAvailable, author));

                Assert.NotNull(exception);
                Assert.Equal("businessrule.book-price-must-be-positive", exception.Message);
            }

            [Fact]
            public void FullArgsConstructor_GivenInvalidAuthor_ShouldThrowBusinessRuleValidationException()
            {
                BookId bookId = new(_faker.Random.Int(min: 1));
                bool isAvailable = _faker.Random.Bool();
                string bookTitle = _faker.Lorem.Sentence();
                decimal bookPrice = _faker.Random.Decimal(min: decimal.Zero, max: decimal.MaxValue);

                BusinessRuleValidationException invalidAuthorException = Assert.Throws<BusinessRuleValidationException>(
                    () => new Book(bookId, bookTitle, bookPrice, isAvailable, null!));

                Assert.NotNull(invalidAuthorException);
                Assert.Equal("businessrule.book-must-have-an-author", invalidAuthorException.Message);
            }

            [Fact]
            public void ChangeTitle_GivenValidTitle_ShouldUpdateBookTitle()
            {
                string authorName = _faker.Name.FullName();
                Author author = new(authorName);

                string bookTitle = _faker.Lorem.Sentence();
                decimal bookPrice = _faker.Random.Decimal(min: decimal.Zero, max: decimal.MaxValue);
                
                Book book = new(bookTitle, bookPrice, author);
                
                string newTitle = _faker.Lorem.Sentence();
                book.ChangeTitle(newTitle);
                
                Assert.Equal(newTitle, book.Title);
            }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData(" ")]
            public void ChangeTitle_GivenInvalidTitle_ShouldThrowBusinessRuleValidationException(string? invalidTitle)
            {
                string authorName = _faker.Name.FullName();
                Author author = new(authorName);

                string bookTitle = _faker.Lorem.Sentence();
                decimal bookPrice = _faker.Random.Decimal(min: decimal.Zero, max: decimal.MaxValue);
                
                Book book = new(bookTitle, bookPrice, author);
                
                BusinessRuleValidationException exception = Assert.Throws<BusinessRuleValidationException>(
                    () => book.ChangeTitle(invalidTitle!));
                
                Assert.NotNull(exception);
                Assert.Equal("businessrule.book-title-has-invalid-length", exception.Message);
            }

            [Fact]
            public void ChangePrice_GivenValidPrice_ShouldUpdateBookPrice()
            {
                string authorName = _faker.Name.FullName();
                Author author = new(authorName);
                
                string bookTitle = _faker.Lorem.Sentence();
                decimal bookPrice = _faker.Random.Decimal(min: decimal.Zero, max: decimal.MaxValue);

                Book book = new(bookTitle, bookPrice, author);

                decimal newPrice = _faker.Random.Decimal(min: decimal.Zero, max: decimal.MaxValue);

                book.ChangePrice(newPrice);
                
                Assert.Equal(newPrice, book.Price);
            }

            [Fact]
            public void ChangePrice_GivenInvalidPrice_ShouldThrowBusinessRuleValidationException()
            {
                string authorName = _faker.Name.FullName();
                Author author = new(authorName);

                string bookTitle = _faker.Lorem.Sentence();
                decimal bookPrice = _faker.Random.Decimal(min: decimal.Zero, max: decimal.MaxValue);
                
                Book book = new(bookTitle, bookPrice, author);
                
                decimal invalidNewPrice = _faker.Random.Decimal(min: decimal.MinValue, max: decimal.MinusOne);
                
                BusinessRuleValidationException exception = Assert.Throws<BusinessRuleValidationException>(
                    () => book.ChangePrice(invalidNewPrice));
                
                Assert.NotNull(exception);
                Assert.Equal("businessrule.book-price-must-be-positive", exception.Message);
            }

            [Fact]
            public void MarkAsUnavailable_ShouldSetIsAvailableToFalse()
            {
                string authorName = _faker.Name.FullName();
                Author author = new(authorName);

                string bookTitle = _faker.Lorem.Sentence();
                decimal bookPrice = _faker.Random.Decimal(min: decimal.Zero, max: decimal.MaxValue);
                
                Book book = new(bookTitle, bookPrice, author);
                book.MarkAsUnavailable();
                
                Assert.False(book.IsAvailable);
            }

            [Fact]
            public void MarkAsAvailable_ShouldSetIsAvailableToTrue()
            {
                BookId bookId = new(_faker.Random.Int(min: 1));
                string authorName = _faker.Name.FullName();
                Author author = new(authorName);
                
                string bookTitle = _faker.Lorem.Sentence();
                decimal bookPrice = _faker.Random.Decimal(min: decimal.Zero, max: decimal.MaxValue);
                
                Book book = new(bookId, bookTitle, bookPrice, isAvailable: false, author);
                book.MarkAsAvailable();
                
                Assert.True(book.IsAvailable);
            }

            [Fact]
            public void ChangeAuthor_GivenValidAuthor_ShouldUpdateBookAuthor()
            {
                string authorName = _faker.Name.FullName();
                Author author = new(authorName);
                
                string bookTitle = _faker.Lorem.Sentence();
                decimal bookPrice = _faker.Random.Decimal(min: decimal.Zero, max: decimal.MaxValue);
                
                Book book = new(bookTitle, bookPrice, author);
                
                string newAuthorName = _faker.Name.FullName();
                Author newAuthor = new(newAuthorName);
                
                book.ChangeAuthor(newAuthor);
                
                Assert.Equal(newAuthor, book.Author);
            }

            [Fact]
            public void ChangeAuthor_GivenInvalidAuthor_ShouldThrowBusinessRuleValidationException()
            {
                string authorName = _faker.Name.FullName();
                Author author = new(authorName);
                
                string bookTitle = _faker.Lorem.Sentence();
                decimal bookPrice = _faker.Random.Decimal(min: decimal.Zero, max: decimal.MaxValue);
                
                Book book = new(bookTitle, bookPrice, author);
                
                BusinessRuleValidationException exception = Assert.Throws<BusinessRuleValidationException>(
                    () => book.ChangeAuthor(null!));
                
                Assert.NotNull(exception);
                Assert.Equal("businessrule.book-must-have-an-author", exception.Message);
            }
        }

        public sealed class UsingFluentAssertions
        {
            [Fact]
            public void PartialArgsConstructor_GivenValidArgs_ShouldCreateBook()
            {
                string authorName = _faker.Name.FullName();
                Author author = new(authorName);

                string bookTitle = _faker.Lorem.Sentence();
                decimal bookPrice = _faker.Random.Decimal(min: decimal.Zero, max: decimal.MaxValue);

                Book book = new(bookTitle, bookPrice, author);

                book.Id.Should().Be(BookId.Empty);
                book.Title.Should().Be(bookTitle);
                book.Price.Should().Be(bookPrice);
                book.Author.Should().Be(author);
                book.IsAvailable.Should().BeTrue();
            }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData(" ")]
            public void PartialArgsConstructor_GivenInvalidTitle_ShouldThrowBusinessRuleValidationException(string? invalidTitle)
            {
                const int aboveMaxLength = 201;

                string authorName = _faker.Name.FullName();
                Author author = new(authorName);

                decimal bookPrice = _faker.Random.Decimal(min: decimal.Zero, decimal.MaxValue);

                BusinessRuleValidationException nullOrWhitespaceException = Assert.Throws<BusinessRuleValidationException>(
                    () => new Book(invalidTitle!, bookPrice, author));

                BusinessRuleValidationException aboveMaxLengthException = Assert.Throws<BusinessRuleValidationException>(
                    () => new Book(_faker.Random.String(length: aboveMaxLength), bookPrice, author));

                nullOrWhitespaceException.Should().NotBeNull();
                nullOrWhitespaceException.Message.Should().Be("businessrule.book-title-has-invalid-length");

                aboveMaxLengthException.Should().NotBeNull();
                aboveMaxLengthException.Message.Should().Be("businessrule.book-title-has-invalid-length");
            }

            [Fact]
            public void PartialArgsConstructor_GivenInvalidPrice_ShouldThrowBusinessRuleValidationException()
            {
                string authorName = _faker.Name.FullName();
                Author author = new(authorName);

                string bookTitle = _faker.Lorem.Sentence();
                decimal invalidBookPrice = _faker.Random.Decimal(min: decimal.MinValue, max: decimal.MinusOne);

                BusinessRuleValidationException exception = Assert.Throws<BusinessRuleValidationException>(
                    () => new Book(bookTitle, invalidBookPrice, author));

                exception.Should().NotBeNull();
                exception.Message.Should().Be("businessrule.book-price-must-be-positive");
            }

            [Fact]
            public void PartialArgsConstructor_GivenInvalidAuthor_ShouldThrowBusinessRuleValidationException()
            {
                string bookTitle = _faker.Lorem.Sentence();
                decimal bookPrice = _faker.Random.Decimal(min: decimal.Zero, max: decimal.MaxValue);

                BusinessRuleValidationException invalidAuthorException = Assert.Throws<BusinessRuleValidationException>(
                    () => new Book(bookTitle, bookPrice, null!));

                invalidAuthorException.Should().NotBeNull();
                invalidAuthorException.Message.Should().Be("businessrule.book-must-have-an-author");
            }

            [Fact]
            public void FullArgsConstructor_GivenValidArgs_ShouldCreateBook()
            {
                string authorName = _faker.Name.FullName();
                Author author = new(authorName);

                BookId bookId = new(_faker.Random.Int(min: 1));
                string bookTitle = _faker.Lorem.Sentence();
                decimal bookPrice = _faker.Random.Decimal(min: decimal.Zero, max: decimal.MaxValue);
                bool isAvailable = _faker.Random.Bool();

                Book book = new(bookId, bookTitle, bookPrice, isAvailable, author);

                book.Id.Should().Be(bookId);
                book.Title.Should().Be(bookTitle);
                book.Price.Should().Be(bookPrice);
                book.Author.Should().Be(author);
                book.IsAvailable.Should().Be(isAvailable);
            }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData(" ")]
            public void FullArgsConstructor_GivenInvalidTitle_ShouldThrowBusinessRuleValidationException(string? invalidTitle)
            {
                const int aboveMaxLength = 201;

                string authorName = _faker.Name.FullName();
                Author author = new(authorName);

                BookId bookId = new(_faker.Random.Int(min: 1));
                bool isAvailable = _faker.Random.Bool();
                decimal bookPrice = _faker.Random.Decimal(min: decimal.Zero, decimal.MaxValue);

                BusinessRuleValidationException nullOrWhitespaceException = Assert.Throws<BusinessRuleValidationException>(
                    () => new Book(bookId, invalidTitle!, bookPrice, isAvailable, author));

                BusinessRuleValidationException aboveMaxLengthException = Assert.Throws<BusinessRuleValidationException>(
                    () => new Book(bookId, _faker.Random.String(length: aboveMaxLength), bookPrice, isAvailable, author));

                nullOrWhitespaceException.Should().NotBeNull();
                nullOrWhitespaceException.Message.Should().Be("businessrule.book-title-has-invalid-length");

                aboveMaxLengthException.Should().NotBeNull();
                aboveMaxLengthException.Message.Should().Be("businessrule.book-title-has-invalid-length");
            }

            [Fact]
            public void FullArgsConstructor_GivenInvalidPrice_ShouldThrowBusinessRuleValidationException()
            {
                string authorName = _faker.Name.FullName();
                Author author = new(authorName);

                BookId bookId = new(_faker.Random.Int(min: 1));
                bool isAvailable = _faker.Random.Bool();
                string bookTitle = _faker.Lorem.Sentence();
                decimal invalidBookPrice = _faker.Random.Decimal(min: decimal.MinValue, max: decimal.MinusOne);

                BusinessRuleValidationException exception = Assert.Throws<BusinessRuleValidationException>(
                    () => new Book(bookId, bookTitle, invalidBookPrice, isAvailable, author));

                exception.Should().NotBeNull();
                exception.Message.Should().Be("businessrule.book-price-must-be-positive");
            }

            [Fact]
            public void FullArgsConstructor_GivenInvalidAuthor_ShouldThrowBusinessRuleValidationException()
            {
                BookId bookId = new(_faker.Random.Int(min: 1));
                bool isAvailable = _faker.Random.Bool();
                string bookTitle = _faker.Lorem.Sentence();
                decimal bookPrice = _faker.Random.Decimal(min: decimal.Zero, max: decimal.MaxValue);

                BusinessRuleValidationException invalidAuthorException = Assert.Throws<BusinessRuleValidationException>(
                    () => new Book(bookId, bookTitle, bookPrice, isAvailable, null!));

                invalidAuthorException.Should().NotBeNull();
                invalidAuthorException.Message.Should().Be("businessrule.book-must-have-an-author");
            }

            [Fact]
            public void ChangeTitle_GivenValidTitle_ShouldUpdateBookTitle()
            {
                string authorName = _faker.Name.FullName();
                Author author = new(authorName);

                string bookTitle = _faker.Lorem.Sentence();
                decimal bookPrice = _faker.Random.Decimal(min: decimal.Zero, max: decimal.MaxValue);

                Book book = new(bookTitle, bookPrice, author);

                string newTitle = _faker.Lorem.Sentence();
                book.ChangeTitle(newTitle);

                book.Title.Should().Be(newTitle);
            }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData(" ")]
            public void ChangeTitle_GivenInvalidTitle_ShouldThrowBusinessRuleValidationException(string? invalidTitle)
            {
                string authorName = _faker.Name.FullName();
                Author author = new(authorName);

                string bookTitle = _faker.Lorem.Sentence();
                decimal bookPrice = _faker.Random.Decimal(min: decimal.Zero, max: decimal.MaxValue);

                Book book = new(bookTitle, bookPrice, author);

                BusinessRuleValidationException exception = Assert.Throws<BusinessRuleValidationException>(
                    () => book.ChangeTitle(invalidTitle!));

                exception.Should().NotBeNull();
                exception.Message.Should().Be("businessrule.book-title-has-invalid-length");
            }

            [Fact]
            public void ChangePrice_GivenValidPrice_ShouldUpdateBookPrice()
            {
                string authorName = _faker.Name.FullName();
                Author author = new(authorName);

                string bookTitle = _faker.Lorem.Sentence();
                decimal bookPrice = _faker.Random.Decimal(min: decimal.Zero, max: decimal.MaxValue);

                Book book = new(bookTitle, bookPrice, author);

                decimal newPrice = _faker.Random.Decimal(min: decimal.Zero, max: decimal.MaxValue);

                book.ChangePrice(newPrice);

                book.Price.Should().Be(newPrice);
            }

            [Fact]
            public void ChangePrice_GivenInvalidPrice_ShouldThrowBusinessRuleValidationException()
            {
                string authorName = _faker.Name.FullName();
                Author author = new(authorName);

                string bookTitle = _faker.Lorem.Sentence();
                decimal bookPrice = _faker.Random.Decimal(min: decimal.Zero, max: decimal.MaxValue);

                Book book = new(bookTitle, bookPrice, author);

                decimal invalidNewPrice = _faker.Random.Decimal(min: decimal.MinValue, max: decimal.MinusOne);

                BusinessRuleValidationException exception = Assert.Throws<BusinessRuleValidationException>(
                    () => book.ChangePrice(invalidNewPrice));

                exception.Should().NotBeNull();
                exception.Message.Should().Be("businessrule.book-price-must-be-positive");
            }

            [Fact]
            public void MarkAsUnavailable_ShouldSetIsAvailableToFalse()
            {
                string authorName = _faker.Name.FullName();
                Author author = new(authorName);

                string bookTitle = _faker.Lorem.Sentence();
                decimal bookPrice = _faker.Random.Decimal(min: decimal.Zero, max: decimal.MaxValue);

                Book book = new(bookTitle, bookPrice, author);
                book.MarkAsUnavailable();

                book.IsAvailable.Should().BeFalse();
            }

            [Fact]
            public void MarkAsAvailable_ShouldSetIsAvailableToTrue()
            {
                BookId bookId = new(_faker.Random.Int(min: 1));
                string authorName = _faker.Name.FullName();
                Author author = new(authorName);

                string bookTitle = _faker.Lorem.Sentence();
                decimal bookPrice = _faker.Random.Decimal(min: decimal.Zero, max: decimal.MaxValue);

                Book book = new(bookId, bookTitle, bookPrice, isAvailable: false, author);
                book.MarkAsAvailable();

                book.IsAvailable.Should().BeTrue();
            }

            [Fact]
            public void ChangeAuthor_GivenValidAuthor_ShouldUpdateBookAuthor()
            {
                string authorName = _faker.Name.FullName();
                Author author = new(authorName);

                string bookTitle = _faker.Lorem.Sentence();
                decimal bookPrice = _faker.Random.Decimal(min: decimal.Zero, max: decimal.MaxValue);

                Book book = new(bookTitle, bookPrice, author);

                string newAuthorName = _faker.Name.FullName();
                Author newAuthor = new(newAuthorName);

                book.ChangeAuthor(newAuthor);

                book.Author.Should().Be(newAuthor);
            }

            [Fact]
            public void ChangeAuthor_GivenInvalidAuthor_ShouldThrowBusinessRuleValidationException()
            {
                string authorName = _faker.Name.FullName();
                Author author = new(authorName);

                string bookTitle = _faker.Lorem.Sentence();
                decimal bookPrice = _faker.Random.Decimal(min: decimal.Zero, max: decimal.MaxValue);

                Book book = new(bookTitle, bookPrice, author);

                BusinessRuleValidationException exception = Assert.Throws<BusinessRuleValidationException>(
                    () => book.ChangeAuthor(null!));

                exception.Should().NotBeNull();
                exception.Message.Should().Be("businessrule.book-must-have-an-author");
            }
        }
    }
}
