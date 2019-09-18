using ECSFoundation.ECS.Systems;
using ECSImplementation.Global;
using ECSImplementation.Scenes;
using Microsoft.Xna.Framework;
using System;

namespace ECSImplementation.ECS.Systems
{
    public class MatchSystem : ISystem
    {

        public const uint Score = 3;
        public MatchSystem(SceneManager sceneManager) => _sceneManager = sceneManager;

        public void Update(GameTime gameTime)
        {
            if (!MatchState.Player1Scored && !MatchState.Player2Scored)
                return;

            MatchState.MatchNumber++;

            if (MatchState.Player1Scored)
            {
                MatchState.ScorePlayer1++;
                if (MatchState.ScorePlayer1 >= Score)
                {
                    Console.WriteLine($"Player 1 win the game {MatchState.ScorePlayer1} - {MatchState.ScorePlayer2}");
                    _sceneManager.SetNewScene(new GameOverScene((uint)MatchState.ScorePlayer1, (uint)MatchState.ScorePlayer2));
                    MatchState.Reset();
                }

            }
            else
            {
                MatchState.ScorePlayer2++;
                if (MatchState.ScorePlayer2 >= Score)
                {
                    Console.WriteLine($"Player 2 win the game {MatchState.ScorePlayer1} - {MatchState.ScorePlayer2}");
                    _sceneManager.SetNewScene(new GameOverScene((uint)MatchState.ScorePlayer1, (uint)MatchState.ScorePlayer2));
                    MatchState.Reset();
                }
            }

            MatchState.Player1Scored = false;
            MatchState.Player2Scored = false;

            _sceneManager.Reset();
        }

        SceneManager _sceneManager;
    }
}
