using System.Diagnostics;
using System;

namespace AdventOfCode2021
{
    class Program
    {
        static void Main(string[] args)
        {
            Problem p = new Day15("PuzzleInputs/day15.txt");
            Stopwatch watch = new Stopwatch();
            watch.Start();
            p.Part1();
            watch.Stop();
            Console.WriteLine("Part 1 completed in: " + watch.ElapsedMilliseconds + "ms");

            watch.Restart();
            p.Part2();
            watch.Stop();
            Console.WriteLine("Part 2 completed in: " + watch.ElapsedMilliseconds + "ms");
        }
    }
}