using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.Objects
{
    public class PokemonDto
    {
        public List<Ability> abilities { get; set; }
        public int base_experience { get; set; }
        public int height { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public Sprite sprites { get; set; }
        public List<PokemonType> types { get; set; }
        public int weight { get; set; }        
    }
}
