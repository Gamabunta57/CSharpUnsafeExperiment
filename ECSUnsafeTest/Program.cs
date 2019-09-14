using System;
using System.Threading;
using ECSFoundation.ECS.Entities;
using ECSFoundation.MemoryManagement.MemoryAllocators;
using ECSUnsafeTest.Scenes;

namespace ECSUnsafeTest
{
    class Program
    {
        static void Main()
        {
            using (var memory = MemoryBuilder.BuildMemoryAllocator())
            {
                EntityManager.Init(memory);
                var scene = new Scene();
                scene.Load();

                for (var i = 0; i < 200; i++)
                {
                    Console.WriteLine("=========================: New frame");
                    scene.Update();
                    Thread.Sleep(1000 / 60);
                }

                Console.ReadKey();
            }
            Console.WriteLine("Memory freed");
            Console.ReadKey();
        }
    }
}
