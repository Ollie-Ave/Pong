namespace Pong;

public class GameStateService : IGameStateService
{
    public IScoringService ScoringService { get; }

	private static bool gameOver;

    public GameStateService(IScoringService scoringService)
	{
        ScoringService = scoringService;
		gameOver = false;
    }
    public void EndGame()
    {
		  gameOver = true;
    }

    public bool GameOver()
    {
		  return gameOver;
    }
}
