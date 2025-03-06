using Leeetcode.Problems.Medium;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Leeetcode.Problems.Medium
{
    /// <summary>
    /// 74
    /// </summary>

    internal class MatrixSearch
    {
        public bool SearchMatrix(int[][] matrix, int target)
        {
            var flattened = matrix.SelectMany(m => m).ToArray();

            return BinarySearch(flattened, target, 0, flattened.Length - 1);
        }

        public bool BinarySearch(int[] array, int target, int startIndex, int endIndex) =>
            ((startIndex + endIndex) / 2) switch
            {
                int _ when startIndex > endIndex => false,
                int mid when target == array[mid] => true,
                int mid when target < array[mid] => BinarySearch(array, target, startIndex, mid - 1),
                int mid when target > array[mid] => BinarySearch(array, target, mid + 1, endIndex),
                _ => false
            };
    }


    public class MatrixSearchTests()
    {
        [TestCaseSource(nameof(TestData))]
        public void Test((int[][] matrix, int target, bool expectedResult) testData)
        {
            var (matrix, target, expectedResult) = testData;
            var matrixSearch = new MatrixSearch();
            var result = matrixSearch.SearchMatrix(matrix, target);
            result.ShouldBe(expectedResult);
        }

        public static IEnumerable<(int[][] matrix, int target, bool expectedResult)> TestData()
        {
            yield return (new int[][] { [1, 3, 5, 7], [10, 11, 16, 20], [23, 30, 34, 60] }, 3, true);
            yield return (new int[][] { [1, 3, 5, 7], [10, 11, 16, 20], [23, 30, 34, 60] }, 13, false);
            yield return (new int[][] { [1] }, 1, true);
            yield return (new int[][] { [1] }, 3, false);
            yield return (new int[][] { [] }, 1, false);
        }

    }
}

