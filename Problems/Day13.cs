using System;
using System.Collections.Generic;
using System.Linq;
namespace AdventOfCode2021
{
    public class Day13: Problem
    {

        protected Dictionary<Vector2, char> points = new Dictionary<Vector2, char>();
        protected string[] folds;

        public Day13(string inputPath): base(inputPath)
        {
            foreach(string line in puzzleInput.Where(x => x.Contains(','))) {
                int[] coords = line.Split(',').Select(x => int.Parse(x)).ToArray();
                points[new Vector2(coords[0], coords[1])] = '#';
            }

            folds = puzzleInput.Where(x => x.Contains("fold along")).Select(x => x.Split("along")[1].Trim()).ToArray();

            Console.WriteLine("There are " + points.Keys.Count() + " points on the paper and " + folds.Length + " fold instructions");
        }

        public override void Part1()
        {
            Dictionary<Vector2, char> afterOneFold = fold(points, folds[0]);
            Console.WriteLine("After 1 fold there are: " + afterOneFold.Keys.Count() + " points");
        }

        public override void Part2()
        {
            Dictionary<Vector2, char> curr = points;
            foreach (string foldInstruction in folds) {
                curr = fold(curr, foldInstruction);
            }

            int xMax = curr.Keys.MaxBy(point => point.x).x;
            int yMax = curr.Keys.MaxBy(point => point.y).y;

            for(int y = 0; y <= yMax; y++) {
                for(int x = 0; x <= xMax; x++) {
                    if(curr.ContainsKey(new Vector2(x, y))) {
                        Console.Write("#");
                    } else {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
        }

        protected Dictionary<Vector2, char> fold(Dictionary<Vector2, char> points, string foldInstruction)
        {
            Console.WriteLine("Folding with instruction " + foldInstruction);
            Dictionary<Vector2, char> result = new Dictionary<Vector2, char>();
            char dir = foldInstruction.Split('=')[0][0];
            int line = int.Parse(foldInstruction.Split('=')[1]);


            foreach(Vector2 point in points.Keys){
                Vector2 newPoint = point.Clone();
                if(dir == 'x') {
                    if(point.x > line) {
                        newPoint.x = line - (point.x - line);
                    }
                } else {
                    if(point.y > line) {
                        newPoint.y = line - (point.y - line);
                    }
                }
                result[newPoint] = '#';
            }

            return result;
        }
    }
}