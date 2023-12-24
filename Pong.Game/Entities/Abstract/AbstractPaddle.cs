using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Pong;

public abstract class AbstractPaddle
{
    private const int PaddleSpeed = 20;

    public required string Id { get; init; }

    public required string TextureName { get; init; }

    public Vector2 Position { get; set; }

    public Texture2D? Texture { get; set; }

	public void UpdateCallback(PongGame game, Keys upKey, Keys downKey)
	{
        ArgumentNullException.ThrowIfNull(Texture);

        var keyboard = Keyboard.GetState();

        Vector2 paddlePos = Position;

        if (keyboard.IsKeyDown(upKey) &&
                paddlePos.Y > 0)
        {
            paddlePos.Y -= PaddleSpeed;
        }
        else if (keyboard.IsKeyDown(downKey) &&
                paddlePos.Y < game.Window.ClientBounds.Height - Texture.Height)
        {
            paddlePos.Y +=  PaddleSpeed;
        }

        Position = paddlePos;
	}
}