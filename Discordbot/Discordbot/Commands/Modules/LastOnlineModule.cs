using System.Threading.Tasks;
using Discord.Commands;
using Discordbot.Infrastructure.Services;

namespace Discordbot.Commands.Modules
{
    public class LastOnlineModule : ModuleBase<SocketCommandContext>
    {
        
        private readonly IDiscordUserService _discordUserService;

        public LastOnlineModule(IDiscordUserService discordUserService)
        {
            _discordUserService = discordUserService;
        }

        [Command("online")]
        [Summary("Echoes a message.")]
        public Task OnlineAsync([Remainder] [Summary("Username of the time ")]
            ulong username)
        {
            return ReplyAsync(_discordUserService.isUserOnline(username).ToString());
        }
    }
}