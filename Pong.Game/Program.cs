using System;
using Microsoft.Extensions.DependencyInjection;
using Pong;

internal class Program
{
    private static void Main(string[] args)
    {
        IServiceCollection services = new ServiceCollection();

		services.AddSingleton<PongGame>();
		services.AddSingleton<ICollisionService, CollisionService>();
        services.AddSingleton<IScoringService, ScoringService>();
        services.AddSingleton<IGameStateService, GameStateService>();

		IServiceProvider serviceProvider = services.BuildServiceProvider();

        using var game = serviceProvider.GetRequiredService<PongGame>();

        game.Run();
    }
}