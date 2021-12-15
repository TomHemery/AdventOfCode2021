using System.Collections.Generic;
namespace AdventOfCode2021
{
    public static class CoordinateGrid
    {
        public static List<Vector2> GetOrthoNeighbours(Vector2 point, int[,] grid)
        {
            List<Vector2> result = new List<Vector2>();
            if(point.x - 1 >= 0) result.Add(new Vector2(point.x - 1, point.y));
            if(point.x + 1 < grid.GetLength(0)) result.Add(new Vector2(point.x + 1, point.y));
            if(point.y - 1 >= 0) result.Add(new Vector2(point.x, point.y - 1));
            if(point.y + 1 < grid.GetLength(1)) result.Add(new Vector2(point.x, point.y + 1));
            return result;
        }
    }
}