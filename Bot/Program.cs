using DiscordProvider.Bot;

public class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("Input token: ");
        var token = Console.ReadLine();
        
        Console.WriteLine("Input channel Id");
        var channelId = ulong.Parse(Console.ReadLine());
        
        var bot = new BotLauncher();
        await bot.Launch(token, channelId);
    }
}


