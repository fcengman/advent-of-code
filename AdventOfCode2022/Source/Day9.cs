using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2022
{

    public class Rope
    {
        public (int, int) Head;
        public List<(int, int)> Knots;

        public Rope(int size)
        {
            Head = (0, 0);
            Knots = new List<(int, int)>();
            for(int i = 0; i < size; i++)
            {
                Knots.Add((0, 0));
            }
        }

        public void MoveHead(string dir)
        {
            switch(dir[0])
            {
                case 'L':
                    Head.Item2--;
                    break;
                case 'R':
                    Head.Item2++;
                    break;
                case 'U':
                    Head.Item1--;
                    break;
                case 'D':
                    Head.Item1++;
                    break;
            }
        }

        //public void MoveKnots()
        //{
        //    if (Math.Abs(Head.Item2 - Knots[0].Item2) > 1 || Math.Abs(Head.Item1 - Knots[0].Item1) > 1)
        //    {
        //        if (head.Item2 > tails[0].Item2)
        //            tails[0] = (tails[0].Item1, tails[0].Item2 + 1);
        //        if (head.Item2 < tails[0].Item2)
        //            tails[0] = (tails[0].Item1, tails[0].Item2 - 1);

        //        if (head.Item1 > tails[0].Item1)
        //            tails[0] = (tails[0].Item1 + 1, tails[0].Item2);
        //        if (head.Item1 < tails[0].Item1)
        //            tails[0] = (tails[0].Item1 - 1, tails[0].Item2);
        //    }
        //    for (int i = 1; i < Knots.Count; i++)
        //    {

        //    }
        //}

    }


    internal class Day9 : Problem
    {
        List<(string, int)> moves;
        
        protected override void Part1()
        {
            var rope = new Rope(1);
            var tailPos = 1;
            HashSet<(int, int)> tailMoves = new HashSet<(int, int)>();
            for (int count = 0; count < moves.Count(); count++)
            {
                for(int move = 0; 0 < moves[count].Item2; move++)
                {
                    rope.MoveHead(moves[count].Item1);
                    //rope.MoveKnots();
                    tailMoves.Add(rope.Knots[tailPos]);
                }
            }
            Console.WriteLine($"Total Moves (one knot): {moves.Count() + 1}");
        }

        protected override void Part2()
        {
            //head = (25,25);
            //tails = new List<(int, int)>() { (25, 25), (25, 25), (25, 25), (25, 25), (25, 25), (25, 25), (25, 25), (25, 25), (25, 25) };

            //moves = new HashSet<(int, int)>();

            //for (int count = 0; count < movesDir.Count(); count++)
            //{
            //    if (movesDir[count] == "L")
            //    {
            //        for (int i = 0; i < moveCount[count]; i++)
            //        {
            //            head = (head.Item1, head.Item2 - 1);
            //            MoveHead();
            //            for (int k = 1; k < tails.Count; k++)
            //            {
            //                MoveTails(k);
            //            }
            //        }
            //    }
            //    if (movesDir[count] == "R")
            //    {
            //        for (int i = 0; i < moveCount[count]; i++)
            //        {
            //            head = (head.Item1, head.Item2 + 1);
            //            MoveHead();
            //            for (int k = 1; k < tails.Count; k++)
            //            {
            //                MoveTails(k);
            //            }
            //        }
                    
            //    }
            //    if (movesDir[count] == "D")
            //    {
            //        for (int i = 0; i < moveCount[count]; i++)
            //        {
            //            head = (head.Item1 + 1, head.Item2);
            //            MoveHead();
            //            for (int k = 1; k < tails.Count; k++)
            //            {
            //                MoveTails(k);
            //            }
            //        }
            //    }
            //    if (movesDir[count] == "U")
            //    {
            //        for(int i = 0; i < moveCount[count]; i++)
            //        {
            //            head = (head.Item1 - 1, head.Item2);
            //            MoveHead();
            //            for (int k = 1; k < tails.Count; k++)
            //            {
            //                MoveTails(k);
            //            }
            //        } 
            //    }
            //    PrintMultipleKnots();
            //}

            //Console.WriteLine($"Total Moves (multiple knots): {moves.Count() + 1}");
            //Console.ReadLine();
        }

        //private void MoveHead()
        //{
        //    // row                                          // column
        //    if (Math.Abs(head.Item2 - tails[0].Item2) < 2 && Math.Abs(head.Item1 - tails[0].Item1) < 2) return;

        //    if (head.Item2 > tails[0].Item2)
        //        tails[0] = (tails[0].Item1, tails[0].Item2 + 1);
        //    if (head.Item2 < tails[0].Item2)
        //        tails[0] = (tails[0].Item1, tails[0].Item2 - 1);

        //    if (head.Item1 > tails[0].Item1)
        //        tails[0] = (tails[0].Item1 + 1, tails[0].Item2);
        //    if (head.Item1 < tails[0].Item1)
        //        tails[0] = (tails[0].Item1 - 1, tails[0].Item2);
        //}

        //private void MoveTails(int k)
        //{
        //    // row                                          // column
        //    if (Math.Abs(tails[k-1].Item2 - tails[k].Item2) < 2 && Math.Abs(tails[k-1].Item1 - tails[k].Item1) < 2) return;
            
        //    if(tails[k-1].Item2 > tails[k].Item2)
        //        tails[k] = (tails[k].Item1, tails[k].Item2+1);
        //    if(tails[k-1].Item2 < tails[k].Item2)
        //        tails[k] = (tails[k].Item1, tails[k].Item2-1);

        //    if(tails[k-1].Item1 > tails[k].Item1)
        //        tails[k] = (tails[k].Item1+1, tails[k].Item2);
        //    if (tails[k-1].Item1 < tails[k].Item1)
        //        tails[k] = (tails[k].Item1-1, tails[k].Item2);


        //    if(k == 8)
        //        moves.Add((tails[k].Item1, tails[k].Item2));
        //}
        


        private void PrintMultipleKnots(Rope rope, int size)
        {
            rope.Knots.ForEach(t => Console.Write($" {t} "));
            Console.WriteLine();
            for (int i = -size/2; i < size/2; i++)
            {
                for (int j = -size/2; j < size/2; j++)
                {
                    var toWrite = " . ";
                    for(int k = rope.Knots.Count()-1; k >= 0; k--)
                    {
                        if (rope.Knots[k] == (i, j))
                            toWrite = $" {k + 1} ";
                    }
                    if (rope.Head == (i, j))
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
            moves = new List<(string, int)>();
            foreach (var line in input)
            {
                var elements = line.Split(' ');
                moves.Add((elements[0], int.Parse(elements[1])));
            }
        }
    }
}
