namespace ECSUnsafeTest.core
{
    public struct Vector2
    {
        public int X;
        public int Y;

        public static Vector2 operator +(Vector2 self, Vector2 other)
        {
            self.X += other.X;
            self.Y += other.Y;
            return self;
        }

        public static Vector2 operator -(Vector2 self, Vector2 other)
        {
            self.X -= other.X;
            self.Y -= other.Y;
            return self;
        }

        public override string ToString()
        {
            return $"{nameof(Vector2)}( x: {X}, y: {Y} )";
        }
    }
}
