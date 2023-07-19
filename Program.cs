using Discord;
using Discord.Net;
using Discord.WebSocket;
using Newtonsoft.Json;

public class Program
{

    public static Task Main(string[] args) => new Program().MainAsync();

    private DiscordSocketClient _client;

    public async Task MainAsync()
    {
        _client = new DiscordSocketClient();
        _client.Log += Log;
        _client.Ready += Client_Ready;

        var token = File.ReadAllText("token.txt");

        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

        // Keeps bot online until forcefully stopped
        await Task.Delay(-1);
    }

    private Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }

    public async Task Client_Ready()
    {
        ulong guildID = Convert.ToUInt64(File.ReadAllText("guildID.txt"));

        // Let's build a guild command! We're going to need a guild so lets just put that in a variable.
        var guild = _client.GetGuild(guildID);

        // Next, lets create our slash command builder. This is like the embed builder but for slash commands.
        var guildCommand = new SlashCommandBuilder();

        // Note: Names have to be all lowercase and match the regular expression ^[\w-]{3,32}$

        // Let's do our global command
        var globalCommand = new SlashCommandBuilder();
        globalCommand.WithName("first-global-command");
        globalCommand.WithDescription("This is my first global slash command");

        try
        {
            
        }
        catch(ApplicationCommandException exception)
        {
            // If our command was invalid, we should catch an ApplicationCommandException. 
            // This exception contains the path of the error as well as the error message. 
            // You can serialize the Error field in the exception to get a visual of where your error is.
            var json = JsonConvert.SerializeObject(exception.Errors, Formatting.Indented);

            // You can send this error somewhere or just print it to the console, for this example we're just going to print it.
            Console.WriteLine(json);
        }
    }
}