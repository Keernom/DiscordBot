using Discord;
using Discord.WebSocket;
using Discord_Bot.Entites;
using Newtonsoft.Json;
using RestSharp;

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

            var answer = await GetAnswer(m.Content);
            await Console.Out.WriteLineAsync(answer);
            var a = JsonConvert.DeserializeObject<YaGPTResult>(answer);
            string answerText = a.Result.Alternatives[0].Message.text;
            
            await m.Channel.SendMessageAsync(answerText);
        }

        public static async Task<string> GetAnswer(string userMessage)
        {
            var opts = new CompletionOptions()
            {
                stream = false,
                temperature = 1f,
                maxTokens = 150
            };

            List<Message> messages = new List<Message>()
            {
                //new Message()
                //{
                //    role = "system",
                //    text = "Ты финансовый консультант"
                //},
                new Message()
                {
                    role = "user",
                    text = userMessage
                }
            };

            string modelUri = $"gpt://{MySecrets.FOLDER_ID}/yandexgpt";

            var body = new RequestBody() 
            { 
                modelUri = modelUri,
                completionOptions = opts,
                messages = messages
            };

            string jsonBody = JsonConvert.SerializeObject(body);

            var client = new RestClient("https://llm.api.cloud.yandex.net/foundationModels/v1/completion");
            var request = new RestSharp.RestRequest();
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Api-Key {MySecrets.API_KEY}");
            request.AddBody(jsonBody);

            var response = await client.ExecutePostAsync<object>(request);
            return response.Content;
        }
    }
}