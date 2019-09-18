using ECSFoundation.ECS.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECSImplementation.ECS.Systems.DrawSystem
{
    public class TitleScreenDrawSystem : IDrawSystem
    {
        public TitleScreenDrawSystem()
        {
            _sentence = "Best pong ever";
            _font = Global.Texture.MainFont;
            _position = new Vector2
            {
                X = (640 - _font.MeasureString(_sentence).X) / 2,
                Y = 100
            };
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(_font, _sentence, _position, Color.White);
            spriteBatch.End();
        }

        readonly string _sentence;
        readonly SpriteFont _font;
        Vector2 _position;

    }
}
