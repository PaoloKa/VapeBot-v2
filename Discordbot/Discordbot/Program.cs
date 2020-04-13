using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Discordbot.Commands;
using Discordbot.Infrastructure.Services;
using Discordbot.Infrastructure.Services.Scheduler;
using Discordbot.Infrastructure.Services.Scheduler.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Discordbot
{
    public class Program
    {
		public IConfigurationRoot configuration;

		public static void Main(string[] args)
         => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
			var serviceCollection = new ServiceCollection();
			ConfigureServices(serviceCollection);
			var provider = serviceCollection.BuildServiceProvider();
			provider.GetRequiredService<LoggingService>();  
			provider.GetRequiredService<CommandHandler>(); 

			await provider.GetRequiredService<DiscordStartupService>().StartAsync();

			//block this task until app is exited
			await Task.Delay(-1);
		}

		private  void ConfigureServices(IServiceCollection serviceCollection)
		{

			configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
				.AddJsonFile("appsettings.json", false)
				.Build();

			serviceCollection.AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
			{                                      
				LogLevel = LogSeverity.Verbose,     // Tell the logger to give Verbose amount of info
				MessageCacheSize = 1000             // Cache 1,000 messages per channel
			}))
			.AddSingleton(new CommandService(new CommandServiceConfig
			{                                       
				LogLevel = LogSeverity.Verbose,     // Tell the logger to give Verbose amount of info
				DefaultRunMode = RunMode.Async,     // Force all commands to run async by default
			}))
			.AddSingleton<CommandHandler>()
			.AddSingleton<IDiscordTasksService, DiscordTasksService>()
			.AddSingleton<DiscordStartupService>()
			.AddSingleton<LoggingService>()
			.AddSingleton<IDiscordUserService, DiscordUserService>()
			.AddSingleton<IConfigurationRoot>(configuration);
		}

	}
}
