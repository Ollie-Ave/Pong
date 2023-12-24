namespace Pong;

public interface IGameStateService
{

    IScoringService ScoringService { get; }

	void EndGame();

	bool GameOver();
}