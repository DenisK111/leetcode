using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leeetcode.Problems.Medium
{
    /// <summary>
    /// 22
    /// </summary>
    internal class GenerateParentheses
    {
        public IList<string> GenerateParenthesis(int n)
        {
            string input = new string('(',n) + new string(')',n);
            List<string> result = new List<string>();
            char[] chars = input.ToCharArray();            

            Permute(chars, new bool[chars.Length], new List<char>(n * 2), result);

            // Print results
            return result;
        }

        static int usedOpen = 0;
        static int usedClosed = 0;

        static bool isValid() => usedOpen >= usedClosed;

        static void Increment(char c)
        {
            switch (c)
            {
                case '(': usedOpen++; break;
                case ')': usedClosed++; break;
                default:break;

            }
        }

        static void Decrement(char c)
        {
            switch (c)
            {
                case '(': usedOpen--; break;
                case ')': usedClosed--; break;
                default: break;

            }
        }

        private static void Permute(char[] chars, bool[] used, List<char> current, List<string> result)
        {           

            if (current.Count == chars.Length)
            {
                result.Add(string.Join("",current));
                return;
            }

            for (int i = 0; i < chars.Length; i++)
            {
                // Skip used characters
                if (used[i]) continue;

                // Avoid duplicate permutations by skipping identical characters that are not used yet
                if (i > 0 && chars[i] == chars[i - 1] && !used[i - 1]) continue;


                Increment(chars[i]);
                if (!isValid())
                {
                    Decrement(chars[i]);
                    continue;
                }
                // Mark character as used
                used[i] = true;
                current.Add(chars[i]);                
                Permute(chars, used, current, result);
                used[i] = false;  // Backtrack
                Decrement(chars[i]);
                current.RemoveAt(current.Count - 1);
            }
        }

        [TestCase(3, new string[] { "((()))", "(()())", "(())()", "()(())", "()()()" })]
        [TestCase(1, new string[] { "()"})]
        public void Test(int value, string[] expectedResult) => GenerateParenthesis(value).ToArray().ShouldBeEquivalentTo(expectedResult);
    }
}
