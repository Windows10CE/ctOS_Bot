using Discord.Commands;
using Discord;
using System.Threading.Tasks;

namespace DiscordBot_Test.Modules {

    public class BotCommands : ModuleBase<SocketCommandContext> {

        [Command("help")]
        public async Task Help() {
            await Context.Channel.SendMessageAsync("**Help** - This Help Prompt\n**Ping** - Pings the bot\n**Repeat <Text>** - Repeats any text put into the command\n**whoIs <@mention>** - Outputs the username and discrim of whoever you mention");
        }

        [Command("ping")]
        public async Task Ping() {
            await Context.Channel.SendMessageAsync("Pong!");
        }

        [Command("repeat")]
        public async Task Repeat(string repeat) {
            await Context.Channel.SendMessageAsync(repeat);
        }

        [Command("whoIs")]
        public async Task WhoIs(IGuildUser user) {
            await Context.Channel.SendMessageAsync("That person's username and discriminator are: " + user.ToString());
        }

        [Command("profile")]
        public async Task Profile(string name) {
            await ReplyAsync("WIP");
        }

        [Command("import")]
        public async Task Import(EmbedField file) {
            if(file.Name == @"*.json") {
                await ReplyAsync("Testing, file name is " + file.Name);
            }else {
                await ReplyAsync("File is not .json");
            }
        }
    }
}
