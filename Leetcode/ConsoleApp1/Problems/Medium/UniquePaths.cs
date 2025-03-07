﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leeetcode.Problems.Medium
{
    /// <summary>
    /// 62
    /// </summary>
    internal class UniquePaths
    {        
        public int Solve(int m, int n)
        {
            var count = 0;
            int finish = 2;
            int visited = 1;

            var matrix = new int[m, n];
            matrix[m - 1, n - 1] = finish;
            
            var rowLength = matrix.GetLength(0);
            var colLength = matrix.GetLength(1);
            Traverse(matrix, 0, 0);

            return count;

            void Traverse(int[,] matrix, int row, int col)
            {
                if (row >= rowLength ||  col >= colLength) return;
                if (matrix[row, col] == finish)
                {
                    PrintMatrix(matrix);
                    Console.WriteLine();
                    count++;
                    return;
                }
                matrix[row, col] = visited;   
                Traverse(matrix, row + 1, col);
                Traverse(matrix, row, col + 1);
                matrix[row, col] = visited;
            }
        }


        private void PrintMatrix(int[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.Write(matrix[row, col]);
                }

                Console.WriteLine();
            }
        }        
    }

    public class UniquePathsTests
    {
        [TestCase(3, 7,ExpectedResult = 28)]
        [TestCase(3, 2, ExpectedResult = 3)]        
        [TestCase(1, 2, ExpectedResult = 1)]
        [TestCase(1, 10, ExpectedResult = 1)]
        [TestCase(2, 2, ExpectedResult = 2)]
        public int Test(int m, int n) => new UniquePaths().Solve(m, n);
    }
    
}
