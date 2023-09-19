using System;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using Discord.Commands;
using System.Diagnostics;

//configure the bot

class DeluBot
{
    private DiscordSocketClient _client;
    private CommandService _commandService;
    private DiscordSocketConfig _config;
    private SocketGuild _guild; //discord guild information 
    private SocketRole _role; //users role information (modifiable)
    //private SocketUser _user;

    public async Task RunDelu()
    {
        _client = new DiscordSocketClient();
        _commandService = new CommandService();
        //Instantiate within runMethod

        _client.Ready += ReadyAsync; //delegate of readysync method

        await _client.LoginAsync(TokenType.Bot, "MTE1MzgxODU5NDU2MDQ0MjQyOA.GshLmN.TVo_3NhZiKunDsgLSF7vqswShv4EVxNAqo79-c"); //login to the discord bot with token OAuth2
    }

    private async Task ReadyAsync()
    {
        Console.WriteLine(($"{_client.CurrentUser.Username} is connected and ready");
        Console.WriteLine("Press any key to stop the bot...");


    }

    static async Task Main()
    {
        var bot = new DeluBot();
        await bot.RunDelu();
        Console.ReadKey();
    }



}



