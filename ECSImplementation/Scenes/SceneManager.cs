using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECSImplementation.Scenes
{
    public class SceneManager
    {

        public SceneManager(IScene startingScene)
        {
            _currentScene = startingScene;
            _sceneToLoad = _currentScene;
        }

        public void SetNewScene(IScene newScene)
        {
            _sceneToLoad = newScene;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) => _currentScene.Draw(gameTime, spriteBatch);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Load() => _currentScene.Load(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Unload() => _currentScene.Unload();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset() => _currentScene.Reset();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Update(GameTime gameTime)
        {
            _currentScene.Update(gameTime);
            if(_currentScene != _sceneToLoad)
            {
                Unload();
                _currentScene = _sceneToLoad;
                Load();
            }
        }


        IScene _sceneToLoad;
        IScene _currentScene;
    }
}
