using System.Net.Http.Json;
using System.Text.Json;

namespace OWApi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            HttpClient httpClient = new HttpClient();

            async void getResp()
            {
                var apiCountResponse = await httpClient.GetFromJsonAsync<JsonElement>("http://ow-api.com/v1/stats/pc/us/wilki-11280/profile");
                Thread.Sleep(1000);
                Console.WriteLine(apiCountResponse);
                Console.WriteLine("done :3");
            }

            getResp();

            Console.WriteLine("done 2");

            Thread.Sleep(5000);

            // test
        }
    }
}