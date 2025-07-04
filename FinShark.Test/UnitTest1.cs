
using FinShark.Service.SumOfNumbers;

namespace FinShark.Service.Test
{
    public class UnitTest1
    {
        [Fact]

        public void Test1()
        {
            var sum1 = new Sum();
            var res = sum1.SumNumbers(1, 2);
            Assert.Equal(3, res);
        }
    }
}