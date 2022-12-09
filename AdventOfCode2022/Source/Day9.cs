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
        List<string> movesDir;
        List<int> moveCount;
        (int, int) head;
        (int, int) tail;
        HashSet<(int, int)> moves;
        List<(int, int)> tails;

        protected override void Part1()
        {
            head = (0, 0);
            tail = (0, 0);
            HashSet<(int, int)> moves = new HashSet<(int, int)>();
            var headPrev = head;
            for (int count = 0; count < movesDir.Count(); count++)
            {
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
                    }
                }
            }
            PrintKnot();

            Console.WriteLine($"Total Moves (one knot): {moves.Count() + 1}");
        }

        protected override void Part2()
        {
            head = (25,25);
            tails = new List<(int, int)>() { (25, 25), (25, 25), (25, 25), (25, 25), (25, 25), (25, 25), (25, 25), (25, 25), (25, 25) };

            moves = new HashSet<(int, int)>();

            for (int count = 0; count < movesDir.Count(); count++)
            {
                if (movesDir[count] == "L")
                {
                    for (int i = 0; i < moveCount[count]; i++)
                    {
                        head = (head.Item1, head.Item2 - 1);
                        MoveHead();
                        for (int k = 1; k < tails.Count; k++)
                        {
                            MoveTails(k);
                        }
                    }
                }
                if (movesDir[count] == "R")
                {
                    for (int i = 0; i < moveCount[count]; i++)
                    {
                        head = (head.Item1, head.Item2 + 1);
                        MoveHead();
                        for (int k = 1; k < tails.Count; k++)
                        {
                            MoveTails(k);
                        }
                    }
                    
                }
                if (movesDir[count] == "D")
                {
                    for (int i = 0; i < moveCount[count]; i++)
                    {
                        head = (head.Item1 + 1, head.Item2);
                        MoveHead();
                        for (int k = 1; k < tails.Count; k++)
                        {
                            MoveTails(k);
                        }
                    }
                }
                if (movesDir[count] == "U")
                {
                    for(int i = 0; i < moveCount[count]; i++)
                    {
                        head = (head.Item1 - 1, head.Item2);
                        MoveHead();
                        for (int k = 1; k < tails.Count; k++)
                        {
                            MoveTails(k);
                        }
                    } 
                }
                PrintMultipleKnots();
            }

            Console.WriteLine($"Total Moves (multiple knots): {moves.Count() + 1}");
            Console.ReadLine();
        }

        private void MoveHead()
        {
            // row                                          // column
            if (Math.Abs(head.Item2 - tails[0].Item2) < 2 && Math.Abs(head.Item1 - tails[0].Item1) < 2) return;

            if (head.Item2 > tails[0].Item2)
                tails[0] = (tails[0].Item1, tails[0].Item2 + 1);
            if (head.Item2 < tails[0].Item2)
                tails[0] = (tails[0].Item1, tails[0].Item2 - 1);

            if (head.Item1 > tails[0].Item1)
                tails[0] = (tails[0].Item1 + 1, tails[0].Item2);
            if (head.Item1 < tails[0].Item1)
                tails[0] = (tails[0].Item1 - 1, tails[0].Item2);
        }

        private void MoveTails(int k)
        {
            // row                                          // column
            if (Math.Abs(tails[k-1].Item2 - tails[k].Item2) < 2 && Math.Abs(tails[k-1].Item1 - tails[k].Item1) < 2) return;
            
            if(tails[k-1].Item2 > tails[k].Item2)
                tails[k] = (tails[k].Item1, tails[k].Item2+1);
            if(tails[k-1].Item2 < tails[k].Item2)
                tails[k] = (tails[k].Item1, tails[k].Item2-1);

            if(tails[k-1].Item1 > tails[k].Item1)
                tails[k] = (tails[k].Item1+1, tails[k].Item2);
            if (tails[k-1].Item1 < tails[k].Item1)
                tails[k] = (tails[k].Item1-1, tails[k].Item2);


            if(k == 8)
                moves.Add((tails[k].Item1, tails[k].Item2));
        }
        private void PrintKnot()
        {
            for (int i = 0; i < 50; i++)
            {
                for(int j = 0; j < 50; j++)
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


        private void PrintMultipleKnots()
        {
            tails.ForEach(t => Console.Write($" {t} "));
            Console.WriteLine();
            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    var toWrite = " . ";
                    for(int k = tails.Count()-1; k >= 0; k--)
                    {
                        if (tails[k] == (i, j))
                            toWrite = $" {k + 1} ";
                    }
                    if (head == (i, j))
                    {
                        toWrite = " H ";
                    }
                    Console.Write(toWrite);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        protected override void ParseInput(string fileName)
        {
            base.ParseInput(fileName);
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
