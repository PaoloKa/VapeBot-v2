using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Discordbot.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Discordbot
{
    public class Program
    {
		private DiscordSocketClient _client;
		public static IConfigurationRoot configuration;

		public static void Main(string[] args)
         => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
			ServiceCollection serviceCollection = new ServiceCollection();
			ConfigureServices(serviceCollection);

			Console.WriteLine(configuration[("DiscordToken")]);
			CreateBot();
			// Block this task until the program is closed.
			await Task.Delay(-1);
		}

		public async void CreateBot()
		{
			_client = new DiscordSocketClient();

			_client.Log += Log;
			_client.MessageReceived += PingTester;
			await _client.LoginAsync(TokenType.Bot, ("MzgwNjcwNjYzNTU3NTEzMjE4.XpSQig.3qOxEAHM3L86nbFrmxtxWgsoBw4"));
			await _client.StartAsync();

			await new CommandHandler(_client, new CommandService()).InstallCommandsAsync(); //TODO: maybe use DI here? 
		}

		private static void ConfigureServices(IServiceCollection serviceCollection)
		{
			// Build configuration
			configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
				.AddJsonFile("appsettings.json", false)
				.Build();
			// Add access to generic IConfigurationRoot
			serviceCollection.AddSingleton<IConfigurationRoot>(configuration);
		}

		//User for development to test if the bot can actually receive requests
		private async Task PingTester(SocketMessage message)
		{
			if (message.Content == "!ping")
			{
				await message.Channel.SendMessageAsync("Pong!");
			}
		}
		//Also use DI
		private Task Log(LogMessage msg)
		{
			Console.WriteLine(msg.ToString());
			return Task.CompletedTask;
		}
	}
}
