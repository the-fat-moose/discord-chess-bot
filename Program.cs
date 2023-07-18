using Discord.WebSocket;
using Discord.Net;
public class Program
{

    public static Task Main(string[] args) => new Program().MainAsync();

    private DiscordSocketClient _client;

    public async Task MainAsync()
    {
        _client = new DiscordSocketClient();
        _client.Log += Log;

        var token = File.ReadAllText("token.txt");

        await _client.LoginAsync(Discord.TokenType.Bot, token);
        await _client.StartAsync();

        // Keeps bot online until forcefully stopped
        await Task.Delay(-1);
    }

    private Task Log(Discord.LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
}