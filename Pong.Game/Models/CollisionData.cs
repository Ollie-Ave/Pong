namespace Pong;

public record CollisionData
{
	public required IEntity Entity { get; init; }
}