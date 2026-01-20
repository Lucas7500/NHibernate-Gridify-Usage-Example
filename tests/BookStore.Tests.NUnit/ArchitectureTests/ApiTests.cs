using Asp.Versioning;
using BookStore.Tests.NUnit.ArchitectureTests.Utils;
using Microsoft.AspNetCore.Mvc;
using NetArchTest.Rules;

namespace BookStore.Tests.NUnit.ArchitectureTests
{
    [TestFixture]
    public sealed class ApiTests
    {
        [Test]
        public void Controllers_ShouldNotDependOnInfrastructure()
        {
            TestResult result = Types
                .InAssembly(typeof(API.AssemblyReference).Assembly)
                .That()
                .ResideInNamespace(Namespaces.API + ".Controllers")
                .Should()
                .NotHaveDependencyOn(Namespaces.Infrastructure)
                .GetResult();

            Assert.That(result.IsSuccessful, Is.True);
        }

        [Test]
        public void Controllers_ShouldBeVersionedCorrectly()
        {
            TestResult result = Types
                .InAssembly(typeof(API.AssemblyReference).Assembly)
                .That()
                .Inherit(typeof(ControllerBase))
                .And()
                .AreNotAbstract()
                .Should()
                .HaveCustomAttribute(typeof(ApiVersionAttribute))
                .GetResult();

            Assert.That(result.IsSuccessful, Is.True);
        }
    }
}
