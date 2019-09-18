using ECSFoundation.ECS.Systems;
using ECSImplementation.Menus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECSImplementation.ECS.Systems.DrawSystem
{
    class MenuDrawSystem : IDrawSystem
    {
        public MenuDrawSystem(IMenu menu)
        {
            _menu = menu;
            _font = Global.Texture.MainFont;
            _bgTexture = Global.Texture.MainTexture;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var refPosition = new Vector2(0, 480 - 100);
            var position = refPosition;
            var selection = _menu.CurrentSelected;
            spriteBatch.Begin();
            for(var i = 0; i < _menu.Items.Length; i++)
            {
                ref var item = ref _menu.Items[i];
                var size = _font.MeasureString(item.Item1);
                position.X = (640 - size.X) / 2;
                position.Y = refPosition.Y + (size.Y * i);

                spriteBatch.DrawString(_font, item.Item1, position, selection == i ? Color.Yellow : Color.White);
            }
            spriteBatch.End();
        }

        readonly IMenu _menu;
        readonly SpriteFont _font;
        readonly Texture2D _bgTexture;
    }
}
