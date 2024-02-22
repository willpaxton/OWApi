using System.Net.Http.Json;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace OWApi
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Regex battletagRegex = new Regex("[A-Za-z0-9]+#[0-9]+");
            // Note: I know this regex doesn't encapsalate every allowed character, just... it's alot

            string battletag = String.Empty;
            string platform = String.Empty;
            string region = String.Empty;
            bool firstAsk = true;

            do
            {
                if (!firstAsk) Console.WriteLine("Inncorect format, please use [username]#[discriminator]");

                Console.Write("What is your battletag?: ");

                battletag = Console.ReadLine();

                firstAsk = false;
            } while (!battletagRegex.IsMatch(battletag));

            firstAsk = true;

            do
            {
                if (!firstAsk) Console.WriteLine("Please answer with 1, 2, or 3");
                Console.Write("\nWhat region do you play in?\n1) NA (US)\n2) EU\n3) Asia\nEnter your response here: ");
                string response = Console.ReadLine();

                if (response == "1") region = "us";
                if (response == "2") region = "eu";
                if (response == "3") region = "asia";


                firstAsk = false;
            } while (region == String.Empty);

            firstAsk = true;

            do
            {
                if (!firstAsk) Console.WriteLine("Please answer with 1 or 2");
                Console.Write("\nDo you play on PC or Console?:\n1) PC\n2) Console\nEnter your reponse here: ");
                string response = Console.ReadLine();
                if (response == "1") platform = "pc";
                if (response == "2") platform = "console";

                firstAsk = false;
            } while (platform == String.Empty);

            firstAsk = true;

            string specificCall = String.Empty;

            do
            {
                if (!firstAsk) Console.WriteLine("Please answer with 1, 2, 3, 4, or 5");
                Console.Write("\nWhat would you like the API to return?\n1) Your Name\n2) Quickplay Winrate\n3) Competitive Winrate\n4) Specific Hero Quickplay Time Played\n5) Specific Hero Competitive Time Played\nEnter your answer here: ");
                string response = Console.ReadLine();

                if (response == "1") specificCall = "name";
                if (response == "2") specificCall = "qw";
                if (response == "3") specificCall = "cw";
                if (response == "4") specificCall = "hqp";
                if (response == "5") specificCall = "hcp";


                firstAsk = false;
            } while (specificCall == String.Empty);

            string specHero = String.Empty;

            if (specificCall == "hqp" || specificCall == "hcp")
            {
                Console.Write("\nWhat hero?: ");
                specHero = Console.ReadLine();
            }

            OWAPIDriver APIDriver = new OWAPIDriver(battletag, region, platform);

            bool isitdoneyetpls = false;

            /*async void doThings()
            {
                Console.WriteLine("frick");
                string responseABC = Console.ReadLine();
                string data = await APIDriver.GetPlayerData();
                await Console.Out.WriteLineAsync(data);
                await Task.Delay(5000);
            }*/

            static async Task GetAsync(HttpClient httpClient)
            {
                using HttpResponseMessage response = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/todos/3");

                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"{jsonResponse}\n");

                // Expected output:
                //   GET https://jsonplaceholder.typicode.com/todos/3 HTTP/1.1
                //   {
                //     "userId": 1,
                //     "id": 3,
                //     "title": "fugiat veniam minus",
                //     "completed": false
                //   }
            }

            // https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient idk :(
            // HttpClient httpClient = new HttpClient();
            // await GetAsync(httpClient);


            // Console.WriteLine(APIDriver.GetPokemonCount());

            Console.WriteLine();

            if (specificCall == "name") await APIDriver.GetPlayerData();
            if (specificCall == "qw") await APIDriver.GetPlayerQuickPlayWinrate();
            if (specificCall == "cw") await APIDriver.GetPlayerCompetitiveWinrate();
            if (specificCall == "hqp") await APIDriver.GetPlayerQuickplayHero(specHero);
            if (specificCall == "hcp") await APIDriver.GetPlayerCompetitiveHero(specHero);


        }
    }
}