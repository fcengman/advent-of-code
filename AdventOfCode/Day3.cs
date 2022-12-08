
using System.Collections.Generic;

namespace AdventOfCode2022
{
    internal class Day3
    {
        private string priorities => " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";


        public void Run()
        {
            var lines = File.ReadAllLines("Day1.txt").ToList();

            var sum = 0.0;
            //Part 1
            lines.ForEach(l => sum += priorities.IndexOf(FindDuplicate(l)));
            Console.WriteLine($"Rugsack Sum: {sum}");

            // part 2
            sum = 0.0;
            for (int i = 0; i < lines.Count(); i += 3)
            {
                sum += priorities.IndexOf(FindBadge(lines[i], lines[i + 1], lines[i + 2]));
            }
            Console.WriteLine($"Elves Sum: {sum}");

            Console.ReadLine();
        }

        public char FindBadge(string l1, string l2, string l3)
        {
            var one = l1.ToCharArray();
            var two = l2.ToCharArray();
            var three = l3.ToCharArray();
            return one.Intersect(two).Intersect(three).First();
        }

        public char FindDuplicate(string line)
        {
            HashSet<char> left = new HashSet<char>();
            HashSet<char> right = new HashSet<char>();
            for (int i = 0; i < line.Length / 2; i++)
            {
                left.Add(line[i]);
                right.Add(line[line.Length - 1 - i]);
            }

            return left.Intersect(right).ToArray().First();
        }
    }
}
