using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong;

public interface IEntity
{
	string Id { get; init; }

	string TextureName { get; init; }

	Vector2 Position { get; set; }

	public Texture2D? Texture { get; set; }

	void UpdateCallback(PongGame game);
}