using System;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace DiscordBot_Test
{
    public class Program
    {
        static void Main(string[] args)
        // Start the Async method
        => new Program().MainAsync().GetAwaiter().GetResult();

        private CommandHandler _handler;

        DiscordSocketClient client;

        public async Task MainAsync() {
            client = new DiscordSocketClient();

            // Log events
            client.Log += Log;

            Console.WriteLine("Press Any Key To Exit");

            // Connecting to Discord
            string tokenPath = @"token";
            string token = File.ReadAllText(tokenPath);
            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            // Starting the CommandHandler
            _handler = new CommandHandler(client);

            // Shutdown stuff
            char shutdown;
            shutdown = Console.ReadKey().KeyChar;
            Console.WriteLine("");
            #pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
            while (shutdown != null) {
            #pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
                await Shutdown();
            }

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg) {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        public async Task Shutdown() {
            await client.StopAsync();
            Console.WriteLine("Goodbye");
            Thread.Sleep(2500);
            Environment.Exit(0);
        }
    }
}