using System;
using System.Collections.Generic;
using ECSUnsafeTest.Component;
using ECSUnsafeTest.core;
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
            var player = allocator.New();
            var player2 = allocator.New();

            player.Heading = new Heading { Value = new Vector2 { X = 0, Y = 1 } };
            player.Position = new Position { Value = new Vector2 { X = 10, Y = 1 } };

            player2.Heading = new Heading { Value = new Vector2 { X = 1, Y = 0 } };
            player2.Position = new Position { Value = new Vector2 { X = 1, Y = 10 } };

            Console.WriteLine($"P1 id: {player.BaseEntity.Id}");
            Console.WriteLine($"P2 id: {player2.BaseEntity.Id}");

            foreach (var system in systemList)
            {
                system.OnRegisterEntity(player);
                system.OnRegisterEntity(player2);
            }
        }

        public void Update()
        {
            foreach (var system in systemList)
                system.Update();
        }
    }
}
