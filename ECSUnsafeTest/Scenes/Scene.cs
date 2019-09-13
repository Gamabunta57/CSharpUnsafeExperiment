using System;
using System.Collections.Generic;
using ECSFoundation.ECS.Entities;
using ECSFoundation.ECS.Systems;
using ECSUnsafeTest.ECS.Systems;
using ECSUnsafeTest.Entities;
using ECSUnsafeTest.utils;

namespace ECSUnsafeTest.Scenes
{
    public class Scene
    {
        public Scene() => systemList = new List<ISystem>
            {
                new PhysicSystem(),
                new ProcessMovableSystem()
            };

        public void Load()
        {
            var player = EntityManager.NewEntity<PlayerEntity>();
            var player2 = EntityManager.NewEntity<PlayerEntity>();
            var ball = EntityManager.NewEntity<BallEntity>();

            player.Heading.Value = new Vector2 { X = 0, Y = 1 };
            player.Position.Value = new Vector2 { X = 0, Y = 1 };
            player.Collider.halfExtent = new Vector2 { X = 1, Y = 5 };
            player.Collider.center = new Vector2 { X = 1, Y = 5 };

            player2.Heading.Value = new Vector2 { X = 1, Y = 0 };
            player2.Position.Value = new Vector2 { X = 100, Y = 1 };
            player2.Collider.halfExtent = new Vector2 { X = 1, Y = 5 };
            player2.Collider.center = new Vector2 { X = 1, Y = 5 };

            ball.Heading.Value = new Vector2 { X = 1, Y = 1 };
            ball.Position.Value = new Vector2 { X = 0, Y = 1 };
            ball.Collider.halfExtent = new Vector2 { X = 2, Y = 2 };
            ball.Collider.center = new Vector2 { X = 2, Y = 2 };

            Console.WriteLine($"P1   id: {player.BaseEntity.Id}\t/\tPosition: {player.Position.Value}\t/\tHeading: {player.Heading.Value}");
            Console.WriteLine($"P2   id: {player2.BaseEntity.Id}\t/\tPosition: {player2.Position.Value}\t/\tHeading: {player2.Heading.Value}");
            Console.WriteLine($"Ball id: {ball.BaseEntity.Id}\t/\tPosition: {ball.Position.Value}\t/\tHeading: {ball.Heading.Value}");
        }

        public void Update()
        {
            foreach (var system in systemList)
                system.Update();
        }

        readonly IList<ISystem> systemList;
    }
}
