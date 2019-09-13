using System;
using ECSFoundation.ECS.Entities;
using ECSFoundation.MemoryManagement.MemoryAllocators;
using ECSUnsafeTest.Scenes;

namespace ECSUnsafeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var memory = MemoryBuilder.BuildMemoryAllocator();
            using (memory)
            {
                EntityManager.Init(memory);
                var scene = new Scene();
                scene.Load();

                for (var i = 0; i < 10; i++)
                    scene.Update();
            }

            Console.WriteLine("Memory freed");
        }
    }
}
