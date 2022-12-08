using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Problem
    {
        protected List<string> input;

        public Problem()
        {
            input = new List<string>();
        }

        public void Run(string filePath)
        {
            ParseInput(filePath);
            Part1();
            Part2();
        }

        protected virtual void Part1()
        {

        }
        protected virtual void Part2()
        {

        }

        protected virtual void ParseInput(string filePath)
        {
            input = File.ReadAllLines(filePath).ToList();
        }

        public bool TestDay()
        {
            Debug.WriteLine("Hello from Day 9");
            return true;
        }
    }
}
