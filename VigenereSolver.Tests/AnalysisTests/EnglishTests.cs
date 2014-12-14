using Microsoft.VisualStudio.TestTools.UnitTesting;
using VigenereSolver.Library.Analysis;

namespace VigenereSolver.Tests.AnalysisTests
{
    [TestClass]
    public class EnglishTests
    {
        [TestMethod]
        public void IsEnglishShouldPass_Test()
        {
            const string validEnglish = "This is a test of our routine which will evaluate a given string to see if it is an English phrase.";

            var actual = English.IsEnglish(validEnglish);

            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void IsEnglishShouldNotPass_Test()
        {
            const string invalidEnglish = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla facilisis ipsum sed ante tincidunt, a tristique nisl eleifend. Sed commodo nisl vitae scelerisque eleifend.";

            var actual = English.IsEnglish(invalidEnglish);

            Assert.AreEqual(false, actual);
        }

        /// <summary>
        ///     If the dictionary is updated, this test will need to be updated
        /// </summary>
        [TestMethod]
        public void GetStringWordsInDictionary_Test()
        {
            const string validEnglish = "This is a test of our routine which will evaluate a given string to see if it is an English phrase.";

            var actual = English.GetStringWordsInDictionaryRatio(validEnglish);

            Assert.AreEqual(0.52380952380952384d, actual);
        }

        /// <summary>
        ///     If the dictionary is updated, this test will need to be updated
        /// </summary>
        [TestMethod]
        public void GetDictionaryWordsInString_Test()
        {
            const string validEnglish = "This is a test of our routine which will evaluate a given string to see if it is an English phrase.";

            var actual = English.GetDictionaryWordsInStringRatio(validEnglish);

            Assert.AreEqual(1.0d, actual);
        }
    }
}
