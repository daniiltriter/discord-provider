using DiscordProvider.Connection;
using Discord;
using Discord.WebSocket;

namespace DiscordProvider.Bot;

public class BotLauncher
{
    private ulong _channelId;
    
    private readonly Provider _provider = new();
    private DiscordSocketClient _discordClient;
    private readonly Logger _logger = new();
    
    public async Task Launch(string token, ulong channelId)
    {
        var configurations = new DiscordSocketConfig();
        configurations.GatewayIntents = GatewayIntents.All;

        _discordClient = new DiscordSocketClient(configurations);
        
        _channelId = channelId;
        _discordClient.Log += _logger.Log;

        _provider.OnMessage += HandleMessage;

        await _discordClient.LoginAsync(TokenType.Bot, token);
        await _discordClient.StartAsync();
        Task.Run(async () => await _provider.StartAsync(8888));
        
        await Task.Delay(-1);
    }

    private async void HandleMessage(string message)
    {
        var groups = _discordClient.Guilds;
        var channel = _discordClient.GetChannel(_channelId) as ITextChannel;
        await channel?.SendMessageAsync(message);
    }
}