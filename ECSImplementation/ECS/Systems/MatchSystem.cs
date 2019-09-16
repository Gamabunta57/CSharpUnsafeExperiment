using ECSFoundation.ECS.Systems;
using ECSImplementation.Global;
using ECSImplementation.Scenes;
using Microsoft.Xna.Framework;
using System;

namespace ECSImplementation.ECS.Systems
{
    class MatchSystem : ISystem
    {
        Scene _mainScene;
        public MatchSystem(Scene mainScene) => _mainScene = mainScene;

        public void Update(GameTime gameTime)
        {
            if (!GameState.Player1Scored && !GameState.Player2Scored)
                return;

            GameState.MatchNumber++;

            if (GameState.Player1Scored)
            {
                GameState.ScorePlayer1++;
                if (GameState.ScorePlayer1 >= 10)
                {
                    Console.WriteLine($"Player 1 win the game {GameState.ScorePlayer1} - {GameState.ScorePlayer2}");
                    Reset();
                }

            }
            else
            {
                GameState.ScorePlayer2++;
                if (GameState.ScorePlayer2 >= 10)
                {
                    Console.WriteLine($"Player 2 win the game {GameState.ScorePlayer1} - {GameState.ScorePlayer2}");
                    Reset();
                }
            }

            GameState.Player1Scored = false;
            GameState.Player2Scored = false;

            _mainScene.Reset();
        }

        void Reset()
        {
            GameState.ScorePlayer1 = 0;
            GameState.ScorePlayer2 = 0;
            GameState.MatchNumber = 1;
            GameState.Player1Scored = false;
            GameState.Player2Scored = false;
        }
    }
}
