using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    internal class Day2_2021
    {

        private List<string> input;
        private int depth = 0;
        private int horizontal = 0;
        private int aim = 0;


        public void Run()
        {
            ParseInput(@"../../../Input/Day3/Day2.txt");

            Part1();
            Part2();
            Console.ReadLine();
        }

        public void Part1()
        {

            foreach (var line in input)
            {
                var elements = line.Split(' ');
                var move = int.Parse(elements[1]);
                if (elements[0] == "forward")
                {
                    horizontal += move;
                }
                else if (elements[0] == "down")
                {
                    depth += move;
                }
                else if (elements[0] == "up")
                {
                    depth -= move;
                }
                Console.WriteLine($"Horizontal: {horizontal}, Depth: {depth}");
            }
            Console.WriteLine($"Final Position: {horizontal * depth}");
        }

        public void Part2()
        {
            horizontal = 0;
            depth = 0;
            foreach (var line in input)
            {
                var elements = line.Split(' ');
                var move = int.Parse(elements[1]);
                if (elements[0] == "forward")
                {
                    horizontal += move;
                    depth += move * aim;
                }
                else if (elements[0] == "down")
                {
                    aim += move;
                }
                else if (elements[0] == "up")
                {
                    aim -= move;
                }
                Console.WriteLine($"Horizontal: {horizontal}, Depth: {depth}, Aim: {aim}");
            }
            Console.WriteLine($"Final Position: {horizontal * depth}");
        }

        public void ParseInput(string fileName)
        {
            input = File.ReadAllLines(fileName).ToList();
        }
    }
}
