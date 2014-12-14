using System.Collections.Generic;
using System.Text;

namespace VigenereSolver.Library.Helpers
{
    public static class Combinator
    {
        /// <summary>
        ///     Takes in the list of characters for each key position and returns all possible valid combinations
        /// </summary>
        /// <param name="letters"></param>
        /// <returns></returns>
        public static List<StringBuilder> Combinations(List<char[]> letters)
        {
            //Take the top most character array which will be processed at this level in the recursive call stack
            var myChars = letters[0];
            letters.RemoveAt(0);
            var myStringSize = letters.Count + 1;
            var output = new List<StringBuilder>();

            //This happens if we're at the deepest in the recursion (last letter)
            if (myStringSize == 1)
            {
                foreach (var c in myChars)
                {
                    var newString = new StringBuilder(myStringSize);
                    newString.Append(c);
                    output.Add(newString);
                }
                return output;
            }

            //Take combinations returned by deeper in the stack and append this levels combinations
            foreach (var s in Combinations(letters))
            {
                foreach (var c in myChars)
                {
                    var newString = new StringBuilder(myStringSize);
                    newString.Append(c);
                    newString.Append(s);
                    output.Add(newString);
                }
            }
            return output;
        }
    }
}
