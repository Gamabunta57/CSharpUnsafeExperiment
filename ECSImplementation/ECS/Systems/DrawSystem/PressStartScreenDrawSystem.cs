using ECSFoundation.ECS.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ECSImplementation.ECS.Systems.DrawSystem
{
    class PressStartScreenDrawSystem : IDrawSystem
    {
        public PressStartScreenDrawSystem()
        {
            _font = Global.Texture.MainFont;

            _title = "Best pong ever";
            _halfSize = _font.MeasureString(_title) / 2;
            _position = new Vector2
            {
                X = (640 / 2),
                Y = 100
            };


            _pressStart = "Press any button to start";
            _pressStartHalfSize = _font.MeasureString(_pressStart) / 2;
            _pressStartPosition = new Vector2
            {
                X = (640 / 2),
                Y = 480 - 100
            };

            _delay = .75f;
            _timer = _delay;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(_font, _title, _position, Color.White, 0, _halfSize, .8f, SpriteEffects.None, 0);

            if (_hasToDisplayPressStart) { 
                spriteBatch.DrawString(_font, _pressStart, _pressStartPosition, Color.White, 0, _pressStartHalfSize, .3f, SpriteEffects.None, 0);
            }

            spriteBatch.End();

            _timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_timer > 0)
                return;

            _timer += _delay;
            _hasToDisplayPressStart = !_hasToDisplayPressStart;
        }

        readonly string _title;
        readonly string _pressStart;
        readonly SpriteFont _font;
        readonly float _delay;

        float _timer;
        bool _hasToDisplayPressStart;
        Vector2 _position;
        Vector2 _halfSize;
        Vector2 _pressStartHalfSize;
        Vector2 _pressStartPosition;
    }
}
