using System.Collections.Generic;
using ECSFoundation.ECS.Entities;
using ECSFoundation.ECS.Systems;
using ECSImplementation.ECS.Component;
using ECSImplementation.Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECSImplementation.ECS.Systems.DrawSystem
{

    public interface IDrawable
    {
        ref Position Position { get; }
        ref SpriteInfo SpriteInfo { get; }
    }

    public class MainSceneDrawSystem : IDrawSystem
    {
        public MainSceneDrawSystem() {
            EntityManager.OnNewEntityCreated += OnNewEntityCreated;
            _mainTexture = Global.Texture.MainTexture;
            _mainFont = Global.Texture.MainFont;
            var stringSize = _mainFont.MeasureString("0 0");

            _scorePosition.X = 320 - stringSize.X / 2;
            _scorePosition.Y = 480 / 8 - stringSize.Y / 2;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(_mainFont, $"{GameState.ScorePlayer1} {GameState.ScorePlayer2}", _scorePosition, Color.White);

            var rectangle = new Rectangle();
            for(var i = 0; i < _entityList.Count; i++)
            {
                ref var pos = ref _entityList[i].Position;
                ref var sprite = ref _entityList[i].SpriteInfo;

                rectangle.X = (int)pos.Value.X;
                rectangle.Y = (int)pos.Value.Y;
                rectangle.Width = (int)sprite.Size.X;
                rectangle.Height = (int)sprite.Size.Y;

                spriteBatch.Draw(_mainTexture, rectangle, sprite.Color);
            }
            spriteBatch.End();
        }

        void OnNewEntityCreated(IEntity entity)
        {
            if (entity is IDrawable o)
                _entityList.Add(o);
        }


        readonly IList<IDrawable> _entityList = new List<IDrawable>();
        readonly Texture2D _mainTexture;
        readonly SpriteFont _mainFont;
        readonly Vector2 _scorePosition;
    }
}
