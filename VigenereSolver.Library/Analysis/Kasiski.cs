using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VigenereSolver.Library.Helpers;

namespace VigenereSolver.Library.Analysis
{
    /// <summary>
    ///     Class containing methods involved in performing a Kasiski/Babbage analysis of a given Vigenere ciphertext
    /// </summary>
    public static class Kasiski
    {

        /// <summary>
        ///     Returns every Nth letter for each keyLength set of letters in text.
        /// </summary>
        /// <param name="n"></param>
        /// <param name="keyLength"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string GetNthSubkeysLetters(int n, int keyLength, string message)
        {
            //Apply RegEx to message to we're not looking at non-alpha characters
            var filteredMessage = Constants.NonlettersPattern.Replace(message, string.Empty);

            var outputBuffer = new StringBuilder();
            for (var i = n - 1; i < filteredMessage.Length; i += keyLength)
            {
                outputBuffer.Append(filteredMessage[i]);
            }

            return outputBuffer.ToString();
        }

        /// <summary>
        ///     Finds 3-5 character repeating sequences
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static Dictionary<string, List<int>> FindRepeatSequencesSpacings(string message)
        {
            var output = new Dictionary<string, List<int>>();

            //Apply RegEx to message to we're not looking at non-alpha characters
            var filteredMessage = Constants.NonlettersPattern.Replace(message.ToUpper(), string.Empty).ToUpper();

            //Sets the length of sequences we'll search for
            for (var i = 3; i < 6; i++)
            {
                //Sets our sequence start
                for (var j = 0; j < filteredMessage.Length-i; j++)
                {
                    //Fing other matching sequences in the string
                    var currentSequence = filteredMessage.Substring(j, i);

                    var sequenceFoundPosition = filteredMessage.IndexOf(currentSequence, j+1, StringComparison.Ordinal);
                    while (sequenceFoundPosition > 0)
                    {
                        //Calulate the Lenth Apart
                        var lengthApart = (sequenceFoundPosition + i) - (j + i);

                        //Mark the position we found it in and increment the number of times we saw it
                        if(!output.ContainsKey(currentSequence))
                            output.Add(currentSequence, new List<int>());
                        if(!output[currentSequence].Contains(lengthApart))
                            output[currentSequence].Add(lengthApart);

                        //Find the next instance
                        sequenceFoundPosition = filteredMessage.IndexOf(currentSequence, sequenceFoundPosition + 1, StringComparison.Ordinal);
                    }
                }             
            }
            return output;
        }

        /// <summary>
        ///     Returns a list of useful factors of num. By "useful" we mean factors less than MAX_KEY_LENGTH + 1.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static List<int> GetUsefulFactors(int number)
        {
            var output = new List<int>();

            for (var i = 2; i <= Constants.MaxKeyLength; i++)
            {
                if (number%i == 0)
                    output.Add(i);
            }

            if (output.Contains(1))
                output.Remove(1);

            return output;
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="sequenceFactors"></param>
        /// <returns></returns>
        public static Dictionary<int, int> GetMostCommonFactors(List<List<int>> sequenceFactors)
        {
            var output = new Dictionary<int, int>();

            foreach (var factor in sequenceFactors.SelectMany(seqFactor => seqFactor))
            {
                if (!output.ContainsKey(factor))
                {
                    output.Add(factor, 1);
                }
                else
                {
                    output[factor]++;
                }
            }

            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public static List<int> KasiskiExamination(string cipherText)
        {
            //First we get the sequence spacing
            var seqSpacing = FindRepeatSequencesSpacings(cipherText);

            //Find the Factors
            //Unrolled
            //var seqList = new List<List<int>>();
            //foreach (var seq in seqSpacing.Values)
            //{
            //    foreach (var spacing in seq)
            //    {
            //        seqList.Add(GetUsefulFactors(spacing));
            //    }
            //}

            var seqList = (from seq in seqSpacing.Values from spacing in seq select GetUsefulFactors(spacing)).ToList();

            //Find the most common factors
            var likelyKeyLengths = GetMostCommonFactors(seqList);

            //Sort the dictionary by factor ocurrences descending
            var sortedCommonFactors = (from entry in likelyKeyLengths orderby entry.Value descending select entry)
                     .Take(3)
                     .ToDictionary(pair => pair.Key, pair => pair.Value);

            //Take the top 3 and return those
            return sortedCommonFactors.Keys.ToList();

        }
    }
}
