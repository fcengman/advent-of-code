using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class HorizontalCrabs
    {
        int test;
        public void Run()
        {
            var input = File.ReadAllText("HorizontalCrabs.txt");
            var splitInput = input.Split(",");
            List<int> numberInput = new List<int>();
            splitInput.ToList().ForEach(i => numberInput.Add(int.Parse(i)));
            numberInput.Sort();
            List<double> distance = new List<double>();
            test = 0;
            foreach (var num in numberInput)
            {
                var sum = numberInput.Select(n => ((Math.Abs(num - n) * Math.Abs(num - n) -1 ) /2) ).Sum();
                distance.Add(sum);
            }
            var minDist = distance.IndexOf(distance.Min());
        }
    }
}
