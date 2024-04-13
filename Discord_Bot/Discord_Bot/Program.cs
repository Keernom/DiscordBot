using Discord;
using Discord.WebSocket;

namespace Discord_Bot
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            DiscordSocketConfig config = new DiscordSocketConfig();

            config.GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent;

            var discordSocket = new DiscordSocketClient(config);

            discordSocket.Log += Log;
            await discordSocket.LoginAsync(TokenType.Bot, MySecrets.BOT_TOKEN);
            await discordSocket.StartAsync();

            discordSocket.MessageReceived += MessageReceived;
            // Блокировка до закрытия программы
            await Task.Delay(-1);
        }

        private static Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private static async Task MessageReceived(SocketMessage m)
        {
            if (m.Author.IsBot) return;

            switch (m.Content)
            {
                case "ping":
                    await m.Channel.SendMessageAsync("pong");
                    break;
                default:
                    Console.WriteLine($"{m.Author.Username}: {m.Content}");
                    break;
            }
        }
    }
}