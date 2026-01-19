using BookStore.Domain.Validation;
using BookStore.Domain.ValueObjects.Contracts;
using BookStore.Tests.xUnit.ArchitectureTests.Utils;
using NetArchTest.Rules;

namespace BookStore.Tests.xUnit.ArchitectureTests
{
    public static class DomainTests
    {
        public sealed class UsingStandardAssertions
        {
            [Fact]
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

                Assert.True(result.IsSuccessful);
            }

            [Fact]
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

                Assert.True(result.IsSuccessful);
                Assert.All(
                    stronglyTypedIds, 
                    idType => Assert.True(idType.IsValueType));
            }

            [Fact]
            public void BusinessRules_ShouldNotBePublic()
            {
                TestResult result = Types
                    .InAssembly(typeof(Domain.AssemblyReference).Assembly)
                    .That()
                    .Inherit(typeof(IBusinessRule))
                    .Should()
                    .NotBePublic()
                    .GetResult();

                Assert.True(result.IsSuccessful);
            }

            [Fact]
            public void Domain_Contracts_ShouldFollowConventions()
            {
                TestResult result = Types
                    .InAssembly(typeof(Domain.AssemblyReference).Assembly)
                    .That()
                    .ResideInNamespaceEndingWith("Contracts")
                    .Should()
                    .BeInterfaces().Or().BeAbstract()
                    .GetResult();

                Assert.True(result.IsSuccessful);
            }
        }
        
        public sealed class UsingFluentAssertions
        {
            [Fact]
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

                result.IsSuccessful.Should().BeTrue();
            }

            [Fact]
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

                result.IsSuccessful.Should().BeTrue();
                stronglyTypedIds
                    .Should()
                    .AllSatisfy(
                        idType => idType.IsValueType.Should().BeTrue());
            }

            [Fact]
            public void BusinessRules_ShouldNotBePublic()
            {
                TestResult result = Types
                    .InAssembly(typeof(Domain.AssemblyReference).Assembly)
                    .That()
                    .Inherit(typeof(IBusinessRule))
                    .Should()
                    .NotBePublic()
                    .GetResult();

                result.IsSuccessful.Should().BeTrue();
            }

            [Fact]
            public void Domain_Contracts_ShouldFollowConventions()
            {
                TestResult result = Types
                    .InAssembly(typeof(Domain.AssemblyReference).Assembly)
                    .That()
                    .ResideInNamespaceEndingWith("Contracts")
                    .Should()
                    .BeInterfaces().Or().BeAbstract()
                    .GetResult();

                result.IsSuccessful.Should().BeTrue();
            }
        }
    }
}
