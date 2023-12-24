using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong;

public class Ball : IEntity
{
	private float BallSpeed = 10;

    private readonly ICollisionService collisionService;

    private readonly IScoringService scoringService;

    private readonly IGameStateService gameStateService;

	private static DateTime creationTime = DateTime.Now;

    public Ball(ICollisionService collisionService, IScoringService scoringService, IGameStateService gameStateService)
	{
        this.collisionService = collisionService;
        this.scoringService = scoringService;
        this.gameStateService = gameStateService;
    }

    public required string Id { get; init; }

	public required string TextureName { get; init; }

	public required Vector2 Position { get; set; }

	public Texture2D? Texture { get; set; }

	private Vector2 direction = new(1, 1);

	public void UpdateCallback(PongGame game)
	{
		ArgumentNullException.ThrowIfNull(Texture);

		if (DateTime.Now - creationTime < TimeSpan.FromSeconds(3))
		{
			return;
		}

		Position += new Vector2(BallSpeed * direction.X, BallSpeed * direction.Y);

		var collisions = collisionService.GetCollisions(game, this);

		var player = collisions.SingleOrDefault(x => x.Entity.Id.Contains(string.Format(EntityIdentifiers.PlayerTemplate, string.Empty)));

		if (player is not null)
		{
			direction.X *= -1;
			direction.Y *= -1;

			scoringService.UpdateScore(player.Entity.Id, 1);

			BallSpeed += 0.2f;
		}

		if (Position.Y < 0 || Position.Y > game.Window.ClientBounds.Height - Texture.Height)
		{
			direction.Y *= -1;
		}

		if (Position.X < 0 || Position.X > game.Window.ClientBounds.Width - Texture.Width)
		{
			gameStateService.EndGame();
		}
	}
}