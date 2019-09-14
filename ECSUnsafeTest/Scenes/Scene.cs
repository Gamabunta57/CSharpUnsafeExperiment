using System;
using System.Collections.Generic;
using ECSFoundation.ECS.Entities;
using ECSFoundation.ECS.Systems;
using ECSUnsafeTest.ECS.Component;
using ECSUnsafeTest.ECS.Entities;
using ECSUnsafeTest.ECS.Systems;
using ECSUnsafeTest.Global;
using ECSUnsafeTest.utils;

namespace ECSUnsafeTest.Scenes
{
    public class Scene
    {

        PlayerEntity _player1;
        PlayerEntity _player2;
        BallEntity _ball;
        WallEntity _wallTop;
        WallEntity _wallBottom;
        WallEntity _goalLeft;
        WallEntity _goalRight;

        public Scene() => systemList = new List<ISystem>
            {
                new PhysicSystem(),
                new ProcessMovableSystem(),
                new MatchSystem(this)
            };

        public void Load()
        {
            _player1 = EntityManager.NewEntity<PlayerEntity>();
            _player2 = EntityManager.NewEntity<PlayerEntity>();
            _ball = EntityManager.NewEntity<BallEntity>();
            _wallTop = EntityManager.NewEntity<WallEntity>();
            _wallBottom = EntityManager.NewEntity<WallEntity>();
            _goalLeft = EntityManager.NewEntity<WallEntity>();
            _goalRight = EntityManager.NewEntity<WallEntity>();

            Console.WriteLine($"#{_player1.BaseEntity.Id}: Player1");
            Console.WriteLine($"#{_player2.BaseEntity.Id}: Player2");
            Console.WriteLine($"#{_ball.BaseEntity.Id}: Ball");
            Console.WriteLine($"#{_wallTop.BaseEntity.Id}: WallTop");
            Console.WriteLine($"#{_wallBottom.BaseEntity.Id}: WallBottom");
            Console.WriteLine($"#{_goalLeft.BaseEntity.Id}: GoalLeft");
            Console.WriteLine($"#{_goalRight.BaseEntity.Id}: GoalRight");

            Reset();
        }

        public void Update()
        {
            foreach (var system in systemList)
                system.Update();
        }

        public void Reset()
        {
            _player1.Heading.Value = new Vector2 { X = 0, Y = 0 };
            _player1.Position.Value = new Vector2 { X = 2, Y = 10 };
            _player1.Collider.halfExtent = new Vector2 { X = 1, Y = 5 };
            _player1.Collider.Center = new Vector2 { X = 0, Y = 0 };
            _player1.Collider.type = ColliderType.Player;

            _player2.Heading.Value = new Vector2 { X = 0, Y = 0 };
            _player2.Position.Value = new Vector2 { X = 38, Y = 10 };
            _player2.Collider.halfExtent = new Vector2 { X = 1, Y = 5 };
            _player2.Collider.Center = new Vector2 { X = 0, Y = 0 };
            _player2.Collider.type = ColliderType.Player;

            _ball.Heading.Value = new Vector2 { X = -1, Y = 1 };
            _ball.Position.Value = new Vector2 { X = 20, Y = 10 };
            _ball.Collider.halfExtent = new Vector2 { X = 2, Y = 2 };
            _ball.Collider.Center = new Vector2 { X = 0, Y = 0 };
            _ball.Collider.type = ColliderType.Ball;

            _wallTop.Position.Value = new Vector2 { X = 20, Y = -1 };
            _wallTop.Collider.halfExtent = new Vector2 { X = 20, Y = 1 };
            _wallTop.Collider.Center = new Vector2 { X = 0, Y = 0 };
            _wallTop.Collider.type = ColliderType.Wall;

            _wallBottom.Position.Value = new Vector2 { X = 20, Y = 21 };
            _wallBottom.Collider.halfExtent = new Vector2 { X = 20, Y = 1 };
            _wallBottom.Collider.Center = new Vector2 { X = 0, Y = 0 };
            _wallBottom.Collider.type = ColliderType.Wall;

            _goalLeft.Position.Value = new Vector2 { X = -1, Y = 10 };
            _goalLeft.Collider.halfExtent = new Vector2 { X = 1, Y = 10 };
            _goalLeft.Collider.Center = new Vector2 { X = 0, Y = 0 };
            _goalLeft.Collider.type = ColliderType.GoalPlayer2;

            _goalRight.Position.Value = new Vector2 { X = 41, Y = 10 };
            _goalRight.Collider.halfExtent = new Vector2 { X = 1, Y = 10 };
            _goalRight.Collider.Center = new Vector2 { X = 0, Y = 0 };
            _goalRight.Collider.type = ColliderType.GoalPlayer2;

            Console.WriteLine($"Match #{GameState.MatchNumber}");
            Console.WriteLine($"Score {GameState.ScorePlayer1} - {GameState.ScorePlayer2}");
        }

        readonly IList<ISystem> systemList;
    }
}
