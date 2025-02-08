using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leeetcode.Problems.Medium
{
    internal class DistinctColorsAmongBalls
    {
        /// <summary>
        /// 3160
        /// </summary>
        /// 

        public int[] QueryResults(int limit, int[][] queries)
        {
            var balls = new Dictionary<int,int>();
            var colorsCount = new Dictionary<int, int>();
            var output = new int[queries.GetLength(0)];            

            for (int i = 0; i < output.Length; i++)
            
            {
                var currBallColour = balls.GetValueOrDefault(queries[i][0]);
                if (currBallColour == queries[i][1])
                {
                    output[i] = colorsCount.Count;
                    continue;
                }
                if (!colorsCount.ContainsKey(queries[i][1]))
                {
                    colorsCount[queries[i][1]] = 0;
                }

                colorsCount[queries[i][1]]++;

                if (colorsCount.ContainsKey(currBallColour) && colorsCount[currBallColour] > 1)
                {
                    colorsCount[currBallColour]--;
                }
                else
                {
                    colorsCount.Remove(currBallColour);
                }

                balls[queries[i][0]] = queries[i][1];
                output[i] = colorsCount.Count;
            }

            return output;

        }

        [TestCaseSource(nameof(TestSet))]
        [TestCaseSource(nameof(TestSet2))]
        public void Test((int limit, int[][] queries, int[] expected) testData)
        {            
            var result = QueryResults(testData.limit, testData.queries);

            result.ShouldBeEquivalentTo(testData.expected);
        }

        internal static IEnumerable<(int, int[][], int[])> TestSet()
        {
            int[][] query = [[1, 4], [2, 5], [1, 3], [3, 4]];
            int limit = 4;
            int[] expected = [1, 2, 2, 3];
            yield return (limit,query, expected);
        }

        internal static IEnumerable<(int, int[][], int[])> TestSet2()
        {
            int[][] query = [[0, 1], [1, 2], [2, 2], [3, 4], [4, 5]];
            int limit = 4;
            int[] expected = [1, 2, 2, 3, 4];
            yield return (limit, query, expected);
        }
    }
}
