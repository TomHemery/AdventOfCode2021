using System;
using System.Collections.Generic;
using System.Linq;
namespace AdventOfCode2021 
{
    public class Day18: Problem
    {
        protected List<SnailTreeNode> allNumbers;

        public Day18(string inputPath): base(inputPath)
        {
            allNumbers = new List<SnailTreeNode>();
            foreach(string line in puzzleInput) {
                allNumbers.Add(new SnailTreeNode(line));
            }
        }

        public override void Part1()
        {
            SnailTreeNode result = allNumbers.First();
            foreach (SnailTreeNode num in allNumbers.Skip(1)) {
                result = SnailTreeNode.Add(result, num);
            }
            Console.WriteLine("Result of adding all snail nums: " + result);
            Console.WriteLine("Magnitude: " + result.GetMagnitude());
        }

        public override void Part2()
        {
            int maxMag = 0;
            for(int i = 0; i < puzzleInput.Length; i++) {
                for(int j = i + 1; j < puzzleInput.Length; j++) {
                    SnailTreeNode a = new SnailTreeNode(puzzleInput[i]);
                    SnailTreeNode b = new SnailTreeNode(puzzleInput[j]);

                    SnailTreeNode sum = SnailTreeNode.Add(a, b);
                    int mag = sum.GetMagnitude();
                    if(mag > maxMag) maxMag = mag;

                    a = new SnailTreeNode(puzzleInput[i]);
                    b = new SnailTreeNode(puzzleInput[j]);
                    sum = SnailTreeNode.Add(b, a);
                    mag = sum.GetMagnitude();
                    if(mag > maxMag) maxMag = mag;     
                }
            }

            Console.WriteLine("Max magintude of adding two numbers: " + maxMag);
        }

        
    }
}