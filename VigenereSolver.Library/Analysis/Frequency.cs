using System.Collections.Generic;
using System.Linq;
using System.Text;
using VigenereSolver.Library.Helpers;

namespace VigenereSolver.Library.Analysis
{
    /// <summary>
    ///     Class containing methods on performing common frequency analysis of strings
    /// </summary>
    public static class Frequency
    {
        /// <summary>
        ///     Returns an empty dictionary of all english letters with counts of zero
        /// </summary>
        /// <returns></returns>
        private static Dictionary<char, int> BuildNewFrequencyDictionary()
        {
            return Constants.Letters.ToDictionary(c => c, c => 0);
        }

        /// <summary>
        ///     Returns a dictionary containing characters and the count of occurances of those characters in the specified message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static Dictionary<char, int> GetLetterCount(string message)
        {
            var output = BuildNewFrequencyDictionary();
            foreach (var c in message.ToUpper().Where(output.ContainsKey))
            {
                output[c] += 1;
            }

            return output;
        }

        /// <summary>
        ///     Returns the character frequency order by highest freqency and ETAOIN order
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string GetFrequencyOrder(string message)
        {
            var output = new StringBuilder(26);

            //Get Letter Counts
            var letterFreqency = GetLetterCount(message);

            //Group Letters by common ount
            var commonFrequencies = letterFreqency.GroupBy(r => r.Value).OrderByDescending(key => key.Key)
                  .ToDictionary(t => t.Key, t => t.Select(r => r.Key).ToList());

            var sortedCommonFrequencies = new Dictionary<int, List<char>>(commonFrequencies);

            //Make Letter groups follow ETAOIN format
            foreach (var commonFrequency in commonFrequencies)
            {
                var sorted = Constants.ETAOIN.Where(c => commonFrequency.Value.Contains(c)).ToList();
                sortedCommonFrequencies[commonFrequency.Key] = sorted;
            }

            //Go through now and build the output string
            foreach (var c in sortedCommonFrequencies.SelectMany(commonFrequency => commonFrequency.Value))
            {
                output.Append(c);
            }

            return output.ToString();
        }

        /// <summary>
        ///     Creates a match score based on the occurange number of the Top 6 Most Frequent & The Bottom 6 Least Frequent characters
        ///     in the English language when compared to the specified message.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static int EnglishFreqMatchScore(string message)
        {
            var frequencyOrder = GetFrequencyOrder(message);

            var top6Freq = frequencyOrder.Substring(0, 6);
            var bottom6Freq = frequencyOrder.Substring(frequencyOrder.Length - 7, 6);

            var top6ETAOIN = Constants.ETAOIN.Substring(0, 6);
            var bottom6ETAOIN = Constants.ETAOIN.Substring(Constants.ETAOIN.Length - 7, 6);

            return top6Freq.Count(top6ETAOIN.Contains) + bottom6Freq.Count(bottom6ETAOIN.Contains);
        }
    }
}
