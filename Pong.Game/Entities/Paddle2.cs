using Microsoft.Xna.Framework.Input;

namespace Pong;

public class Paddle2 : AbstractPaddle, IEntity
{
    public void UpdateCallback(PongGame game)
    {
        UpdateCallback(game, Keys.Up, Keys.Down);
    }
}