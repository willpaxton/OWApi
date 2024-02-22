using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OWApi
{
    public class OWAPIDriver
    {
        private HttpClient _httpClient;
        internal string _battletag;
        internal string _region;
        internal string _platform;

        public OWAPIDriver(string battletag, string region, string platform) 
        { 
            _httpClient = new HttpClient();
            _battletag = battletag;
            _region = region;
            _platform = platform;
        }

        public async Task<string> GetPlayerData()
        {
            var apiGeneralDataResponse = await _httpClient.GetFromJsonAsync<JsonElement>($"http://ow-api.com/v1/stats/{_platform}/{_region}/{_battletag.Replace("#", "-")}/profile");

            //await Console.Out.WriteLineAsync(apiGeneralDataResponse.GetProperty("quickPlayStats").ToString());

            string name = apiGeneralDataResponse.GetProperty("name").ToString();

            await Console.Out.WriteLineAsync(name.ToString());

            return name;

        }

        public async Task<string> GetPlayerQuickPlayWinrate()
        {
            var apiGeneralDataResponse = await _httpClient.GetFromJsonAsync<JsonElement>($"http://ow-api.com/v1/stats/{_platform}/{_region}/{_battletag.Replace("#", "-")}/profile");

            //await Console.Out.WriteLineAsync(apiGeneralDataResponse.GetProperty("quickPlayStats").ToString());

            double gamesWon = Convert.ToDouble(apiGeneralDataResponse.GetProperty("quickPlayStats").GetProperty("games").GetProperty("won").ToString());

            double gamesPlayed = Convert.ToDouble(apiGeneralDataResponse.GetProperty("quickPlayStats").GetProperty("games").GetProperty("played").ToString());

            string winPercentage = $"{(Math.Round((gamesWon / gamesPlayed), 4) * 100)}%";

            await Console.Out.WriteLineAsync(winPercentage.ToString());

            return winPercentage.ToString();

        }

        public async Task<string> GetPlayerCompetitiveWinrate()
        {
            var apiGeneralDataResponse = await _httpClient.GetFromJsonAsync<JsonElement>($"http://ow-api.com/v1/stats/{_platform}/{_region}/{_battletag.Replace("#", "-")}/profile");

            //await Console.Out.WriteLineAsync(apiGeneralDataResponse.GetProperty("competitiveStats").ToString());

            double gamesWon = Convert.ToDouble(apiGeneralDataResponse.GetProperty("competitiveStats").GetProperty("games").GetProperty("won").ToString());

            double gamesPlayed = Convert.ToDouble(apiGeneralDataResponse.GetProperty("competitiveStats").GetProperty("games").GetProperty("played").ToString());

            string winPercentage = $"{(Math.Round((gamesWon / gamesPlayed), 4)) * 100}%";

            await Console.Out.WriteLineAsync(winPercentage.ToString());

            return winPercentage.ToString();

        }

        public async Task<string> GetPlayerQuickplayHero(string hero)
        {
            var apiGeneralDataResponse = await _httpClient.GetFromJsonAsync<JsonElement>($"http://ow-api.com/v1/stats/{_platform}/{_region}/{_battletag.Replace("#", "-")}/heroes/{hero}");

   

            string timePlayed = apiGeneralDataResponse.GetProperty("quickPlayStats").GetProperty("topHeroes").GetProperty(hero).GetProperty("timePlayed").ToString();

            await Console.Out.WriteLineAsync(timePlayed.ToString());

            return timePlayed.ToString();

        }        
        
        public async Task<string> GetPlayerCompetitiveHero(string hero)
        {
            var apiGeneralDataResponse = await _httpClient.GetFromJsonAsync<JsonElement>($"http://ow-api.com/v1/stats/{_platform}/{_region}/{_battletag.Replace("#", "-")}/heroes/{hero}");

   

            string timePlayed = apiGeneralDataResponse.GetProperty("competitiveStats").GetProperty("topHeroes").GetProperty(hero).GetProperty("timePlayed").ToString();

            await Console.Out.WriteLineAsync(timePlayed.ToString());

            return timePlayed.ToString();

        }

        public async Task<int> GetPokemonCount()
        {
            //Get the total number of current pokemon from the API
            var apiCountResponse = await _httpClient.GetFromJsonAsync<JsonElement>("https://pokeapi.co/api/v2/pokemon-species?limit=0");
            return apiCountResponse.GetProperty("count").GetInt32();
        }
    }
}
