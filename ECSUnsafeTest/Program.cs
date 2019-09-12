using ECSUnsafeTest.Entities;
using ECSUnsafeTest.MemoryAllocators;
using ECSUnsafeTest.Scenes;
using System;

namespace ECSUnsafeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var memoryBuilder = new MemoryBuilder();

            using (var memory = memoryBuilder.AutoSetData())
            {
                var player_test = memory.NewEntity<PlayerEntity>();

                var scene = new Scene(memory);
                scene.Load();

                for (var i = 0; i < 10;i++)
                    scene.Update();

                Console.ReadKey();
            }

            Console.WriteLine("Memory freed");
            Console.ReadKey();
        }
    }
}
