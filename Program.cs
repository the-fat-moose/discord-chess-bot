using Discord.WebSocket;
using Discord.Net;
using dotenv.net;
public class Program
{

    public static Task Main(string[] args) => new Program().MainAsync();

    private DiscordSocketClient _client;

    public async Task MainAsync()
    {
        _client = new DiscordSocketClient();
        _client.Log += Log;

        var token = "INSERT SECURE METHOD OF OBTAINING DISCORD TOKEN HERE";

        await _client.LoginAsync(Discord.TokenType.Bot, token);
        await _client.StartAsync();

        await Task.Delay(-1);
    }

    private Task Log(Discord.LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
}