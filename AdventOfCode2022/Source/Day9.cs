using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Day9 : Problem
    {
        char[,] grid;
        List<string> movesDir;
        List<int> moveCount;
        (int, int) head;
        (int, int) tail;
        HashSet<(int, int)> moves;

        protected override void Part1()
        {
            head = (0, 0);
            tail = (0, 0);
            HashSet<(int, int)> moves = new HashSet<(int, int)>();
            var headPrev = head;
            //PrintGrid();
            for (int count = 0; count < movesDir.Count(); count++)
            {
                Console.WriteLine($"head: {head}, tail: {tail}");
                //Console.WriteLine($"Dir: {movesDir[count]}, Count: {moveCount[count]}");
                if (movesDir[count] == "L")
                {
                    for (int i = 0; i < moveCount[count]; i++)
                    {

                        headPrev = head;
                        head = (head.Item1, head.Item2 - 1);

                        if (Math.Abs(tail.Item2 - head.Item2) > 1 || Math.Abs(tail.Item1 - head.Item1) > 1)
                        {
                            tail = headPrev;
                            moves.Add((tail.Item1, tail.Item2));
                        }

                        //PrintGrid();
                    }
                }
                if (movesDir[count] == "R")
                {
                    for (int i = 0; i < moveCount[count]; i++)
                    {

                        headPrev = head;
                        head = (head.Item1, head.Item2 + 1);

                        if (Math.Abs(tail.Item2 - head.Item2) > 1 || Math.Abs(tail.Item1 - head.Item1) > 1)
                        {
                            tail = headPrev;
                            moves.Add((tail.Item1, tail.Item2));
                        }

                        //PrintGrid();
                    }
                }
                if (movesDir[count] == "D")
                {
                    for (int i = 0; i < moveCount[count]; i++)
                    {

                        headPrev = head;
                        head = (head.Item1 + 1, head.Item2);

                        if (Math.Abs(tail.Item1 - head.Item1) > 1 || Math.Abs(tail.Item2 - head.Item2) > 1)
                        {
                            tail = headPrev;
                            moves.Add((tail.Item1, tail.Item2));
                        }

                        //PrintGrid();
                    }
                }
                if (movesDir[count] == "U")
                {
                    for (int i = 0; i < moveCount[count]; i++)
                    {

                        headPrev = head;
                        head = (head.Item1 - 1, head.Item2);

                        if (Math.Abs(tail.Item1 - head.Item1) > 1 || Math.Abs(tail.Item2 - head.Item2) > 1)
                        {
                            tail = headPrev;
                            moves.Add((tail.Item1, tail.Item2));
                        }

                        //PrintGrid();
                    }
                }
            }
            //PrintTable();

            Console.WriteLine($"Total Moves: {moves.Count() + 1}");
        }

        protected override void Part2()
        {
            head = (0, 0);
            var tailone = (0, 0);
            var tailtwo = (0, 0);
            var tailthree = (0, 0);
            var tailfour = (0, 0);
            var tailfive = (0, 0);
            var tailsix = (0, 0);
            var tailseven = (0, 0);
            var taileight = (0, 0);
            var tailnine = (0, 0);
            List<(int, int)> tails = new List<(int, int)>() { tailone, tailtwo, tailthree, tailfour, tailfive, tailsix, tailseven, taileight, tailnine };

            moves = new HashSet<(int, int)>();
            var headPrev = head;
            var prevTail = head;
            //PrintGrid();
            for (int count = 0; count < movesDir.Count(); count++)
            {
                Console.WriteLine($"head: {head}, tail: {tail}");

                for (int i = 0; i < moveCount[count]; i++)
                {
                    for (int t = 0; t < tails.Count(); t++)
                    {
                        if (t == 0)
                        {
                            if (movesDir[count] == "L")
                            {
                                MoveLeft(head, tails[t], isFirst: true);  //PrintGrid();
                            }
                            if (movesDir[count] == "R")
                            {
                                MoveRight(head, tails[t], isFirst: true);
                            }
                            if (movesDir[count] == "D")
                            {
                                MoveDown(head, tails[t], isFirst: true);
                            }
                            if (movesDir[count] == "U")
                            {
                                MoveUp(head, tails[t], isFirst: true);
                            }
                        }
                        else
                        {
                            prevTail = tails[t - 1];

                            var IsLast = (t == tails.Count() - 1) ? true : false;

                            //Console.WriteLine($"Dir: {movesDir[count]}, Count: {moveCount[count]}");
                            if (movesDir[count] == "L")
                            {
                                MoveLeft(tails[t], prevTail, false, isLast: IsLast);
                            }
                            if (movesDir[count] == "R")
                            {
                                MoveRight(tails[t], prevTail, false, isLast: IsLast);
                            }
                            if (movesDir[count] == "D")
                            {
                                MoveDown(tails[t], prevTail, false, isLast: IsLast);
                            }
                            if (movesDir[count] == "U")
                            {
                                MoveUp(tails[t], prevTail, false, isLast: IsLast);
                            }
                        }
                    }
                }
            }
            //PrintTable();

            Console.WriteLine($"Total Moves: {moves.Count() + 1}");
        }
        private void MoveLeft((int, int) head, (int, int) tail, bool isFirst = false, bool isLast = false)
        {
            var headPrev = head;
            if (isFirst)
                {

               
                    head = (head.Item1, head.Item2 - 1);
            }

                if (Math.Abs(tail.Item2 - head.Item2) > 1 || Math.Abs(tail.Item1 - head.Item1) > 1)
                {
                    tail = headPrev;
                    if(isLast)
                        moves.Add((tail.Item1, tail.Item2));
                }
            

        }
        private void MoveRight((int, int) head, (int, int) tail, bool isFirst, bool isLast = false)
        {
            

                var headPrev = head;
            if(isFirst)
            {
                head = (head.Item1, head.Item2 + 1);

            }

                if (Math.Abs(tail.Item2 - head.Item2) > 1 || Math.Abs(tail.Item1 - head.Item1) > 1)
                {
                    tail = headPrev;
                    if(isLast)
                        moves.Add((tail.Item1, tail.Item2));
                }

                //PrintGrid();
            

        }

        private void MoveDown((int, int) head, (int, int) tail, bool isFirst, bool isLast = false)
        {
            

                var headPrev = head;
            if(isFirst)
            {
                head = (head.Item1 + 1, head.Item2);

            }

                if (Math.Abs(tail.Item1 - head.Item1) > 1 || Math.Abs(tail.Item2 - head.Item2) > 1)
                {
                    tail = headPrev;
                    if(isLast)
                        moves.Add((tail.Item1, tail.Item2));
                }

                //PrintGrid();
            

        }
        private void MoveUp((int, int) head, (int, int) tail, bool isFirst, bool isLast = false)
        {
           

                var headPrev = head;
            if(isFirst)
            {
                head = (head.Item1 - 1, head.Item2);

            }

                if (Math.Abs(tail.Item1 - head.Item1) > 1 || Math.Abs(tail.Item2 - head.Item2) > 1)
                {
                    tail = headPrev;
                    if(isLast)
                        moves.Add((tail.Item1, tail.Item2));
                }

                //PrintGrid();
            

        }
        private void PrintGrid()
        {
            for (int i = 0; i < 150; i++)
            {
                for(int j = 0; j < 150; j++)
                {
                    if(head == (i, j) && tail == (i, j))
                    {
                        Console.Write(" H ");
                    }
                    else if(head == (i, j))
                    {
                        Console.Write(" H ");
                    }
                    else if(tail == (i, j))
                    {
                        Console.Write(" T ");
                    }
                    else
                    {
                        Console.Write(" . ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private void PrintTable()
        {
            for (int i = 0; i < 150; i++)
            {
                for (int j = 0; j < 150; j++)
                {
                    Console.Write($" {grid[i, j]} ");
                }
                Console.WriteLine();
            }
        }

        protected override void ParseInput(string fileName)
        {
            base.ParseInput(fileName);
            grid = new char[100, 100];
            movesDir = new List<string>();
            moveCount = new List<int>();
            foreach (var line in input)
            {
                var elements = line.Split(' ');
                movesDir.Add(elements[0]);
                moveCount.Add(int.Parse(elements[1]));
            }
        }
    }
}
