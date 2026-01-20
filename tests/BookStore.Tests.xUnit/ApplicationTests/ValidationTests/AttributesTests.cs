using BookStore.Application.Validation.Attributes;
using System.ComponentModel.DataAnnotations;

using SystemValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace BookStore.Tests.xUnit.ApplicationTests.ValidationTests
{
    public static class AttributesTests
    {
        private static readonly Faker _faker = new();

        public sealed class UsingStandardAssertions
        {
            [Fact]
            public void IntegerIdAttribute_GivenNegativeOrZeroValue_ShouldReturnValidationError()
            {
                IntegerIdAttribute attribute = new();
                ValidationContext validationContext = new(new object());

                SystemValidationResult? resultNegative = attribute.GetValidationResult(_faker.Random.Int(max: -1), validationContext);
                SystemValidationResult? resultZero = attribute.GetValidationResult(0, validationContext);
                
                Assert.NotNull(resultNegative);
                Assert.Equal("Integer Id cannot be negative or zero.", resultNegative.ErrorMessage);
                
                Assert.NotNull(resultZero);
                Assert.Equal("Integer Id cannot be negative or zero.", resultZero.ErrorMessage);
            }
            
            [Fact]
            public void IntegerIdAttribute_GivenPositiveValue_ShouldReturnValidationSuccess()
            {
                IntegerIdAttribute attribute = new();
                ValidationContext validationContext = new(new object());

                SystemValidationResult? resultPositive = attribute.GetValidationResult(_faker.Random.Number(int.MaxValue), validationContext);
                
                Assert.Null(resultPositive);
            }

            [Fact]
            public void NonNegativeAttribute_GivenNegativeValue_ShouldReturnValidationError()
            {
                NonNegativeAttribute attribute = new();
                ValidationContext validationContext = new(new object());
                SystemValidationResult? resultNegative = attribute.GetValidationResult(-0.01m, validationContext);
                
                Assert.NotNull(resultNegative);
                Assert.Equal("Value must be non-negative.", resultNegative.ErrorMessage);
            }

            [Fact]
            public void NonNegativeAttribute_GivenZeroOrPositiveValue_ShouldReturnValidationSuccess()
            {
                NonNegativeAttribute attribute = new();
                ValidationContext validationContext = new(new object());
                
                SystemValidationResult? resultZero = attribute.GetValidationResult(0m, validationContext);
                SystemValidationResult? resultPositive = attribute.GetValidationResult(_faker.Random.Decimal(0.01m, decimal.MaxValue), validationContext);
                
                Assert.Null(resultZero);
                Assert.Null(resultPositive);
            }

            [Fact]
            public void NotEmptyGuidAttribute_GivenEmptyGuid_ShouldReturnValidationError()
            {
                NotEmptyGuidAttribute attribute = new();
                ValidationContext validationContext = new(new object());

                SystemValidationResult? result = attribute.GetValidationResult(Guid.Empty, validationContext);
                
                Assert.NotNull(result);
                Assert.Equal("Guid cannot be empty.", result.ErrorMessage);
            }

            [Fact]
            public void NotEmptyGuidAttribute_GivenNonEmptyGuid_ShouldReturnValidationSuccess()
            {
                NotEmptyGuidAttribute attribute = new();
                ValidationContext validationContext = new(new object());
                
                SystemValidationResult? result = attribute.GetValidationResult(Guid.NewGuid(), validationContext);
                
                Assert.Null(result);
            }
        }
        
        public sealed class UsingFluentAssertions
        {
            [Fact]
            public void IntegerIdAttribute_GivenNegativeOrZeroValue_ShouldReturnValidationError()
            {
                IntegerIdAttribute attribute = new();
                ValidationContext validationContext = new(new object());

                SystemValidationResult? resultNegative = attribute.GetValidationResult(_faker.Random.Int(max: -1), validationContext);
                SystemValidationResult? resultZero = attribute.GetValidationResult(0, validationContext);

                resultNegative.Should().NotBeNull();
                resultNegative.ErrorMessage.Should().Be("Integer Id cannot be negative or zero.");

                resultZero.Should().NotBeNull();
                resultZero.ErrorMessage.Should().Be("Integer Id cannot be negative or zero.");
            }

            [Fact]
            public void IntegerIdAttribute_GivenPositiveValue_ShouldReturnValidationSuccess()
            {
                IntegerIdAttribute attribute = new();
                ValidationContext validationContext = new(new object());

                SystemValidationResult? resultPositive = attribute.GetValidationResult(_faker.Random.Number(int.MaxValue), validationContext);

                resultPositive.Should().BeNull();
            }

            [Fact]
            public void NonNegativeAttribute_GivenNegativeValue_ShouldReturnValidationError()
            {
                NonNegativeAttribute attribute = new();
                ValidationContext validationContext = new(new object());
                SystemValidationResult? resultNegative = attribute.GetValidationResult(-0.01m, validationContext);

                resultNegative.Should().NotBeNull();
                resultNegative.ErrorMessage.Should().Be("Value must be non-negative.");
            }

            [Fact]
            public void NonNegativeAttribute_GivenZeroOrPositiveValue_ShouldReturnValidationSuccess()
            {
                NonNegativeAttribute attribute = new();
                ValidationContext validationContext = new(new object());

                SystemValidationResult? resultZero = attribute.GetValidationResult(0m, validationContext);
                SystemValidationResult? resultPositive = attribute.GetValidationResult(_faker.Random.Decimal(0.01m, decimal.MaxValue), validationContext);

                resultZero.Should().BeNull();
                resultPositive.Should().BeNull();
            }

            [Fact]
            public void NotEmptyGuidAttribute_GivenEmptyGuid_ShouldReturnValidationError()
            {
                NotEmptyGuidAttribute attribute = new();
                ValidationContext validationContext = new(new object());

                SystemValidationResult? result = attribute.GetValidationResult(Guid.Empty, validationContext);

                result.Should().NotBeNull();
                result.ErrorMessage.Should().Be("Guid cannot be empty.");
            }

            [Fact]
            public void NotEmptyGuidAttribute_GivenNonEmptyGuid_ShouldReturnValidationSuccess()
            {
                NotEmptyGuidAttribute attribute = new();
                ValidationContext validationContext = new(new object());

                SystemValidationResult? result = attribute.GetValidationResult(Guid.NewGuid(), validationContext);

                result.Should().BeNull();
            }
        }
    }
}
