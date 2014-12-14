using System;
using System.Text;
using VigenereSolver.Library.Helpers;

namespace VigenereSolver.Library.Cipher
{
    /// <summary>
    ///     Classic implementation of the Vigenere Cipher based on standard English character set
    /// 
    ///     This routine preserves formatting and punctuation
    /// </summary>
    public class Vigenere
    {
        private readonly string _key;

        /// <summary>
        ///     Default Constructor
        /// </summary>
        public Vigenere()
        {
            
        }

        /// <summary>
        ///     Overloaded Constructor for a persistent cipher key
        /// </summary>
        /// <param name="key"></param>
        public Vigenere(string key)
        {
            _key = key;
        }

        /// <summary>
        ///     Enciphers specified cleartext with persistent key
        /// </summary>
        /// <param name="clearText"></param>
        /// <returns></returns>
        public string Encipher(string clearText)
        {
            if(string.IsNullOrEmpty(_key))
                throw new Exception("Cipher Key was not initialized with class. Please initialize Vigenere class with key or invoke overloaded method with Key");

            return Encipher(clearText, _key);
        }

        /// <summary>
        ///     Enciphers specified cleartext with the specified key
        /// </summary>
        /// <param name="clearText"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Encipher(string clearText, string key)
        {
            var output = new StringBuilder(clearText.Length);
            var keyIndex = 0;
            key = key.ToUpper();

            foreach (var clearTextChar in clearText)
            {
                var clearTextCharNum = Constants.Letters.IndexOf(Char.ToUpper(clearTextChar));

                //Character found in the "LETTERS" constant
                if (clearTextCharNum > -1)
                {
                    clearTextCharNum += Constants.Letters.IndexOf(key[keyIndex]);

                    //Handle wrap-around
                    clearTextCharNum %= Constants.Letters.Length;

                    //Appent the ciphertext to output and preserve case
                    output.Append(char.IsUpper(clearTextChar)
                        ? Constants.Letters[clearTextCharNum]
                        : Char.ToLower(Constants.Letters[clearTextCharNum]));

                    keyIndex++;
                    if (keyIndex == key.Length)
                        keyIndex = 0;
                }
                //Characters not found in the "LETTERS" constant will just be converted as-is
                else
                {
                    output.Append(clearTextChar);
                }
            }
            return output.ToString();
        }

        /// <summary>
        ///     Deciphers specified ciphertext with persistent key
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public string Decipher(string cipherText)
        {
            if (string.IsNullOrEmpty(_key))
                throw new Exception("Cipher Key was not initialized with class. Please initialize Vigenere class with key or invoke overloaded method with Key");

            return Decipher(cipherText, _key);
        }

        /// <summary>
        ///      Deciphers specified cipihertext with the specified key
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Decipher(string cipherText, string key)
        {
            var output = new StringBuilder(cipherText.Length);
            var keyIndex = 0;
            key = key.ToUpper();

            foreach (var cipherTextChar in cipherText)
            {
                var cipherTextCharNum = Constants.Letters.IndexOf(Char.ToUpper(cipherTextChar));

                //Character found in the "LETTERS" constant
                if (cipherTextCharNum > -1)
                {
                    cipherTextCharNum -= Constants.Letters.IndexOf(key[keyIndex]);

                    //Handle wrap-around
                    cipherTextCharNum %= Constants.Letters.Length;

                    //If we go negative, perform wrap from end of letters
                    if (cipherTextCharNum < 0)
                        cipherTextCharNum += Constants.Letters.Length;

                    //Appent the ciphertext to output and preserve case
                    output.Append(char.IsUpper(cipherTextChar)
                        ? Constants.Letters[cipherTextCharNum]
                        : Char.ToLower(Constants.Letters[cipherTextCharNum]));

                    keyIndex++;
                    if (keyIndex == key.Length)
                        keyIndex = 0;
                }
                //Characters not found in the "LETTERS" constant will just be converted as-is
                else
                {
                    output.Append(cipherTextChar);
                }
            }
            return output.ToString();
        }
    }
}
