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
        private string _name;
        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value.First().ToString().ToUpper() + value.Substring(1);
            }
        }
        public Sprite sprites { get; set; }
        public List<PokemonType> types { get; set; }
        public int weight { get; set; }
    }
}
