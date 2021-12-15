using System.Collections.Generic;
using System;
using System.Linq;
namespace AdventOfCode2021
{
    public class Day9: Problem
    {
        int[,] heightMap;

        public Day9(string inputPath): base(inputPath)
        {
            heightMap = new int[puzzleInput[0].Length, puzzleInput.Length];
            for (int y = 0; y < puzzleInput.Length; y++) {
                for (int x = 0; x < puzzleInput[y].Length; x++) {
                    heightMap[x, y] = (int)Char.GetNumericValue(puzzleInput[y][x]);
                }
            }
        }

        public override void Part1()
        {
            int sumRiskLevels = 0;

            for (int y = 0; y < heightMap.GetLength(1); y++) {
                for (int x = 0; x < heightMap.GetLength(0); x++) {
                    if (LowestInNeighbourhood(heightMap, x, y)){
                        sumRiskLevels += heightMap[x, y] + 1;
                    }
                }
            }

            Console.WriteLine("Sum of risk levels: " + sumRiskLevels);
        }

        public override void Part2()
        {
            List<Vector2> lowPoints = new List<Vector2>();
            for (int y = 0; y < heightMap.GetLength(1); y++) {
                for (int x = 0; x < heightMap.GetLength(0); x++) {
                    if (LowestInNeighbourhood(heightMap, x, y)){
                        lowPoints.Add(new Vector2(x, y));
                    }
                }
            }

            List<Basin> basins = new List<Basin>();
            foreach(Vector2 lowPoint in lowPoints) {
                Basin newBasin = new Basin();
                newBasin.setLowPoint(lowPoint);
                RecursiveBuildBasin(newBasin, lowPoint);
                basins.Add(newBasin);
            }

            List<int> largest = new List<int>();

            foreach(Basin basin in basins)
            {
                int min = largest.Count() > 0 ? largest.Min() : 0;
                int size = basin.GetSize();
                if(size > min)
                {
                    largest.Add(size);
                    if(largest.Count() > 3) largest.Remove(largest.Min());
                }
            }

            int mult = 1;
            foreach(int val in largest) mult *= val;

            Console.WriteLine("Mult of 3 largest sizes: " + mult);
        }

        protected void RecursiveBuildBasin(Basin b, Vector2 point)
        {
            if (!b.Contains(point)) {
                b.addPoint(point);
                foreach (Vector2 neighbour in CoordinateGrid.GetOrthoNeighbours(point, heightMap)) {
                    int val = heightMap[neighbour.x, neighbour.y];
                    if(val >= heightMap[point.x, point.y] && val < 9) {
                        RecursiveBuildBasin(b, neighbour);
                    }
                }
            }
        }

        protected bool LowestInNeighbourhood(int[,] heightMap, int xPos, int yPos)
        {
            foreach(Vector2 neighbour in CoordinateGrid.GetOrthoNeighbours(new Vector2(xPos, yPos), heightMap))
            {
                if(heightMap[neighbour.x, neighbour.y] <= heightMap[xPos, yPos]) return false;
            }

            return true;
        }
    }
}