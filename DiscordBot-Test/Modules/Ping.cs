using System;
using System.Collections.Generic;
using System.Text;
using Discord.Commands;
using System.Threading.Tasks;

namespace DiscordBot_Test.Modules {
    public class Ping : ModuleBase<SocketCommandContext> {
        [Command("ping")]
        public async Task PingAsync() {
            await ReplyAsync("Pong!");
        }
    }
}
