using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VigenereSolver.Library.Analysis;

namespace VigenereSolver.Tests.AnalysisTests
{
    [TestClass]
    public class BabbageTests
    {
        [TestMethod]
        public void GetNthSubkeysLetters_Test()
        {
            var result1 = Kasiski.GetNthSubkeysLetters(1, 3, "ABCABCABC");
            Assert.AreEqual("AAA", result1);

            var result2 = Kasiski.GetNthSubkeysLetters(2, 3, "ABCABCABC");
            Assert.AreEqual("BBB", result2);

            var result3 = Kasiski.GetNthSubkeysLetters(3, 3, "ABCABCABC");
            Assert.AreEqual("CCC", result3);

            var result4 = Kasiski.GetNthSubkeysLetters(1, 5, "ABCDEFGHI");
            Assert.AreEqual("AF", result4);

            var result5 = Kasiski.GetNthSubkeysLetters(1, 3, "ABC ABC ABC");
            Assert.AreEqual("AAA", result5);
        }

        [TestMethod]
        public void FindRepeatSequencesSpacings_Test()
        {
            var result1 =
                Kasiski.FindRepeatSequencesSpacings(
                    "PPQCAXQVEKGYBNKMAZUYBNGBALJONITSZMJYIMVRAGVOHTVRAUCTKSGDDWUOXITLAZUVAVVRAZCVKBQPIWPOU");

            var expected = new Dictionary<string, List<int>>()
            {
                {"YBN", new List<int> {8}},
                {"AZU", new List<int> {48}},
                {"VRA", new List<int> {8,32,24}}
            };

            CollectionAssert.AreEqual(expected["YBN"], result1["YBN"]);
            CollectionAssert.AreEqual(expected["AZU"], result1["AZU"]);
            CollectionAssert.AreEqual(expected["VRA"], result1["VRA"]);

            Assert.AreEqual(expected.Count, result1.Count);
        }

        [TestMethod]
        public void GetUsefulFactors_Test()
        {

            var result1 = Kasiski.GetUsefulFactors(48);

            var expected = new List<int>()
            {
                2,
                3,
                4,
                6,
                8,
                12,
                16
            };

            CollectionAssert.AreEqual(expected, result1);
        }

        [TestMethod]
        public void KasiskiExamination_Test()
        {
            var result1 =
                Kasiski.KasiskiExamination(
                    "PPQCAXQVEKGYBNKMAZUYBNGBALJONITSZMJYIMVRAGVOHTVRAUCTKSGDDWUOXITLAZUVAVVRAZCVKBQPIWPOU");

            var expected = new List<int> {2, 4, 8};

            CollectionAssert.AreEqual(expected, result1);

        }
    }
}
