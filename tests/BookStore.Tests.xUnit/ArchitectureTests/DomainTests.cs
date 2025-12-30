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
                    .InAssembly(typeof(Domain.IAssemblyReference).Assembly)
                    .That()
                    .ResideInNamespace(Namespaces.Domain)
                    .Should()
                    .NotHaveDependencyOnAny(
                        "MyApp.Application",
                        "MyApp.Infrastructure",
                        "MyApp.API")
                    .GetResult();

                Assert.True(result.IsSuccessful);
            }
        }
        
        public sealed class UsingFluentAssertions
        {

        }
    }
}
