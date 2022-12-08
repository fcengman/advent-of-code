using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    internal class Day1_2021
    {

        private List<string> input;
        private int count = 0;
        private int sum = 0;

        public void Run()
        {
            ParseInput(@"../../../Input/Day1.txt");
            Part1();
            Part2();
            Console.ReadLine();
        }




        public void ParseInput(string fileName)
        {
            input = File.ReadAllLines(fileName).ToList();
        }

        public void Part1()
        {
            int current = int.Parse(input[0]);
            foreach (var line in input)
            {
                if (line == input[0]) continue;
                if (current < int.Parse(line))
                {
                    count++;
                }
                current = int.Parse(line);
            }
            Console.WriteLine($"Total increases: {count}");

        }

        public void Part2()
        {
            int current = int.Parse(input[0]) + int.Parse(input[1]) + int.Parse(input[2]);
            for (int i = 1; i < input.Count - 2; i++)
            {
                var nextWindow = int.Parse(input[i]) + int.Parse(input[i + 1]) + int.Parse(input[i + 2]);
                if (current < nextWindow)
                {
                    sum++;
                }
                current = nextWindow;
            }
            Console.WriteLine($"Part 2 Total increases: {sum}");
        }

        



    }
}
