using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Pokedex.Objects;

namespace Pokedex.Repositories
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IMemoryCache _cache;
        private readonly string host;
        private string requestUrl;
        private HttpClient client;

        public PokemonRepository(IHttpClientFactory clientFactory, IMemoryCache cache)
        {
            _clientFactory = clientFactory;
            _cache = cache;
            host = "https://pokeapi.co";
            client = _clientFactory.CreateClient();
        }
        public async Task<PokemonDto> GetPokemonDetails(int id)
        {
            var route = $"/api/v2/pokemon/{id}";
            requestUrl = host + route;

            // check if pokemon details cached
            if (_cache.TryGetValue(id, out PokemonDto cacheEntry))
            {
                // pokemon object in cache - return
                return cacheEntry;
            }

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                var response = await client.SendAsync(request);
                var responseString = await response.Content.ReadAsStringAsync();
                var pokemonObject = JsonConvert.DeserializeObject<PokemonDto>(responseString);

                // add to cache
                _cache.Set(id, pokemonObject);

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

            // check if pokemon details cached
            if (_cache.TryGetValue(name, out PokemonDto cacheEntry))
            {
                // pokemon object in cache - return
                return cacheEntry;
            }

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                var response = await client.SendAsync(request);
                var responseString = await response.Content.ReadAsStringAsync();
                var pokemonObject = JsonConvert.DeserializeObject<PokemonDto>(responseString);

                // add to cache
                _cache.Set(name, pokemonObject);

                return pokemonObject;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Message: {e.Message}");
            }

            return new PokemonDto();
        }

        public async Task<IEnumerable<PokemonDto>> GetPokemonList(int offset = 0, int limit = 20)
        {
            var route = $"/api/v2/pokemon?offset={offset}&limit={limit}";
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
