using BookStore.Tests.xUnit.ArchitectureTests.Utils;
using NetArchTest.Rules;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Tests.xUnit.ArchitectureTests
{
    public static class ApplicationTests
    {
        public sealed class UsingStandardAssertions
        {
            [Fact]
            public void Application_ShouldNotDependOnInfrastructure()
            {
                TestResult result = Types
                    .InAssembly(typeof(Application.AssemblyReference).Assembly)
                    .That()
                    .ResideInNamespace(Namespaces.Application)
                    .Should()
                    .NotHaveDependencyOn(Namespaces.Infrastructure)
                    .GetResult();

                Assert.True(result.IsSuccessful);
            }

            [Fact]
            public void ValidationAttributes_ShouldFollowConventions()
            {
                TestResult result = Types
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

                Assert.True(result.IsSuccessful);
            }
            
            [Fact]
            public void UseCases_ShouldFollowConventions()
            {
                TestResult result = Types
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

                Assert.True(result.IsSuccessful);
            }

            [Fact]
            public void Application_Contracts_ShouldFollowConventions()
            {
                TestResult result = Types
                    .InAssembly(typeof(Application.AssemblyReference).Assembly)
                    .That()
                    .ResideInNamespaceEndingWith("Contracts")
                    .Should()
                    .BeInterfaces().Or().BeAbstract()
                    .GetResult();

                Assert.True(result.IsSuccessful);
            }
            
            [Fact]
            public void DTOs_Requests_ShouldFollowConventions()
            {
                TestResult result = Types
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

                Assert.True(result.IsSuccessful);
            }
            
            [Fact]
            public void DTOs_RequestsValidators_ShouldFollowConventions()
            {
                TestResult result = Types
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

                Assert.True(result.IsSuccessful);
            }

            [Fact]
            public void DTOs_Responses_ShouldFollowConventions()
            {
                TestResult result = Types
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

                Assert.True(result.IsSuccessful);
            }
        }

        public sealed class UsingFluentAssertions
        {
            [Fact]
            public void Application_ShouldNotDependOnInfrastructure()
            {
                TestResult result = Types
                    .InAssembly(typeof(Application.AssemblyReference).Assembly)
                    .That()
                    .ResideInNamespace(Namespaces.Application)
                    .Should()
                    .NotHaveDependencyOn(Namespaces.Infrastructure)
                    .GetResult();

                result.IsSuccessful.Should().BeTrue();
            }

            [Fact]
            public void ValidationAttributes_ShouldFollowConventions()
            {
                TestResult result = Types
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

                result.IsSuccessful.Should().BeTrue();
            }

            [Fact]
            public void UseCases_ShouldFollowConventions()
            {
                TestResult result = Types
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

                result.IsSuccessful.Should().BeTrue();
            }

            [Fact]
            public void Application_Contracts_ShouldFollowConventions()
            {
                TestResult result = Types
                    .InAssembly(typeof(Application.AssemblyReference).Assembly)
                    .That()
                    .ResideInNamespaceEndingWith("Contracts")
                    .Should()
                    .BeInterfaces().Or().BeAbstract()
                    .GetResult();

                result.IsSuccessful.Should().BeTrue();
            }

            [Fact]
            public void DTOs_Requests_ShouldFollowConventions()
            {
                TestResult result = Types
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

                result.IsSuccessful.Should().BeTrue();
            }

            [Fact]
            public void DTOs_RequestsValidators_ShouldFollowConventions()
            {
                TestResult result = Types
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

                result.IsSuccessful.Should().BeTrue();
            }

            [Fact]
            public void DTOs_Responses_ShouldFollowConventions()
            {
                TestResult result = Types
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

                result.IsSuccessful.Should().BeTrue();
            }
        }
    }
}
