using System.Collections.Generic;

namespace Pong;

public interface ICollisionService
{
	List<CollisionData> GetCollisions(PongGame game, IEntity targetEntity);
}