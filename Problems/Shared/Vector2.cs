using System.Numerics;
namespace AdventOfCode2021
{
    public struct Vector2 {
        public int x;
        public int y;

        public Vector2(int x, int y) 
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2 operator +(Vector2 a, Vector2 b) => new Vector2(a.x + b.x, a.y + b.y);
        public static Vector2 operator -(Vector2 a, Vector2 b) => new Vector2(a.x - b.x, a.y - b.y);

        public bool Equals(Vector2 other) 
        {
            return other.x == x && other.y == y;
        }

        public override string ToString()
        {
            return "[" + x + ", " + y + "]";
        }

        public Vector2 Clone()
        {
            return new Vector2(x, y);
        }
    }
}