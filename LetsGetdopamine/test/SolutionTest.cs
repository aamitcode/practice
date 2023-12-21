using src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    [TestClass]
    public class SolutionTest
    {
        [TestMethod]
        public void Test3Sum()
        {
            var target = new Solution();
            var result = target.ThreeSum([-1, 0, 1, 0]);
            Assert.AreEqual(1, result.Count);
        }
    }
}
