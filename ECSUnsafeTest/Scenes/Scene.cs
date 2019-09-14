using System;
using System.Collections.Generic;
using ECSFoundation.ECS.Entities;
using ECSFoundation.ECS.Systems;
using ECSUnsafeTest.ECS.Component;
using ECSUnsafeTest.ECS.Entities;
using ECSUnsafeTest.ECS.Systems;
using ECSUnsafeTest.ECS.Systems.Subsytem;
using ECSUnsafeTest.Global;
using ECSUnsafeTest.utils;

namespace ECSUnsafeTest.Scenes
{
    public class Scene
    {
        public Scene() {

            _physics = new PhysicSystem((uint) Enum.GetNames(typeof(CollisionLayer)).Length);
            _physics.SetLayersAsCollidableTogether(CollisionLayer.Player, CollisionLayer.Ball, CollisionResponse.OnPlayerAndBallCollide);
            _physics.SetLayersAsCollidableTogether(CollisionLayer.Player, CollisionLayer.Wall, CollisionResponse.OnPlayerHitsTheWall);
            _physics.SetLayersAsCollidableTogether(CollisionLayer.Ball, CollisionLayer.Wall, CollisionResponse.OnBallTouchWall);
            _physics.SetLayersAsCollidableTogether(CollisionLayer.Ball, CollisionLayer.GoalPlayer1, CollisionResponse.OnBallReachGoalPlayer1);
            _physics.SetLayersAsCollidableTogether(CollisionLayer.Ball, CollisionLayer.GoalPlayer2, CollisionResponse.OnBallReachGoalPlayer2);

            _systemList = new List<ISystem>
            {
                _physics,
                new ProcessMovableSystem(),
                new MatchSystem(this)
            };
        }

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

            DoReset(true);
        }
        public void Reset() => DoReset(false);

        public void Update()
        {
            foreach (var system in _systemList)
                system.Update();
        }

        void DoReset(bool isFullReset)
        {
            _player1.Heading.Value = new Vector2 { X = 0, Y = 0 };
            _player1.Position.Value = new Vector2 { X = 2, Y = 10 };
            _player1.Collider.halfExtent = new Vector2 { X = 1, Y = 5 };
            _player1.Collider.Center = new Vector2 { X = 0, Y = 0 };
            _player1.Collider.type = CollisionLayer.Player;

            _player2.Heading.Value = new Vector2 { X = 0, Y = 0 };
            _player2.Position.Value = new Vector2 { X = 38, Y = 10 };
            _player2.Collider.halfExtent = new Vector2 { X = 1, Y = 5 };
            _player2.Collider.Center = new Vector2 { X = 0, Y = 0 };
            _player2.Collider.type = CollisionLayer.Player;

            _ball.Heading.Value = new Vector2 { X = -1, Y = 1 };
            _ball.Position.Value = new Vector2 { X = 20, Y = 10 };
            _ball.Collider.halfExtent = new Vector2 { X = 2, Y = 2 };
            _ball.Collider.Center = new Vector2 { X = 0, Y = 0 };
            _ball.Collider.type = CollisionLayer.Ball;

            _wallTop.Position.Value = new Vector2 { X = 20, Y = -1 };
            _wallTop.Collider.halfExtent = new Vector2 { X = 20, Y = 1 };
            _wallTop.Collider.Center = new Vector2 { X = 0, Y = 0 };
            _wallTop.Collider.type = CollisionLayer.Wall;

            _wallBottom.Position.Value = new Vector2 { X = 20, Y = 21 };
            _wallBottom.Collider.halfExtent = new Vector2 { X = 20, Y = 1 };
            _wallBottom.Collider.Center = new Vector2 { X = 0, Y = 0 };
            _wallBottom.Collider.type = CollisionLayer.Wall;

            _goalLeft.Position.Value = new Vector2 { X = -1, Y = 10 };
            _goalLeft.Collider.halfExtent = new Vector2 { X = 1, Y = 10 };
            _goalLeft.Collider.Center = new Vector2 { X = 0, Y = 0 };
            _goalLeft.Collider.type = CollisionLayer.GoalPlayer2;

            _goalRight.Position.Value = new Vector2 { X = 41, Y = 10 };
            _goalRight.Collider.halfExtent = new Vector2 { X = 1, Y = 10 };
            _goalRight.Collider.Center = new Vector2 { X = 0, Y = 0 };
            _goalRight.Collider.type = CollisionLayer.GoalPlayer2;

            if (isFullReset)
            {
                _physics.NotifyNewEntity(_player1);
                _physics.NotifyNewEntity(_player2);
                _physics.NotifyNewEntity(_ball);
                _physics.NotifyNewEntity(_wallBottom);
                _physics.NotifyNewEntity(_wallTop);
                _physics.NotifyNewEntity(_goalLeft);
                _physics.NotifyNewEntity(_goalRight);
            }

            Console.WriteLine($"Match #{GameState.MatchNumber}");
            Console.WriteLine($"Score {GameState.ScorePlayer1} - {GameState.ScorePlayer2}");
        }

        readonly IList<ISystem> _systemList;
        readonly PhysicSystem _physics;

        PlayerEntity _player1;
        PlayerEntity _player2;
        BallEntity _ball;
        WallEntity _wallTop;
        WallEntity _wallBottom;
        WallEntity _goalLeft;
        WallEntity _goalRight;
    }
}
