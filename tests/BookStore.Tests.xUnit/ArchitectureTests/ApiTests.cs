using Asp.Versioning;
using BookStore.Tests.xUnit.ArchitectureTests.Utils;
using Microsoft.AspNetCore.Mvc;
using NetArchTest.Rules;

namespace BookStore.Tests.xUnit.ArchitectureTests
{
    public static class ApiTests
    {
        public sealed class UsingStandardAssertions
        {
            [Fact]
            public void Controllers_ShouldNotDependOnInfrastructure()
            {
                TestResult result = Types
                    .InAssembly(typeof(API.AssemblyReference).Assembly)
                    .That()
                    .ResideInNamespace(Namespaces.API + ".Controllers")
                    .Should()
                    .NotHaveDependencyOn(Namespaces.Infrastructure)
                    .GetResult();

                Assert.True(result.IsSuccessful);
            }
            
            [Fact]
            public void Controllers_ShouldBeVersionedCorrectly()
            {
                TestResult result = Types
                    .InAssembly(typeof(API.AssemblyReference).Assembly)
                    .That()
                    .Inherit(typeof(ControllerBase))
                    .Should()
                    .HaveCustomAttribute(typeof(ApiVersionAttribute))
                    .And()
                    .ResideInNamespaceEndingWith(".v1")
                    .GetResult();

                Assert.True(result.IsSuccessful);
            }
        }

        public sealed class UsingFluentAssertions
        {
            [Fact]
            public void Controllers_ShouldNotDependOnInfrastructure()
            {
                TestResult result = Types
                    .InAssembly(typeof(API.AssemblyReference).Assembly)
                    .That()
                    .ResideInNamespace(Namespaces.API + ".Controllers")
                    .Should()
                    .NotHaveDependencyOn(Namespaces.Infrastructure)
                    .GetResult();

                result.IsSuccessful.Should().BeTrue();
            }

            [Fact]
            public void Controllers_ShouldBeVersionedCorrectly()
            {
                TestResult result = Types
                    .InAssembly(typeof(API.AssemblyReference).Assembly)
                    .That()
                    .Inherit(typeof(ControllerBase))
                    .Should()
                    .HaveCustomAttribute(typeof(ApiVersionAttribute))
                    .And()
                    .ResideInNamespaceEndingWith(".v1")
                    .GetResult();

                result.IsSuccessful.Should().BeTrue();
            }
        }
    }
}
