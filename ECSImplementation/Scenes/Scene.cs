using System;
using System.Collections.Generic;
using ECSFoundation.ECS.Entities;
using ECSFoundation.ECS.Systems;
using ECSImplementation.ECS.Component;
using ECSImplementation.ECS.Entities;
using ECSImplementation.ECS.Systems;
using ECSImplementation.ECS.Systems.DrawSystem;
using ECSImplementation.ECS.Systems.Subsytem;
using ECSImplementation.Global;
using ECSImplementation.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECSImplementation.Scenes
{
    public class Scene : IScene
    {
        public Scene()
        {
            _physics = new PhysicSystem((uint)Enum.GetNames(typeof(CollisionLayer)).Length);
            _activeSystemList = new List<ISystem>();
            _activeDrawSystemList = new List<IDrawSystem>();

            _pauseSystemList = new List<ISystem>();
            _pauseDrawSystemList = new List<IDrawSystem>();

            _rand = new Random();
        }

        public void Load(SceneManager sceneManager)
        {
            _sceneManager = sceneManager;
            var inputSystem = new ProcessInputSystem();

            _activeSystemList.Add(inputSystem);
            _activeSystemList.Add(new ProcessMovableSystem());
            _activeSystemList.Add(_physics);
            _activeSystemList.Add(new MatchSystem(_sceneManager));

            _activeDrawSystemList.Add(new MainSceneDrawSystem());

            var menu = new Menu(new Tuple<string, Action>[] {
                new Tuple<string, Action>("Resume", ResumeFromPause),
                new Tuple<string, Action>("Return to main menu", ReturnToMainMenu)
            });

            _pauseSystemList.Add(new MenuInputSystem(menu));

            _pauseDrawSystemList.Add(new MainSceneDrawSystem());
            _pauseDrawSystemList.Add(new PauseMenuDrawSystem(menu));

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
            if (MatchState.AskForPaused)
            {
                foreach (var system in _pauseSystemList)
                    system.Update(gameTime);
            }
            else
            {
                foreach (var system in _activeSystemList)
                    system.Update(gameTime);
            }
            
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (MatchState.AskForPaused)
            {
                for (var i = 0; i < _pauseDrawSystemList.Count; i++)
                    _pauseDrawSystemList[i].Draw(gameTime, spriteBatch);
            }
            else
            {
                for (var i = 0; i < _activeDrawSystemList.Count; i++)
                    _activeDrawSystemList[i].Draw(gameTime, spriteBatch);
            }
        }

        void DoReset(bool isFullReset)
        {

            var collider = new Vector2 { X = 5, Y = 30 };
            _player1.Heading.Value = new Vector2 { X = 0, Y = 0 };
            _player1.Heading.Velocity = 120;
            _player1.Position.Value = new Vector2 { X = 20, Y = 240 };
            _player1.Collider.halfExtent = collider;
            _player1.Collider.Center = collider;
            _player1.Collider.type = CollisionLayer.Player;
            _player1.SpriteInfo.Size = collider * 2;
            _player1.SpriteInfo.Color = Color.MediumPurple;

            collider = new Vector2 { X = 5, Y = 30 };
            _player2.Heading.Value = new Vector2 { X = 0, Y = 0 };
            _player2.Heading.Velocity = 120;
            _player2.Position.Value = new Vector2 { X = 620, Y = 240 };
            _player2.Collider.halfExtent = collider;
            _player2.Collider.Center = collider;
            _player2.Collider.type = CollisionLayer.Player;
            _player2.SpriteInfo.Size = collider * 2;
            _player2.SpriteInfo.Color = Color.MediumSeaGreen;

            collider = new Vector2 { X = 5, Y = 5 };
            _ball.Heading.Value = GetRandomVectorOrientation();
            _ball.Heading.Velocity = 150;
            _ball.Position.Value = new Vector2 { X = 315, Y = 235 };
            _ball.Collider.halfExtent = collider;
            _ball.Collider.Center = collider;
            _ball.Collider.type = CollisionLayer.Ball;
            _ball.SpriteInfo.Size = collider * 2;
            _ball.SpriteInfo.Color = Color.White;

            collider = new Vector2 { X = 320, Y = 10 };
            _wallTop.Position.Value = new Vector2 { X = 0, Y = 0 };
            _wallTop.Collider.halfExtent = collider;
            _wallTop.Collider.Center = collider;
            _wallTop.Collider.type = CollisionLayer.Wall;
            _wallTop.SpriteInfo.Size = collider * 2;
            _wallTop.SpriteInfo.Color = Color.DarkGoldenrod;

            collider = new Vector2 { X = 320, Y = 10 };
            _wallBottom.Position.Value = new Vector2 { X = 0, Y = 460 };
            _wallBottom.Collider.halfExtent = collider;
            _wallBottom.Collider.Center = collider;
            _wallBottom.Collider.type = CollisionLayer.Wall;
            _wallBottom.SpriteInfo.Size = collider * 2;
            _wallBottom.SpriteInfo.Color = Color.DarkGoldenrod;

            collider = new Vector2 { X = 10, Y = 240 };
            _goalLeft.Position.Value = new Vector2 { X = -20, Y = 0 };
            _goalLeft.Collider.halfExtent = collider;
            _goalLeft.Collider.Center = collider;
            _goalLeft.Collider.type = CollisionLayer.GoalPlayer2;
            _goalLeft.SpriteInfo.Size = collider * 2;
            _goalLeft.SpriteInfo.Color = Color.White;

            collider = new Vector2 { X = 10, Y = 240 };
            _goalRight.Position.Value = new Vector2 { X = 640, Y = 0 };
            _goalRight.Collider.halfExtent = collider;
            _goalRight.Collider.Center = collider;
            _goalRight.Collider.type = CollisionLayer.GoalPlayer1;
            _goalRight.SpriteInfo.Size = collider * 2;
            _goalRight.SpriteInfo.Color = Color.White;

            if (isFullReset)
            {
                _physics.NotifyNewEntity(_player1);
                _physics.NotifyNewEntity(_player2);
                _physics.NotifyNewEntity(_ball);
                _physics.NotifyNewEntity(_wallBottom);
                _physics.NotifyNewEntity(_wallTop);
                _physics.NotifyNewEntity(_goalLeft);
                _physics.NotifyNewEntity(_goalRight);

                MatchState.Reset();
            }

            Console.WriteLine($"Match #{MatchState.MatchNumber}");
            Console.WriteLine($"Score {MatchState.ScorePlayer1} - {MatchState.ScorePlayer2}");
        }
        public void Unload()
        {
            _activeSystemList.Clear();
            _activeDrawSystemList.Clear();
            _pauseSystemList.Clear();
            _pauseDrawSystemList.Clear();

            EntityManager.Reset();
        }

        Vector2 GetRandomVectorOrientation()
        {
            var radian = _rand.Next(-45, 45) * (2 * Math.PI / 360);
            var vector = new Vector2((float)Math.Cos(radian), (float)Math.Sin(radian));
            if (_rand.NextDouble() > .5)
                vector = -vector;
            return vector;
        }

        void ResumeFromPause() => MatchState.AskForPaused = false;
        void ReturnToMainMenu() => _sceneManager.SetNewScene(new TitleScreenScene());

        readonly IList<ISystem> _activeSystemList;
        readonly IList<IDrawSystem> _activeDrawSystemList;

        readonly IList<ISystem> _pauseSystemList;
        readonly IList<IDrawSystem> _pauseDrawSystemList;

        readonly PhysicSystem _physics;
        readonly Random _rand;

        SceneManager _sceneManager;

        PlayerEntity _player1;
        PlayerEntity _player2;
        BallEntity _ball;
        WallEntity _wallTop;
        WallEntity _wallBottom;
        WallEntity _goalLeft;
        WallEntity _goalRight;
    }
}
