using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leeetcode.Problems.Easy
{
    /// <summary>
    /// 26
    /// </summary>
    internal class RemoveDuplicatesFromSortedArray
    {
        public int RemoveDuplicates(int[] nums)
        {
            var duplicates = 0;
            var currIndex = 1;
            for (int i = 1; i < nums.Length; i++) {

                if (nums[i] == nums[i - 1])
                {
                    duplicates++;
                    continue;
                }
                nums[currIndex++] = nums[i];                
            }                               
            return nums.Length - duplicates;            
        }
    }
}
