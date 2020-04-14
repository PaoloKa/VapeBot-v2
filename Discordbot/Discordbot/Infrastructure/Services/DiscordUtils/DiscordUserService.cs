using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discordbot.Infrastructure.Services
{
    public interface IDiscordUserService
    {
        bool isUserOnline(ulong userId);
    }
    public class DiscordUserService : IDiscordUserService
    {
        private readonly DiscordSocketClient _discord;
        private readonly IConfigurationRoot _config;

        public DiscordUserService(DiscordSocketClient discord, IConfigurationRoot config)
        {
            this._discord = discord;
            this._config = config;
        }
        public bool isUserOnline(ulong userId)
        {
            var user = _discord.GetGuild(ulong.Parse(_config["ServerId"])).GetUser(userId);
            if (user == null)
                return false;
            switch (user.Status)
            {
                case Discord.UserStatus.AFK:
                case Discord.UserStatus.DoNotDisturb:
                case Discord.UserStatus.Idle:
                case Discord.UserStatus.Invisible:
                case Discord.UserStatus.Online:
                    return true;
            }
            return false;
        }
    }
}
