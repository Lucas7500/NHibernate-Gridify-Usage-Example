using Asp.Versioning;
using BookStore.Tests.xUnit.ArchitectureTests.Utils;
using Microsoft.AspNetCore.Mvc;
using NetArchTest.Rules;

namespace BookStore.Tests.MSTest.ArchitectureTests
{
    [TestClass]
    public sealed class ApiTests
    {
        [TestMethod]
        public void Controllers_ShouldNotDependOnInfrastructure()
        {
            NetArchTest.Rules.TestResult result = Types
                .InAssembly(typeof(API.AssemblyReference).Assembly)
                .That()
                .ResideInNamespace(Namespaces.API + ".Controllers")
                .Should()
                .NotHaveDependencyOn(Namespaces.Infrastructure)
                .GetResult();

            Assert.IsTrue(result.IsSuccessful);
        }

        [TestMethod]
        public void Controllers_ShouldBeVersionedCorrectly()
        {
            NetArchTest.Rules.TestResult result = Types
                .InAssembly(typeof(API.AssemblyReference).Assembly)
                .That()
                .Inherit(typeof(ControllerBase))
                .And()
                .AreNotAbstract()
                .Should()
                .HaveCustomAttribute(typeof(ApiVersionAttribute))
                .GetResult();

            Assert.IsTrue(result.IsSuccessful);
        }
    }
}
