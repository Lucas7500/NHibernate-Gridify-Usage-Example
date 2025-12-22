using BookStore.Domain.Exceptions;
using BookStore.Domain.ValueObjects;
using BookStore.Domain.ValueObjects.Contracts;

namespace BookStore.Tests.NUnit.DomainTests.ValueObjectsTests
{
    [TestFixture]
    public sealed class AuthorIdTests
    {
        public Faker _faker;

        [SetUp]
        public void SetUp()
        {
            _faker = new Faker();
        }

        [Test]
        public void AuthorId_ImplementsIStronglyTypedIdInterface()
        {
            Type authorIdType = typeof(AuthorId);
            Type stronglyTypedIdInterfaceType = typeof(IStronglyTypedId<Guid>);

            Assert.That(stronglyTypedIdInterfaceType.IsAssignableFrom(authorIdType), Is.True, 
                message: $"{authorIdType.Name} does not implement {stronglyTypedIdInterfaceType.Name} interface.");
        }

        [Test]
        public void NewId_ShouldReturnValidAuthorId()
        {
            AuthorId newAuthorId = AuthorId.NewId();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(newAuthorId, Is.Not.EqualTo(AuthorId.Empty));
                Assert.That(newAuthorId.HasValue, Is.True);
            }
        }

        [Test]
        public void Empty_ShouldReturnAuthorIdWithEmptyGuid()
        {
            AuthorId emptyAuthorId = AuthorId.Empty;

            using (Assert.EnterMultipleScope())
            {
                Assert.That(emptyAuthorId.Value, Is.EqualTo(Guid.Empty));
                Assert.That(emptyAuthorId.HasValue, Is.False);
            }
        }

        [Test]
        public void Constructor_WithoutParameters_ShouldCreateEmptyAuthorId()
        {
            AuthorId authorId = new();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(authorId.Value, Is.EqualTo(Guid.Empty));
                Assert.That(authorId.HasValue, Is.False);
            }
        }

        [Test]
        public void Constructor_WithGuidParameter_ShouldCreateAuthorIdWithGivenValue()
        {
            Guid guid = Guid.NewGuid();
            AuthorId authorId = new(guid);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(authorId.Value, Is.EqualTo(guid));
                Assert.That(authorId.HasValue, Is.True);
            }
        }

        [Test]
        public void Constructor_WithEmptyGuid_ShouldThrowInvalidGuidException()
        {
            Guid emptyGuid = Guid.Empty;

            Assert.Throws<InvalidGuidException>(() => new AuthorId(emptyGuid));
        }

        [Test]
        public void Constructor_WithStringParameter_ShouldCreateAuthorIdWithGivenValue()
        {
            Guid guid = Guid.NewGuid();
            string guidStr = guid.ToString();
            AuthorId authorId = new(guidStr);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(guid, Is.EqualTo(authorId.Value));
                Assert.That(authorId.HasValue, Is.True);
            }
        }

        [Test]
        public void Constructor_WithInvalidGuidString_ShouldThrowInvalidGuidException()
        {
            string invalidGuidStr = _faker.Random.Word();

            Assert.Throws<InvalidGuidException>(() => new AuthorId(invalidGuidStr));
        }

        [Test]
        public void ToString_ShouldReturnGuidStringRepresentation()
        {
            Guid guid = Guid.NewGuid();
            AuthorId authorId = new(guid);

            Assert.That(authorId.ToString(), Is.EqualTo(guid.ToString()));
        }

        [Test]
        public void AuthorId_EqualityCheck_ShouldBeTrueForSameValues()
        {
            Guid guid = Guid.NewGuid();

            AuthorId authorId1 = new(guid);
            AuthorId authorId2 = new(guid);

            Assert.That(authorId2, Is.EqualTo(authorId1));
        }

        [Test]
        public void AuthorId_EqualityCheck_ShouldBeFalseForDifferentValues()
        {
            AuthorId authorId1 = new(Guid.NewGuid());
            AuthorId authorId2 = new(Guid.NewGuid());

            Assert.That(authorId1, Is.Not.EqualTo(authorId2));
        }

        [Test]
        public void AuthorId_HashCode_ShouldBeSameForSameValues()
        {
            Guid guid = Guid.NewGuid();

            AuthorId authorId1 = new(guid);
            AuthorId authorId2 = new(guid);

            Assert.That(authorId1.GetHashCode(), Is.EqualTo(authorId2.GetHashCode()));
        }

        [Test]
        public void AuthorId_HashCode_ShouldBeDifferentForDifferentValues()
        {
            AuthorId authorId1 = new(Guid.NewGuid());
            AuthorId authorId2 = new(Guid.NewGuid());

            Assert.That(authorId1.GetHashCode(), Is.Not.EqualTo(authorId2.GetHashCode()));
        }
    }
}
