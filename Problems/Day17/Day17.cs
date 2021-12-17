using System;
using System.Linq;
namespace AdventOfCode2021
{
    public class Day17: Problem
    {
        Vector2[] targetArea;
        public Day17(string inputPath): base(inputPath)
        {
            int[] ySpec = puzzleInput[0].Split(',')[1].Trim().Substring(2).Split("..").Select(int.Parse).ToArray();
            int[] xSpec = puzzleInput[0].Split(',')[0].Split(':')[1].Trim().Substring(2).Split("..").Select(int.Parse).ToArray();

            targetArea = new Vector2[]{new Vector2(xSpec[0], ySpec[0]), new Vector2(xSpec[1], ySpec[1])};
        }

        public override void Part1()
        {
            int highestY;
            Vector2 bestInitialVel = new Vector2();
            Simulate(out highestY, out bestInitialVel, out _);
            Console.WriteLine("Highest y achieved: " + highestY + " with velocity: " + bestInitialVel);
        }

        public override void Part2()
        {
            int numberOfOptions = 0;
            Simulate(out _, out _, out numberOfOptions, -500);
            Console.WriteLine("Number of velocity options: " + numberOfOptions);
        }   

        protected void Simulate(out int highestY, out Vector2 bestInitialVel, out int velocityCount, int yMin = 1)
        {
            int yMax = 500;
            int steps = 400;
            highestY = int.MinValue;
            velocityCount = 0;

            Probe bestProbe = null;

            // Target area is around 100 units away, x needs to be at least 14 to reach that far
            // If the x velocity is greater than the right most point of the target area it will over shoot instantly
            for (int xVel = 14; xVel <= targetArea[1].x; xVel ++) {
                for(int yVel = yMin; yVel <= yMax; yVel ++) {
                    Probe testProbe = new Probe(new Vector2(xVel, yVel), targetArea);

                    for(int i = 0; i < steps; i++) {
                        if (testProbe.Update()) {
                            velocityCount++;
                            if(testProbe.highestY > highestY) {
                                bestProbe = testProbe;
                                highestY = testProbe.highestY;
                            } 
                            break;
                        } else if (testProbe.pos.y < targetArea[0].y || testProbe.pos.x > targetArea[1].x) {
                            // To the right of or below target area, give up
                            break;
                        }
                    }
                }
            }

            bestInitialVel = bestProbe.initalVelocity;
        }
    }
}