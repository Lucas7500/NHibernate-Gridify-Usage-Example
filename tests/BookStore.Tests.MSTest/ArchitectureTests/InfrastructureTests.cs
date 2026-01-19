using BookStore.Domain.Persistence.Contracts;
using BookStore.Domain.Persistence.Contracts.Base;
using BookStore.Tests.xUnit.ArchitectureTests.Utils;
using FluentMigrator;
using FluentNHibernate.Mapping;
using NetArchTest.Rules;

namespace BookStore.Tests.MSTest.ArchitectureTests
{
    [TestClass]
    public sealed class InfrastructureTests
    {
        [TestMethod]
        public void Infrastructure_ShouldNotDependOnAPI()
        {
            NetArchTest.Rules.TestResult result = Types
                .InAssembly(typeof(Infra.AssemblyReference).Assembly)
                .That()
                .ResideInNamespace(Namespaces.Infrastructure)
                .Should()
                .NotHaveDependencyOn(Namespaces.API)
                .GetResult();

            Assert.IsTrue(result.IsSuccessful);
        }

        [TestMethod]
        public void NHibernate_Mappings_ShouldFollowConventions()
        {
            NetArchTest.Rules.TestResult result = Types
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

            Assert.IsTrue(result.IsSuccessful);
        }

        [TestMethod]
        public void Repositories_ShouldFollowConventions()
        {
            NetArchTest.Rules.TestResult result = Types
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

            Assert.IsTrue(result.IsSuccessful);
        }

        [TestMethod]
        public void UnitOfWork_ShouldFollowConventions()
        {
            NetArchTest.Rules.TestResult result = Types
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

            Assert.IsTrue(result.IsSuccessful);
        }

        [TestMethod]
        public void Migrations_ShouldFollowConventions()
        {
            NetArchTest.Rules.TestResult result = Types
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

            Assert.IsTrue(result.IsSuccessful);
        }

        [TestMethod]
        public void QueryServices_ShouldFollowConventions()
        {
            NetArchTest.Rules.TestResult result = Types
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

            Assert.IsTrue(result.IsSuccessful);
        }
    }
}
