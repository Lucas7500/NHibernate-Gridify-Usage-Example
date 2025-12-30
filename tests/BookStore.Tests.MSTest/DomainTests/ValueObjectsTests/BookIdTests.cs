using BookStore.Domain.Exceptions;
using BookStore.Domain.ValueObjects;
using BookStore.Domain.ValueObjects.Contracts;

namespace BookStore.Tests.MSTest.DomainTests.ValueObjectsTests
{
    [TestClass]
    public sealed class BookIdTests
    {
        private Faker _faker = null!;

        [TestInitialize]
        public void TestInitialize()
        {
            _faker = new Faker();
        }

        [TestMethod]
        public void BookId_ImplementsIStronglyTypedIdInterface()
        {
            Type bookIdType = typeof(BookId);
            Type stronglyTypedIdInterfaceType = typeof(IStronglyTypedId<int>);

            Assert.IsTrue(stronglyTypedIdInterfaceType.IsAssignableFrom(bookIdType),
                message: $"{bookIdType.Name} does not implement {stronglyTypedIdInterfaceType.Name} interface.");
        }

        [TestMethod]
        public void Empty_ShouldReturnBookIdWithIntegerDefaultValue()
        {
            BookId emptyBookId = BookId.Empty;

            Assert.AreEqual(default, emptyBookId.Value);
            Assert.IsFalse(emptyBookId.HasValue);
        }

        [TestMethod]
        public void Constructor_WithoutParameters_ShouldCreateEmptyBookId()
        {
            BookId bookId = new();

            Assert.AreEqual(default, bookId.Value);
            Assert.IsFalse(bookId.HasValue);
        }

        [TestMethod]
        public void Constructor_WithIntParameter_ShouldCreateBookIdWithGivenValue()
        {
            int integer = _faker.Random.Int(min: 1);
            BookId bookId = new(integer);

            Assert.AreEqual(integer, bookId.Value);
            Assert.IsTrue(bookId.HasValue);
        }

        [TestMethod]
        public void Constructor_WithIntegerDefaultValue_ShouldThrowInvalidIntegerIdException()
        {
            int emptyint = default;

            Assert.ThrowsExactly<InvalidIntegerIdException>(() => new BookId(emptyint));
        }

        [TestMethod]
        public void Constructor_WithStringParameter_ShouldCreateBookIdWithGivenValue()
        {
            int integer = _faker.Random.Int(min: 1);
            string intStr = integer.ToString();
            BookId bookId = new(intStr);

            Assert.AreEqual(integer, bookId.Value);
            Assert.IsTrue(bookId.HasValue);
        }

        [TestMethod]
        public void Constructor_WithInvalidIntegerString_ShouldThrowInvalidIntegerIdException()
        {
            string invalidintStr = _faker.Random.Word();

            Assert.ThrowsExactly<InvalidIntegerIdException>(() => new BookId(invalidintStr));
        }

        [TestMethod]
        public void ToString_ShouldReturnIntegerStringRepresentation()
        {
            int integer = _faker.Random.Int(min: 1);
            BookId bookId = new(integer);

            Assert.AreEqual(integer.ToString(), bookId.ToString());
        }

        [TestMethod]
        public void BookId_EqualityCheck_ShouldBeTrueForSameValues()
        {
            int integer = _faker.Random.Int(min: 1);

            BookId bookId1 = new(integer);
            BookId bookId2 = new(integer);

            Assert.AreEqual(bookId1, bookId2);
        }

        [TestMethod]
        public void BookId_EqualityCheck_ShouldBeFalseForDifferentValues()
        {
            int integer1 = _faker.Random.Int(min: 1);
            int integer2 = _faker.Random.Int(min: 1);

            if (integer1 == integer2)
            {
                integer2 = integer1 + 1;
            }

            BookId bookId1 = new(integer1);
            BookId bookId2 = new(integer2);

            Assert.AreNotEqual(bookId1, bookId2);
        }

        [TestMethod]
        public void BookId_HashCode_ShouldBeSameForSameValues()
        {
            int integer = _faker.Random.Int(min: 1);

            BookId bookId1 = new(integer);
            BookId bookId2 = new(integer);

            Assert.AreEqual(bookId1.GetHashCode(), bookId2.GetHashCode());
        }

        [TestMethod]
        public void BookId_HashCode_ShouldBeDifferentForDifferentValues()
        {
            int integer1 = _faker.Random.Int(min: 1);
            int integer2 = _faker.Random.Int(min: 1);

            BookId bookId1 = new(integer1);
            BookId bookId2 = new(integer2);

            Assert.AreNotEqual(bookId1.GetHashCode(), bookId2.GetHashCode());
        }
    }
}
