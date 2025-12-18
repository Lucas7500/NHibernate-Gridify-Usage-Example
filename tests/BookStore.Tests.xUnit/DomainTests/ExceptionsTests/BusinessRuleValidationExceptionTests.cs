using BookStore.Domain.Exceptions;

namespace BookStore.Tests.xUnit.DomainTests.ExceptionsTests
{
    public static class BusinessRuleValidationExceptionTests
    {
        private static readonly Faker _faker = new();

        public class UsingStandardAssertions
        {
            [Fact]
            public void ThrowIf_GivenTrueCondition_ThenShouldThrowException()
            {
                bool condition = true;
                string message = _faker.Lorem.Paragraph();

                BusinessRuleValidationException exception = Assert
                    .Throws<BusinessRuleValidationException>(
                        () => BusinessRuleValidationException.ThrowIf(condition, message));

                Assert.Equal(message, exception.Message);
            }

            [Fact]
            public void ThrowIf_GivenFalseCondition_ThenShouldNotThrowException()
            {
                bool condition = false;
                string message = _faker.Lorem.Paragraph();

                BusinessRuleValidationException.ThrowIf(condition, message);

                Exception exception = Record.Exception(
                    () => BusinessRuleValidationException.ThrowIf(condition, message));

                Assert.Null(exception);
            }
        }
        
        public class UsingFluentAssertions
        {
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
