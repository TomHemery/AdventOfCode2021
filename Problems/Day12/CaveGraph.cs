using System;
using System.Collections.Generic;
namespace AdventOfCode2021
{
    public class CaveGraph
    {
        Dictionary<string, CaveNode> caves = new Dictionary<string, CaveNode>();
        public CaveNode startNode {get; private set;} = null;
        public CaveNode endNode {get; private set;} = null;

        public CaveGraph(string[] initialState)
        {
            foreach(string line in initialState)
            {
                Console.WriteLine(line);
                string[] parts = line.Split("-");

                CaveNode curr;
                if (!caves.ContainsKey(parts[0])) {
                    curr = new CaveNode(parts[0]);
                    caves[parts[0]] = curr;
                    if(parts[0] == "start") startNode = curr;
                    else if(parts[0] == "end") endNode = curr;
                } else {
                    curr = caves[parts[0]];
                }

                CaveNode neighbour;
                if (!caves.ContainsKey(parts[1])) {
                    neighbour = new CaveNode(parts[1]);
                    caves[parts[1]] = neighbour;
                    if(parts[1] == "start") startNode = neighbour;
                    else if(parts[1] == "end") endNode = neighbour;
                } else {
                    neighbour = caves[parts[1]];
                }

                curr.AddNeighbour(neighbour);
                neighbour.AddNeighbour(curr);
            }
        }

        public int CountPathsFrom(CaveNode node)
        {
            int paths = 0;
            node.timesVisited++;

            foreach (CaveNode neighbour in node.neighbours) {
                if (neighbour.name == "end") {
                    paths ++;
                } else if (!neighbour.Visited() || !neighbour.isSmall) {
                    paths += CountPathsFrom(neighbour);
                }
            }

            node.timesVisited--;
            return paths;
        }

        public int CountPathsFromSingleSmallRepeat(CaveNode node, bool usedRepeat = false)
        {
            int paths = 0;
            node.timesVisited++;

            foreach (CaveNode neighbour in node.neighbours) {
                if (neighbour == startNode) {
                    continue;
                } else if (neighbour == endNode) {
                    paths ++;
                } else if (!neighbour.isSmall || !neighbour.Visited()) { // Big cave or we haven't visited before
                    paths += CountPathsFromSingleSmallRepeat(neighbour, usedRepeat);
                } else if (usedRepeat == false && neighbour.isSmall && neighbour.Visited()) { // small cave we've visited, will be our one repeat
                    paths += CountPathsFromSingleSmallRepeat(neighbour, true);
                }
            }

            node.timesVisited--;
            return paths;
        }

    }
}