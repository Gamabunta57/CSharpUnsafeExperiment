using System;
using System.Collections.Generic;
using ECSFoundation.ECS.Entities;
using ECSFoundation.ECS.Systems;
using ECSUnsafeTest.ECS.Component;
using ECSUnsafeTest.ECS.Entities;
using ECSUnsafeTest.ECS.Systems;
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
            var wallTop = EntityManager.NewEntity<WallEntity>();
            var wallBottom = EntityManager.NewEntity<WallEntity>();
            var goalLeft = EntityManager.NewEntity<WallEntity>();
            var goalRight = EntityManager.NewEntity<WallEntity>();

            player.Heading.Value = new Vector2 { X = 0, Y = 0 };
            player.Position.Value = new Vector2 { X = 2, Y = 10 };
            player.Collider.halfExtent = new Vector2 { X = 1, Y = 5 };
            player.Collider.Center = new Vector2 { X = 0, Y = 0 };
            player.Collider.type = ColliderType.Player;

            player2.Heading.Value = new Vector2 { X = 0, Y = 0 };
            player2.Position.Value = new Vector2 { X = 38, Y = 10 };
            player2.Collider.halfExtent = new Vector2 { X = 1, Y = 5 };
            player2.Collider.Center = new Vector2 { X = 0, Y = 0 };
            player2.Collider.type = ColliderType.Player;

            ball.Heading.Value = new Vector2 { X = -1, Y = 1 };
            ball.Position.Value = new Vector2 { X = 20, Y = 10 };
            ball.Collider.halfExtent = new Vector2 { X = 2, Y = 2 };
            ball.Collider.Center = new Vector2 { X = 0, Y =0 };
            ball.Collider.type = ColliderType.Ball;

            wallTop.Position.Value = new Vector2 { X = 20, Y = -1 };
            wallTop.Collider.halfExtent = new Vector2 { X = 20, Y = 1 };
            wallTop.Collider.Center = new Vector2 { X = 0, Y = 0 };
            wallTop.Collider.type = ColliderType.Wall;

            wallBottom.Position.Value = new Vector2 { X = 20, Y = 21 };
            wallBottom.Collider.halfExtent = new Vector2 { X = 20, Y = 1 };
            wallBottom.Collider.Center = new Vector2 { X = 0, Y = 0 };
            wallBottom.Collider.type = ColliderType.Wall;

            goalLeft.Position.Value = new Vector2 { X = -1, Y = 10 };
            goalLeft.Collider.halfExtent = new Vector2 { X = 1, Y = 10 };
            goalLeft.Collider.Center = new Vector2 { X = 0, Y = 0 };
            goalLeft.Collider.type = ColliderType.Goal;

            goalRight.Position.Value = new Vector2 { X = 41, Y = 10 };
            goalRight.Collider.halfExtent = new Vector2 { X = 1, Y = 10 };
            goalRight.Collider.Center = new Vector2 { X = 0, Y = 0 };
            goalRight.Collider.type = ColliderType.Goal;

            Console.WriteLine($"#{player.BaseEntity.Id}: Player1");
            Console.WriteLine($"#{player2.BaseEntity.Id}: Player2");
            Console.WriteLine($"#{ball.BaseEntity.Id}: Ball");
            Console.WriteLine($"#{wallTop.BaseEntity.Id}: WallTop");
            Console.WriteLine($"#{wallBottom.BaseEntity.Id}: WallBottom");
            Console.WriteLine($"#{goalLeft.BaseEntity.Id}: GoalLeft");
            Console.WriteLine($"#{goalRight.BaseEntity.Id}: GoalRight");
        }

        public void Update()
        {
            foreach (var system in systemList)
                system.Update();
        }

        readonly IList<ISystem> systemList;
    }
}
