using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day6: Problem
    {
        public Day6(string inputPath): base(inputPath)
        {

        }

        public override void Part1()
        {
            List<short> fish = puzzleInput[0].Split(",").Select(short.Parse).ToList();
            for(int i = 0; i < 80; i++) {
                for(int index = fish.Count - 1; index >= 0; index --) {
                    fish[i]--;
                    if(fish[i] < 0) {
                        fish[i] = 6;
                        fish.Add(8);
                    }
                }
            }
            Console.WriteLine("There are " + fish.Count() + " fish");
        }

        public override void Part2()
        {
        }
    }
}