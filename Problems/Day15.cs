using System;
using System.Collections.Generic;
namespace AdventOfCode2021
{
    public class Day15: Problem
    {

        protected int[,] caveMap;
        protected int[,] largeCaveMap;

        public Day15(string inputPath): base(inputPath)
        {
            caveMap = new int[puzzleInput.Length, puzzleInput[0].Length];
            for(int y = 0; y < puzzleInput.Length; y++) {
                for (int x = 0; x < puzzleInput[y].Length; x++) {
                    caveMap[x, y] = (int)char.GetNumericValue(puzzleInput[y][x]);
                }
            }

            largeCaveMap = new int[caveMap.GetLength(0) * 5, caveMap.GetLength(1) * 5];
            for(int yOffset = 0; yOffset < largeCaveMap.GetLength(1); yOffset += caveMap.GetLength(1)) {
                for(int xOffset = 0; xOffset < largeCaveMap.GetLength(0); xOffset += caveMap.GetLength(0)) {
                    for(int y = 0; y < caveMap.GetLength(1); y++) {
                        for(int x = 0; x < caveMap.GetLength(0); x++) {
                            int newVal = xOffset + yOffset + caveMap[x, y];
                            largeCaveMap[xOffset + x, yOffset + y] = newVal % 9 == 0 ? 9 : newVal % 9;
                        }
                    }
                }
            }
        }

        public override void Part1()
        {
            Console.WriteLine("Total cost from start to end: " + AStar(
                new Vector2(0, 0), 
                new Vector2(caveMap.GetLength(0) - 1, caveMap.GetLength(1) - 1), 
                caveMap
            ));
        }

        public override void Part2()
        {
            Console.WriteLine("Total cost from start to end, big map: " + AStar(
                new Vector2(0, 0), 
                new Vector2(largeCaveMap.GetLength(0) - 1, largeCaveMap.GetLength(1) - 1), 
                largeCaveMap
            ));
        }

        protected int AStar(Vector2 start, Vector2 goal, int[,] caveMap) 
        {
            PriorityQueue<Vector2, int> openSet = new PriorityQueue<Vector2, int>();
            int startHeuristicScore = goal.x - start.x + goal.y - start.y;
            openSet.Enqueue(start, startHeuristicScore);

            Dictionary<Vector2, Vector2> cameFrom = new Dictionary<Vector2, Vector2>();
            Dictionary<Vector2, int> gScore = new Dictionary<Vector2, int>(){{start, 0}};
            Dictionary<Vector2, int> fScore = new Dictionary<Vector2, int>(){{start, startHeuristicScore}};

            while (openSet.Count > 0) {
                Vector2 current = openSet.Dequeue();
                if (current.Equals(goal)) {
                    return gScore[goal];
                }

                foreach (Vector2 neighbour in CoordinateGrid.GetOrthoNeighbours(current, caveMap)) {
                    int tentativeScore = gScore[current] + caveMap[neighbour.x, neighbour.y];
                    int bestScore = gScore.ContainsKey(neighbour) ? gScore[neighbour] : int.MaxValue;
                    if (tentativeScore < bestScore) {
                        cameFrom[neighbour] = current;
                        gScore[neighbour] = tentativeScore;
                        int heuristicScore = tentativeScore + goal.x - neighbour.x + goal.y - neighbour.y;
                        fScore[neighbour] = heuristicScore;
                        openSet.Enqueue(neighbour, heuristicScore);
                    }
                }
            }

            return -1;
        }
    }
}