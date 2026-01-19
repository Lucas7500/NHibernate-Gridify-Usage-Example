using BookStore.Domain.Persistence.Contracts;
using BookStore.Domain.Persistence.Contracts.Base;
using BookStore.Tests.xUnit.ArchitectureTests.Utils;
using FluentMigrator;
using FluentNHibernate.Mapping;
using NetArchTest.Rules;

namespace BookStore.Tests.xUnit.ArchitectureTests
{
    public static class InfrastructureTests
    {
        public sealed class UsingStandardAssertions
        {
            [Fact]
            public void Infrastructure_ShouldNotDependOnAPI()
            {
                TestResult result = Types
                    .InAssembly(typeof(Infra.AssemblyReference).Assembly)
                    .That()
                    .ResideInNamespace(Namespaces.Infrastructure)
                    .Should()
                    .NotHaveDependencyOn(Namespaces.API)
                    .GetResult();

                Assert.True(result.IsSuccessful);
            }

            [Fact]
            public void NHibernate_Mappings_ShouldFollowConventions()
            {
                TestResult result = Types
                    .InAssembly(typeof(Infra.AssemblyReference).Assembly)
                    .That()
                    .Inherit(typeof(ClassMap<>))
                    .Or()
                    .Inherit(typeof(SubclassMap<>))
                    .Should()
                    .ResideInNamespace(Namespaces.Infrastructure + ".NHibernate.Mapping")
                    .And()
                    .HaveNameEndingWith("Map")
                    .And()
                    .BeSealed()
                    .And()
                    .NotBePublic()
                    .GetResult();

                Assert.True(result.IsSuccessful);
            }
            
            [Fact]
            public void Repositories_ShouldFollowConventions()
            {
                TestResult result = Types
                    .InAssembly(typeof(Infra.AssemblyReference).Assembly)
                    .That()
                    .Inherit(typeof(IRepository<,,>))
                    .Should()
                    .ResideInNamespaceStartingWith(Namespaces.Infrastructure + ".Repositories")
                    .And()
                    .HaveNameEndingWith("Repository")
                    .And()
                    .BeSealed()
                    .And()
                    .NotBePublic()
                    .GetResult();

                Assert.True(result.IsSuccessful);
            }
            
            [Fact]
            public void UnitOfWork_ShouldFollowConventions()
            {
                TestResult result = Types
                    .InAssembly(typeof(Infra.AssemblyReference).Assembly)
                    .That()
                    .Inherit(typeof(IUnitOfWork))
                    .Should()
                    .HaveNameEndingWith("UnitOfWork")
                    .And()
                    .BeSealed()
                    .And()
                    .NotBePublic()
                    .GetResult();

                Assert.True(result.IsSuccessful);
            }
            
            [Fact]
            public void Migrations_ShouldFollowConventions()
            {
                TestResult result = Types
                    .InAssembly(typeof(Infra.AssemblyReference).Assembly)
                    .That()
                    .Inherit(typeof(Migration))
                    .Should()
                    .ResideInNamespace(Namespaces.Infrastructure + ".Migrations")
                    .And()
                    .BeSealed()
                    .And()
                    .BePublic()
                    .GetResult();

                Assert.True(result.IsSuccessful);
            }

            [Fact]
            public void QueryServices_ShouldFollowConventions()
            {
                TestResult result = Types
                    .InAssembly(typeof(Infra.AssemblyReference).Assembly)
                    .That()
                    .HaveNameEndingWith("QueryService")
                    .Should()
                    .ResideInNamespaceStartingWith(Namespaces.Infrastructure + ".QueryServices")
                    .And()
                    .BeSealed()
                    .And()
                    .NotBePublic()
                    .GetResult();

                Assert.True(result.IsSuccessful);
            }
        }

        public sealed class UsingFluentAssertions
        {
            [Fact]
            public void Infrastructure_ShouldNotDependOnAPI()
            {
                TestResult result = Types
                    .InAssembly(typeof(Infra.AssemblyReference).Assembly)
                    .That()
                    .ResideInNamespace(Namespaces.Infrastructure)
                    .Should()
                    .NotHaveDependencyOn(Namespaces.API)
                    .GetResult();

                result.IsSuccessful.Should().BeTrue();
            }

            [Fact]
            public void NHibernate_Mappings_ShouldFollowConventions()
            {
                TestResult result = Types
                    .InAssembly(typeof(Infra.AssemblyReference).Assembly)
                    .That()
                    .Inherit(typeof(ClassMap<>))
                    .Or()
                    .Inherit(typeof(SubclassMap<>))
                    .Should()
                    .ResideInNamespace(Namespaces.Infrastructure + ".NHibernate.Mapping")
                    .And()
                    .HaveNameEndingWith("Map")
                    .GetResult();

                IEnumerable<Type> mappingsTypes = Types
                    .InAssembly(typeof(Infra.AssemblyReference).Assembly)
                    .That()
                    .Inherit(typeof(ClassMap<>))
                    .Or()
                    .Inherit(typeof(SubclassMap<>))
                    .GetTypes();

                result.IsSuccessful.Should().BeTrue();
            }

            [Fact]
            public void Repositories_ShouldFollowConventions()
            {
                TestResult result = Types
                    .InAssembly(typeof(Infra.AssemblyReference).Assembly)
                    .That()
                    .Inherit(typeof(IRepository<,,>))
                    .Should()
                    .ResideInNamespaceStartingWith(Namespaces.Infrastructure + ".Repositories")
                    .And()
                    .HaveNameEndingWith("Repository")
                    .And()
                    .BeSealed()
                    .And()
                    .NotBePublic()
                    .GetResult();

                result.IsSuccessful.Should().BeTrue();
            }

            [Fact]
            public void UnitOfWork_ShouldFollowConventions()
            {
                TestResult result = Types
                    .InAssembly(typeof(Infra.AssemblyReference).Assembly)
                    .That()
                    .Inherit(typeof(IUnitOfWork))
                    .Should()
                    .HaveNameEndingWith("UnitOfWork")
                    .And()
                    .BeSealed()
                    .And()
                    .NotBePublic()
                    .GetResult();

                result.IsSuccessful.Should().BeTrue();
            }

            [Fact]
            public void Migrations_ShouldFollowConventions()
            {
                TestResult result = Types
                    .InAssembly(typeof(Infra.AssemblyReference).Assembly)
                    .That()
                    .Inherit(typeof(Migration))
                    .Should()
                    .ResideInNamespace(Namespaces.Infrastructure + ".Migrations")
                    .And()
                    .BeSealed()
                    .And()
                    .BePublic()
                    .GetResult();

                result.IsSuccessful.Should().BeTrue();
            }

            [Fact]
            public void QueryServices_ShouldFollowConventions()
            {
                TestResult result = Types
                    .InAssembly(typeof(Infra.AssemblyReference).Assembly)
                    .That()
                    .HaveNameEndingWith("QueryService")
                    .Should()
                    .ResideInNamespaceStartingWith(Namespaces.Infrastructure + ".QueryServices")
                    .And()
                    .BeSealed()
                    .And()
                    .NotBePublic()
                    .GetResult();

                result.IsSuccessful.Should().BeTrue();
            }
        }
    }
}
