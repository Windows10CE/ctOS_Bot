using System;
using System.Collections.Generic;
using System.Text;
using Discord.Commands;
using System.Threading.Tasks;
using Discord;

namespace DiscordBot_Test.Modules {
    public class Help : ModuleBase<SocketCommandContext> {
        [Command("help")]
        public async Task HelpAsync() {
            EmbedBuilder builder = new EmbedBuilder();

            builder
                .AddInlineField("Help", "This Help Message")
                .AddInlineField("Ping", "Pong!")
                .AddField("Profile", "ctOS_Registration Integration, WIP.")
                .AddInlineField("Profile show [name]", "Shows the profile with that name.")
                .AddInlineField("Profile create", "Type \"profile create\" to see the syntax.")
                .WithColor(Color.Blue);

            await ReplyAsync("", false, builder.Build());
        }
    }
}
