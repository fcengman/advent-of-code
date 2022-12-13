using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public class HeightNode
    {
        public (int, int) position;
        public HeightNode parent;
        public List<HeightNode> children;
        public char height;
    }
    internal class Day12 : Problem
    {
        HeightNode[,] grid;
        public string stringDict => "abcdefghijklmnopqrstuvwxyz";
        List<HeightNode> Start;
        HeightNode End;
        int Length;
        int Height;
        List<List<char>> paths;
        protected override void Part1()
        {
            Length = grid.GetLength(1);
            Height = grid.GetLength(0);
            int currentRow = 0;
            int currentColumn = 0;
            paths = new List<List<char>>();

            foreach(var node in grid.Cast<HeightNode>())
            {
                FindChildren(node);
            }
            //PrintGrid();
            //printAllPaths(Start, End);
            var pred = new HeightNode[input.Count, input[0].Length];
            var dest = new int[input.Count, input[0].Length];
            foreach(var node in Start)
            {
                BFS(node, End, pred, dest);
                List<char> path = new List<char>();
                HeightNode crawl = End;
                path.Add(crawl.height);
                while (crawl != node)
                {
                    
                    path.Add(pred[crawl.position.Item1, crawl.position.Item2].height);
                    crawl = pred[crawl.position.Item1, crawl.position.Item2];
                    if(paths.Count > 0 && path.Count > paths.OrderBy(x => x.Count).First().Count)
                    {
                        path.Clear();
                        break;
                    }
                }
                if(path.Count != 0)
                    paths.Add(path);
            }


            var shortestPath = paths.OrderBy(x => x.Count).First();

            shortestPath.ForEach(p => Console.Write($" {p} >"));
            Console.WriteLine($"Shortest Path: {shortestPath.Count-1}");
        }

        private void BFS(HeightNode start, HeightNode end, HeightNode[,] pred, int[,] dist)
        {
            Queue<HeightNode> queue = new Queue<HeightNode>();
            bool[,] isVisited = new bool[Height, Length];
            isVisited[start.position.Item1, start.position.Item2] = true;
            dist[start.position.Item1, start.position.Item2] = 0;
            pred[start.position.Item1, start.position.Item2] = start;
            queue.Enqueue(start);

            while(queue.Count != 0)
            {
                HeightNode u = queue.Dequeue();

                foreach(var child in u.children)
                {
                    if(isVisited[child.position.Item1, child.position.Item2] == false)
                    {
                        isVisited[child.position.Item1, child.position.Item2] = true;
                        dist[child.position.Item1, child.position.Item2] = dist[start.position.Item1, start.position.Item2]+1;
                        pred[child.position.Item1, child.position.Item2] = u;
                        queue.Enqueue(child);

                        if (u == end)
                            return;
                    }
                }
            }
        }


        private void PrintGrid()
        {
            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    var line = $"Positions: {grid[i, j].position} Children: ";
                    grid[i, j].children.ForEach(x => line += $" {x.position} ");
                    Console.WriteLine(line);
                }
            }
        }
        private void FindChildren(HeightNode currentNode)
        {
            int row = currentNode.position.Item1;
            int column = currentNode.position.Item2;

            if (row < Height-1)
            {
                var node = grid[row+1, column];
                if(currentNode.height >= node.height-1)
                    currentNode.children.Add(node);
            }
            if (row - 1 != -1)
            {
                var node = grid[row-1,column];
                if (currentNode.height >= node.height - 1)
                    currentNode.children.Add(node);
            }
            if (column < Length-1)
            {
                var node = grid[row, column+1];
                if (currentNode.height >= node.height - 1)
                    currentNode.children.Add(node);


            }
            if (column - 1 != -1)
            {
                var node = grid[row, column-1];
                if (currentNode.height >= node.height - 1)
                    currentNode.children.Add(node);
            }
        }



        protected override void Part2()
        {
            
        }

        protected override void ParseInput(string fileName)
        {
            base.ParseInput(fileName);
            grid = new HeightNode[input.Count, input[0].Length];
            Start = new List<HeightNode>();
            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    char h = input[i][j];
                    if (h == 'S')
                        h = 'a';
                    if (h == 'E')
                        h = 'z';
                    grid[i, j] = new HeightNode()
                    {
                        position = (i, j),
                        parent = null,
                        children = new List<HeightNode>(),
                        height = h
                    };

                    if (input[i][j] == 'S')
                        Start.Add(grid[i, j]);
                    if (input[i][j] == 'a')
                        Start.Add(grid[i, j]);
                    if (input[i][j] == 'E')
                        End = grid[i, j];
                }
            }
        }
    }
}
