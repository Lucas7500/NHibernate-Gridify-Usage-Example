using BookStore.Application.Validation.Attributes;
using System.ComponentModel.DataAnnotations;

using SystemValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace BookStore.Tests.NUnit.ApplicationTests.ValidationTests
{
    [TestFixture]
    public sealed class AttributesTests
    {
        private Faker _faker;

        [SetUp]
        public void SetUp()
        {
            _faker = new Faker();
        }

        [Test]
        public void IntegerIdAttribute_GivenNegativeOrZeroValue_ShouldReturnValidationError()
        {
            IntegerIdAttribute attribute = new();
            ValidationContext validationContext = new(new object());

            SystemValidationResult? resultNegative = attribute.GetValidationResult(_faker.Random.Int(max: -1), validationContext);
            SystemValidationResult? resultZero = attribute.GetValidationResult(0, validationContext);

            
            using (Assert.EnterMultipleScope())
            {
                Assert.That(resultNegative, Is.Not.Null);
                Assert.That(resultZero, Is.Not.Null);
            }

            using (Assert.EnterMultipleScope())
            {
                Assert.That(resultNegative.ErrorMessage, Is.EqualTo("Integer Id cannot be negative or zero."));
                Assert.That(resultZero.ErrorMessage, Is.EqualTo("Integer Id cannot be negative or zero."));
            }
        }

        [Test]
        public void IntegerIdAttribute_GivenPositiveValue_ShouldReturnValidationSuccess()
        {
            IntegerIdAttribute attribute = new();
            ValidationContext validationContext = new(new object());

            SystemValidationResult? resultPositive = attribute.GetValidationResult(_faker.Random.Number(int.MaxValue), validationContext);

            Assert.That(resultPositive, Is.Null);
        }

        [Test]
        public void NonNegativeAttribute_GivenNegativeValue_ShouldReturnValidationError()
        {
            NonNegativeAttribute attribute = new();
            ValidationContext validationContext = new(new object());
            SystemValidationResult? resultNegative = attribute.GetValidationResult(-0.01m, validationContext);

            Assert.That(resultNegative, Is.Not.Null);
            Assert.That(resultNegative.ErrorMessage, Is.EqualTo("Value must be non-negative."));
        }

        [Test]
        public void NonNegativeAttribute_GivenZeroOrPositiveValue_ShouldReturnValidationSuccess()
        {
            NonNegativeAttribute attribute = new();
            ValidationContext validationContext = new(new object());

            SystemValidationResult? resultZero = attribute.GetValidationResult(0m, validationContext);
            SystemValidationResult? resultPositive = attribute.GetValidationResult(_faker.Random.Decimal(0.01m, decimal.MaxValue), validationContext);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(resultZero, Is.Null);
                Assert.That(resultPositive, Is.Null);
            }
        }

        [Test]
        public void NotEmptyGuidAttribute_GivenEmptyGuid_ShouldReturnValidationError()
        {
            NotEmptyGuidAttribute attribute = new();
            ValidationContext validationContext = new(new object());

            SystemValidationResult? result = attribute.GetValidationResult(Guid.Empty, validationContext);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.ErrorMessage, Is.EqualTo("Guid cannot be empty."));
        }

        [Test]
        public void NotEmptyGuidAttribute_GivenNonEmptyGuid_ShouldReturnValidationSuccess()
        {
            NotEmptyGuidAttribute attribute = new();
            ValidationContext validationContext = new(new object());

            SystemValidationResult? result = attribute.GetValidationResult(Guid.NewGuid(), validationContext);

            Assert.That(result, Is.Null);
        }
    }
}
