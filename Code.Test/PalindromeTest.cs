using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Code.Test
{
    [TestClass]
    public class PalindromeTest
    {
        [TestMethod]
        public void SampleTest()
        {
            var s = File.ReadAllText(@"f:\Pactice\test.txt");
            var result = Palindrome.palindromeIndex("hgygsvlfcwnswtuhmyaljkqlqjjqlqkjlaymhutwsnwcwflvsgygh");
            Assert.AreEqual(44, result);
        }
    }
}
