using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Day5
    {

        private List<Stack<char>> stacks;
        Regex re;
        List<string> moves;

        public void Run()
        {
            stacks = new List<Stack<char>>() {  new Stack<char>(), new Stack<char>(), new Stack<char>(), new Stack<char>(), new Stack<char>(), new Stack<char>(), new Stack<char>(), new Stack<char>(), new Stack<char>()};
            ParseInput("day5-2.txt");

            PrintStacks();

            moves = File.ReadAllLines("Day5.txt").ToList();
            string pattern = @"move (\d+) from (\d+) to (\d+)";
            re = new Regex(pattern);
            Part1();
            ParseInput("day5-2.txt");
            Part2();
            

        }


       public void Part1()
        {
            Console.WriteLine($"Move crates one at a time:");

            foreach (var mv in moves)
            {
                Match m = re.Match(mv);
                var move = int.Parse(m.Groups[1].Value);
                var from = int.Parse(m.Groups[2].Value);
                var to = int.Parse(m.Groups[3].Value);
                Stack<char> tempStack = new Stack<char>();
                for (int j = 0; j < move; j++)
                {
                    stacks[to-1].Push(stacks[from - 1].Pop());
                }
            }
            PrintStacks();
        }


        public void Part2()
        {
            Console.WriteLine($"Move crates together:");
            foreach (var mv in moves)
            {
                Match m = re.Match(mv);
                var move = int.Parse(m.Groups[1].Value);
                var from = int.Parse(m.Groups[2].Value);
                var to = int.Parse(m.Groups[3].Value);
                Stack<char> tempStack = new Stack<char>();
                for (int j = 0; j < move; j++)
                {
                    tempStack.Push(stacks[from - 1].Pop());
                }
                while (tempStack.Count > 0)
                {
                    stacks[to - 1].Push(tempStack.Pop());

                }
            }
            PrintStacks();
        }

        public void ParseInput(string fileName)
        {
            stacks.ForEach(s => s.Clear());
            List<string> lines = File.ReadAllLines(fileName).ToList();
            int i = 0;
            foreach (var line in lines)
            {
                line.ToCharArray().ToList().ForEach(ch => stacks[i].Push(ch));
                i++;
            }
        }

        public void PrintStacks()
        {
            for (int i = 0; i < 9; i++)
            {
                Console.Write($"{i + 1}: ");
                stacks[i].ToList().ForEach(s => Console.Write($" [{s}] "));
                Console.WriteLine();
                Console.WriteLine();
            }
        }

    }
}
