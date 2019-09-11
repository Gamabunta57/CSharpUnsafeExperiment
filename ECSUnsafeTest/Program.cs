
using ECSUnsafeTest.Component;
using ECSUnsafeTest.core;
using ECSUnsafeTest.MemoryAllocators;

namespace ECSUnsafeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var allocator = new MemoryAllocator();

            ref var position1 = ref allocator.New<Heading>();
            ref var position2 = ref allocator.New<Heading>();
            ref var heading1 = ref allocator.New<Position>();
            ref var heading2 = ref allocator.New<Position>();

            position1.X = 0;
            position1.Y = 0;

            position2.X = 1;
            position2.Y = 1;

            heading1.value.X = 2;
            heading1.value.Y = 2;

            heading2.value.X = 3;
            heading2.value.Y = 3;

            System.Console.WriteLine($"Position 1X: {position1.X}");
            System.Console.WriteLine($"Position 1Y: {position1.Y}");
            System.Console.WriteLine($"Position 2X: {position2.X}");
            System.Console.WriteLine($"Position 2Y: {position2.Y}");

            System.Console.WriteLine($"Heading 1X: {heading1.value.X}");
            System.Console.WriteLine($"Heading 1Y: {heading1.value.Y}");
            System.Console.WriteLine($"Heading 2X: {heading2.value.X}");
            System.Console.WriteLine($"Heading 2Y: {heading2.value.Y}");

            ref var position3 = ref allocator.Get<Vector2>(0);
            ref var position4 = ref allocator.Get<Vector2>(1);
            ref var heading3 = ref allocator.Get<Vector2>(2);
            ref var heading4 = ref allocator.Get<Vector2>(3);

            position3.X = 4;
            position3.Y = 4;

            position4.X = 5;
            position4.Y = 5;

            heading3.X = 6;
            heading3.Y = 6;

            heading4.X = 7;
            heading4.Y = 7;

            System.Console.WriteLine($"Position 3X: {position3.X}");
            System.Console.WriteLine($"Position 3Y: {position3.Y}");
            System.Console.WriteLine($"Position 4X: {position4.X}");
            System.Console.WriteLine($"Position 4Y: {position4.Y}");

            System.Console.WriteLine($"Heading 3X: {heading3.X}");
            System.Console.WriteLine($"Heading 3Y: {heading3.Y}");
            System.Console.WriteLine($"Heading 4X: {heading4.X}");
            System.Console.WriteLine($"Heading 4Y: {heading4.Y}");

            System.Console.WriteLine($"Position 1X: {position1.X}");
            System.Console.WriteLine($"Position 1Y: {position1.Y}");
            System.Console.WriteLine($"Position 2X: {position2.X}");
            System.Console.WriteLine($"Position 2Y: {position2.Y}");

            System.Console.WriteLine($"Heading 1X: {heading1.value.X}");
            System.Console.WriteLine($"Heading 1Y: {heading1.value.Y}");
            System.Console.WriteLine($"Heading 2X: {heading2.value.X}");
            System.Console.WriteLine($"Heading 2Y: {heading2.value.Y}");
        }
    }
}
