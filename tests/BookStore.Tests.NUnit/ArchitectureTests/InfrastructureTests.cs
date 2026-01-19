using BookStore.Domain.Persistence.Contracts;
using BookStore.Domain.Persistence.Contracts.Base;
using BookStore.Tests.NUnit.ArchitectureTests.Utils;
using FluentMigrator;
using FluentNHibernate.Mapping;
using NetArchTest.Rules;

namespace BookStore.Tests.NUnit.ArchitectureTests
{
    [TestFixture]
    public sealed class InfrastructureTests
    {
        [Test]
        public void Infrastructure_ShouldNotDependOnAPI()
        {
            TestResult result = Types
                .InAssembly(typeof(Infra.AssemblyReference).Assembly)
                .That()
                .ResideInNamespace(Namespaces.Infrastructure)
                .Should()
                .NotHaveDependencyOn(Namespaces.API)
                .GetResult();

            Assert.That(result.IsSuccessful, Is.True);
        }

        [Test]
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

            Assert.That(result.IsSuccessful, Is.True);
        }

        [Test]
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

            Assert.That(result.IsSuccessful, Is.True);
        }

        [Test]
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

            Assert.That(result.IsSuccessful, Is.True);
        }

        [Test]
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

            Assert.That(result.IsSuccessful, Is.True);
        }

        [Test]
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

            Assert.That(result.IsSuccessful, Is.True);
        }
    }
}
