using ECSFoundation.ECS.Systems;
using ECSImplementation.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECSImplementation.ECS.Systems.DrawSystem
{
    class PauseMenuDrawSystem : IDrawSystem
    {
        public PauseMenuDrawSystem(IMenu menu)
        {
            _menu = menu;
            _font = Global.Texture.MainFont;
            _bgTexture = Global.Texture.MainTexture;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var refPosition = _menu.Position;
            var position = refPosition;
            var selection = _menu.CurrentSelected;

            spriteBatch.Begin();
            spriteBatch.Draw(_bgTexture, new Rectangle(0, 0, 640, 480), new Color(0,0,0,(int)(255*.8)));

            for(var i = 0; i < _menu.Items.Length; i++)
            {
                ref var item = ref _menu.Items[i];
                var size = _font.MeasureString(item.Item1);

                position.Y = refPosition.Y + (size.Y * i);

                spriteBatch.DrawString(_font, item.Item1, position, selection == i ? Color.Yellow : Color.White, 0, size / 2, .6f, SpriteEffects.None, 0);
            }
            spriteBatch.End();
        }

        readonly IMenu _menu;
        readonly SpriteFont _font;
        readonly Texture2D _bgTexture;
    }
}
