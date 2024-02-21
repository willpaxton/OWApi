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

            return apiGeneralDataResponse.GetProperty("quickPlayStats").ToString();

        }

        public async Task<int> GetPokemonCount()
        {
            //Get the total number of current pokemon from the API
            var apiCountResponse = await _httpClient.GetFromJsonAsync<JsonElement>("https://pokeapi.co/api/v2/pokemon-species?limit=0");
            return apiCountResponse.GetProperty("count").GetInt32();
        }
    }
}
