using Pokedex.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.Repositories
{
    public interface IPokemonRepository
    {
        Task<IEnumerable<PokemonDto>> GetPokemonList(int offset = 0, int limit = 20);
        Task<PokemonDto> GetPokemonDetails(int id);
        Task<PokemonDto> GetPokemonDetails(string name);        
    }
}
