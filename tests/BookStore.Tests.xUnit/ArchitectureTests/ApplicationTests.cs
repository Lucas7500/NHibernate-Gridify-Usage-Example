using BookStore.Tests.xUnit.ArchitectureTests.Utils;
using NetArchTest.Rules;

namespace BookStore.Tests.xUnit.ArchitectureTests
{
    public static class ApplicationTests
    {
        public sealed class UsingStandardAssertions
        {
            public sealed class UsingFluentAssertions
            {
                [Fact]
                public void Application_ShouldNotDependOnInfrastructure()
                {
                    TestResult result = Types
                        .InAssembly(typeof(Application.IAssemblyReference).Assembly)
                        .That()
                        .ResideInNamespace(Namespaces.Application)
                        .Should()
                        .NotHaveDependencyOn(Namespaces.Infrastructure)
                        .GetResult();

                    Assert.True(result.IsSuccessful);
                }
            }
        }

        public sealed class UsingFluentAssertions
        {

        }
    }
}
