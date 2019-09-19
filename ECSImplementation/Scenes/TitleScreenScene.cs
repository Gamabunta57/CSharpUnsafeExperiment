using System;
using System.Collections.Generic;
using ECSFoundation.ECS.Entities;
using ECSFoundation.ECS.Systems;
using ECSImplementation.ECS.Systems;
using ECSImplementation.ECS.Systems.DrawSystem;
using ECSImplementation.Global;
using ECSImplementation.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECSImplementation.Scenes
{
    public class TitleScreenScene : IScene
    {

        public void Load(SceneManager sceneManager)
        {
            _sceneManager = sceneManager;

            var menu = new Menu(new Tuple<string, Action>[] {
                new Tuple<string, Action>("Play game", StartGameMenuItem),
                new Tuple<string, Action>("Quit", QuitGameMenuItem)
            }, new Vector2(640 / 2, 480 / 2));

            _activeSystemList.Add(new MenuInputSystem(menu));

            _activeDrawSystemList.Add(new TitleScreenDrawSystem());
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

        void StartGameMenuItem() => _sceneManager.SetNewScene(new Scene());
        void QuitGameMenuItem()
        {
            Console.WriteLine("Ask for return to main menu");
            MatchState.AskForExit = true;
        }


        readonly IList<ISystem> _activeSystemList = new List<ISystem>();
        readonly IList<IDrawSystem> _activeDrawSystemList = new List<IDrawSystem>();

        SceneManager _sceneManager;
    }
}
