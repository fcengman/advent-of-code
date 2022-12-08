using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    internal class Day3_2021
    {
        private List<string> input;
        private short gamma = 0;
        private short epsilon = 0;

        public void Run()
        {
            ParseInput();

            Part1();
            Part2();
            Console.ReadLine();
        }

        

        private void Part1()
        {
            var length = input.Count / 2;
            List<int> counts = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            foreach (var line in input)
            {
                counts[0] += IsOne(line[0]);
                counts[1] += IsOne(line[1]);
                counts[2] += IsOne(line[2]);
                counts[3] += IsOne(line[3]);
                counts[4] += IsOne(line[4]);
                counts[5] += IsOne(line[5]);
                counts[6] += IsOne(line[6]);
                counts[7] += IsOne(line[7]);
                counts[8] += IsOne(line[8]);
                counts[9] += IsOne(line[9]);
                counts[10] += IsOne(line[10]);
                counts[11] += IsOne(line[11]);
            }
            string output = "";
            foreach (int count in counts)
            {
                output += (count >= length) ? "1" : "0";
            }

            gamma = Convert.ToInt16(output, 2);
            epsilon = (short)(~gamma & 0b0000111111111111);
            Console.WriteLine($"Gamma: {Convert.ToString(gamma, 2)}\nEpsilon: {Convert.ToString(epsilon, 2)}");
            Console.WriteLine($"Power Consumption: {gamma * epsilon}");
        }

        private int IsOne(char bit) => (bit == '1') ? 1 : 0;


        private void Part2()
        {
            var length = input.Count / 2;
            int count = 0;
            int position = 0;
            List<string> oxygen = input;
            List<string> scrubber = input;
            while (oxygen.Count > 1)
            {
                foreach (var line in oxygen)
                {
                    count += IsOne(line[position]);
                }
                length = (oxygen.Count % 2 == 0) ? oxygen.Count / 2 : (oxygen.Count / 2) + 1;
                var keepBit = (count >= length) ? '1' : '0';
                oxygen = (from i in oxygen
                          where i[position] == keepBit
                          select i).ToList();
                position++;
                count = 0;

            }
            length = input.Count / 2;
            position = 0;
            while (scrubber.Count > 1)
            {
                foreach (var line in scrubber)
                {
                    count += IsOne(line[position]);
                }
                length = (scrubber.Count % 2 == 0) ? scrubber.Count / 2 : (scrubber.Count / 2) + 1;
                var keepBit = (count >= length) ? '0' : '1';
                scrubber = (from i in scrubber
                            where i[position] == keepBit
                            select i).ToList();
                position++;
                count = 0;

            }
            Console.WriteLine($"Oxygen: {oxygen[0]}\nEpsilon: {scrubber[0]}");
            Console.WriteLine($"Power Consumption: {Convert.ToInt16(oxygen[0], 2) * Convert.ToInt16(scrubber[0], 2)}");
        }


        private void ParseInput()
        {
            input = File.ReadAllLines("../../../Input/Day3.txt").ToList();
        }


    }
}
