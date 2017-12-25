using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Security.Hashing.HashGen.MD5;
using Security.Hashing.HashGen.MD5Static;

namespace Security.Hashing.Tests
{
    [TestClass]
    public class MD5HasherTests
    {
        private MD5 _hasher;

        [TestInitialize]
        public void Init()
        {
            _hasher = new MD5();
        }

        [TestMethod]
        public void TestEmptyString()
        {
            string input = String.Empty;
            string inputHash = "D41D8CD98F00B204E9800998ECF8427E";

            _hasher.Value = input;

            string hash = _hasher.FingerPrint;

            Assert.AreEqual(inputHash, hash);
        }

        [TestMethod]
        public void TestA()
        {
            string input = "a";
            string inputHash = "0CC175B9C0F1B6A831C399E269772661";

            _hasher.Value = input;

            string hash = _hasher.FingerPrint;

            Assert.AreEqual(inputHash, hash);
        }

        [TestMethod]
        public void TestABC()
        {
            string input = "abc";
            string inputHash = "900150983CD24FB0D6963F7D28E17F72";

            _hasher.Value = input;

            string hash = _hasher.FingerPrint;

            Assert.AreEqual(inputHash, hash);
        }

        [TestMethod]
        public void TestMessageDigest()
        {
            string input = "message digest";
            string inputHash = "F96B697D7CB7938D525A2F31AAF161D0";

            _hasher.Value = input;

            string hash = _hasher.FingerPrint;

            Assert.AreEqual(inputHash, hash);
        }

        [TestMethod]
        public void Test1()
        {
            string input = "abcdefghijklmnopqrstuvwxyz";
            string inputHash = "C3FCD3D76192E4007DFB496CCA67E13B";

            _hasher.Value = input;

            string hash = _hasher.FingerPrint;

            Assert.AreEqual(inputHash, hash);
        }
        
        [TestMethod]
        public void Test2()
        {
            string input = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string inputHash = "D174AB98D277D9F5A5611C2C9F419D9F";

            _hasher.Value = input;

            string hash = _hasher.FingerPrint;

            Assert.AreEqual(inputHash, hash);
        }
        
        [TestMethod]
        public void Test3()
        {
            string input = "12345678901234567890123456789012345678901234567890123456789012345678901234567890";
            string inputHash = "57EDF4A22BE3C955AC49DA2E2107B67A";

            _hasher.Value = input;

            string hash = _hasher.FingerPrint;

            Assert.AreEqual(inputHash, hash);
        }
    }
}
