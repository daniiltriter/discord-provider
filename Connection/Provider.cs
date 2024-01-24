using System.Net.Sockets;
using System.Text;

namespace DiscordProvider.Connection;

public delegate void MessageHandler(string message);

public class Provider : IDisposable, IAsyncDisposable
{
    private TcpListener _tcpListener;
    private readonly List<TcpClient> _clients = new();
    public event MessageHandler OnMessage;

    public async Task StartAsync(int port)
    {
        _tcpListener = new TcpListener(port);
        _tcpListener.Start();
        Console.WriteLine("Server started!");
        while (true)
        {
            var client = await _tcpListener.AcceptTcpClientAsync();
            _clients.Add(client);
            Task.Run(async () => await ProcessClientAsync(client));
        }
    }

    private async Task ProcessClientAsync(TcpClient client)
    {
        var buffer = new byte[1024];
        await using var stream = client.GetStream();

        while (true)
        {
            var bytes = await stream.ReadAsync(buffer);
            if (bytes == '\n') continue;

            if (bytes == 0)
            {
                _clients.Remove(client);
                break;
            }
            
            if (bytes > buffer.Length)
            {
                Console.WriteLine("Stream bytes overload");
            }
            
            var content = Encoding.UTF8.GetString(buffer);

            var isEmpty = string.IsNullOrWhiteSpace(content);
            if (!isEmpty)
            {
                OnMessage.Invoke(content);
            }
        }
    }
    
    public ValueTask DisposeAsync()
    {
        _tcpListener.Stop();
        _clients.Clear();
        
        return ValueTask.CompletedTask;
    }

    public void Dispose()
    {
        _tcpListener.Stop();
        _clients.Clear();
    }
}