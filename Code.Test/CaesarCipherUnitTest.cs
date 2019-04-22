using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Code.Test
{
    [TestClass]
    public class CaesarCipherUnitTest
    {
        [TestMethod]
        public void SampleTest()
        {
            var result = CaesarCipher.caesarCipher("middle-Outz", 2);
            Assert.AreEqual("okffng-Qwvb", result);
        }
    }
}
