using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluBot
{
    public class CommandsModule : ModuleBase<SocketCommandContext>
    {
        [Command("Shout")]
        public async Task ShoutCommandAsync()
        {
            await ReplyAsync("I am Delulu, your personal Delusional Bot");
        }


    }
}
