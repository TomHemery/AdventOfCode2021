using System;

namespace AdventOfCode2021
{
    public class Day11: Problem
    {
        int[,] grid;
        long totalFlashes = 0;

        int flashesThisStep = 0;

        public Day11(string inputPath): base(inputPath)
        {

        }

        protected void ResetGrid()
        {
            grid = new int[puzzleInput[0].Length, puzzleInput.Length];
            for (int y = 0; y < puzzleInput.Length; y++) {
                for (int x = 0; x < puzzleInput[y].Length; x++) {
                    int val = (int)char.GetNumericValue(puzzleInput[y][x]);
                    grid[x, y] = val;
                }
            }
        }

        public override void Part1()
        {
            ResetGrid();
            for(int i = 0; i < 100; i++) {
                Step();
            }
            Console.WriteLine("Total flashes: " + totalFlashes);
        }

        public override void Part2()
        {
            ResetGrid();
            int stepCount = 1;
            while(Step() != grid.Length) {
                stepCount++;
            }
            Console.WriteLine("Step count: " + stepCount);
        }

        protected int Step()
        {
            for(int y = 0; y < grid.GetLength(1); y++) {
                for(int x = 0; x < grid.GetLength(0); x++) {
                    grid[x, y]++;
                }
            }
            for(int y = 0; y < grid.GetLength(1); y++) {
                for(int x = 0; x < grid.GetLength(0); x++) {
                    if(grid[x, y] > 9) {
                        Flash(x, y);
                    }
                }
            }
            int stepFlashes = flashesThisStep;
            flashesThisStep = 0;
            return stepFlashes;
        }

        protected void Flash(int x, int y)
        {
            totalFlashes++;
            flashesThisStep++;
            grid[x, y] = 0;
            for (int xOffset = -1; xOffset <= 1; xOffset++) {
                for (int yOffset = -1; yOffset <= 1; yOffset ++) {
                    int xPrime = x + xOffset;
                    int yPrime = y + yOffset;
                    if (xPrime >= 0 && xPrime < grid.GetLength(0) && yPrime >= 0 && yPrime < grid.GetLength(1)) {
                        grid[xPrime, yPrime] = grid[xPrime, yPrime] != 0 ? grid[xPrime, yPrime] + 1 : 0;
                        if(grid[xPrime, yPrime] > 9) {
                            Flash(xPrime, yPrime);
                        }
                    } 
                }
            }
        }

        protected void PrintGrid()
        {
            for(int y = 0; y < grid.GetLength(1); y++) {
                for(int x = 0; x < grid.GetLength(0); x++) {
                    Console.Write(grid[x, y]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}