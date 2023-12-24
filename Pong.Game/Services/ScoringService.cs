using System.Collections.Generic;
using System.Linq;

namespace Pong;

public class ScoringService : IScoringService
{
	private static readonly List<ScoreResult> scores = new()
	{
		new ScoreResult()
		{
			PlayerId = string.Format(EntityIdentifiers.PlayerTemplate, 1),
			Score = 0,
		},
		new ScoreResult()
		{
			PlayerId = string.Format(EntityIdentifiers.PlayerTemplate, 2),
			Score = 0,
		},
	};

    public string GetScoreDisplay()
    {
		  return string.Join(" - ", scores.Select(x => x.Score));
    }

    public List<ScoreResult> GetScores(string id)
	{
		return scores;
	}

	public void UpdateScore(string playerId, int incrementation)
	{
		ScoreResult? player = scores.Single(x => x.PlayerId == playerId);

		player.Score += incrementation;
	}
}