using System;
using System.Collections.Generic;
using System.Text;
using Discord.Commands;
using System.Threading.Tasks;

namespace DiscordBot_Test.Modules {
    public class Help : ModuleBase<SocketCommandContext> {
        [Command("help")]
        public async Task HelpAsync() {
            await ReplyAsync("**Help** - This Help Message\n**Ping** - Pong!");
        }
    }
}
