using Microsoft.Xna.Framework.Input;

namespace Pong;

public class Paddle1 : AbstractPaddle, IEntity
{
    public void UpdateCallback(PongGame game)
    {
        UpdateCallback(game, Keys.W, Keys.S);
    }
}