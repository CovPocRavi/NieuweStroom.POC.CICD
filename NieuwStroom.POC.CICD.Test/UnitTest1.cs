using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NieuwStroom.POC.CICD.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //arrange
            var priceCalculator = new NieuweStroom.POC.CICD.PriceCalculator();
            
            // act
            for (int i = 0; i < 1000; i++)
            {
                int actual = priceCalculator.CalculatePrice(i, i + 1);
                int expected = i * (i + 1);
                // Assert
                Assert.AreEqual (expected, actual);
            }
            
        }
    }
}
