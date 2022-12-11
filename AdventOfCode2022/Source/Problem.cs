using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal abstract class Problem
    {
        protected List<string> input;
        protected string path;

        public Problem()
        {
            input = new List<string>();
        }

        public virtual void Run(string filePath)
        {
            path = filePath; 
            ParseInput(filePath);
            Part1();
            Part2();
            Console.ReadLine();
        }

        protected abstract void Part1();
        protected abstract void Part2();

        protected virtual void ParseInput(string filePath)
        {
            input = File.ReadAllLines(filePath).ToList();
        }

        public bool TestDay()
        {
            Debug.WriteLine($"Hello from {this.GetType()}");
            return true;
        }
    }
}
