using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Packet
    {
        internal string left;
        internal string right;

        public override string ToString()
        {
            return $"left: {left} right: {right}";
        }
    }

    internal class Day13 : Problem
    {
        public List<Packet> packets;
        public int count;
        public List<int> indices;
        protected override void Part2()
        {
            
            List<string> allPackets = input.Where(x => x.Length > 0).ToList();
            allPackets.Add("[[2]]");
            allPackets.Add("[[6]]");
            allPackets.Sort(ComparePacket);
            allPackets.ForEach(x => Console.WriteLine(x));
            var one = allPackets.IndexOf("[[2]]")+1;
            var two = allPackets.IndexOf("[[6]]")+1;
            Console.WriteLine($"[[2]] Position: {one} [[6]] Position: {two}. Distress Signal Decoder: {one * two}");
        }

        private int ComparePacket(string s1, string s2)
        {
            return CompareLists(s2, s1) == -1 ? -1 : 1;
        }

        public int CompareNumber(int left, int right)
        {
            if(left == right) return 0;
            return (left > right) ? -1 : 1; 

        }

        private bool IsList(string list)
        {
            foreach(var c in list)
            {
                if (c == '[' || c == ']')
                    return true;
            }
            return false;
        }


        protected override void Part1()
        {
            int count = 0;
            indices = new List<int>();
            int idx = 1;
            foreach (var packet in packets)
            {

                var left = packet.left;
                var right = packet.right;

                if (CompareLists(left, right) != -1)
                {
                    count++;
                    indices.Add(idx);
                }
                idx++;

            }
            Console.WriteLine($"Sum of all indices: {indices.Sum()}");
        }

      

        public int CompareLists(string left, string right)
        {
            // trim brackets
            left = left.Substring(1, left.Count() - 2);
            right = right.Substring(1, right.Count() - 2);

            // Get all elements in each list.
            var leftList = SplitList(left);
            var rightList = SplitList(right);
            
            // Iterate over left list.  
            for(var i = 0; i < leftList.Count(); i++)
            {
                // if right side runs out =  not equal.
                if (i >= rightList.Count()) return -1;
                
                var leftElement = leftList[i];
                var rightElement = rightList[i];

                // ignore when both are empty lists
                if(leftElement == "[]" && rightElement == "[]") continue;

                /// Case 1: Both are lists
                if (IsList(leftElement) && IsList(rightElement))
                {
                    return CompareLists(leftElement, rightElement);
                }
                
                // Case 2: Both are numbers
                else if (char.IsDigit(leftElement[0]) && char.IsDigit(rightElement[0]))
                {
                    if (CompareNumber(int.Parse(leftElement), int.Parse(rightElement)) == -1)
                    {
                        return -1;
                    }
                    if (CompareNumber(int.Parse(leftElement), int.Parse(rightElement)) == 1)
                    {
                        return 1;
                    }
                }

                // Case 3a: Convert right side to list
                else if (IsList(leftElement))
                {
                    rightElement = $"[{rightElement}]";
                    return CompareLists(leftElement, rightElement);
                }
                // case 3b: convert left side to list. 
                else if (IsList(rightElement))
                {
                    leftElement = $"[{leftElement}]";
                    return CompareLists(leftElement, rightElement);
                }
            }
            return 0; 
        }

        public List<string> SplitList(string list)
        {
            int idx = 0;
            int start = 0;
            List<string> results = new List<string>();
            if (list.Length == 0) return results; //Empty list
            char currentElement;
            while (idx < list.Length)
            {
                
                Stack<char> stack = new Stack<char>();
                while (idx < list.Length)
                {
                    currentElement = list[idx];
                    if (currentElement == '[')
                    {
                        stack.Push(currentElement);
                    }
                    if (currentElement == ',' && stack.Count == 0)
                        break;
                    if (currentElement == ']')
                    {
                        stack.Pop();

                    }
                    idx++;
                }
                results.Add(list.Substring(start, idx - start));
                idx++;
                start = idx;
            }
            return results;
            
        }

        protected override void ParseInput(string fileName)
        {
            base.ParseInput(fileName);
            packets = new List<Packet>();
            for(int i = 0; i < input.Count; i+=3)
            {
                packets.Add(new Packet() { left = input[i], right = input[i+1] });
            }
        }
    }
}
