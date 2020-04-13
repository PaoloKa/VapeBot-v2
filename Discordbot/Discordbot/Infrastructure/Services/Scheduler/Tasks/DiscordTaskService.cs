using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discordbot.Infrastructure.Services.Scheduler.Tasks
{
    public interface IDiscordTasksService
    {
        void SchedulaAllTasks();
    }

    public class DiscordTasksService : IDiscordTasksService
    {
        private readonly IDiscordUserService _discordUserService;
        private readonly DiscordSocketClient _discord;
        private readonly IConfigurationRoot _config;

        public DiscordTasksService(IDiscordUserService discordUserService, DiscordSocketClient discord, IConfigurationRoot config)
        {
            this._discordUserService = discordUserService;
            this._discord = discord;
            this._config = config;
        }

        public void SchedulaAllTasks()
        {
            SchedulerFactory.IntervalInSeconds(DateTime.Now.Hour, DateTime.Now.Minute + 1, 10, () =>
            {
                if (!_discordUserService.isUserOnline(ulong.Parse(_config["WakeUpTargetUserId"])))
                {
                    Console.WriteLine("Not online");
                } else
                {
                    Console.WriteLine("online :)");
                }
            });
        }
    }
}
