using System;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using Discord.Commands;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;
using DeluBot;
using System.Net;
using Microsoft.Extensions.DependencyInjection;

//configure the bot

class DeluluBot
{
    private DiscordSocketClient _client;
    private CommandService _commandService;
    private DiscordSocketConfig _config;
    private SocketGuild _guild; //discord guild information 
    private SocketRole _role; //users role information (modifiable)

    private IServiceProvider _serviceProvider;

    //private SocketUser _user;

    //structure of Json config 
    private class BotConfig
    {
        public string? BotToken { get; set; }
    }

    public async Task RunDelu()
    {
        _config = new DiscordSocketConfig();
        _config = configurationBuilder(_config);

        _client = new DiscordSocketClient(_config);
        

        _commandService = new CommandService();
        //Instantiate within runMethod

        _client.Ready += ReadyAsync; //delegate of readysync method register commandModule

        //await _commandService.AddModuleAsync<MyModule>(_serviceProvider); 

        var botconfig = LoadBotConfig("botToken.json");

        //Json file is not null here, the path directory messes up with github
        if (botconfig != null) 
        {
            string hashedBotToken = DeluBot.Hashing.ToSHA256(botconfig.BotToken);

            await _client.LoginAsync(TokenType.Bot, botconfig.BotToken); //login to the discord bot with token OAuth2
            await _client.StartAsync();
        }
    }

    public DiscordSocketConfig configurationBuilder(DiscordSocketConfig settings)
    {
            settings.AlwaysDownloadUsers = true;
            settings.LargeThreshold = 250;

            return settings;
    }

    private async Task ReadyAsync()
    {
        Console.WriteLine($"{_client.CurrentUser.Username} is connected and ready");
        Console.WriteLine("Press Q key to stop the bot...");
    }
    

    static async Task Main()
    {

        var bot = new DeluluBot();

        await bot.RunDelu();

        ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
        if (keyInfo.Key != ConsoleKey.Q)
        {
            Console.WriteLine("Bot is stopping...");
            await bot._client.StopAsync();
            Console.ReadKey();
        }

        Console.WriteLine("Bot Collapsed");
        Console.ReadKey();
    }

    private BotConfig LoadBotConfig(string configFile)
    {
        try
        {
            string Json = File.ReadAllText(configFile);
            return JsonConvert.DeserializeObject<BotConfig>(Json);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Config file '{configFile}' not found");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error Loading bot configuration: {ex.Message}");
        }

        return null;
    }
}
