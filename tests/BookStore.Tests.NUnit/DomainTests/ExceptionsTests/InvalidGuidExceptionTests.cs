using BookStore.Domain.Exceptions;

namespace BookStore.Tests.NUnit.DomainTests.ExceptionsTests
{
    [TestFixture]
    public sealed class InvalidGuidExceptionTests
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
            InvalidGuidException exception = new(value);

            Assert.That(exception.Message, Is.EqualTo($"The provided value is not a valid GUID: {value}"));
        }

        [Test]
        public void ThrowIfInvalidGuid_GivenInvalidGuidString_ThenShouldThrowException()
        {
            string invalidGuid = _faker.Random.Word();

            InvalidGuidException exception = Assert
                .Throws<InvalidGuidException>(
                    () => InvalidGuidException.ThrowIfInvalidGuid(invalidGuid));

            Assert.That(exception.Message, Is.EqualTo($"The provided value is not a valid GUID: {invalidGuid}"));
        }

        [Test]
        public void ThrowIfInvalidGuid_GivenValidGuidString_ThenShouldNotThrowException()
        {
            string validGuid = Guid.NewGuid().ToString();

            Assert.DoesNotThrow(
                () => InvalidGuidException.ThrowIfInvalidGuid(validGuid));
        }

        [Test]
        public void ThrowIfEmpty_GivenEmptyGuid_ThenShouldThrowException()
        {
            Guid emptyGuid = Guid.Empty;

            InvalidGuidException exception = Assert
                .Throws<InvalidGuidException>(
                    () => InvalidGuidException.ThrowIfEmpty(emptyGuid));

            Assert.That(exception.Message, Is.EqualTo($"The provided value is not a valid GUID: {emptyGuid}"));
        }

        [Test]
        public void ThrowIfEmpty_GivenNonEmptyGuid_ThenShouldNotThrowException()
        {
            Guid nonEmptyGuid = Guid.NewGuid();

            Assert.DoesNotThrow(
                () => InvalidGuidException.ThrowIfEmpty(nonEmptyGuid));
        }
    }
}
