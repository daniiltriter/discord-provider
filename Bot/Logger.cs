using Discord;

namespace DiscordProvider.Bot;

public class Logger
{
    public Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
}