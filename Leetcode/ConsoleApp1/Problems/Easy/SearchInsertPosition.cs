using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leeetcode.Problems.Easy
{
    /// <summary>
    /// 35
    /// </summary>
    internal class SearchInsertPosition
    {
        public int SearchInsert(int[] nums, int target)
        {
            int left = 0, right = nums.Length - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2; 

                if (nums[mid] == target)
                    return mid; 

                if (nums[mid] < target)
                    left = mid + 1; 
                else
                    right = mid - 1; 
            }

            return left;
        }

        [TestCase(new int[] { 1, 3, 5, 6 }, 5, ExpectedResult = 2)]
        [TestCase(new int[] { 1, 3, 5, 6 }, 2, ExpectedResult = 1)]
        [TestCase(new int[] { 1, 3, 5, 6 }, 7, ExpectedResult = 4)]
        [TestCase(new int[] { }, 2, ExpectedResult = 0)]
        public int Test(int[] nums, int target) => SearchInsert(nums, target);

    }
}
