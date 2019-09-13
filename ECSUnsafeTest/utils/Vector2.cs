namespace ECSUnsafeTest.utils
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
        public static Vector2 operator *(Vector2 self, int scalar)
        {
            self.X *= 2;
            self.Y *= 2;
            return self;
        }

        public override string ToString() => $"{nameof(Vector2)}( x: {X}, y: {Y} )";
    }
}
