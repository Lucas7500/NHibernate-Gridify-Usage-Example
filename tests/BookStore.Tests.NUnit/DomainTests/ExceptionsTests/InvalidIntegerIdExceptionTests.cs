using BookStore.Domain.Exceptions;

namespace BookStore.Tests.NUnit.DomainTests.ExceptionsTests
{
    [TestFixture]
    public sealed class InvalidIntegerIdExceptionTests
    {
        private Faker _faker;

        [SetUp]
        public void SetUp()
        {
            _faker = new Faker();
        }

        [Test]
        public void Constructor_GivenValue_ThenShouldSetMessage()
        {
            string value = _faker.Random.Word();
            InvalidIntegerIdException exception = new(value);

            Assert.That(exception.Message, Is.EqualTo($"The provided value is not a valid integer id: {value}"));
        }

        [Test]
        public void ThrowIfNegativeOrZero_GivenNegativeIntegerId_ThenShouldThrowException()
        {
            int invalidIntegerId = _faker.Random.Int(max: -1);

            InvalidIntegerIdException exception = Assert.Throws<InvalidIntegerIdException>(
                    () => InvalidIntegerIdException.ThrowIfNegativeOrZero(invalidIntegerId));

            Assert.That(exception.Message, Is.EqualTo($"The provided value is not a valid integer id: {invalidIntegerId}"));
        }

        [Test]
        public void ThrowIfNegativeOrZero_GivenZeroAsIntegerId_ThenShouldThrowException()
        {
            int invalidIntegerId = 0;

            InvalidIntegerIdException exception = Assert.Throws<InvalidIntegerIdException>(
                   () => InvalidIntegerIdException.ThrowIfNegativeOrZero(invalidIntegerId));

            Assert.That(exception.Message, Is.EqualTo($"The provided value is not a valid integer id: {invalidIntegerId}"));
        }

        [Test]
        public void ThrowIfNegativeOrZero_GivenInvalidIntegerString_ThenShouldThrowException()
        {
            string invalidIntegerIdStr = _faker.Random.Word();

            InvalidIntegerIdException exception = Assert.Throws<InvalidIntegerIdException>(
                   () => InvalidIntegerIdException.ThrowIfNegativeOrZero(invalidIntegerIdStr));

            Assert.That(exception.Message, Is.EqualTo($"The provided value is not a valid integer id: {invalidIntegerIdStr}"));
        }

        [Test]
        public void ThrowIfNegativeOrZero_GivenNegativeIntegerString_ThenShouldThrowException()
        {
            string invalidIntegerIdStr = _faker.Random.Int(max: -1).ToString();

            InvalidIntegerIdException exception = Assert.Throws<InvalidIntegerIdException>(
                   () => InvalidIntegerIdException.ThrowIfNegativeOrZero(invalidIntegerIdStr));

            Assert.That(exception.Message, Is.EqualTo($"The provided value is not a valid integer id: {invalidIntegerIdStr}"));
        }

        [Test]
        public void ThrowIfNegativeOrZero_GivenPositiveInteger_ThenShouldNotThrowException()
        {
            int positiveInteger = _faker.Random.Number(int.MaxValue);

            Assert.DoesNotThrow(
                () => InvalidIntegerIdException.ThrowIfNegativeOrZero(positiveInteger));
        }
    }
}
