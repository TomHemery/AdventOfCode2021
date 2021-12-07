using System;
namespace AdventOfCode2021 
{
    public class Day2: Problem
    {
        public Day2(string inputPath): base(inputPath)
        {

        }

        public override void Part1() 
        {
            int horizontalPostition = 0;
            int depth = 0;

            foreach (string line in puzzleInput) {
                string[] parts = line.Split(' ');
                int value = int.Parse(parts[1]);
                switch(parts[0]) {
                    case "forward":
                        horizontalPostition += value;
                        break;
                    case "down":
                        depth += value;
                        break;
                    case "up":
                        depth -= value;
                        break;
                }
            }

            Console.WriteLine(
                "Horizontal Postition: " + 
                horizontalPostition + 
                " Depth: " + 
                depth + 
                " multiplied: " + 
                (depth * horizontalPostition)
            );
        }

        public override void Part2()
        {
            int horizontalPostition = 0;
            int depth = 0;
            int aim = 0;

            foreach (string line in puzzleInput) {
                string[] parts = line.Split(' ');
                int value = int.Parse(parts[1]);
                switch(parts[0]) {
                    case "forward":
                        horizontalPostition += value;
                        depth += aim * value;
                        break;
                    case "down":
                        aim += value;
                        break;
                    case "up":
                        aim -= value;
                        break;
                }
            }

            Console.WriteLine(
                "Horizontal Postition: " + 
                horizontalPostition + 
                " Depth: " + 
                depth + 
                " multiplied: " + 
                (depth * horizontalPostition)
            );
        }
    }
}