using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Pokedex.Objects;
using Pokedex.Repositories;

namespace Pokedex.Pages.Pokemon
{
    public class AllModel : PageModel
    {
        private readonly IPokemonRepository _pokemonRepository;

        public IEnumerable<PokemonDto> Pokemon { get; set; }
        public int numberOfPages { get; set; }

        [BindProperty(SupportsGet = true)]                       // 
        public int itemCount { get; set; } = 20;

        [BindProperty(SupportsGet = true)]
        public int pageNumber { get; set; } = 1;

        public AllModel(IPokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }
        public async Task OnGetAsync()
        {
            Pokemon = await _pokemonRepository.GetPokemonList((pageNumber - 1) * itemCount, itemCount);
            numberOfPages = Convert.ToInt32(Math.Ceiling((double)_pokemonRepository.GetPokemonCount() / itemCount));
        }
    }
}