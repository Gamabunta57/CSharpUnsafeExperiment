using System;
using System.Collections.Generic;
using ECSUnsafeTest.ECS.Component;
using ECSUnsafeTest.core;
using ECSUnsafeTest.Entities;
using ECSUnsafeTest.MemoryManagement.MemoryAllocators;
using ECSUnsafeTest.Systems;

namespace ECSUnsafeTest.Scenes
{
    public class Scene
    {
        readonly MemoryAllocator allocator;
        readonly IList<ProcessMovableSystem> systemList;

        public Scene(MemoryAllocator allocator)
        {
            this.allocator = allocator;
            systemList = new List<ProcessMovableSystem>
            {
                new ProcessMovableSystem(allocator)
            };
        }

        public void Load()
        {
            var player = allocator.NewEntity<PlayerEntity>();
            var player2 = allocator.NewEntity<PlayerEntity>();
            var ball = allocator.NewEntity<BallEntity>();

            player.Heading = new Heading { Value = new Vector2 { X = 0, Y = 1 } };
            player.Position = new Position { Value = new Vector2 { X = 10, Y = 1 } };

            player2.Heading = new Heading { Value = new Vector2 { X = 1, Y = 0 } };
            player2.Position = new Position { Value = new Vector2 { X = 1, Y = 10 } };

            ball.Heading = new Heading { Value = new Vector2 { X = 1, Y = 1 } };
            ball.Position = new Position { Value = new Vector2 { X = 50, Y = 50 } };

            Console.WriteLine($"P1   id: {player.BaseEntity.Id}");
            Console.WriteLine($"P2   id: {player2.BaseEntity.Id}");
            Console.WriteLine($"Ball id: {ball.BaseEntity.Id}");

            foreach (var system in systemList)
            {
                system.OnRegisterEntity(player);
                system.OnRegisterEntity(player2);
                system.OnRegisterEntity(ball);
            }
        }

        public void Update()
        {
            foreach (var system in systemList)
                system.Update();
        }
    }
}
