using BookStore.Application.Validation.Attributes;
using System.ComponentModel.DataAnnotations;

using SystemValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace BookStore.Tests.MSTest.ApplicationTests.ValidationTests
{
    [TestClass]
    public sealed class AttributesTests
    {
        private Faker _faker = null!;

        [TestInitialize]
        public void TestInitialize()
        {
            _faker = new Faker();
        }

        [TestMethod]
        public void IntegerIdAttribute_GivenNegativeOrZeroValue_ShouldReturnValidationError()
        {
            IntegerIdAttribute attribute = new();
            ValidationContext validationContext = new(new object());

            SystemValidationResult? resultNegative = attribute.GetValidationResult(_faker.Random.Int(max: -1), validationContext);
            SystemValidationResult? resultZero = attribute.GetValidationResult(0, validationContext);

            Assert.IsNotNull(resultNegative);
            Assert.AreEqual("Integer Id cannot be negative or zero.", resultNegative.ErrorMessage);

            Assert.IsNotNull(resultZero);
            Assert.AreEqual("Integer Id cannot be negative or zero.", resultZero.ErrorMessage);
        }

        [TestMethod]
        public void IntegerIdAttribute_GivenPositiveValue_ShouldReturnValidationSuccess()
        {
            IntegerIdAttribute attribute = new();
            ValidationContext validationContext = new(new object());

            SystemValidationResult? resultPositive = attribute.GetValidationResult(_faker.Random.Number(int.MaxValue), validationContext);

            Assert.IsNull(resultPositive);
        }

        [TestMethod]
        public void NonNegativeAttribute_GivenNegativeValue_ShouldReturnValidationError()
        {
            NonNegativeAttribute attribute = new();
            ValidationContext validationContext = new(new object());
            SystemValidationResult? resultNegative = attribute.GetValidationResult(-0.01m, validationContext);

            Assert.IsNotNull(resultNegative);
            Assert.AreEqual("Value must be non-negative.", resultNegative.ErrorMessage);
        }

        [TestMethod]
        public void NonNegativeAttribute_GivenZeroOrPositiveValue_ShouldReturnValidationSuccess()
        {
            NonNegativeAttribute attribute = new();
            ValidationContext validationContext = new(new object());

            SystemValidationResult? resultZero = attribute.GetValidationResult(0m, validationContext);
            SystemValidationResult? resultPositive = attribute.GetValidationResult(_faker.Random.Decimal(0.01m, decimal.MaxValue), validationContext);

            Assert.IsNull(resultZero);
            Assert.IsNull(resultPositive);
        }

        [TestMethod]
        public void NotEmptyGuidAttribute_GivenEmptyGuid_ShouldReturnValidationError()
        {
            NotEmptyGuidAttribute attribute = new();
            ValidationContext validationContext = new(new object());

            SystemValidationResult? result = attribute.GetValidationResult(Guid.Empty, validationContext);

            Assert.IsNotNull(result);
            Assert.AreEqual("Guid cannot be empty.", result.ErrorMessage);
        }

        [TestMethod]
        public void NotEmptyGuidAttribute_GivenNonEmptyGuid_ShouldReturnValidationSuccess()
        {
            NotEmptyGuidAttribute attribute = new();
            ValidationContext validationContext = new(new object());

            SystemValidationResult? result = attribute.GetValidationResult(Guid.NewGuid(), validationContext);

            Assert.IsNull(result);
        }
    }
}
