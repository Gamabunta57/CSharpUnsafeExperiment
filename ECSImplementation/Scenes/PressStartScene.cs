using System.Collections.Generic;
using ECSFoundation.ECS.Entities;
using ECSFoundation.ECS.Systems;
using ECSImplementation.ECS.Systems;
using ECSImplementation.ECS.Systems.DrawSystem;
using ECSImplementation.Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECSImplementation.Scenes
{
    public class PressStartScene : IScene
    {
        public void Load(SceneManager sceneManager)
        {
            _sceneManager = sceneManager;

            _activeSystemList.Add(new DiscoverInputSystem());
            _activeDrawSystemList.Add(new PressStartScreenDrawSystem());
            ControllerState.PressAnyKeyResultType = ControllerType.Undefined;
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

            if (ControllerState.PressAnyKeyResultType != ControllerType.Undefined)
                _sceneManager.SetNewScene(new TitleScreenScene());
        }

        public void Unload()
        {
            _activeSystemList.Clear();
            _activeDrawSystemList.Clear();

            EntityManager.Reset();
        }
               
        readonly IList<ISystem> _activeSystemList = new List<ISystem>();
        readonly IList<IDrawSystem> _activeDrawSystemList = new List<IDrawSystem>();

        SceneManager _sceneManager;
    }
}
