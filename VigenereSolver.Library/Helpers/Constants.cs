using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace VigenereSolver.Library.Helpers
{
    public static class Constants
    {
        public static readonly Regex NonlettersPattern = new Regex("[^A-Z]", RegexOptions.Compiled);

        /// <summary>
        ///     Number of Most Frequent Letters to extract during a Kasiski Analysis
        /// </summary>
        public const int NumMostFreqLetters = 4;

        /// <summary>
        ///     Maximum key length supported
        /// </summary>
        public const int MaxKeyLength = 16;

        /// <summary>
        ///     English Alphabet
        /// </summary>
        public const string Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        ///     Frequent English Letters in order of most frequent
        ///     From: http://en.wikipedia.org/wiki/Letter_frequency
        /// </summary>
        public const string ETAOIN = "ETAOINSHRDLCUMWFGYPBVKJXQZ";

        /// <summary>
        ///     Freqency of English Letters
        ///     From: http://en.wikipedia.org/wiki/Letter_frequency
        /// </summary>
        public static Dictionary<char, double> EnglishLetterFreq = new Dictionary<char, double>()
        {
            {'E',12.70},
            {'T', 9.06},
            {'A', 8.17},
            {'O', 7.51},
            {'I', 6.97},
            {'N', 6.75},
            {'S', 6.33},
            {'H', 6.09},
            {'R', 5.99},
            {'D', 4.25},
            {'L', 4.03},
            {'C', 2.78},
            {'U', 2.76},
            {'M', 2.41},
            {'W', 2.36},
            {'F', 2.23},
            {'G', 2.02},
            {'Y', 1.97},
            {'P', 1.93},
            {'B', 1.29},
            {'V', 0.98},
            {'K', 0.77},
            {'J', 0.15},
            {'X', 0.15},
            {'Q', 0.10},
            {'Z', 0.07}
        };
    }
}
