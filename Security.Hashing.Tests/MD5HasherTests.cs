using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Security.Hashing.HashGen.MD5;

namespace Security.Hashing.Tests
{
    [TestClass]
    public class MD5HasherTests
    {
        private MD5Hasher _hasher;

        [TestInitialize]
        public void Init()
        {
            _hasher = new MD5Hasher();
        }

        [TestMethod]
        public void TestEmptyString()
        {
            string input = String.Empty;
            string inputHash = "D41D8CD98F00B204E9800998ECF8427E";

            string hash = _hasher.Hash(input);

            Assert.AreEqual(inputHash, hash);
        }
    }
}
