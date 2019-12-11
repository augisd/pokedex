using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Pokedex.Repositories;
using Pokedex.Objects;

namespace Pokedex.Pages.Pokemon
{
    public class DetailsModel : PageModel
    {
        private readonly IPokemonRepository _pokemonRepository;
        public Objects.PokemonDto Details { get; set; }

        public DetailsModel(IPokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }
        public async Task OnGetAsync()
        {
            Details = await _pokemonRepository.GetPokemonDetails("ivysaur");
        }
    }
}