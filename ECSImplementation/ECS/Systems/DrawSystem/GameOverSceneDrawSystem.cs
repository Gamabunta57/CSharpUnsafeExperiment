using ECSFoundation.ECS.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECSImplementation.ECS.Systems.DrawSystem
{
    public class GameOverSceneDrawSystem : IDrawSystem
    {
        public GameOverSceneDrawSystem(uint scoreP1, uint scoreP2)
        {
            _winSentence = scoreP1 > scoreP2 ? "Player 1 wins" : "Player 2 wins";
            _scoreP1 = scoreP1;
            _scoreP2 = scoreP2;
            _font = Global.Texture.MainFont;
            _position = new Vector2
            {
                X = (640 - _font.MeasureString(_winSentence).X) / 2,
                Y = 100
            };
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(_font, _winSentence, _position, Color.MediumPurple);
            spriteBatch.End();
        }

        uint _scoreP1;
        uint _scoreP2;

        string _winSentence;
        SpriteFont _font;
        Vector2 _position;

    }
}
