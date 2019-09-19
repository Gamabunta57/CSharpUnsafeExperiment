using ECSFoundation.ECS.Entities;
using ECSFoundation.ECS.Systems;
using ECSImplementation.ECS.Systems;
using ECSImplementation.ECS.Systems.DrawSystem;
using ECSImplementation.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ECSImplementation.Scenes
{
    public class GameOverScene : IScene
    {

        public GameOverScene(uint scorePlayer1, uint scorePlayer2)
        {
            _scoreP1 = scorePlayer1;
            _scoreP2 = scorePlayer2;
        }
        public void Load(SceneManager sceneManager)
        {
            _sceneManager = sceneManager;

            var menu = new Menu(new Tuple<string, Action>[] {
                new Tuple<string, Action>("Play again", PlayAgainMenuItem),
                new Tuple<string, Action>("Return to main menu", ReturnToMainMenu)
            }, new Vector2(640 / 2, 480 / 2));

            _activeSystemList.Add(new MenuInputSystem(menu));

            _activeDrawSystemList.Add(new GameOverSceneDrawSystem(_scoreP1, _scoreP2));
            _activeDrawSystemList.Add(new MenuDrawSystem(menu));
        }

        public void Reset() { }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var system in _activeDrawSystemList)
                system.Draw(gameTime, spriteBatch);
        }
        public void Update(GameTime gameTime)
        {
            foreach (var system in _activeSystemList)
                system.Update(gameTime);
        }

        public void Unload()
        {
            _activeSystemList.Clear();
            _activeDrawSystemList.Clear();

            EntityManager.Reset();
        }

        void PlayAgainMenuItem() => _sceneManager.SetNewScene(new Scene());
        void ReturnToMainMenu() => _sceneManager.SetNewScene(new TitleScreenScene());


        readonly IList<ISystem> _activeSystemList = new List<ISystem>();
        readonly IList<IDrawSystem> _activeDrawSystemList = new List<IDrawSystem>();

        uint _scoreP1;
        uint _scoreP2;
        SceneManager _sceneManager;
    }
}
