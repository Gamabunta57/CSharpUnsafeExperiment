using System;
using System.Collections.Generic;
using ECSUnsafeTest.Entities;
using ECSUnsafeTest.MemoryAllocators;
using ECSUnsafeTest.Systems;

namespace ECSUnsafeTest.Scenes
{
    public class Scene
    {
        readonly MasterMemoryAllocator allocator;
        readonly IList<ProcessPlayerSystem> systemList;

        public Scene(MasterMemoryAllocator allocator)
        {
            this.allocator = allocator;
            systemList = new List<ProcessPlayerSystem>
            {
                new ProcessPlayerSystem(allocator)
            };
        }

        public void Load()
        {
            ref var player = ref allocator.New<PlayerEntity>();
            ref var player2 = ref allocator.New<PlayerEntity>();

            player.heading = new core.Vector2 { X = 0, Y = 1 };
            player.position = new core.Vector2 { X = 0, Y = 1 };

            player2.heading = new core.Vector2 { X = 1, Y = 0 };
            player2.position = new core.Vector2 { X = 10, Y = 1 };

            Console.WriteLine($"P1 id: {player.baseEntity.Id}");
            Console.WriteLine($"P2 id: {player2.baseEntity.Id}");

            foreach (var system in systemList)
            {
                system.OnRegisterEntity(player.baseEntity.Id);
                system.OnRegisterEntity(player2.baseEntity.Id);
            }
        }

        public void Update()
        {
            foreach (var system in systemList)
                system.Update();
        }
    }
}
