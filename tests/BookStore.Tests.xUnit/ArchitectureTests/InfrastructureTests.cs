using BookStore.Tests.xUnit.ArchitectureTests.Utils;
using NetArchTest.Rules;

namespace BookStore.Tests.xUnit.ArchitectureTests
{
    public static class InfrastructureTests
    {
        public sealed class UsingStandardAssertions
        {
            [Fact]
            public void Infrastructure_ShouldOnlyDependOnApplicationAndDomain()
            {
                var result = Types
                    .InAssembly(typeof(Infra.IAssemblyReference).Assembly)
                    .That()
                    .ResideInNamespace(Namespaces.Infrastructure)
                    .Should()
                    .OnlyHaveDependenciesOn(
                        Namespaces.Infrastructure,
                        Namespaces.Application,
                        Namespaces.Domain,
                        "System",
                        "Microsoft")
                    .GetResult();

                Assert.True(result.IsSuccessful, result.GetFailingTypesMessage());
            }
        }

        public sealed class UsingFluentAssertions
        {

        }
    }
}
