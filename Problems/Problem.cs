using System.IO;
namespace AdventOfCode2021
{
    public abstract class Problem 
    {
        protected string[] puzzleInput;

        public Problem(string inputPath)
        {
            puzzleInput = File.ReadAllLines(inputPath);
        }

        public abstract void Part1();
        public abstract void Part2();
    }
}