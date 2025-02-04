using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leeetcode.Problems.Easy
{
    /// <summary>
    /// 1800
    /// </summary>

    internal static class MaxAscendingSubArraySum
    {
        public static int MaxAscendingSum(int[] nums)
        {
            var max = nums[0];
            var curr = max;            

            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] > nums[i -1])
                {
                    curr += nums[i];
                    if (curr > max)
                    {
                        max=curr;
                    }
                }
                else
                {
                    curr = nums[i];
                }
            }

            return max;
        }
    }

    [TestFixture]
    public class Test()
    {

        [TestCase(new int[] { 10, 20, 30, 5, 10, 50 }, 65)]
        [TestCase(new int[] { 10, 20, 30, 40, 50 }, 150)]
        [TestCase(new int[] { 12, 17, 15, 13, 10, 11, 12 }, 33)]
        [TestCase(new int[] { 1 }, 1)]

        public void MaxAscendingSum_ReturnsCorrectSum(int[] numbers, int expectedSum)
        {
            // Act
            int result = MaxAscendingSubArraySum.MaxAscendingSum(numbers);

            // Assert
            result.ShouldBe(expectedSum);
        }

        public static IEnumerable<object[]> GetArrayTestData()
            {
                yield return new object[] { new int[] { 10, 20, 30, 5, 10, 50 }, 65 };
                yield return new object[] { new int[] { 10, 20, 30, 40, 50 }, 150 };
                yield return new object[] { new int[] { 12, 17, 15, 13, 10, 11, 12 }, 33 };
                yield return new object[] { new int[] { 1 }, 1 };
            }        
    }
}
