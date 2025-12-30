using BookStore.Domain.Exceptions;
using BookStore.Domain.Models.AuthorModel;
using BookStore.Domain.ValueObjects;

namespace BookStore.Tests.NUnit.DomainTests.ModelsTests
{
    public sealed class AuthorTests
    {
        private Faker _faker;

        [SetUp]
        public void Setup()
        {
            _faker = new Faker();
        }

        [Test]
        public void PartialArgsConstructor_GivenValidName_ShouldCreateAuthor()
        {
            string name = _faker.Name.FullName();
            Author author = new(name);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(author.Id.Value, Is.Not.Empty);
                Assert.That(author.Name, Is.EqualTo(name));
            }
        }

        [Test]
        [TestCase(1)]
        [TestCase(101)]
        public void PartialArgsConstructor_GivenInvalidName_ShouldThrowBusinessRuleValidationException(int invalidNameLength)
        {
            string name = _faker.Random.String(length: invalidNameLength);

            BusinessRuleValidationException exception = Assert.Throws<BusinessRuleValidationException>(() => new Author(name));

            Assert.That(exception, Is.Not.Null);
            Assert.That(exception.Message, Is.EqualTo("businessrule.author-name-has-invalid-length"));
        }

        [Test]
        public void FullArgsConstructor_GivenValidIdAndName_ShouldCreateAuthor()
        {
            string name = _faker.Name.FullName();
            AuthorId id = AuthorId.NewId();
            Author author = new(id, name);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(author.Id, Is.EqualTo(id));
                Assert.That(author.Name, Is.EqualTo(name));
            }
        }

        [Test]
        [TestCase(1)]
        [TestCase(101)]
        public void FullArgsConstructor_GivenInvalidName_ShouldThrowBusinessRuleValidationException(int invalidNameLength)
        {
            AuthorId id = AuthorId.NewId();
            string name = _faker.Random.String(length: invalidNameLength);

            BusinessRuleValidationException exception = Assert.Throws<BusinessRuleValidationException>(() => new Author(id, name));

            Assert.That(exception, Is.Not.Null);
            Assert.That(exception.Message, Is.EqualTo("businessrule.author-name-has-invalid-length"));
        }

        [Test]
        public void ChangeName_GivenValidName_ShouldUpdateAuthorName()
        {
            string initialName = _faker.Name.FullName();
            Author author = new(initialName);

            string newName = _faker.Name.FullName();
            author.ChangeName(newName);

            Assert.That(author.Name, Is.EqualTo(newName));
        }

        [Test]
        [TestCase(1)]
        [TestCase(101)]
        public void ChangeName_GivenInvalidName_ShouldThrowBusinessRuleValidationException(int invalidNameLength)
        {
            string initialName = _faker.Name.FullName();
            Author author = new(initialName);

            string invalidName = _faker.Random.String(length: invalidNameLength);
            BusinessRuleValidationException exception = Assert.Throws<BusinessRuleValidationException>(() => author.ChangeName(invalidName));

            Assert.That(exception, Is.Not.Null);
            Assert.That(exception.Message, Is.EqualTo("businessrule.author-name-has-invalid-length"));
        }
    }
}
