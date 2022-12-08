using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Templace
    {
        List<string> input;
        public void Run()
        {
            ParseInput(@"../../../Input/Day");
            Part1();
            Part2();
        }

        private void Part1()
        {
            
        }

        private void Part2()
        {
            
        }

        private void ParseInput(string fileName)
        {
            input = File.ReadAllLines(fileName).ToList();
            
        }
    }
}
