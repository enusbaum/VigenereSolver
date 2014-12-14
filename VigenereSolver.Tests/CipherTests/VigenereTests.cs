using Microsoft.VisualStudio.TestTools.UnitTesting;
using VigenereSolver.Library.Cipher;

namespace VigenereSolver.Tests.CipherTests
{
    /// <summary>
    ///     Unit Tests for Vigenere Cipher
    /// </summary>
    [TestClass]
    public class VigenereTests
    {

        [TestMethod]
        public void EncipherWithKey_Test()
        {
            var cipher = new Vigenere();

            var output = cipher.Encipher("The quick brown fox jumps over the lazy dog", "TEST");

            const string expected = "mlw JNmuD uvGPG jGQ CyEIL sNxK xzx EeRR wsy";

            Assert.AreEqual(expected.ToLower(), output.ToLower());
        }

        [TestMethod]
        public void DecipherWithKey_Test()
        {
            var cipher = new Vigenere();

            var output = cipher.Decipher("mlw JNmuD uvGPG jGQ CyEIL sNxK xzx EeRR wsy", "TEST");

            const string expected = "The quick brown fox jumps over the lazy dog";

            Assert.AreEqual(expected.ToLower(), output.ToLower());
        }
    }
}
