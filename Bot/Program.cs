using System.Net.Sockets;
using Bot;
using Connection;
using Discord;
using Discord.WebSocket;

// using var tcpClient = new TcpClient();
// tcpClient.Connect("127.0.0.1", 8888);
//
// var message = "Hello world";

using var provider = new Provider();
provider.OnMessage += HandleMessage;
await provider.StartAsync(8888);


static void HandleMessage(string message)
{
    Console.WriteLine(message);
}

// var token = args[0];
// var client = new DiscordSocketClient();
// var logger = new Logger();
//
// client.Log += logger.Log;
//
// await client.LoginAsync(TokenType.Bot, token);
// await client.StartAsync();
//
// await Task.Delay(-1);


