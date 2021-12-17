namespace AdventOfCode2021
{
    public class Probe
    {
        public Vector2 pos {get; private set;}
        public int highestY {get; private set;} = int.MinValue;
        public Vector2 initalVelocity {get; private set;}

        protected Vector2 vel;
        protected Vector2[] targetArea;

        public Probe(Vector2 initialVel, Vector2[] targetArea)
        {
            this.initalVelocity = initialVel;
            vel = initialVel;
            pos = new Vector2(0, 0);
            this.targetArea = targetArea;
        }    

        public bool Update()
        {
            pos += vel;
            vel.x = vel.x  > 0 ? vel.x - 1 : 0;
            vel.y--;

            if(pos.y > highestY) highestY = pos.y;

            return pos.x >= targetArea[0].x && pos.y >= targetArea[0].y && pos.x <= targetArea[1].x && pos.y <= targetArea[1].y;
        }
    }
}