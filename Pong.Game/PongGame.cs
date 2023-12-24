using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong;

public class PongGame : Game
{
    private IServiceProvider serviceProvider;

    private readonly IGameStateService gameStateService;

    private GraphicsDeviceManager graphics;

    private SpriteBatch? spriteBatch;

    public List<IEntity> Entities = [];

    private SpriteFont? gameFont;

    public PongGame(IServiceProvider serviceProvider, IGameStateService gameStateService)
    {
        this.serviceProvider = serviceProvider;
        this.gameStateService = gameStateService;

        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Window.AllowUserResizing = false;
        Window.Title = "Pong";
    }

    protected override void Initialize()
    {
        InitializeGraphics();

        Entities =
        [
            new Paddle1()
            {
                Id = string.Format(EntityIdentifiers.PlayerTemplate, 1),
                Position = new(15, 10),
                TextureName = "paddle",
            },
            new Paddle2()
            {
                Id = string.Format(EntityIdentifiers.PlayerTemplate, 2),
                Position = new( Window.ClientBounds.Width -45, 10),
                TextureName = "paddle",
            },
            new Ball(serviceProvider.GetRequiredService<ICollisionService>(),
                     serviceProvider.GetRequiredService<IScoringService>(),
                     serviceProvider.GetRequiredService<IGameStateService>())
            {
                Id = EntityIdentifiers.Ball,
                Position = new( Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2),
                TextureName = "ball",
            }
        ];

        base.Initialize();
    }


    private void InitializeGraphics()
    {
        graphics.IsFullScreen = false;
        graphics.PreferredBackBufferWidth = 1280;
        graphics.PreferredBackBufferHeight = 720;

        graphics.ApplyChanges();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);

        foreach (var entity in Entities)
        {
            entity.Texture = Content.Load<Texture2D>(entity.TextureName);
        }

        gameFont = Content.Load<SpriteFont>("gamefont");
    }

    protected override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

		if (!gameStateService.GameOver())
        {
            foreach (var entity in Entities)
            {
                entity.UpdateCallback(this);
            }
        }
    }

    protected override void Draw(GameTime gameTime)
    {
        ArgumentNullException.ThrowIfNull(spriteBatch);
        ArgumentNullException.ThrowIfNull(gameFont);

        GraphicsDevice.Clear(Color.Black);

        spriteBatch.Begin();

		if (gameStateService.GameOver())
        {
            string gameOverMessage = "Game Over.";

            Vector2 gameOverTextPosition = new(GetTextHorizontalCenter(gameOverMessage), 100);
            spriteBatch.DrawString(gameFont, gameOverMessage, gameOverTextPosition , Color.White);

            string scoreMessage = gameStateService.ScoringService.GetScoreDisplay();

            Vector2 scorePosition = new(GetTextHorizontalCenter(scoreMessage), 200);
            spriteBatch.DrawString(gameFont, scoreMessage , scorePosition  , Color.White);
        }
        else
        {
            string scoreMessage = gameStateService.ScoringService.GetScoreDisplay();
            Vector2 scorePosition = new(GetTextHorizontalCenter(scoreMessage), 20);

            spriteBatch.DrawString(gameFont, scoreMessage, scorePosition, Color.White);

            foreach (var entity in Entities)
            {
                if (entity.Texture is not null)
                {
                    spriteBatch.Draw(entity.Texture, entity.Position, Color.White);
                }
            }
        }

        spriteBatch.End();

        base.Draw(gameTime);
    }

    private float GetTextHorizontalCenter(string scoreMessage)
    {
        ArgumentNullException.ThrowIfNull(gameFont);

        return (Window.ClientBounds.Width / 2) - (gameFont.MeasureString(scoreMessage).X / 2);
    }
}
