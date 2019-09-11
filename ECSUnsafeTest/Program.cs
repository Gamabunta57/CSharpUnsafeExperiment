
using ECSUnsafeTest.Component;
using ECSUnsafeTest.core;
using ECSUnsafeTest.MemoryAllocators;
using System;

namespace ECSUnsafeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var memoryBuilder = new MemoryBuilder();
            var memory = memoryBuilder.AutoSetData();

            using (memory)
            {
                var i = 0;
                for (; i < 1000; i++)
                {
                    memory.New<Position>();
                    memory.New<Heading>();
                }
                ref var position = ref memory.New<Position>();
                ref var heading = ref memory.New<Heading>();

                position.value = new Vector2 { X = 1, Y = 10 };
                heading.value = new Vector2 { X = 1, Y = 0 };

                Console.WriteLine(string.Format("Position before: ({0},{1})", position.value.X, position.value.Y));
                Console.WriteLine(string.Format("Heading before: ({0},{1})", heading.value.X, heading.value.Y));

                ref var position2 = ref memory.Get<Position>((ushort)(i));
                ref var heading2 = ref memory.Get<Heading>((ushort)(i));

                position2.value = new Vector2 { X = 2, Y = 20 };
                heading2.value = new Vector2 { X = 3, Y = 30 };

                Console.WriteLine(string.Format("Position before: ({0},{1})", position2.value.X, position2.value.Y));
                Console.WriteLine(string.Format("Heading before: ({0},{1})", heading2.value.X, heading2.value.Y));

                Console.WriteLine(string.Format("Position before: ({0},{1})", position.value.X, position.value.Y));
                Console.WriteLine(string.Format("Heading before: ({0},{1})", heading.value.X, heading.value.Y));

                Console.ReadKey();
            }
            Console.WriteLine("Memory freed");

            Console.ReadKey();
        }
    }
}
