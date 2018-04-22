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
                .AddInlineField("Profile import", "Requires a JSON file from ctOS_Registration attached to the message.")
                .WithColor(Color.Blue);

            await ReplyAsync("", false, builder.Build());
        }
    }
}
