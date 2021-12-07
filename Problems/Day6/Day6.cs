using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day6: Problem
    {
        protected const int LIFE_CYCLE = 9;
        public Day6(string inputPath): base(inputPath)
        {

        }

        public override void Part1()
        {
            Simulate(80);
        }

        public override void Part2()
        {
            Simulate(256);
        }

        protected void Simulate(int steps)
        {
            List<short> input = puzzleInput[0].Split(",").Select(short.Parse).ToList();
            int[] initial = new int[LIFE_CYCLE] {0, 0, 0, 0, 0, 0, 0, 0, 0};
            foreach(short f in input) initial[f]++;

            FixedIndexedQueue<long> fish = new FixedIndexedQueue<long>(LIFE_CYCLE);
            for(int i = 0; i < initial.Length; i++) {
                fish.Enqueue(initial[i]);
            }

            for (int i = 0; i < steps; i++) {
                long curr = fish.Dequeue();
                fish.Enqueue(curr); // re production
                fish[6] += curr; // reset timer
            }

            long total = 0;
            foreach(long f in fish) total += f;
            Console.WriteLine("Total fish after " + steps + " days: " + total);            
        }
    }
}