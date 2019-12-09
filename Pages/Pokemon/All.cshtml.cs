using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Pokedex.Objects;

namespace Pokedex.Pages.Pokemon
{
    public class AllModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public RootObject Pokemon { get; set; }

        public AllModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task OnGetAsync()
        {
            var host = "https://pokeapi.co";
            var route = "/api/v2/pokemon?offset=0&limit=20";
            var requestUrl = host + route;

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                var client = _clientFactory.CreateClient();
                var response = await client.SendAsync(request);
                var responseString = await response.Content.ReadAsStringAsync();
                var rootObject = JsonConvert.DeserializeObject<RootObject>(responseString);
                Pokemon = rootObject;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Message: {e.Message}");
            }    
        }
    }
}