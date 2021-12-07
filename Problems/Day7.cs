using System.Reflection;
using System.Linq;
using System;
namespace AdventOfCode2021
{
    public class Day7: Problem
    {
        int[] crabs;
        int min;
        int max;

        int median;
        int avg;
        
        public Day7(string inputPath): base(inputPath)
        {
            crabs = puzzleInput[0].Split(",").Select(int.Parse).ToArray();
            Array.Sort(crabs);

            int mid = crabs.Length / 2;
            Console.WriteLine("Median crab position: " + crabs[mid]);
            median = crabs[mid];

            avg = 0;
            foreach(int crab in crabs) avg += crab;
            avg /= crabs.Length;
            Console.WriteLine("Average crab position: " + avg);

            min = crabs.Min();
            max = crabs.Max();
        }

        public override void Part1()
        {
            int total = 0;
            foreach(int val in crabs) {
                total += Math.Abs(val - median);
            }

            Console.WriteLine("Best fuel consumption, part 1: " + total + " at position: " + median);
        }

        public override void Part2()
        {
            long best = long.MaxValue;
            int bestPos = -1;
            // For some mathsy reason the best position will be within 0.5 of the true average. Check around that.
            for(int pos = avg - 1; pos <= avg + 1; pos++) {
                long curr = 0;
                foreach(int val in crabs) {
                    int n = Math.Abs(val - pos);
                    curr += (n * (n + 1)) / 2;
                }
                if (curr < best) {
                    best = curr;
                    bestPos = pos;
                }
            }

            Console.WriteLine("Best fuel consumption, part 2: " + best + " at position: " + bestPos);
        }
    }
}