using System.Numerics;
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
namespace AdventOfCode2021
{
    public class Line
    {
        public Vector2 start = new Vector2();
        public Vector2 end = new Vector2();

        public Line (int x1, int y1, int x2, int y2)
        {
            start.x = x1;
            start.y = y1;

            end.x = x2;
            end.y = y2;
        }

        public void AddToMap(Dictionary<Vector2, int> dict) 
        {
            Vector2 step = end - start;
            step.x = step.x > 0 ? 1 : step.x < 0 ? -1 : 0;
            step.y = step.y > 0 ? 1 : step.y < 0 ? -1 : 0;

            Vector2 stop = end + step;

            for(Vector2 curr = start; !curr.Equals(stop); curr = curr + step) {
                dict[curr] = dict.ContainsKey(curr) ? dict[curr] + 1 : 1;
                if(Math.Abs(curr.x) > 2000) throw new Exception("FUCK");
            }
        }

        public override string ToString() {
            return "[" + start.x + ", " + start.y + " -> " + end.x + ", " + end.y + "]";
        }
    }
}