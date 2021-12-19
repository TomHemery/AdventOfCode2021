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
            Console.WriteLine(result);
            foreach (SnailTreeNode num in allNumbers.Skip(1)) {
                Console.WriteLine("+ " + num);
                result = SnailTreeNode.Add(result, num);
                SnailTreeNode.Reduce(result);
                Console.WriteLine("= " + result);
            }
            Console.WriteLine(result);
        }

        public override void Part2()
        {
        }

        
    }
}