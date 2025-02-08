using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leeetcode.Problems.Easy
{
    internal class LogestCommonPrefix
    {
        public string LongestCommonPrefixSolve(string[] strs)
        {
            var sb = new StringBuilder();

            int minLength = -1;

            for (int i = 0; i < strs.Length; i++)
            {
                minLength = minLength == -1 ? strs[i].Length : Math.Min(minLength, strs[i].Length);
            }


            for (int i = 0; i < minLength; i++)
            {
                if (strs.All(str => str[i] == strs[0][i]))
                {
                    sb.Append(strs[0][i]);
                }
                else
                {
                    break;
                }

            }

            return sb.ToString();
        }

        [TestCase(new string[] { "flower", "flow", "flight" }, "fl")]
        [TestCase(new string[] { "dog", "racecar", "car" }, "")]
        public void Test(string[] input, string expected) => LongestCommonPrefixSolve(input).ShouldBe(expected);        
    }
}
