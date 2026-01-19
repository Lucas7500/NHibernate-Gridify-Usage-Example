using BookStore.Tests.xUnit.ArchitectureTests.Utils;
using NetArchTest.Rules;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Tests.MSTest.ArchitectureTests
{
    [TestClass]
    public sealed class ApplicationTests
    {
        [TestMethod]
        public void Application_ShouldNotDependOnInfrastructure()
        {
            NetArchTest.Rules.TestResult result = Types
                .InAssembly(typeof(Application.AssemblyReference).Assembly)
                .That()
                .ResideInNamespace(Namespaces.Application)
                .Should()
                .NotHaveDependencyOn(Namespaces.Infrastructure)
                .GetResult();

            Assert.IsTrue(result.IsSuccessful);
        }

        [TestMethod]
        public void ValidationAttributes_ShouldFollowConventions()
        {
            NetArchTest.Rules.TestResult result = Types
                .InAssembly(typeof(Application.AssemblyReference).Assembly)
                .That()
                .ResideInNamespace(Namespaces.Application + ".Validation.Attributes")
                .Should()
                .Inherit(typeof(ValidationAttribute))
                .And()
                .HaveCustomAttribute(typeof(AttributeUsageAttribute))
                .And()
                .BeSealed()
                .GetResult();

            Assert.IsTrue(result.IsSuccessful);
        }

        [TestMethod]
        public void UseCases_ShouldFollowConventions()
        {
            NetArchTest.Rules.TestResult result = Types
                .InAssembly(typeof(Application.AssemblyReference).Assembly)
                .That()
                .ResideInNamespaceStartingWith(Namespaces.Application + ".UseCases")
                .And()
                .AreClasses()
                .Should()
                .BeSealed()
                .And()
                .NotBePublic()
                .And()
                .HaveNameEndingWith("UseCase")
                .GetResult();

            Assert.IsTrue(result.IsSuccessful);
        }

        [TestMethod]
        public void Application_Contracts_ShouldFollowConventions()
        {
            NetArchTest.Rules.TestResult result = Types
                .InAssembly(typeof(Application.AssemblyReference).Assembly)
                .That()
                .ResideInNamespaceEndingWith("Contracts")
                .Should()
                .BeInterfaces().Or().BeAbstract()
                .GetResult();

            Assert.IsTrue(result.IsSuccessful);
        }

        [TestMethod]
        public void DTOs_Requests_ShouldFollowConventions()
        {
            NetArchTest.Rules.TestResult result = Types
                .InAssembly(typeof(Application.AssemblyReference).Assembly)
                .That()
                .ResideInNamespaceStartingWith(Namespaces.Application + ".DTOs")
                .And()
                .ResideInNamespaceEndingWith("Requests")
                .Should()
                .HaveNameEndingWith("Request")
                .And()
                .BeClasses()
                .And()
                .BePublic()
                .And()
                .BeSealed()
                .GetResult();

            Assert.IsTrue(result.IsSuccessful);
        }

        [TestMethod]
        public void DTOs_RequestsValidators_ShouldFollowConventions()
        {
            NetArchTest.Rules.TestResult result = Types
                .InAssembly(typeof(Application.AssemblyReference).Assembly)
                .That()
                .ResideInNamespaceStartingWith(Namespaces.Application + ".DTOs")
                .And()
                .ResideInNamespaceEndingWith("Requests.Validation")
                .Should()
                .HaveNameEndingWith("RequestValidator")
                .And()
                .BeClasses()
                .And()
                .NotBePublic()
                .And()
                .BeSealed()
                .GetResult();

            Assert.IsTrue(result.IsSuccessful);
        }

        [TestMethod]
        public void DTOs_Responses_ShouldFollowConventions()
        {
            NetArchTest.Rules.TestResult result = Types
                .InAssembly(typeof(Application.AssemblyReference).Assembly)
                .That()
                .ResideInNamespaceStartingWith(Namespaces.Application + ".DTOs")
                .And()
                .ResideInNamespaceEndingWith("Responses")
                .Should()
                .HaveNameEndingWith("Response")
                .And()
                .BeClasses()
                .And()
                .BePublic()
                .And()
                .BeSealed()
                .GetResult();

            Assert.IsTrue(result.IsSuccessful);
        }
    }
}
