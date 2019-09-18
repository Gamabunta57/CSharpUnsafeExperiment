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
    public class StressScene
    {
        public StressScene()
        {
            _physics = new PhysicSystem((uint)Enum.GetNames(typeof(CollisionLayer)).Length);
            _systemList = new List<ISystem>();
            _rand = new Random();
            _ballList = new BallEntity[_ballCount - 4];
        }

        public void Load()
        {
            var inputSystem = new ProcessInputSystem();

            _systemList.Add(new ProcessMovableSystem());
            _systemList.Add(_physics);

            for(var i = 0; i < _ballCount - 4; i++) { 
                _ballList[i] = EntityManager.NewEntity<BallEntity>();
                Console.WriteLine($"#{_ballList[i].BaseEntity.Id}: Ball");
            }

            _wallTop = EntityManager.NewEntity<WallEntity>();
            _wallBottom = EntityManager.NewEntity<WallEntity>();
            _wallLeft = EntityManager.NewEntity<WallEntity>();
            _wallRight = EntityManager.NewEntity<WallEntity>();

            Console.WriteLine($"#{_wallTop.BaseEntity.Id}: WallTop");
            Console.WriteLine($"#{_wallBottom.BaseEntity.Id}: WallBottom");
            Console.WriteLine($"#{_wallLeft.BaseEntity.Id}: GoalLeft");
            Console.WriteLine($"#{_wallRight.BaseEntity.Id}: GoalRight");

            _physics.SetLayersAsCollidableTogether(CollisionLayer.Ball, CollisionLayer.Wall, CollisionResponse.OnBallTouchWallFull);
            _physics.SetLayersAsCollidable(CollisionLayer.Ball, CollisionResponse.OnBallsCollide);

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

            spriteBatch.Draw(whitePixel, new Rectangle((_wallTop.Position.Value - _wallTop.Collider.halfExtent).ToPoint(), (_wallTop.Collider.halfExtent * 2).ToPoint()), Color.DarkGoldenrod);
            spriteBatch.Draw(whitePixel, new Rectangle((_wallBottom.Position.Value - _wallBottom.Collider.halfExtent).ToPoint(), (_wallBottom.Collider.halfExtent * 2).ToPoint()), Color.DarkGoldenrod);
            spriteBatch.Draw(whitePixel, new Rectangle((_wallLeft.Position.Value - _wallLeft.Collider.halfExtent).ToPoint(), (_wallLeft.Collider.halfExtent * 2).ToPoint()), Color.DarkGoldenrod);
            spriteBatch.Draw(whitePixel, new Rectangle((_wallRight.Position.Value - _wallRight.Collider.halfExtent).ToPoint(), (_wallRight.Collider.halfExtent * 2).ToPoint()), Color.DarkGoldenrod);

            var toggle = false;
            for (var i = 0; i < _ballCount - 4; i++)
            {
                spriteBatch.Draw(whitePixel, new Rectangle((_ballList[i].Position.Value - _ballList[i].Collider.halfExtent).ToPoint(), (_ballList[i].Collider.halfExtent * 2).ToPoint()), toggle ? Color.CadetBlue : Color.MediumVioletRed);
                toggle = !toggle;
            }

            spriteBatch.End();
        }

        void DoReset(bool isFullReset)
        {
            var ballCount = _ballCount;
            for (var i = 0; i < ballCount - 4; i++)
            {
                _ballList[i].Heading.Value = GetRandomVectorOrientation();
                _ballList[i].Heading.Velocity = 150;
                _ballList[i].Position.Value = GetRandomPosition();
                _ballList[i].Collider.halfExtent = new Vector2 { X = 5, Y = 5 };
                _ballList[i].Collider.Center = new Vector2 { X = 0, Y = 0 };
                _ballList[i].Collider.type = CollisionLayer.Ball;
            }

            _wallTop.Position.Value = new Vector2 { X = 320, Y = 10 };
            _wallTop.Collider.halfExtent = new Vector2 { X = 320, Y = 10 };
            _wallTop.Collider.Center = new Vector2 { X = 0, Y = 0 };
            _wallTop.Collider.type = CollisionLayer.Wall;

            _wallBottom.Position.Value = new Vector2 { X = 320, Y = 470 };
            _wallBottom.Collider.halfExtent = new Vector2 { X = 320, Y = 10 };
            _wallBottom.Collider.Center = new Vector2 { X = 0, Y = 0 };
            _wallBottom.Collider.type = CollisionLayer.Wall;

            _wallLeft.Position.Value = new Vector2 { X = 10, Y = 240 };
            _wallLeft.Collider.halfExtent = new Vector2 { X = 10, Y = 240 };
            _wallLeft.Collider.Center = new Vector2 { X = 0, Y = 0 };
            _wallLeft.Collider.type = CollisionLayer.Wall;

            _wallRight.Position.Value = new Vector2 { X = 630, Y = 240 };
            _wallRight.Collider.halfExtent = new Vector2 { X = 10, Y = 240 };
            _wallRight.Collider.Center = new Vector2 { X = 0, Y = 0 };
            _wallRight.Collider.type = CollisionLayer.Wall;

            if (isFullReset)
            {
                for (var i = 0; i < _ballCount - 4; i++)
                    _physics.NotifyNewEntity(_ballList[i]);

                _physics.NotifyNewEntity(_wallBottom);
                _physics.NotifyNewEntity(_wallTop);
                _physics.NotifyNewEntity(_wallLeft);
                _physics.NotifyNewEntity(_wallRight);
            }

            Console.WriteLine($"Match #{MatchState.MatchNumber}");
            Console.WriteLine($"Score {MatchState.ScorePlayer1} - {MatchState.ScorePlayer2}");
        }

        private Vector2 GetRandomVectorOrientation()
        {
            var radian = _rand.NextDouble() * 2 * Math.PI;
            var vec = new Vector2((float)Math.Cos(radian), (float)Math.Sin(radian));
            vec.Normalize();
            return vec;
        }

        private Vector2 GetRandomPosition()
        {
            return new Vector2 { X =(float)(_rand.NextDouble() *600f + 20), Y = (float)(_rand.NextDouble() * 440f + 20) };
        }

        readonly IList<ISystem> _systemList;
        readonly PhysicSystem _physics;
        readonly Random _rand;
        readonly BallEntity[] _ballList;

        WallEntity _wallTop;
        WallEntity _wallBottom;
        WallEntity _wallLeft;
        WallEntity _wallRight;

        int _ballCount = 800 + 4;
    }
}
