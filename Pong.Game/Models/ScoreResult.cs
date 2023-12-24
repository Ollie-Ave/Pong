namespace Pong;

public record ScoreResult()
{
	public required string PlayerId { get; init; }

	public int Score { get; set; }
}