using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Discordbot.Commands;
using System;
using System.Threading.Tasks;

namespace Discordbot
{
    public class Program
    {
		private DiscordSocketClient _client;

		public static void Main(string[] args)
         => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
			_client = new DiscordSocketClient();

			_client.Log += Log;
			_client.MessageReceived += MessageReceived;
			await _client.LoginAsync(TokenType.Bot,(""));
			await _client.StartAsync();

			await new CommandHandler(_client, new CommandService()).InstallCommandsAsync(); //TODO: maybe use DI here? 

			// Block this task until the program is closed.
			await Task.Delay(-1);
		}

		private async Task MessageReceived(SocketMessage message)
		{
			if (message.Content == "!ping")
			{
				await message.Channel.SendMessageAsync("Pong!");
			}
		}

		private Task Log(LogMessage msg)
		{
			Console.WriteLine(msg.ToString());
			return Task.CompletedTask;
		}
	}
}
