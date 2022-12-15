using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Day14 : Problem
    {
        public char[,] grid;
        private bool done;
        protected override void Part1()
        {
            PrintGrid();
            (int, int) source = (6, 0);
            var count = 0;
            while(!done)
            {
                var sand = source;
                while (true)
                {
                    if (CanMove(sand.Item1, sand.Item2+1))
                        sand.Item2++;
                    else if(CanMove(sand.Item1-1, sand.Item2+1))
                    {
                        sand.Item2++;
                        sand.Item1--;
                    }
                    else if (CanMove(sand.Item1+1, sand.Item2+1))
                    {
                        sand.Item1++;
                        sand.Item2++;
                    }
                    else
                    {
                        count++;
                        grid[sand.Item1, sand.Item2] = 'o';
                        break;
                    }
                }
                    PrintGrid();
            }
            Console.WriteLine($"Number of sand pieces: {count-1}");
        }

        private bool CanMove(int column, int row)
        {
            if (row > grid.GetLength(0) - 1 || row < 0)
            {
                done = true;
                return false;
            }
            if (column > grid.GetLength(1) - 1 || column < 0)
            {
                done = true;
                return false;
            }
            return grid[column, row] == '.';
        }

        protected override void Part2()
        {
            
        }

        private void PrintGrid()
        {
            for(int i = 0; i < grid.GetLength(1); i++)
            {
                for(int j = 0; j < grid.GetLength(0); j++)
                {
                    Console.Write($"{grid[j, i]}");
                }
                Console.WriteLine();
            }
        }

        protected override void ParseInput(string fileName)
        {
            base.ParseInput(fileName);
            List<List<int>> rows = new List<List<int>>();
            List<List<int>> columns = new List<List<int>>();
            foreach (var line in input)
            {
                List<int> rowElements = new List<int>();
                List<int> columnElements = new List<int>();
                var elements = line.Split(' ').ToList();
                foreach(var element in elements)
                {
                    if (char.IsDigit(element[0]))
                    {
                        var grid = element.Split(',');
                        columnElements.Add(int.Parse(grid[0]));
                        rowElements.Add(int.Parse(grid[1]));
                    }
                }
                columns.Add(columnElements);
                rows.Add(rowElements);
            }


            var minColumn = columns.Select(x => x.Min()).Min();
            var maxRow = rows.Select(x => x.Max()).Max();
            var maxColumn = columns.Select(x => x.Max()).Max();
            
            columns = columns.Select(x => x.Select(c => c -= minColumn).ToList()).ToList();


            grid = new char[maxRow+1, (maxColumn - minColumn)+1];
            for (int i = 0; i < grid.GetLength(1); i++)
            {
                for (int j = 0; j < grid.GetLength(0); j++)
                {
                    grid[j, i] = '.';
                }
            }
            for (int i = 0; i < columns.Count(); i++)
            {
                for(int j = 1; j < columns[i].Count(); j++)
                {
                    var rowValue = rows[i][j];
                    var prevRowValue = rows[i][j-1];
                    var colValue = columns[i][j];
                    var prevColValue = columns[i][j-1];
                    var rowDiff = rowValue - prevRowValue;
                    var columnDiff = colValue -prevColValue;
                    if(rowDiff != 0)
                    {
                        if(rowDiff > 0)
                        {
                            for(int s = rowDiff; s >= 0; s--)
                            {
                                grid[colValue, rowValue- s] = '#';
                            }
                            //PrintGrid();
                        }
                        else
                        {
                            for (int s = rowDiff; s <= 0; s++)
                            {
                                grid[prevColValue, rowValue + s] = '#';
                            }
                            //PrintGrid();
                        }
                    }
                    else
                    {
                        if (columnDiff > 0)
                        {
                            for (int s = columnDiff; s >= 0; s--)
                            {
                                grid[colValue - s, rowValue] = '#';
                            }
                            PrintGrid();
                        }
                        else
                        {
                            for (int s = columnDiff; s <= 0; s++)
                            {
                                grid[s + prevColValue, rowValue] = '#';
                            }
                            //PrintGrid();
                        }
                    }
                }
            }


            for(int i = 0; i < grid.GetLength(1); i++)
            {
                for(int j = 0; j < grid.GetLength(0); j++)
                {
                    if (grid[i, j] == '#') continue;
                    grid[i, j] = '.';
                }
            }

        }
    }
}
