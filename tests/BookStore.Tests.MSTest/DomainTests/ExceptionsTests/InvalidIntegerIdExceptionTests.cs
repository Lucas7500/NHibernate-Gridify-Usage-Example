using BookStore.Domain.Exceptions;

namespace BookStore.Tests.MSTest.DomainTests.ExceptionsTests
{
    [TestClass]
    public sealed class InvalidIntegerIdExceptionTests
    {
        private Faker _faker = null!;

        [TestInitialize]
        public void Initialize()
        {
            _faker = new Faker();
        }

        [TestMethod]
        public void Constructor_GivenValue_ThenShouldSetMessage()
        {
            string value = _faker.Random.Word();
            InvalidIntegerIdException exception = new(value);

            Assert.AreEqual($"The provided value is not a valid integer id: {value}", exception.Message);
        }

        [TestMethod]
        public void ThrowIfNegativeOrZero_GivenNegativeIntegerId_ThenShouldThrowException()
        {
            int invalidIntegerId = _faker.Random.Int(max: -1);

            InvalidIntegerIdException exception = Assert.Throws<InvalidIntegerIdException>(
                () => InvalidIntegerIdException.ThrowIfNegativeOrZero(invalidIntegerId));

            Assert.AreEqual($"The provided value is not a valid integer id: {invalidIntegerId}", exception.Message);
        }

        [TestMethod]
        public void ThrowIfNegativeOrZero_GivenZeroAsIntegerId_ThenShouldThrowException()
        {
            int invalidIntegerId = 0;

            InvalidIntegerIdException exception = Assert.Throws<InvalidIntegerIdException>(
                () => InvalidIntegerIdException.ThrowIfNegativeOrZero(invalidIntegerId));

            Assert.AreEqual($"The provided value is not a valid integer id: {invalidIntegerId}", exception.Message);
        }

        [TestMethod]
        public void ThrowIfNegativeOrZero_GivenInvalidIntegerString_ThenShouldThrowException()
        {
            string invalidIntegerIdStr = _faker.Random.Word();

            InvalidIntegerIdException exception = Assert.Throws<InvalidIntegerIdException>(
                () => InvalidIntegerIdException.ThrowIfNegativeOrZero(invalidIntegerIdStr));

            Assert.AreEqual($"The provided value is not a valid integer id: {invalidIntegerIdStr}", exception.Message);
        }

        [TestMethod]
        public void ThrowIfNegativeOrZero_GivenNegativeIntegerString_ThenShouldThrowException()
        {
            string invalidIntegerIdStr = _faker.Random.Int(max: -1).ToString();

            InvalidIntegerIdException exception = Assert.Throws<InvalidIntegerIdException>(
                () => InvalidIntegerIdException.ThrowIfNegativeOrZero(invalidIntegerIdStr));

            Assert.AreEqual($"The provided value is not a valid integer id: {invalidIntegerIdStr}", exception.Message);
        }

        [TestMethod]
        public void ThrowIfNegativeOrZero_GivenPositiveInteger_ThenShouldNotThrowException()
        {
            int positiveInteger = _faker.Random.Number(int.MaxValue);

            Exception? exception = null;

            try
            {
                InvalidIntegerIdException.ThrowIfNegativeOrZero(positiveInteger);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsNull(exception);
        }
    }
}
