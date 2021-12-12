using System;
namespace AdventOfCode2021
{
    public class Day12: Problem
    {
        
        protected CaveGraph caveGraph;

        public Day12(string inputPath): base(inputPath)
        {
            caveGraph = new CaveGraph(puzzleInput);
        }

        public override void Part1()
        {
            int totalPaths = caveGraph.CountPathsFrom(caveGraph.startNode);
            Console.WriteLine("Total paths: " + totalPaths);
        }

        public override void Part2()
        {
            int totalPaths = caveGraph.CountPathsFromSingleSmallRepeat(caveGraph.startNode);
            Console.WriteLine("Total paths, 1 small repeat: " + totalPaths);
        }
    }
    
}