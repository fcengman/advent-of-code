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
        public (int, int)[] Knots;
        public int tailPos;

        public Rope(int size)
        {
            Head = (0, 0);
            Knots = new (int, int)[size];
            for(int i = 0; i < size; i++)
            {
                Knots[i] = (0, 0);
            }
            tailPos = size - 1;
        }

        public void RunInput(List<(string, int)> moves, HashSet<(int, int)> tailMoves)
        {
            for (int count = 0; count < moves.Count(); count++)
            {
                for (int move = 0; move < moves[count].Item2; move++)
                {
                    MoveHead(moves[count].Item1);
                    MoveKnots();
                    tailMoves.Add(Knots[tailPos]);
                }
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

        public void MoveKnots()
        {
            CatchUp(ref Head, ref Knots[0]);
            
            for (int i = 1; i < Knots.Length; i++)
                CatchUp(ref Knots[i - 1], ref Knots[i]);
            
        }

        private void CatchUp(ref (int, int) head, ref (int, int) tail)
        {
            if (Math.Abs(head.Item2 - tail.Item2) < 2 &&
                    Math.Abs(head.Item1 - tail.Item1) < 2) return;
            
            // Left - Right
            if (head.Item2 != tail.Item2) 
                tail.Item2 += Math.Sign(head.Item2 - tail.Item2);
            
            // Up - Down
            if (head.Item1 != tail.Item1) 
                tail.Item1 += Math.Sign(head.Item1 - tail.Item1);
        }

    }

    internal class Day9 : Problem
    {
        List<(string, int)> moves;
        
        protected override void Part1()
        {
            var rope = new Rope(1);
            HashSet<(int, int)> tailMoves = new HashSet<(int, int)>();
            rope.RunInput(moves, tailMoves);
            Console.WriteLine($"Total Moves ({rope.Knots.Length} knot): {tailMoves.Count()}");
            Console.ReadLine();
        }

        protected override void Part2()
        {
            var rope = new Rope(9);
            HashSet<(int, int)> tailMoves = new HashSet<(int, int)>();
            rope.RunInput(moves, tailMoves);
            Console.WriteLine($"Total Moves ({rope.Knots.Length} knot): {tailMoves.Count()}");
        }

        private void PrintKnots(Rope rope, int size)
        {
            rope.Knots.ToList().ForEach(t => Console.Write($" {t} "));
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
