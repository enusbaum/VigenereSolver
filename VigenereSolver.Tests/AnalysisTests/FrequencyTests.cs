using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VigenereSolver.Library.Analysis;

namespace VigenereSolver.Tests.AnalysisTests
{
    [TestClass]
    public class FrequencyTests
    {
        [TestMethod]
        public void GetLetterCount_Test()
        {
            var result1 = Frequency.GetLetterCount("The quick brown fox jumps over the lazy dog");

            var expected = new Dictionary<char, int>()
            {
                {'A', 1},
                {'B', 1},
                {'C', 1},
                {'D', 1},
                {'E', 3},
                {'F', 1},
                {'G', 1},
                {'H', 2},
                {'I', 1},
                {'J', 1},
                {'K', 1},
                {'L', 1},
                {'M', 1},
                {'N', 1},
                {'O', 4},
                {'P', 1},
                {'Q', 1},
                {'R', 2},
                {'S', 1},
                {'T', 2},
                {'U', 2},
                {'V', 1},
                {'W', 1},
                {'X', 1},
                {'Y', 1},
                {'Z', 1}
            };

            CollectionAssert.AreEqual(expected, result1);
        }

        [TestMethod]
        public void GetFrequencyOrder_Test()
        {
            var result1 =
                Frequency.GetFrequencyOrder(
                    "Forsaking monastic tradition, twelve jovial friars gave up their vocation for a questionable existence on the flying trapeze");

            const string expected = "EITAONRSLFVCGHUPDMWYBKJXQZ";

            Assert.AreEqual(expected, result1);
        }

        [TestMethod]
        public void EnglishFreqMatchScore_Test()
        {
            var result1 = Frequency.EnglishFreqMatchScore(
                "In my younger and more vulnerable years my father gave me some advice that I've been turning over in my mind ever since. \"Whenever you feel like criticizing any one,\" he told me, \"just remember that all the people in this world haven't had the advantages that you've had.");

            const int expected = 8;

            Assert.AreEqual(expected, result1);
        }
    }
}
