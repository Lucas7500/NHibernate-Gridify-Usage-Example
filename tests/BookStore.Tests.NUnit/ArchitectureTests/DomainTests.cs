using BookStore.Domain.Validation;
using BookStore.Domain.ValueObjects.Contracts;
using BookStore.Tests.NUnit.ArchitectureTests.Utils;
using NetArchTest.Rules;

namespace BookStore.Tests.NUnit.ArchitectureTests
{
    [TestFixture]
    public sealed class DomainTests
    {
        [Test]
        public void Domain_ShouldNotDependOnOtherProjects()
        {
            TestResult result = Types
                .InAssembly(typeof(Domain.AssemblyReference).Assembly)
                .That()
                .ResideInNamespace(Namespaces.Domain)
                .Should()
                .NotHaveDependencyOnAny(
                    Namespaces.API,
                    Namespaces.Infrastructure,
                    Namespaces.Application)
                .GetResult();

            Assert.That(result.IsSuccessful, Is.True);
        }

        [Test]
        public void StronglyTypedIds_ShouldFollowConventions()
        {
            TestResult result = Types
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

            Assert.That(result.IsSuccessful, Is.True);
            Assert.That(stronglyTypedIds, Is.All.Matches<Type>(idType => idType.IsValueType));
        }

        [Test]
        public void BusinessRules_ShouldNotBePublic()
        {
            TestResult result = Types
                .InAssembly(typeof(Domain.AssemblyReference).Assembly)
                .That()
                .Inherit(typeof(IBusinessRule))
                .Should()
                .NotBePublic()
                .GetResult();

            Assert.That(result.IsSuccessful, Is.True);
        }

        [Test]
        public void Domain_Contracts_ShouldFollowConventions()
        {
            TestResult result = Types
                .InAssembly(typeof(Domain.AssemblyReference).Assembly)
                .That()
                .ResideInNamespaceEndingWith("Contracts")
                .Should()
                .BeInterfaces().Or().BeAbstract()
                .GetResult();

            Assert.That(result.IsSuccessful, Is.True);
        }
    }
}
