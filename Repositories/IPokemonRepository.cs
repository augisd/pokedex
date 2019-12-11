using Pokedex.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.Repositories
{
    public interface IPokemonRepository
    {
        Task<RootObject> GetPokemonList1();
        Task<IEnumerable<PokemonDto>> GetPokemonList();
        Task<Sprite> GetPokemonSprites();
        Task<PokemonDto> GetPokemonDetails(int id);
        Task<PokemonDto> GetPokemonDetails(string name);        
    }
}
