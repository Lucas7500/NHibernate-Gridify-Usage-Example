using BookStore.Tests.xUnit.ArchitectureTests.Utils;
using NetArchTest.Rules;

namespace BookStore.Tests.xUnit.ArchitectureTests
{
    public static class ApiTests
    {
        public sealed class UsingStandardAssertions
        {
            public sealed class UsingFluentAssertions
            {
                [Fact]
                public void Controllers_ShouldNotDependOnInfrastructure()
                {
                    var result = Types
                        .InAssembly(typeof(API.IAssemblyReference).Assembly)
                        .That()
                        .ResideInNamespace($"{Namespaces.API}.Controllers")
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
