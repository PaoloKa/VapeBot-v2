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
            //12, 30 , 24
            SchedulerFactory.IntervalInHours(DateTime.Now.Hour, DateTime.Now.Minute + 1, 10, () =>
            {
                if (!_discordUserService.isUserOnline(ulong.Parse(_config["WakeUpTargetUserId"])))
                {
                    _discord.GetGuild(ulong.Parse(_config["ServerId"])).GetTextChannel(ulong.Parse(_config["GeneralId"])).SendMessageAsync("Thomas is wakker geraakt voor 1u!!");
                } else
                {
                    _discord.GetGuild(ulong.Parse(_config["ServerId"])).GetTextChannel(ulong.Parse(_config["GeneralId"])).SendMessageAsync("Thomas is niet wakker geraakt voor 1u!! :(");
                }
            });
        }
    }
}
