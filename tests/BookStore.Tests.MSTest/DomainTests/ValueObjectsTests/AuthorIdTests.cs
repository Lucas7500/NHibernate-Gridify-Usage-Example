using BookStore.Domain.Exceptions;
using BookStore.Domain.ValueObjects;
using BookStore.Domain.ValueObjects.Contracts;

namespace BookStore.Tests.MSTest.DomainTests.ValueObjectsTests
{
    [TestClass]
    public sealed class AuthorIdTests
    {
        public Faker _faker = null!;

        [TestInitialize]
        public void TestInitialize()
        {
            _faker = new Faker();
        }

        [TestMethod]
        public void AuthorId_ImplementsIStronglyTypedIdInterface()
        {
            Type authorIdType = typeof(AuthorId);
            Type stronglyTypedIdInterfaceType = typeof(IStronglyTypedId<Guid>);

            Assert.IsTrue(stronglyTypedIdInterfaceType.IsAssignableFrom(authorIdType),
                message: $"{authorIdType.Name} does not implement {stronglyTypedIdInterfaceType.Name} interface.");
        }

        [TestMethod]
        public void NewId_ShouldReturnValidAuthorId()
        {
            AuthorId newAuthorId = AuthorId.NewId();

            Assert.AreNotEqual(AuthorId.Empty, newAuthorId);
            Assert.IsTrue(newAuthorId.HasValue);
        }

        [TestMethod]
        public void Empty_ShouldReturnAuthorIdWithEmptyGuid()
        {
            AuthorId emptyAuthorId = AuthorId.Empty;

            Assert.AreEqual(Guid.Empty, emptyAuthorId.Value);
            Assert.IsFalse(emptyAuthorId.HasValue);
        }

        [TestMethod]
        public void Constructor_WithoutParameters_ShouldCreateEmptyAuthorId()
        {
            AuthorId authorId = new();

            Assert.AreEqual(Guid.Empty, authorId.Value);
            Assert.IsFalse(authorId.HasValue);
        }

        [TestMethod]
        public void Constructor_WithGuidParameter_ShouldCreateAuthorIdWithGivenValue()
        {
            Guid guid = Guid.NewGuid();
            AuthorId authorId = new(guid);

            Assert.AreEqual(guid, authorId.Value);
            Assert.IsTrue(authorId.HasValue);
        }

        [TestMethod]
        public void Constructor_WithEmptyGuid_ShouldThrowInvalidGuidException()
        {
            Guid emptyGuid = Guid.Empty;

            Assert.ThrowsExactly<InvalidGuidException>(() => new AuthorId(emptyGuid));
        }

        [TestMethod]
        public void Constructor_WithStringParameter_ShouldCreateAuthorIdWithGivenValue()
        {
            Guid guid = Guid.NewGuid();
            string guidStr = guid.ToString();
            AuthorId authorId = new(guidStr);

            Assert.AreEqual(guid, authorId.Value);
            Assert.IsTrue(authorId.HasValue);
        }

        [TestMethod]
        public void Constructor_WithInvalidGuidString_ShouldThrowInvalidGuidException()
        {
            string invalidGuidStr = _faker.Random.Word();

            Assert.ThrowsExactly<InvalidGuidException>(() => new AuthorId(invalidGuidStr));
        }

        [TestMethod]
        public void ToString_ShouldReturnGuidStringRepresentation()
        {
            Guid guid = Guid.NewGuid();
            AuthorId authorId = new(guid);

            Assert.AreEqual(guid.ToString(), authorId.ToString());
        }

        [TestMethod]
        public void AuthorId_EqualityCheck_ShouldBeTrueForSameValues()
        {
            Guid guid = Guid.NewGuid();

            AuthorId authorId1 = new(guid);
            AuthorId authorId2 = new(guid);

            Assert.AreEqual(authorId1, authorId2);
        }

        [TestMethod]
        public void AuthorId_EqualityCheck_ShouldBeFalseForDifferentValues()
        {
            AuthorId authorId1 = new(Guid.NewGuid());
            AuthorId authorId2 = new(Guid.NewGuid());

            Assert.AreNotEqual(authorId1, authorId2);
        }

        [TestMethod]
        public void AuthorId_HashCode_ShouldBeSameForSameValues()
        {
            Guid guid = Guid.NewGuid();

            AuthorId authorId1 = new(guid);
            AuthorId authorId2 = new(guid);

            Assert.AreEqual(authorId1.GetHashCode(), authorId2.GetHashCode());
        }

        [TestMethod]
        public void AuthorId_HashCode_ShouldBeDifferentForDifferentValues()
        {
            AuthorId authorId1 = new(Guid.NewGuid());
            AuthorId authorId2 = new(Guid.NewGuid());

            Assert.AreNotEqual(authorId1.GetHashCode(), authorId2.GetHashCode());
        }
    }
}
