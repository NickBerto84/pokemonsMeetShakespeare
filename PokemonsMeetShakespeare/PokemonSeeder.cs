using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PokemonsMeetShakespeare.Entities;

namespace PokemonsMeetShakespeare
{
    public class PokemonSeeder
    {
        #region members

        private readonly PokemonContext _pokemonContext;

        #endregion // members


        #region ctors

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="pokemonContext"></param>
        public PokemonSeeder(PokemonContext pokemonContext)
        {
            _pokemonContext = pokemonContext;
        }

        #endregion  // ctors


        #region public methods

        /// <summary>
        /// This method seeds database
        /// </summary>
        public void Seed()
        {
            if (_pokemonContext.Database.CanConnect())
            {
                if (!_pokemonContext.Pokemons.Any())
                {
                    InsertSampleData();
                }
            }
        }

        #endregion  // public methods


        #region private methods

        /// <summary>
        /// This method inserts sample data in the DB
        /// </summary>
        private void InsertSampleData()
        {
            var pokemons = new List<Pokemon>
            {
                new Pokemon
                {
                    Name = "Taming of the Sandshrew",
                    Description = "Charizard flies 'round the sky in search of powerful opponents. 't breathes fire of such most wondrous heat yond 't melts aught. However,  't nev'r turns its fiery breath on any opponent weaker than itself.",
                },
                new Pokemon
                {
                    Name = "Hamlet",
                    Description = "To be a Pokemon Master, or not to be a Pokemon Master? That is the question!",
                },
                new Pokemon
                {
                    Name = "charizard",
                    Description = "...",
                },
                new Pokemon
                {
                    Name = "A Midsummer Night's Dreameater",
                    Description = "...",
                },
                new Pokemon
                {
                    Name = "Two Gentleman of Fearow-na",
                    Description = "...",
                }
            };

            _pokemonContext.AddRange(pokemons);
            _pokemonContext.SaveChanges();
        }

        #endregion  // private methods

    }
}
