using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public struct Operation
    {
        public string op;
        public int variable;
    }
    public class Monkey
    {
        public int id;
        public Operation operation;
        public Queue<long> startingItems;
        public int inspectCount;
        public long test;
        public int monkeyTrue;
        public int monkeyFalse;

        public Monkey()
        {
            startingItems = new Queue<long>();
            operation = new Operation();
        }

        public long Inspect(long item)
        {
            if (operation.op == "*")
                item *= (operation.variable == -1) ? item : operation.variable;
            else
                item += (operation.variable == -1) ? item : operation.variable;

            return item;
        }

        public int Test(long item)
        {
            return (item % test == 0) ? monkeyTrue : monkeyFalse;
        }

        public void PrintItems()
        {
            Console.Write($"Monkey: {id} (");
            foreach(var item in startingItems)
            {
                Console.Write($" {item}, ");
            }
            Console.Write(")");
            Console.WriteLine();
        }

        public void PrintInspection()
        {
            Console.WriteLine($"Monkey: {id} inspected items {inspectCount} times");
        }

        public override string ToString()
        {
            var output = $"Monkey: {id}\n Operation: {operation.op} {operation.variable}\n Starting Items: ";
            foreach (var item in startingItems)
            {
                output += $"{item}, ";
            }
            output += $"\n If True: {monkeyTrue}\n If False: {monkeyFalse}\n";
            return output;
        }
    }
    internal class Day11 : Problem
    {
        public List<Monkey> monkeys;
        protected override void Part1()
        {
            for (int i = 0; i < 20; i++)
            {
                foreach (var m in monkeys)
                {
                    long item = 0;
                    while (m.startingItems.Count > 0)
                    {
                        m.inspectCount++;
                        item = m.startingItems.Dequeue();
                        item = m.Inspect(item);
                        item /= 3;
                        monkeys[m.Test(item)].startingItems.Enqueue(item);
                    }
                }
            }
            PrintResults(monkeys, "Part 1:");
        }

        protected override void Part2()
        {
            monkeys.Clear();
            ParseInput(path);
            long moduloOperator = 1;
            monkeys.ForEach(m => moduloOperator *= m.test);

            for (int i = 0; i < 10000; i++)
            {
                foreach (var m in monkeys)
                {
                    long item = 0;
                    while (m.startingItems.Count > 0)
                    {
                        m.inspectCount++;
                        item = m.startingItems.Dequeue();
                        item = m.Inspect(item);
                        item %= moduloOperator;
                        monkeys[m.Test(item)].startingItems.Enqueue(item);
                    }
                }
            }
            PrintResults(monkeys, "Part 2:");
        }

        private void PrintResults(List<Monkey> monkeys, string part)
        {
            var inspectLists = monkeys.OrderByDescending(m => m.inspectCount)
                                      .Take(2)
                                      .Select(m => m.inspectCount)
                                      .ToArray();
            ulong total = ((ulong)inspectLists[0] * (ulong)inspectLists[1]);
            Console.WriteLine($"{part}\n1: {inspectLists[0]}, 2: {inspectLists[1]}, Total: {total}");
        }

        protected override void ParseInput(string fileName)
        {
            base.ParseInput(fileName);
            monkeys = new List<Monkey>();
            for (int i = 0; i < input.Count; i+=7)
            {
                Monkey m = new Monkey();
                //id
                Regex re = new Regex(@"Monkey (\d+):");
                Match match = re.Match(input[i]);
                m.id = int.Parse(match.Groups[1].Value);
                //starting items
                re = new Regex(@"Starting items: (.*)");
                match = re.Match(input[i+1]);
                var items = match.Groups[1].Value.Split(',');
                foreach(var item in items)
                {
                    m.startingItems.Enqueue(long.Parse(item.Trim()));
                }
                //operation
                re = new Regex(@"Operation: new = (.*)");
                match = re.Match(input[i + 2]);
                var elements = match.Groups[1].Value.Split(' ');
                m.operation = new Operation()
                {
                    op = elements[1],
                    variable = (elements[2] == "old") ? -1 : int.Parse(elements[2])
                };
                //test
                re = new Regex(@"(\d+)");
                match = re.Match(input[i + 3]);
                m.test = long.Parse(match.Groups[1].Value);
                // if true
                match = re.Match(input[i + 4]);
                m.monkeyTrue = int.Parse(match.Groups[1].Value);
                // if false
                match = re.Match(input[i + 5]);
                m.monkeyFalse = int.Parse(match.Groups[1].Value);
                monkeys.Add(m);
                
            }
        }
    }
}
