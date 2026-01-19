using BookStore.Domain.Validation;
using BookStore.Domain.ValueObjects.Contracts;
using BookStore.Tests.xUnit.ArchitectureTests.Utils;
using NetArchTest.Rules;

namespace BookStore.Tests.MSTest.ArchitectureTests
{
    [TestClass]
    public class DomainTests
    {
        [TestMethod]
        public void Domain_ShouldNotDependOnOtherProjects()
        {
            NetArchTest.Rules.TestResult result = Types
                .InAssembly(typeof(Domain.AssemblyReference).Assembly)
                .That()
                .ResideInNamespace(Namespaces.Domain)
                .Should()
                .NotHaveDependencyOnAny(
                    Namespaces.API,
                    Namespaces.Infrastructure,
                    Namespaces.Application)
                .GetResult();

            Assert.IsTrue(result.IsSuccessful);
        }

        [TestMethod]
        public void StronglyTypedIds_ShouldFollowConventions()
        {
            NetArchTest.Rules.TestResult result = Types
                .InAssembly(typeof(Domain.AssemblyReference).Assembly)
                .That()
                .Inherit(typeof(IStronglyTypedId<>))
                .Should()
                .BeClasses()
                .And()
                .HaveNameEndingWith("Id")
                .And()
                .NotBeAbstract()
                .GetResult();

            IEnumerable<Type> stronglyTypedIds = Types
                .InAssembly(typeof(Domain.AssemblyReference).Assembly)
                .That()
                .Inherit(typeof(IStronglyTypedId<>))
                .GetTypes();

            Assert.IsTrue(result.IsSuccessful);
            Assert.IsTrue(stronglyTypedIds.All(v => v.IsValueType));
        }

        [TestMethod]
        public void BusinessRules_ShouldNotBePublic()
        {
            NetArchTest.Rules.TestResult result = Types
                .InAssembly(typeof(Domain.AssemblyReference).Assembly)
                .That()
                .Inherit(typeof(IBusinessRule))
                .Should()
                .NotBePublic()
                .GetResult();

            Assert.IsTrue(result.IsSuccessful);
        }

        [TestMethod]
        public void Domain_Contracts_ShouldFollowConventions()
        {
            NetArchTest.Rules.TestResult result = Types
                .InAssembly(typeof(Domain.AssemblyReference).Assembly)
                .That()
                .ResideInNamespaceEndingWith("Contracts")
                .Should()
                .BeInterfaces().Or().BeAbstract()
                .GetResult();

            Assert.IsTrue(result.IsSuccessful);
        }
    }
}
