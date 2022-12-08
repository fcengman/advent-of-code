using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2022
{


    
    internal class Day8
    {
        int[,] trees;
        int outsideTotal;
        List<string> input;
        private int Length => input[0].Length;
        private int Height => input.Count();
        public void Run()
        {
            ParseInput(@"../../../Input/Day8/test.txt");
            Part1();
            Part2();
        }

        private  void Part1()
        {
            outsideTotal = (Length * 2) + (Height * 2 - 4);
            int current, treeCount = 0;
            bool isVisible = false;

            //Rows
            for (int i = 1; i < Length-1; i++)
            {
                //Columns
                for (int j = 1; j < Height-1; j++)
                {
                    isVisible = false;
                    current = trees[i, j];
                    // Up
                    for (int u = 0; u <= i ; u++)
                    {
                        if (u == i)
                        {
                            isVisible = true;
                        }
                        if (trees[u, j] >= current) break;
                    }
                    //left
                    for (int v = 0; v <= j; v++)
                    {
                        if (v == j)
                        {
                            isVisible = true;
                        }
                        if (trees[i, v] >= current) break;
                            
                    }
                    //down
                    for (int u = Length-1; u >= i; u--)
                    {
                        if (u == i)
                        {
                            isVisible = true;
                        }
                        if (trees[u, j] >= current) break;
                    }
                    //right
                    for (int v = Height-1; v >= j; v--)
                    {
                        if (v == j)
                        {
                            isVisible = true;
                        }
                        if (trees[i, v] >= current) break;

                    }

                    if (isVisible)
                        treeCount++;
                }
            }

            Console.WriteLine($"Total outside trees: {outsideTotal}\nTotal Inside Trees: {treeCount}\nTotal Trees: {outsideTotal + treeCount}");
            Console.ReadLine();
        }

        private void Part2()
        {
            int[,] viewingScore = new int[Length, Height];
            int current, left, right, up, down;
            // Rows
            for (int i = 0; i < Length - 1; i++)
            {
                // Columns
                for (int j = 0; j < Height - 1; j++)
                {
                    left = right = up = down = 0;
                    current = trees[i, j];
                    //up
                    for (int u = i-1; u >= 0; u--)
                    {
                        
                        if (trees[u, j] < current) up++;
                        else if (trees[u, j] >= current)
                        {
                            up++;
                            break;
                        }
                    }
                    //left
                    for (int v = j-1; v >= 0; v--)
                    {
                        if (trees[i, v] < current) left++;
                        else if (trees[i, v] >= current)
                        {
                            left++;
                            break;
                        }
                    }
                    //down
                    for (int u = i+1; u < Height; u++)
                    {
                        if (trees[u, j] < current) down++;
                        else if (trees[u, j] >= current)
                        {
                            down++;
                            break;
                        }
                    }
                    //right
                    for (int v = j+1; v < Length; v++)
                    {
                        
                        if (trees[i, v] < current) right++;
                        else if (trees[i, v] >= current)
                        {
                            right++;
                            break;
                        }
                    }
                    viewingScore[i, j] = left * right * up * down;
                }
            }

            int max = viewingScore.Cast<int>().Max();
            Print2DArray(viewingScore);
            Console.WriteLine($"Max Viewing Score: {max}");
            Console.ReadLine();
        }

        private void Print2DArray(int[,] matrix)
        {
            Console.WriteLine($"Viewing Scores:");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        private void ParseInput(string fileName)
        {
            input = File.ReadAllLines(fileName).ToList();
            trees = new int[Length, Height];
            for (int i = 0; i < input.Count(); i++)
            {
                var current = input[i];
                for(int j = 0; j < current.Length; j++)
                {
                    trees[i, j] = int.Parse(current[j].ToString());
                }
            }
        }
    }
}
