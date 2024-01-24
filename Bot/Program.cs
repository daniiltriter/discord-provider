using DiscordProvider.Bot;

public class Program
{
    public static async Task Main(string[] args)
    {
        var bot = new BotLauncher();
        await bot.Launch(args[0], ulong.Parse(args[1]));
    }
}


