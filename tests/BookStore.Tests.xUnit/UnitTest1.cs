namespace BookStore.Tests.xUnit
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
        }

        public struct Teste
        {
            public Teste(string a)
            {
                A = a;
            }

            public string A { get; }
        }
    }
}