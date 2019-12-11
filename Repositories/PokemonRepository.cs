using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Pokedex.Objects;

namespace Pokedex.Repositories
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string host;
        private string requestUrl;
        private HttpClient client;

        public PokemonRepository(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            host = "https://pokeapi.co";
            client = _clientFactory.CreateClient();
        }
        public async Task<PokemonDto> GetPokemonDetails(int id)
        {
            var route = $"/api/v2/pokemon/{id}";
            requestUrl = host + route;

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                var response = await client.SendAsync(request);
                var responseString = await response.Content.ReadAsStringAsync();
                var pokemonObject = JsonConvert.DeserializeObject<PokemonDto>(responseString);
                Console.WriteLine(responseString);
                return pokemonObject;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Message: {e.Message}");
            }

            return new PokemonDto();
        }

        public async Task<PokemonDto> GetPokemonDetails(string name)
        {
            var route = $"/api/v2/pokemon/{name}";
            requestUrl = host + route;

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                var response = await client.SendAsync(request);
                var responseString = await response.Content.ReadAsStringAsync();
                var pokemonObject = JsonConvert.DeserializeObject<PokemonDto>(responseString);

                return pokemonObject;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Message: {e.Message}");
            }

            return new PokemonDto();
        }

        public async Task<IEnumerable<PokemonDto>> GetPokemonList()
        {
            var route = "/api/v2/pokemon/";
            List<PokemonDto> pokemonList = new List<PokemonDto>();
            requestUrl = host + route;
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                var response = await client.SendAsync(request);
                var responseString = await response.Content.ReadAsStringAsync();
                var rootObject = JsonConvert.DeserializeObject<RootObject>(responseString);
                
                foreach (var pokemon in rootObject.results)
                {
                    string name = pokemon.name;
                    pokemonList.Add(await GetPokemonDetails(name));
                }
                return pokemonList;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Message: {e.Message}");
            }

            return new List<PokemonDto>();
        }
    }
}
