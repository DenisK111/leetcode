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
    /// 48
    /// </summary>
    internal class RotateImage
    {
        public void Rotate(int[][] matrix)
        {
            var length = matrix.Length;

            for (int row = 0; row <= length / 2; row++)
            {
                for (int col = row; col < length - row - 1; col++)
                {                    
                    var temp = matrix[col][length - 1 - row];                     
                    matrix[col][length -1 - row] = matrix[row][col];
                    var temp2 = matrix[length - 1 - row][length-1-col];
                    matrix[length - 1 - row][length - 1 - col] = temp;
                    temp = matrix[length-1-col][row];
                    matrix[length - 1 - col][row] = temp2;
                    matrix[row][col] = temp;
                }
            }

        }

        [TestCaseSource(nameof(TestData))]
        public void Test((int[][] matrix, int[][] expected) testData)
        {
            Rotate(testData.matrix);
            testData.matrix.ShouldBeEquivalentTo(testData.expected);
        }

        private static IEnumerable<(int[][],int[][])> TestData() {
            yield return ([[1, 2, 3], [4, 5, 6], [7, 8, 9]], [[7, 4, 1], [8, 5, 2], [9, 6, 3]]);
            yield return ([[5, 1, 9, 11], [2, 4, 8, 10], [13, 3, 6, 7], [15, 14, 12, 16]], [[15, 13, 2, 5], [14, 3, 4, 1], [12, 6, 8, 9], [16, 7, 10, 11]]);
        }
    }
}
