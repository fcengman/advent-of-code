using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode1
{
    internal class Day6
    {
        string input;


        public void Run()
        {
            input = File.ReadAllText("Day6.txt");
            FindDistinct(4); // Part 1
            FindDistinct(14); // Part 2

            Console.ReadLine();
        }

        private void FindDistinct(int length)
        {
            for (int i = 0; i < input.Length - length; i++)
            {
                var subset = input.Substring(i, length);
                if (subset.Distinct().Count() == length)
                {
                    Console.WriteLine($"Index Position for length ({length}): {i+length}");
                    break;
                }
            }
        }
    }
}
