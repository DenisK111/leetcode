using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leeetcode.Problems.Medium
{
    /// <summary>
    /// 2349
    /// </summary>

    internal class NumberContainers
    {
        private readonly Dictionary<int, int> _indexToNumber = new();
        private readonly Dictionary<int, SortedSet<int>> _numberToIndexes = new();
        public NumberContainers()
        {

        }

        public void Change(int index, int number)
        {

            if (_indexToNumber.ContainsKey(index))
            {
                var prevNumber = _indexToNumber[index];
                if (number == prevNumber) return;
                _numberToIndexes[prevNumber].Remove(index);
            }
            _indexToNumber[index] = number;
            if (!_numberToIndexes.ContainsKey(number))
            {
                _numberToIndexes.Add(number, new SortedSet<int>() { });
            }
            _numberToIndexes[number].Add(index);
        }

        public int Find(int number) => _numberToIndexes.TryGetValue(number, out var value) ?
            value.Count > 0 ? value.ElementAt(0) : -1 : -1;


        [TestCaseSource(nameof(TestSet1))]
        [TestCaseSource(nameof(TestSet2))]
        public void Test((string[] commands, int[][] input, object[] expected) testData)
        {
            var (commands, input, expected) = testData;
            var numbersContainer = new NumberContainers();
            var output = new object[expected.Length];
            output[0] = null!;

            for (int i = 1; i < commands.Length; i++)
            {
                switch (commands[i])
                {
                    case "find": output[i] = numbersContainer.Find(input[i][0]); break;
                    case "change":
                        numbersContainer.Change(input[i][0], input[i][1]);
                        output[i] = null!;
                        break;
                    default: throw new ArgumentException("Invalid Test Data!");
                }
            }

            output.ShouldBeEquivalentTo(expected);
        }

        internal static IEnumerable<(string[], int[][], object[])> TestSet1()
        {
            string[] commands = ["NumberContainers", "find", "change", "change", "change", "change", "find", "change", "find"];
            int[][] input = [[], [10], [2, 10], [1, 10], [3, 10], [5, 10], [10], [1, 20], [10]];
            object[] expected = [null!, -1, null!, null!, null!, null!, 1, null!, 2];
            yield return (commands, input, expected);
        }

        internal static IEnumerable<(string[], int[][], object[])> TestSet2()
        {
            string[] commands = ["NumberContainers", "change", "change", "find", "change", "find"];
            int[][] input = [[], [1, 10], [1, 10], [10], [1, 20], [10]];
            object[] expected = [null!, null!, null!, 1, null!, -1];
            yield return (commands, input, expected);
        }
    }
}
