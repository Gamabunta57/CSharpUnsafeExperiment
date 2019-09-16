using System;
using System.Collections.Generic;
using ECSFoundation.ECS.Entities;
using ECSFoundation.ECS.Systems;
using ECSImplementation.ECS.Component;
using ECSImplementation.ECS.Entities;
using ECSImplementation.ECS.Systems;
using ECSImplementation.ECS.Systems.Subsytem;
using ECSImplementation.Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECSImplementation.Scenes
{
    public class Scene
    {
        public Scene()
        {
            _physics = new PhysicSystem((uint)Enum.GetNames(typeof(CollisionLayer)).Length);
            _systemList = new List<ISystem>();
            _rand = new Random();
        }

        public void Load()
        {
            var inputSystem = new ProcessInputSystem();

            _systemList.Add(inputSystem);
            _systemList.Add(new ProcessMovableSystem());
            _systemList.Add(_physics);
            _systemList.Add(new MatchSystem(this));

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

            _physics.SetLayersAsCollidableTogether(CollisionLayer.Player, CollisionLayer.Ball, CollisionResponse.OnPlayerAndBallCollide);
            _physics.SetLayersAsCollidableTogether(CollisionLayer.Player, CollisionLayer.Wall, CollisionResponse.OnPlayerHitsTheWall);
            _physics.SetLayersAsCollidableTogether(CollisionLayer.Ball, CollisionLayer.Wall, CollisionResponse.OnBallTouchWall);
            _physics.SetLayersAsCollidableTogether(CollisionLayer.Ball, CollisionLayer.GoalPlayer1, CollisionResponse.OnBallReachGoalPlayer1);
            _physics.SetLayersAsCollidableTogether(CollisionLayer.Ball, CollisionLayer.GoalPlayer2, CollisionResponse.OnBallReachGoalPlayer2);

            inputSystem.Init(_player1, _player2);

            DoReset(true);
        }
        public void Reset() => DoReset(false);

        public void Update(GameTime gameTime)
        {
            foreach (var system in _systemList)
                system.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D whitePixel)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(whitePixel, new Rectangle((_player1.Position.Value - _player1.Collider.halfExtent).ToPoint(), (_player1.Collider.halfExtent * 2).ToPoint()), Color.MediumPurple);
            spriteBatch.Draw(whitePixel, new Rectangle((_player2.Position.Value - _player2.Collider.halfExtent).ToPoint(), (_player2.Collider.halfExtent * 2).ToPoint()), Color.MediumSeaGreen);

            spriteBatch.Draw(whitePixel, new Rectangle((_wallTop.Position.Value - _wallTop.Collider.halfExtent).ToPoint(), (_wallTop.Collider.halfExtent * 2).ToPoint()), Color.DarkGoldenrod);
            spriteBatch.Draw(whitePixel, new Rectangle((_wallBottom.Position.Value - _wallBottom.Collider.halfExtent).ToPoint(), (_wallBottom.Collider.halfExtent * 2).ToPoint()), Color.DarkGoldenrod);

            spriteBatch.Draw(whitePixel, new Rectangle((_ball.Position.Value - _ball.Collider.halfExtent).ToPoint(), (_ball.Collider.halfExtent * 2).ToPoint()), Color.White);

            spriteBatch.End();
        }

        void DoReset(bool isFullReset)
        {
            _player1.Heading.Value = new Vector2 { X = 0, Y = 0 };
            _player1.Heading.Velocity = 120;
            _player1.Position.Value = new Vector2 { X = 20, Y = 240 };
            _player1.Collider.halfExtent = new Vector2 { X = 5, Y = 30 };
            _player1.Collider.Center = new Vector2 { X = 0, Y = 0 };
            _player1.Collider.type = CollisionLayer.Player;

            _player2.Heading.Value = new Vector2 { X = 0, Y = 0 };
            _player2.Heading.Velocity = 120;
            _player2.Position.Value = new Vector2 { X = 620, Y = 240 };
            _player2.Collider.halfExtent = new Vector2 { X = 5, Y = 30 };
            _player2.Collider.Center = new Vector2 { X = 0, Y = 0 };
            _player2.Collider.type = CollisionLayer.Player;


            _ball.Heading.Value = GetRandomVectorOrientation();
            _ball.Heading.Velocity = 150;
            _ball.Position.Value = new Vector2 { X = 320, Y = 240 };
            _ball.Collider.halfExtent = new Vector2 { X = 5, Y = 5 };
            _ball.Collider.Center = new Vector2 { X = 0, Y = 0 };
            _ball.Collider.type = CollisionLayer.Ball;

            _wallTop.Position.Value = new Vector2 { X = 320, Y = 10 };
            _wallTop.Collider.halfExtent = new Vector2 { X = 320, Y = 10 };
            _wallTop.Collider.Center = new Vector2 { X = 0, Y = 0 };
            _wallTop.Collider.type = CollisionLayer.Wall;

            _wallBottom.Position.Value = new Vector2 { X = 320, Y = 470 };
            _wallBottom.Collider.halfExtent = new Vector2 { X = 320, Y = 10 };
            _wallBottom.Collider.Center = new Vector2 { X = 0, Y = 0 };
            _wallBottom.Collider.type = CollisionLayer.Wall;

            _goalLeft.Position.Value = new Vector2 { X = -10, Y = 240 };
            _goalLeft.Collider.halfExtent = new Vector2 { X = 10, Y = 240 };
            _goalLeft.Collider.Center = new Vector2 { X = 0, Y = 0 };
            _goalLeft.Collider.type = CollisionLayer.GoalPlayer2;

            _goalRight.Position.Value = new Vector2 { X = 650, Y = 240 };
            _goalRight.Collider.halfExtent = new Vector2 { X = 10, Y = 240 };
            _goalRight.Collider.Center = new Vector2 { X = 0, Y = 0 };
            _goalRight.Collider.type = CollisionLayer.GoalPlayer1;

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

        private Vector2 GetRandomVectorOrientation()
        {
            var radian = _rand.Next(-45,45) * (2 * Math.PI / 360);
            var vector = new Vector2((float)Math.Cos(radian), (float)Math.Sin(radian));
            if (_rand.NextDouble() > .5)
                vector = -vector;
            return vector;
        }

        readonly IList<ISystem> _systemList;
        readonly PhysicSystem _physics;
        readonly Random _rand;

        PlayerEntity _player1;
        PlayerEntity _player2;
        BallEntity _ball;
        WallEntity _wallTop;
        WallEntity _wallBottom;
        WallEntity _goalLeft;
        WallEntity _goalRight;
    }
}
