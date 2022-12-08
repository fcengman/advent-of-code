using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public class Element
    {
        public string dir;
        public int size;
        public Element parent;
        public List<Element> children;
        public int totalSum;
   }

    
    internal class Day7
    {
        private Element root;
        private List<int> sums;

        private int TotalDiskSpace => 70_000_000;
        private int TotalDirSize => 100_000;
        private int SpaceNeededForPatch => 30_000_000;

        public void Run()
        {
            ParseInput("Day7.txt");
            Part1();
            Part2();
        }

        public void Part1()
        {
            sums = new List<int>();
            AddSums(root);

            var total = (from sum in sums
                         where sum < TotalDirSize
                         select sum).Sum();

            Console.WriteLine($"Sum of the total sizes of directories at most 100_000: {total}");
        }

        public void Part2()
        {
            var usedSpace = FindSumOfAllChildren(root);
            var amountNeeded = SpaceNeededForPatch - (TotalDiskSpace - usedSpace);
            
            int directoryToBeDeleted = (from sum in sums
                                        where sum > amountNeeded
                                        select sum).Min();

            Console.WriteLine($"Total Used Space: {usedSpace}");
            Console.WriteLine($"Free Space: {TotalDiskSpace - usedSpace}");
            Console.WriteLine($"Space Needed: {amountNeeded}");
            Console.WriteLine($"Smallest directory to delete: {directoryToBeDeleted}");
        }

        private void PrintRoot(Element root, string tabs)
        {
            if (root == null) return;

            Console.WriteLine($"{tabs}{root.dir} {root.size} | {root.totalSum}");
            tabs += " | ";
            foreach(var child in root.children)
            {
                PrintRoot(child, tabs);
            }
        }

        public int FindSumOfAllChildren(Element element)
        {
            int sum = element.size;
            foreach(var child in element.children)
            {
                sum += FindSumOfAllChildren(child);
            }
            return sum;

        }

        private void AddSums(Element root)
        {
            if (root == null) return;

            root.totalSum = FindSumOfAllChildren(root);
            sums.Add(root.totalSum);
            root.children.ForEach(child => AddSums(child));
        }

        public void ParseInput(string fileName)
        {
            var input = File.ReadAllLines(fileName).ToList();
            root = new Element() { dir = "root", size = 0, children = new List<Element>(), parent = null };
            Element currentDir = root;
            foreach (var line in input)
            {
                if (line == "$ cd /")
                {
                    while (currentDir.parent != null)
                    {
                        currentDir = currentDir.parent;
                    }
                }
                else if (line.StartsWith("dir"))
                {
                    currentDir.children.Add(new Element() { dir = line.Substring(4), size = 0, children = new List<Element>(), parent = currentDir });
                }
                else if (line == "$ cd ..")
                {
                    if (currentDir.parent != null)
                        currentDir = currentDir.parent;
                }
                else if (line.StartsWith("$ cd"))
                {
                    var elements = line.Split(" ");
                    currentDir = currentDir.children.Find(c => c.dir == elements[2]);
                }
                else if (char.IsDigit(line[0]))
                {
                    var elements = line.Split(" ");
                    currentDir.size += int.Parse(elements[0]);
                }

            }
        }
    }
}
