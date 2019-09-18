using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECSFoundation.ECS.Systems
{
    public interface IDrawSystem
    {
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
