using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public struct Input
    {
        public string Instruction;
        public int V;
    }
    internal class Day10 : Problem
    {
        public List<Input> instructions;
        int[] currentCycles;
        int[] calculatedSignalStrength;
        protected override void Part1()
        {
            int X = 1;
            int clock = 0;
            currentCycles = new int[240];
            calculatedSignalStrength = new int[240];
            foreach (var inst in instructions)
            {
                if(inst.Instruction == "noop")
                {
                    clock++;
                }
                else if(inst.Instruction == "addx")
                {
                    currentCycles[clock+2] = inst.V;
                    clock += 2;
                }
            }

            for(int i = 0; i  < currentCycles.Count(); i++)
            {
                calculatedSignalStrength[i] = X;
                X += currentCycles[i];
            }

            var targets = new int[] { 20, 60, 100, 140, 180, 220 };
            var total = 0;
            foreach(var t in targets)
            {
                Console.WriteLine($"20th Cycle: {calculatedSignalStrength[t]}, Signal Strength: {t * calculatedSignalStrength[t]}");
                total += t * calculatedSignalStrength[t];
            }
            Console.WriteLine($"Total signal strength: {total}");
        }

        private bool ValidatePosition(int i)
        {
            return (calculatedSignalStrength[i] == i % 40 || 
                    calculatedSignalStrength[i] == (i - 1) % 40 || 
                    calculatedSignalStrength[i] == (i - 2) % 40);
        }

        protected override void Part2()
        {
            for(int i = 1; i < calculatedSignalStrength.Length; i++)
            {
                if(ValidatePosition(i))
                {
                    Console.Write("#");
                }
                else
                {
                    Console.Write(".");
                }
                if ((i % 40) == 0 && i != 0)
                    Console.WriteLine();
            }
        }

        protected override void ParseInput(string fileName)
        {
            base.ParseInput(fileName);
            instructions = new List<Input>();
            foreach(var line in input)
            {
                var elements = line.Split(' ');
                Input inp = new Input() { Instruction = elements[0], V = (elements.Length >1) ? int.Parse(elements[1]) : 0 };
                instructions.Add(inp);
            }
        }
    }
}
