using ECSFoundation.ECS.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECSImplementation.ECS.Systems.DrawSystem
{
    public class TitleScreenDrawSystem : IDrawSystem
    {
        public TitleScreenDrawSystem()
        {
            _sentence = "Best pong ever";
            _font = Global.Texture.MainFont;
            _halfSize = _font.MeasureString(_sentence) / 2;
            _position = new Vector2
            {
                X = (640 / 2),
                Y = 100
            };
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(_font, _sentence, _position, Color.White, 0, _halfSize, .8f, SpriteEffects.None, 0);
            spriteBatch.End();
        }

        readonly string _sentence;
        readonly SpriteFont _font;
        Vector2 _position;
        Vector2 _halfSize;

    }
}
