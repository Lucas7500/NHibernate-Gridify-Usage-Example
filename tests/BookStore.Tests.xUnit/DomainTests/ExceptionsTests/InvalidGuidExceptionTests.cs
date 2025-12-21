using BookStore.Domain.Exceptions;

namespace BookStore.Tests.xUnit.DomainTests.ExceptionsTests
{
    public static class InvalidGuidExceptionTests
    {
        private static readonly Faker _faker = new();

        public sealed class UsingStandardAssertions
        {
            [Fact]
            public void Constructor_GivenValue_ThenShouldSetMessage()
            {
                string value = _faker.Random.Word();
                InvalidGuidException exception = new(value);
                
                Assert.Equal($"The provided value is not a valid GUID: {value}", exception.Message);
            }

            [Fact]
            public void ThrowIfInvalidGuid_GivenInvalidGuidString_ThenShouldThrowException()
            {
                string invalidGuid = _faker.Random.Word();

                InvalidGuidException exception = Assert.Throws<InvalidGuidException>(
                    () => InvalidGuidException.ThrowIfInvalidGuid(invalidGuid));

                Assert.Equal($"The provided value is not a valid GUID: {invalidGuid}", exception.Message);
            }

            [Fact]
            public void ThrowIfInvalidGuid_GivenValidGuidString_ThenShouldNotThrowException()
            {
                string validGuid = Guid.NewGuid().ToString();

                Exception exception = Record.Exception(
                    () => InvalidGuidException.ThrowIfInvalidGuid(validGuid));
                
                Assert.Null(exception);
            }

            [Fact]
            public void ThrowIfEmpty_GivenEmptyGuid_ThenShouldThrowException()
            {
                Guid emptyGuid = Guid.Empty;
                
                InvalidGuidException exception = Assert.Throws<InvalidGuidException>(
                    () => InvalidGuidException.ThrowIfEmpty(emptyGuid));
                
                Assert.Equal($"The provided value is not a valid GUID: {emptyGuid}", exception.Message);
            }

            [Fact]
            public void ThrowIfEmpty_GivenNonEmptyGuid_ThenShouldNotThrowException()
            {
                Guid nonEmptyGuid = Guid.NewGuid();

                Exception exception = Record.Exception(
                    () => InvalidGuidException.ThrowIfEmpty(nonEmptyGuid));
                
                Assert.Null(exception);
            }
        }

        public sealed class UsingFluentAssertions
        {
            [Fact]
            public void Constructor_GivenValue_ThenShouldSetMessage()
            {
                string value = _faker.Random.Word();
                InvalidGuidException exception = new(value);
                
                exception.Message
                    .Should()
                    .Be($"The provided value is not a valid GUID: {value}");
            }

            [Fact]
            public void ThrowIfInvalidGuid_GivenInvalidGuidString_ThenShouldThrowException()
            {
                string invalidGuid = _faker.Random.Word();

                Action throwIfInvalidAction = () => InvalidGuidException.ThrowIfInvalidGuid(invalidGuid);
                
                throwIfInvalidAction.Should()
                    .ThrowExactly<InvalidGuidException>()
                    .WithMessage($"The provided value is not a valid GUID: {invalidGuid}");
            }

            [Fact]
            public void ThrowIfInvalidGuid_GivenValidGuidString_ThenShouldNotThrowException()
            {
                string validGuid = Guid.NewGuid().ToString();
                Action throwIfInvalidAction = () => InvalidGuidException.ThrowIfInvalidGuid(validGuid);
                throwIfInvalidAction.Should().NotThrow<InvalidGuidException>();
            }

            [Fact]
            public void ThrowIfEmpty_GivenEmptyGuid_ThenShouldThrowException()
            {
                Guid emptyGuid = Guid.Empty;
                Action throwIfEmptyAction = () => InvalidGuidException.ThrowIfEmpty(emptyGuid);
                
                throwIfEmptyAction.Should()
                    .ThrowExactly<InvalidGuidException>()
                    .WithMessage($"The provided value is not a valid GUID: {emptyGuid}");
            }

            [Fact]
            public void ThrowIfEmpty_GivenNonEmptyGuid_ThenShouldNotThrowException()
            {
                Guid nonEmptyGuid = Guid.NewGuid();
                Action throwIfEmptyAction = () => InvalidGuidException.ThrowIfEmpty(nonEmptyGuid);
                throwIfEmptyAction.Should().NotThrow<InvalidGuidException>();
            }
        }
    }
}
