using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public static class Grid
    {
        public static char [,] Generate2DGridFromInput(List<string> input)
        {
            char [,] grid = new char[input.Count, input[0].Length];
            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    grid[i, j] = input[i][j];
                }
            }
            return grid;
        }
    }
}
