namespace ConnectFour.Model
{
        public class State
        {
                Player[] players;
                int gameRoundsPlayed;
                bool gameOver;

                public State()
                {
                        players = new Player[]
                        {
                                new Player() {Name="Player", Points = 0 },
                                new Player() {Name="Opponent", Points = 0 }
                        };
                        gameRoundsPlayed = 0;
                        gameOver = false;
                }
                public void ResetGame()
                {
                        gameOver = false;
                        players[0].Points = 0;
                        players[1].Points = 0;
                }
                public void EndGame()
                {
                        gameOver = true;
                        gameRoundsPlayed++;
                        // Award winner..
                }
        }
}
