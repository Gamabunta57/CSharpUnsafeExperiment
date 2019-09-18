
namespace ECSImplementation.Global
{
    public static class MatchState
    {
        public static int MatchNumber = 1;
        public static int ScorePlayer1 = 0;
        public static int ScorePlayer2 = 0;
        public static bool Player1Scored = false;
        public static bool Player2Scored = false;
        public static bool AskForPaused = false;
        public static bool AskForExit = false;
        public static bool GameOver = false;

        public static void Reset()
        {
            MatchNumber = 1;
            ScorePlayer1 = 0;
            ScorePlayer2 = 0;
            Player1Scored = false;
            Player2Scored = false;
            AskForPaused = false;
            AskForExit = false;
            GameOver = false; ;
        }
    }
}
