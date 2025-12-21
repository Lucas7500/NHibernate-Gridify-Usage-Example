using BookStore.Domain.Exceptions;

namespace BookStore.Tests.MSTest.DomainTests.ExceptionsTests
{
    [TestClass]
    public sealed class InvalidGuidExceptionTests
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
            InvalidGuidException exception = new(value);

            Assert.AreEqual($"The provided value is not a valid GUID: {value}", exception.Message);
        }

        [TestMethod]
        public void ThrowIfInvalidGuid_GivenInvalidGuidString_ThenShouldThrowException()
        {
            string invalidGuid = _faker.Random.Word();

            InvalidGuidException exception = Assert.ThrowsExactly<InvalidGuidException>(
                () => InvalidGuidException.ThrowIfInvalidGuid(invalidGuid));

            Assert.AreEqual($"The provided value is not a valid GUID: {invalidGuid}", exception.Message);
        }

        [TestMethod]
        public void ThrowIfInvalidGuid_GivenValidGuidString_ThenShouldNotThrowException()
        {
            string validGuid = Guid.NewGuid().ToString();

            Exception? exception = null;

            try
            {
                InvalidGuidException.ThrowIfInvalidGuid(validGuid);

            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsNull(exception);
        }

        [TestMethod]
        public void ThrowIfEmpty_GivenEmptyGuid_ThenShouldThrowException()
        {
            Guid emptyGuid = Guid.Empty;

            InvalidGuidException exception = Assert.ThrowsExactly<InvalidGuidException>(
                () => InvalidGuidException.ThrowIfEmpty(emptyGuid));

            Assert.AreEqual($"The provided value is not a valid GUID: {emptyGuid}", exception.Message);
        }

        [TestMethod]
        public void ThrowIfEmpty_GivenNonEmptyGuid_ThenShouldNotThrowException()
        {
            Guid nonEmptyGuid = Guid.NewGuid();

            Exception? exception = null;

            try
            {
                InvalidGuidException.ThrowIfEmpty(nonEmptyGuid);

            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsNull(exception);
        }
    }
}
