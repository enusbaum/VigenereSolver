using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VigenereSolver.Library.Analysis;
using VigenereSolver.Library.Cipher;

namespace VigenereSolver.Library.Attack
{
    /// <summary>
    ///     Performs a Known Plain Text attack against a Vigenere Cipher
    /// </summary>
    public static class KnownPlaintextAttack
    {
        /// <summary>
        ///     Attacks a Cipher with a known section of plaintext.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static List<string> AttackWithKnownPlaintext(string message, string plainText)
        {
            var possiblePassword = new StringBuilder();
            var possiblePasswords = new List<string>();
            var foundPlainTextPassword = new List<string>();
            var cipher = new Vigenere();

            if (message.IndexOf(' ') > -1)
            {
                var decryptedWords = message.ToUpper().Split(' ');

                //Find words of the same size as the plainText and determine the cipher order
                foreach (var word in decryptedWords)
                {
                    if (word.Length == plainText.Length)
                    {
                        for (var i = 0; i < word.Length; i++)
                        {
                            //Determine difference between decryption and plaintext
                            var decryptIndex = Helpers.Constants.Letters.IndexOf(word[i]);
                            var plaintextIndex = Helpers.Constants.Letters.IndexOf(plainText.ToUpper()[i]);

                            //Wraparound occured
                            var keyCharacterValue = decryptIndex - plaintextIndex;

                            keyCharacterValue %= Helpers.Constants.Letters.Length;

                            if (keyCharacterValue < 0)
                                keyCharacterValue += Helpers.Constants.Letters.Length;

                            possiblePassword.Append(Helpers.Constants.Letters[keyCharacterValue]);
                        }
                    }

                    possiblePasswords.Add(possiblePassword.ToString());
                    possiblePassword.Clear();
                }
            }

            //Remove empty strings from possible passwords
            possiblePasswords.RemoveAll(s => s == string.Empty);

            //Find words that contain the discinct letters in the possible passwords
            var possiblePlainTextPasswords = (from word in possiblePasswords
                let distinct = word.Distinct().ToArray()
                from dictWordList in English.SortedDictionary.OrderByDescending(k => k.Key)
                from dictWord in dictWordList.Value
                let intersection = word.Intersect(dictWord)
                where intersection.Count() == distinct.Length
                select dictWord).ToList();

            //Now Decipher - use those plainTextPasswords to find the possible real one..
            Parallel.ForEach(possiblePlainTextPasswords, key =>
            {
                if (English.IsEnglish(cipher.Decipher(message, key)))
                    foundPlainTextPassword.Add(key);
            });

            return foundPlainTextPassword;
        }
    }
}
