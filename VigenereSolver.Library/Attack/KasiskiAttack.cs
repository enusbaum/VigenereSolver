using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using VigenereSolver.Library.Analysis;
using VigenereSolver.Library.Cipher;
using VigenereSolver.Library.Helpers;

namespace VigenereSolver.Library.Attack
{

    /// <summary>
    ///     Performs the Kasiski/Babbage attack on a Vigenere Cipher
    /// </summary>
    public static class KasiskiAttack
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(KasiskiAttack).Namespace);

        /// <summary>
        ///     Performs a Kasiski attack provided a given key length
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="keyLength"></param>
        /// <returns></returns>
        public static string AttackWithKeyLength(string cipherText, int keyLength)
        {
            var cipher = new Vigenere();
            var allFreqScores = new List<Dictionary<char, int>>();
            for (var i = 1; i < keyLength + 1; i++)
            {
                var nthLetters = Kasiski.GetNthSubkeysLetters(i, keyLength, cipherText.ToUpper());

                var frequencyScore = new Dictionary<char, int>();
                foreach (var letter in Constants.Letters)
                {
                    var decryptedText = cipher.Decipher(nthLetters, Char.ToString(letter));

                    var score = Frequency.EnglishFreqMatchScore(decryptedText);
                    frequencyScore.Add(letter, score);
                }

                //Sort the dictionary by factor ocurrences descending
                var sortedCommonFactors = (from entry in frequencyScore orderby entry.Value descending select entry)
                    .Take(4)
                    .ToDictionary(pair => pair.Key, pair => pair.Value);

                allFreqScores.Add(sortedCommonFactors);
            }

            //Print our possible keys
            var position = 1;
            var possibleKeys = new List<char[]>();
            foreach (var freq in allFreqScores)
            {
                _log.InfoFormat("Possible letters for letter {0} of the key: ", position);
                var possibleKeyOutput = new StringBuilder();
                var keyStore = new char[freq.Keys.Count];
                var keyPosition = 0;
                foreach (var keyPossible in freq.Keys)
                {
                    possibleKeyOutput.AppendFormat("{0}\t", keyPossible);
                    keyStore[keyPosition] = keyPossible;
                    keyPosition++;
                }
                possibleKeys.Add(keyStore);
                _log.InfoFormat(possibleKeyOutput.ToString());
                position++;
            }

            var keyCombinations = Combinator.Combinations(possibleKeys);

            var keyFound = false;
            var key = string.Empty;

            //Go through the possible keys with the given freqency and determine if they produce
            //an English sentence.
            Parallel.ForEach(keyCombinations, (possibleKey, loopState)  =>
            {
                //Stop processing if we find the possible key
                if (keyFound)
                    loopState.Break();

                _log.DebugFormat("Attempting with key: {0}", possibleKey);

                var decryptAttempt = cipher.Decipher(cipherText, possibleKey.ToString());

                if (!English.IsEnglish(decryptAttempt)) return;

                _log.InfoFormat("Found Possible Decryption Key: {0}", possibleKey);
                keyFound = true;
                key = possibleKey.ToString();
            }
            );

            return key;
        }
    }
}
