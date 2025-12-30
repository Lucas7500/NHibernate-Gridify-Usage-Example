using BookStore.Domain.Exceptions;
using BookStore.Domain.Models.AuthorModel;
using BookStore.Domain.ValueObjects;

namespace BookStore.Tests.MSTest.DomainTests.ModelsTests
{
    [TestClass]
    public sealed class AuthorTests
    {
        private Faker _faker = null!;

        [TestInitialize]
        public void TestInitialize()
        {
            _faker = new Faker();
        }

        [TestMethod]
        public void PartialArgsConstructor_GivenValidName_ShouldCreateAuthor()
        {
            string name = _faker.Name.FullName();
            Author author = new(name);

            Assert.AreNotEqual(Guid.Empty, author.Id.Value);
            Assert.AreEqual(name, author.Name);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(101)]
        public void PartialArgsConstructor_GivenInvalidName_ShouldThrowBusinessRuleValidationException(int invalidNameLength)
        {
            string name = _faker.Random.String(length: invalidNameLength);

            BusinessRuleValidationException exception = Assert.Throws<BusinessRuleValidationException>(() => new Author(name));

            Assert.IsNotNull(exception);
            Assert.AreEqual("businessrule.author-name-has-invalid-length", exception.Message);
        }

        [TestMethod]
        public void FullArgsConstructor_GivenValidIdAndName_ShouldCreateAuthor()
        {
            string name = _faker.Name.FullName();
            AuthorId id = AuthorId.NewId();
            Author author = new(id, name);

            Assert.AreEqual(id, author.Id);
            Assert.AreEqual(name, author.Name);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(101)]
        public void FullArgsConstructor_GivenInvalidName_ShouldThrowBusinessRuleValidationException(int invalidNameLength)
        {
            AuthorId id = AuthorId.NewId();
            string name = _faker.Random.String(length: invalidNameLength);

            BusinessRuleValidationException exception = Assert.Throws<BusinessRuleValidationException>(() => new Author(id, name));

            Assert.IsNotNull(exception);
            Assert.AreEqual("businessrule.author-name-has-invalid-length", exception.Message);
        }

        [TestMethod]
        public void ChangeName_GivenValidName_ShouldUpdateAuthorName()
        {
            string initialName = _faker.Name.FullName();
            Author author = new(initialName);

            string newName = _faker.Name.FullName();
            author.ChangeName(newName);

            Assert.AreEqual(newName, author.Name);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(101)]
        public void ChangeName_GivenInvalidName_ShouldThrowBusinessRuleValidationException(int invalidNameLength)
        {
            string initialName = _faker.Name.FullName();
            Author author = new(initialName);

            string invalidName = _faker.Random.String(length: invalidNameLength);
            BusinessRuleValidationException exception = Assert.Throws<BusinessRuleValidationException>(() => author.ChangeName(invalidName));

            Assert.IsNotNull(exception);
            Assert.AreEqual("businessrule.author-name-has-invalid-length", exception.Message);
        }
    }
}
