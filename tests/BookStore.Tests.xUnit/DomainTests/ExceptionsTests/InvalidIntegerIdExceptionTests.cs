using BookStore.Domain.Exceptions;

namespace BookStore.Tests.xUnit.DomainTests.ExceptionsTests
{
    public static class InvalidIntegerIdExceptionTests
    {
        private static readonly Faker _faker = new();

        public sealed class UsingStandardAssertions
        {
            [Fact]
            public void Constructor_GivenValue_ThenShouldSetMessage()
            {
                string value = _faker.Random.Word();
                InvalidIntegerIdException exception = new(value);

                Assert.Equal($"The provided value is not a valid integer id: {value}", exception.Message);
            }

            [Fact]
            public void ThrowIfNegativeOrZero_GivenNegativeIntegerId_ThenShouldThrowException()
            {
                int invalidIntegerId = _faker.Random.Int(max: -1);

                InvalidIntegerIdException exception = Assert.Throws<InvalidIntegerIdException>(
                    () => InvalidIntegerIdException.ThrowIfNegativeOrZero(invalidIntegerId));

                Assert.Equal($"The provided value is not a valid integer id: {invalidIntegerId}", exception.Message);
            }

            [Fact]
            public void ThrowIfNegativeOrZero_GivenZeroAsIntegerId_ThenShouldThrowException()
            {
                int invalidIntegerId = 0;

                InvalidIntegerIdException exception = Assert.Throws<InvalidIntegerIdException>(
                    () => InvalidIntegerIdException.ThrowIfNegativeOrZero(invalidIntegerId));

                Assert.Equal($"The provided value is not a valid integer id: {invalidIntegerId}", exception.Message);
            }
            
            [Fact]
            public void ThrowIfNegativeOrZero_GivenInvalidIntegerString_ThenShouldThrowException()
            {
                string invalidIntegerIdStr = _faker.Random.Word();

                InvalidIntegerIdException exception = Assert.Throws<InvalidIntegerIdException>(
                    () => InvalidIntegerIdException.ThrowIfNegativeOrZero(invalidIntegerIdStr));

                Assert.Equal($"The provided value is not a valid integer id: {invalidIntegerIdStr}", exception.Message);
            }
            
            [Fact]
            public void ThrowIfNegativeOrZero_GivenNegativeIntegerString_ThenShouldThrowException()
            {
                string invalidIntegerIdStr = _faker.Random.Int(max: -1).ToString();

                InvalidIntegerIdException exception = Assert.Throws<InvalidIntegerIdException>(
                    () => InvalidIntegerIdException.ThrowIfNegativeOrZero(invalidIntegerIdStr));

                Assert.Equal($"The provided value is not a valid integer id: {invalidIntegerIdStr}", exception.Message);
            }

            [Fact]
            public void ThrowIfNegativeOrZero_GivenPositiveInteger_ThenShouldNotThrowException()
            {
                int positiveInteger = _faker.Random.Number(int.MaxValue);

                Exception exception = Record.Exception(
                    () => InvalidIntegerIdException.ThrowIfNegativeOrZero(positiveInteger));

                Assert.Null(exception);
            }
        }

        public sealed class UsingFluentAssertions
        {
            [Fact]
            public void Constructor_GivenValue_ThenShouldSetMessage()
            {
                string value = _faker.Random.Word();
                InvalidIntegerIdException exception = new(value);

                exception.Message
                    .Should()
                    .Be($"The provided value is not a valid integer id: {value}");
            }

            [Fact]
            public void ThrowIfNegativeOrZero_GivenNegativeIntegerId_ThenShouldThrowException()
            {
                int invalidIntegerId = _faker.Random.Int(max: -1);

                Action throwIfAction = () => InvalidIntegerIdException.ThrowIfNegativeOrZero(invalidIntegerId);

                throwIfAction
                    .Should()
                    .ThrowExactly<InvalidIntegerIdException>()
                    .WithMessage($"The provided value is not a valid integer id: {invalidIntegerId}");
            }

            [Fact]
            public void ThrowIfNegativeOrZero_GivenZeroAsIntegerId_ThenShouldThrowException()
            {
                int invalidIntegerId = 0;

                Action throwIfAction = () => InvalidIntegerIdException.ThrowIfNegativeOrZero(invalidIntegerId);

                throwIfAction
                    .Should()
                    .ThrowExactly<InvalidIntegerIdException>()
                    .WithMessage($"The provided value is not a valid integer id: {invalidIntegerId}");
            }

            [Fact]
            public void ThrowIfNegativeOrZero_GivenInvalidIntegerString_ThenShouldThrowException()
            {
                string invalidIntegerIdStr = _faker.Random.Word();

                Action throwIfAction = () => InvalidIntegerIdException.ThrowIfNegativeOrZero(invalidIntegerIdStr);

                throwIfAction
                    .Should()
                    .ThrowExactly<InvalidIntegerIdException>()
                    .WithMessage($"The provided value is not a valid integer id: {invalidIntegerIdStr}");
            }

            [Fact]
            public void ThrowIfNegativeOrZero_GivenNegativeIntegerString_ThenShouldThrowException()
            {
                string invalidIntegerIdStr = _faker.Random.Int(max: -1).ToString();

                Action throwIfAction = () => InvalidIntegerIdException.ThrowIfNegativeOrZero(invalidIntegerIdStr);

                throwIfAction
                    .Should()
                    .ThrowExactly<InvalidIntegerIdException>()
                    .WithMessage($"The provided value is not a valid integer id: {invalidIntegerIdStr}");
            }

            [Fact]
            public void ThrowIfNegativeOrZero_GivenPositiveInteger_ThenShouldNotThrowException()
            {
                int positiveInteger = _faker.Random.Number(int.MaxValue);
                Action throwIfAction = () => InvalidIntegerIdException.ThrowIfNegativeOrZero(positiveInteger);
                throwIfAction.Should().NotThrow<InvalidIntegerIdException>();
            }
        }
    }
}
