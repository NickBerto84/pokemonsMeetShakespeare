using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PokemonsMeetShakespeare.Entities;
using PokemonsMeetShakespeare.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PokemonsMeetShakespeare.Controllers
{
    [Route("pokemon")]
    public class PokemonController : ControllerBase
    {
        #region members

        private readonly PokemonContext _pokemonContext;
        private readonly IMapper _mapper;
        private readonly ILogger<PokemonController> _logger;

        #endregion  // members


        #region ctors

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="pokemonContext"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public PokemonController(PokemonContext pokemonContext, IMapper mapper, ILogger<PokemonController> logger)
        {
            _pokemonContext = pokemonContext;
            _mapper = mapper;
            _logger = logger;
        }

        #endregion  // ctors


        #region public methods

        /// <summary>
        /// This method GETs the Pokemon list of descriptions
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<Pokemon>> Get()
        {
            var pokemons = _pokemonContext.Pokemons.ToList();
            var pokemonDtos = _mapper.Map<List<PokemonDto>>(pokemons);
            return Ok(pokemonDtos);
        }

        /// <summary>
        /// This method GETs a Pokemon description
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}")]
        public ActionResult<Pokemon> Get(string name)
        {
            var pokemon = _pokemonContext.Pokemons
                .FirstOrDefault(m => m.Name.Replace(" ", "-").ToLower() == name.ToLower());

            if (pokemon == null)
            {
                _logger.LogError($"POkemon {pokemon} NOT found");
                return NotFound();
            }

            var pokemonDto = _mapper.Map<PokemonDto>(pokemon);
            return Ok(pokemonDto);
        }

        /// <summary>
        /// This method POSTs a Pokemon description
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post([FromBody] PokemonDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var pokemon = _mapper.Map<Pokemon>(model);

            _pokemonContext.Pokemons.Add(pokemon);
            _pokemonContext.SaveChanges();

            var key = pokemon.Name.Replace(" ", "-").ToLower();

            return Created("pokemon/" + key, null);

        }

        /// <summary>
        /// This method PUTs a Pokemon description
        /// </summary>
        /// <param name="name"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{name}")]
        public ActionResult Put(string name, [FromBody] PokemonDto model)
        {
            var pokemon = _pokemonContext.Pokemons
                .FirstOrDefault(m => m.Name.Replace(" ", "-").ToLower() == name.ToLower());

            if (pokemon == null)
            {
                _logger.LogError($"POkemon {pokemon} NOT found");
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            pokemon.Name = pokemon.Name;
            pokemon.Description = pokemon.Description;

            _pokemonContext.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// This method DELETEs a Pokemon description
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpDelete("{name}")]
        public ActionResult<Pokemon> Delete(string name)
        {
            var pokemon = _pokemonContext.Pokemons
                .FirstOrDefault(m => m.Name.Replace(" ", "-").ToLower() == name.ToLower());

            if (pokemon == null)
            {
                _logger.LogError($"POkemon {pokemon} NOT found");
                return NotFound();
            }

            _pokemonContext.Remove(pokemon);
            _pokemonContext.SaveChanges();

            return NoContent();
        }

        #endregion  // public methods
    }
}
