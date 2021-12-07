using System.Collections.Generic;
using System;
using System.Linq;
namespace AdventOfCode2021
{
    public class Day1: Problem 
    {

        public Day1(string inputPath): base(inputPath)
        {

        }

        public override void Part1()
        {
            int prev = -1;
            int timesIncreased = 0;
            foreach(string line in puzzleInput) {
                int value = int.Parse(line);
                if (prev > 1 && value > prev) {
                    timesIncreased ++;
                }
                prev = value;
            }
            Console.WriteLine("Total increases in depth: " + timesIncreased);
        }

        public override void Part2()
        {
            int prev = -1;
            Queue<int> slidingWindowA = new Queue<int>();
            Queue<int> slidingWindowB = new Queue<int>();
            int timesIncreased = 0;
            foreach(string line in puzzleInput) {
                int curr = int.Parse(line);
                slidingWindowB.Enqueue(curr);
                if (prev > -1) {
                    slidingWindowA.Enqueue(prev);
                }

                if(slidingWindowB.Count > 3) {
                    slidingWindowB.Dequeue();
                }
                if (slidingWindowA.Count > 3) {
                    slidingWindowA.Dequeue();
                } 

                if (slidingWindowA.Count == 3) {
                    int sumA = slidingWindowA.Sum();
                    int sumB = slidingWindowB.Sum();

                    if (sumB > sumA) {
                        timesIncreased++;
                    }
                }

                prev = curr;
            }

            Console.WriteLine("Total increases in depth (sliding window): " + timesIncreased);
        }
    }
}