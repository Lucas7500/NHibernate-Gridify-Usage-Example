using BookStore.Domain.Exceptions;

namespace BookStore.Tests.xUnit.DomainTests.ExceptionsTests
{
    public static class BusinessRuleValidationExceptionTests
    {
        private static readonly Faker _faker = new();

        public sealed class UsingStandardAssertions
        {
            [Fact]
            public void Constructor_GivenMessage_ThenShouldSetMessage()
            {
                string message = _faker.Lorem.Paragraph();
                BusinessRuleValidationException exception = new(message);
                Assert.Equal(message, exception.Message);
            }
            
            [Fact]
            public void Constructor_GivenMessageAndInnerException_ThenShouldSetBoth()
            {
                string message = _faker.Lorem.Paragraph();

                Exception innerException = new();
                BusinessRuleValidationException exception = new(message, innerException);
                
                Assert.Equal(message, exception.Message);
                Assert.Equal(innerException, exception.InnerException);
            }

            [Fact]
            public void ThrowIf_GivenTrueCondition_ThenShouldThrowException()
            {
                bool condition = true;
                string message = _faker.Lorem.Paragraph();

                BusinessRuleValidationException exception = Assert.Throws<BusinessRuleValidationException>(
                    () => BusinessRuleValidationException.ThrowIf(condition, message));

                Assert.Equal(message, exception.Message);
            }

            [Fact]
            public void ThrowIf_GivenFalseCondition_ThenShouldNotThrowException()
            {
                bool condition = false;
                string message = _faker.Lorem.Paragraph();

                Exception exception = Record.Exception(
                    () => BusinessRuleValidationException.ThrowIf(condition, message));

                Assert.Null(exception);
            }
        }
        
        public sealed class UsingFluentAssertions
        {
            [Fact]
            public void Constructor_GivenMessage_ThenShouldSetMessage()
            {
                string message = _faker.Lorem.Paragraph();
                BusinessRuleValidationException exception = new(message);
                
                exception.Message.Should().Be(message);
            }

            [Fact]
            public void Constructor_GivenMessageAndInnerException_ThenShouldSetBoth()
            {
                string message = _faker.Lorem.Paragraph();

                Exception innerException = new();
                BusinessRuleValidationException exception = new(message, innerException);
                
                exception.Message.Should().Be(message);
                exception.InnerException.Should().Be(innerException);
            }

            [Fact]
            public void ThrowIf_GivenTrueCondition_ThenShouldThrowException()
            {
                bool condition = true;
                string message = _faker.Lorem.Paragraph();

                Action throwsIfAction = () =>
                    BusinessRuleValidationException.ThrowIf(condition, message);
                
                throwsIfAction.Should()
                    .ThrowExactly<BusinessRuleValidationException>()
                    .WithMessage(message);
            }

            [Fact]
            public void ThrowIf_GivenFalseCondition_ThenShouldNotThrowException()
            {
                bool condition = false;
                string message = _faker.Lorem.Paragraph();

                Action throwsIfAction = () =>
                    BusinessRuleValidationException.ThrowIf(condition, message);

                throwsIfAction.Should().NotThrow<BusinessRuleValidationException>();
            }
        }
    }
}
