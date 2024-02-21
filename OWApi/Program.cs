using System.Net.Http.Json;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace OWApi
{
    internal class Program
    {
        static void Main(string[] args)
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

            HttpClient httpClient = new HttpClient();

            async void getResp()
            {
                var apiCountResponse = await httpClient.GetFromJsonAsync<JsonElement>($"http://ow-api.com/v1/stats/{platform}/{region}/{battletag.Replace("#", "-")}/profile");
                Thread.Sleep(1000);
                Console.WriteLine(apiCountResponse);
                Console.WriteLine("done :3");
            }

            getResp();

            Console.WriteLine("done 2");

            Thread.Sleep(1000000);

            // test
        }
    }
}