using BookStore.Domain.Exceptions;

namespace BookStore.Tests.NUnit.DomainTests.ExceptionsTests
{
    [TestFixture]
    public sealed class BusinessRuleValidationExceptionTests
    {
        private Faker _faker;

        [SetUp]
        public void SetUp()
        {
            _faker = new Faker();
        }

        [Test]
        public void Constructor_GivenMessage_ThenShouldSetMessage()
        {
            string message = _faker.Lorem.Paragraph();
            BusinessRuleValidationException exception = new(message);
            Assert.That(exception.Message, Is.EqualTo(message));
        }

        [Test]
        public void Constructor_GivenMessageAndInnerException_ThenShouldSetBoth()
        {
            string message = _faker.Lorem.Paragraph();

            Exception innerException = new();
            BusinessRuleValidationException exception = new(message, innerException);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(exception.Message, Is.EqualTo(message));
                Assert.That(exception.InnerException, Is.EqualTo(innerException));
            }
        }

        [Test]
        public void ThrowIf_GivenTrueCondition_ThenShouldThrowException()
        {
            bool condition = true;
            string message = _faker.Lorem.Paragraph();

            BusinessRuleValidationException exception = Assert.Throws<BusinessRuleValidationException>(
                () => BusinessRuleValidationException.ThrowIf(condition, message));

            Assert.That(exception.Message, Is.EqualTo(message));
        }

        [Test]
        public void ThrowIf_GivenFalseCondition_ThenShouldNotThrowException()
        {
            bool condition = false;
            string message = _faker.Lorem.Paragraph();

            Assert.DoesNotThrow(
                () => BusinessRuleValidationException.ThrowIf(condition, message));
        }
    }
}
