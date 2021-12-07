using System;
using System.Collections.Generic;
using System.Linq;
namespace AdventOfCode2021
{
    public class Day5: Problem
    {
        protected List<Line> lines = new List<Line>();

        public Day5(string inputPath): base(inputPath)
        {    
            foreach(string inputLine in puzzleInput) {
                string[] parts = inputLine.Split("->");
                lines.Add(new Line(
                    int.Parse(parts[0].Split(",")[0].Trim()),
                    int.Parse(parts[0].Split(",")[1].Trim()),
                    int.Parse(parts[1].Split(",")[0].Trim()),
                    int.Parse(parts[1].Split(",")[1].Trim())
                ));
            }
        }

        public override void Part1()
        {
            List<Line> perpendicularLines = lines.Where(line => line.start.x == line.end.x || line.start.y == line.end.y).ToList();
            Console.WriteLine("There are: " + perpendicularLines.Count + " perpendicular lines");
            int crossings = 0;
            Dictionary<Vector2, int> diagram = new Dictionary<Vector2, int>();
            foreach(Line line in perpendicularLines) {
                line.AddToMap(diagram);
            }
            foreach(int value in diagram.Values) {
                if(value > 1) crossings++;
            }
            Console.WriteLine("Crossings: " + crossings);
        }

        public override void Part2()
        {
            int crossings = 0;
            Dictionary<Vector2, int> diagram = new Dictionary<Vector2, int>();
            foreach(Line line in lines) {
                line.AddToMap(diagram);
            }
            foreach(int value in diagram.Values) {
                if(value > 1) crossings++;
            }
            Console.WriteLine("Crossings: " + crossings);
        }
    }
}