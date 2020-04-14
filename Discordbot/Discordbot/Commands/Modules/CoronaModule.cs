using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Discord.Commands;
using Discordbot.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Discordbot.Commands.Modules
{
    public class CoronaModule : ModuleBase<SocketCommandContext>
    {
        static HttpClient client = new HttpClient();

        [Command("corona")]
        [Summary("displays corona info.")]
        public async Task CoronaAsync([Remainder] [Summary("the country to filter")] string country)
        { 
            HttpResponseMessage response = await client.GetAsync("https://api.covid19api.com/dayone/country/" + country);

            string rawString = await response.Content.ReadAsStringAsync();
            var coronaCountry = JsonConvert.DeserializeObject<List<CountryData>>(rawString);
            
            if (response.IsSuccessStatusCode)
            {
                await ReplyAsync(coronaCountry.Last().Deaths + " DODEN HAHAHAHAHAHAHAHA 😂😂😂");
            }
            else
            {
                await ReplyAsync("Country cannot be found");
            }
        }


    }
}