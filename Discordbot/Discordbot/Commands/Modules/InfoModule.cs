﻿using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Discordbot.Commands.Modules
{
    public class InfoModule : ModuleBase<SocketCommandContext>
    {
        // ~say hello world -> hello world
        [Command("say")]
        [Summary("Echoes a message.")]
        public Task SayAsync([Remainder] [Summary("The text to echo")] string echo)
            => ReplyAsync(echo);

		[Command("userinfo")]
		[Summary
		("Returns info about the current user, or the user parameter, if one passed.")]
		[Alias("user", "whois")]
		public async Task UserInfoAsync(
		[Summary("The (optional) user to get info from")]
		SocketUser user = null)
		{
			var userInfo = user ?? Context.Client.CurrentUser;
			await ReplyAsync($"{userInfo.Username}#{userInfo.Discriminator}");
		}
	}
}
