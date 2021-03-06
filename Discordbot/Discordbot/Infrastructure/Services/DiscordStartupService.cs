﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Discordbot.Commands;
using Discordbot.Infrastructure.Services.Scheduler.Tasks;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Discordbot.Infrastructure.Services
{
    public class DiscordStartupService
    {
        private readonly IServiceProvider _provider;
        private readonly DiscordSocketClient _discord;
        private readonly CommandService _commands;
        private readonly IConfigurationRoot _config;
        private readonly IDiscordTasksService _discordTasksService;

        public DiscordStartupService(
            IServiceProvider provider,
            DiscordSocketClient discord,
            CommandService commands,
            IConfigurationRoot config,
            IDiscordTasksService discordTasksService)
        {
            _provider = provider;
            _config = config;
            _discord = discord;
            _commands = commands;
            _discordTasksService = discordTasksService;
        }

        public async Task StartAsync()
        {
            string discordToken = _config["DiscordToken"];
            if (string.IsNullOrWhiteSpace(discordToken))
                throw new Exception("Please enter your bot's token into the `_configuration.json` file found in the applications root directory.");
            await _discord.LoginAsync(TokenType.Bot, discordToken);  
            await _discord.StartAsync();                               
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);    
            _discordTasksService.SchedulaAllTasks();
        }
    }
}
