using Discord.Commands;
using Discord.WebSocket;
using System.Reflection;
using System.Threading.Tasks;

namespace DiscordBot_Test {
    public class CommandHandler
    {
        private DiscordSocketClient _client;

        private CommandService _service;

        public CommandHandler(DiscordSocketClient client) {
            _client = client;

            _service = new CommandService();

            // Get command infomation
            _service.AddModulesAsync(Assembly.GetEntryAssembly());

            // Send message info to HandleCommandAsync for processing
            _client.MessageReceived += HandleCommandAsync;
        }

        private async Task HandleCommandAsync(SocketMessage s) {
            var msg = s as SocketUserMessage;
            if (msg == null) return;

            // Define the prefix
            const char prefix = '!';

            var context = new SocketCommandContext(_client, msg);

            int argPos = 0;
            // Check for the prefix and execute the command
            if (msg.HasCharPrefix(prefix, ref argPos)) {
                var result = await _service.ExecuteAsync(context, argPos);
                
                // Output any errors into the Discord chat
                if(!result.IsSuccess && result.Error != CommandError.UnknownCommand) {
                    await context.Channel.SendMessageAsync(result.ErrorReason);
                }
            }
        }
    }
}