using System;
using System.Threading;
using ECSFoundation.ECS.Entities;
using ECSFoundation.MemoryManagement.MemoryAllocators;
using ECSImplementation.Scenes;
using Microsoft.Xna.Framework;

namespace ECSImplementation
{
    class Program
    {
        static void Main()
        {
            var timeSpan = new TimeSpan(0, 0, 0, 0, 1000 / 60);
            var gameTime = new GameTime(timeSpan, timeSpan);
            using (var memory = MemoryBuilder.BuildMemoryAllocator())
            {
                EntityManager.Init(memory);
                var scene = new Scene();
                scene.Load();

                for (var i = 0; i < 200; i++)
                {
                    scene.Update(gameTime);
                    Thread.Sleep(1000 / 60);
                }

                Console.ReadKey();
            }
            Console.WriteLine("Memory freed");
            Console.ReadKey();
        }
    }
}
