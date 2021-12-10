using System.Collections.Generic;
namespace AdventOfCode2021
{
    public class Basin
    {
        public List<Vector2> contents = new List<Vector2>();
        public Vector2 lowPoint;

        public bool Contains(Vector2 point)
        {
            return contents.Contains(point);
        }

        public void addPoint(Vector2 point)
        {
            contents.Add(point);
        }

        public void setLowPoint(Vector2 point)
        {
            lowPoint = point;
        }

        public int GetSize()
        {
            return contents.Count;
        }
    }
}