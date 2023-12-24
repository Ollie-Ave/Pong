using System.Collections.Generic;

namespace Pong;

public interface IScoringService
{
	void UpdateScore(string playerId, int incrementation);

	List<ScoreResult> GetScores(string id);

	string GetScoreDisplay();
}