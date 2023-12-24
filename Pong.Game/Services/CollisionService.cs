using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Pong;

public class CollisionService : ICollisionService
{
	private static DateTime lastCollision = DateTime.Now;

	public List<CollisionData> GetCollisions(PongGame game, IEntity targetEntity)
	{
		ArgumentNullException.ThrowIfNull(targetEntity.Texture);

		if (DateTime.Now - lastCollision < TimeSpan.FromMilliseconds(100))
		{
			return [];
		}

		List<CollisionData> collisions = [];

		foreach (var entity in game.Entities)
		{
			if (entity.Texture is null)
			{
				continue;
			}
			else if (entity.Id == targetEntity.Id)
			{
				continue;
			}

			Vector2 entityTopLeft = new(entity.Position.X, entity.Position.Y);
			Vector2 entityTopRight = new(entity.Position.X + entity.Texture.Width, entity.Position.Y);

			Vector2 entityBottomLeft = new(entity.Position.X, entity.Position.Y + entity.Texture.Height);
			Vector2 entityBottomRight = new(entity.Position.X + entity.Texture.Width, entity.Position.Y + entity.Texture.Height);

			Vector2 targetEntityTopLeft = new(targetEntity.Position.X, targetEntity.Position.Y);
			Vector2 targetEntityTopRight = new(targetEntity.Position.X + targetEntity.Texture.Width, targetEntity.Position.Y);

			Vector2 targetEntityBottomLeft = new(targetEntity.Position.X, targetEntity.Position.Y + targetEntity.Texture.Height);
			Vector2 targetEntityBottomRight = new(targetEntity.Position.X + targetEntity.Texture.Width, targetEntity.Position.Y + targetEntity.Texture.Height);

			if (targetEntityTopLeft.X < entityTopRight.X)
			{
				// Ball is to the left of the entity

				if (targetEntityBottomLeft.Y < entityTopLeft.Y)
				{
					// Ball is above the entity

					if (targetEntityBottomRight.X > entityTopLeft.X &&
						targetEntityBottomRight.Y > entityTopLeft.Y)
					{
						collisions.Add(new CollisionData()
						{
							Entity = entity,
						});
					}
				}
				else
				{
					// Ball is below the entity

					if (targetEntityTopRight.X > entityTopLeft.X &&
						targetEntityTopRight.Y < entityBottomLeft.Y)
					{
						collisions.Add(new CollisionData()
						{
							Entity = entity,
						});
					}
				}
			}
			else
			{
				// Ball is to the right of the entity

				if (targetEntityBottomRight.Y < entityTopRight.Y)
				{
					// Ball is above the entity

					if (targetEntityBottomRight.X < entityTopLeft.X &&
						targetEntityBottomRight.Y > entityTopLeft.Y)
					{
						Console.WriteLine($"Collision detected: ball X: {targetEntity.Position.X} Y: {targetEntity.Position.Y} entity X: {entity.Position.X} Y: {entity.Position.Y}");

						collisions.Add(new CollisionData()
						{
							Entity = entity,
						});
					}
				}
				else
				{
					// Ball is below the entity

					if (targetEntityTopRight.X < entityTopLeft.X &&
						targetEntityTopRight.Y < entityBottomLeft.Y)
					{
						Console.WriteLine($"Collision detected: ball X: {targetEntity.Position.X} Y: {targetEntity.Position.Y} entity X: {entity.Position.X} Y: {entity.Position.Y}");

						collisions.Add(new CollisionData()
						{
							Entity = entity,
						});
					}
				}
			}

			if (entity.Texture is not null &&
				entity.Position.X > targetEntity.Position.X &&
				entity.Position.X + entity.Texture.Width < targetEntity.Position.X + targetEntity.Texture.Width)// &&
				// entity.Position.Y > targetEntity.Position.Y &&
				// entity.Position.Y + entity.Texture.Height < targetEntity.Position.Y + targetEntity.Texture.Height)
			{
				Console.WriteLine($"Collision between {targetEntity.Id} and {entity.Id}");
			}
		}

		return collisions;
	}
}