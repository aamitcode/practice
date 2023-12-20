using src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    [TestClass]
    public class SumDigitTest
    {
        [TestMethod]
        public void TestSum1()
        {
            Assert.AreEqual(4, SumDigits.Sum(49));
        }

        [TestMethod]
        public void TestSum2()
        {
            Assert.AreEqual(1, SumDigits.Sum(1));
        }

        [TestMethod]
        public void TestSum3()
        {
            Assert.AreEqual(3, SumDigits.Sum(439230));
        }

        [TestMethod]
        public void TestSum4()
        {
            Assert.AreEqual(1, SumDigits.Sum(1000000000));
        }

        [TestMethod]
        public void TestSum5()
        {
            Assert.AreEqual(0, SumDigits.Sum(0));
        }

        [TestMethod]
        public void TestSum6()
        {
            Assert.AreEqual(1, SumDigits.Sum(int.MaxValue));
        }
    }
}
