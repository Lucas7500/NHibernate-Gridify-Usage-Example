using BookStore.Domain.Exceptions;

namespace BookStore.Tests.MSTest.DomainTests.ExceptionsTests
{
    [TestClass]
    public sealed class BusinessRuleValidationExceptionTests
    {
        private Faker _faker = null!;

        [TestInitialize]
        public void Initialize()
        {
            _faker = new Faker();
        }

        [TestMethod]
        public void Constructor_GivenMessage_ThenShouldSetMessage()
        {
            string message = _faker.Lorem.Paragraph();
            BusinessRuleValidationException exception = new(message);
            Assert.AreEqual(message, exception.Message);
        }

        [TestMethod]
        public void Constructor_GivenMessageAndInnerException_ThenShouldSetBoth()
        {
            string message = _faker.Lorem.Paragraph();

            Exception innerException = new();
            BusinessRuleValidationException exception = new(message, innerException);

            Assert.AreEqual(message, exception.Message);
            Assert.AreEqual(innerException, exception.InnerException);
        }

        [TestMethod]
        public void ThrowIf_GivenTrueCondition_ThenShouldThrowException()
        {
            bool condition = true;
            string message = _faker.Lorem.Paragraph();

            BusinessRuleValidationException exception = Assert.ThrowsExactly<BusinessRuleValidationException>(
                    () => BusinessRuleValidationException.ThrowIf(condition, message));

            Assert.AreEqual(message, exception.Message);
        }

        [TestMethod]
        public void ThrowIf_GivenFalseCondition_ThenShouldNotThrowException()
        {
            bool condition = false;
            string message = _faker.Lorem.Paragraph();

            Exception? exception = null;

            try
            {
                BusinessRuleValidationException.ThrowIf(condition, message);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsNull(exception);
        }
    }
}
