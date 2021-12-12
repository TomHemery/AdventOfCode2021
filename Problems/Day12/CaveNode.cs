using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
namespace AdventOfCode2021
{
    public class CaveNode
    {
        public string name;
        public bool isSmall = true;

        public int timesVisited = 0;

        public List<CaveNode> neighbours = new List<CaveNode>();

        public CaveNode(string name)
        {
            if (name.Any(char.IsUpper)) isSmall = false;
            this.name = name;
        }

        public void AddNeighbour(CaveNode n) 
        {
            neighbours.Add(n);
        }

        public override string ToString()
        {
            string result = "Node " + name + " with neighbours: ";
            foreach(CaveNode neighbour in neighbours) result += neighbour.name + ", ";
            return result;
        }

        public bool Visited()
        {
            return timesVisited > 0;
        }
    }
}