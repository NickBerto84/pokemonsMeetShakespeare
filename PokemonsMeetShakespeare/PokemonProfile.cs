using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PokemonsMeetShakespeare.Entities;
using PokemonsMeetShakespeare.Models;


namespace PokemonsMeetShakespeare
{
    public class PokemonProfile : Profile
    {
        #region ctors

        public PokemonProfile()
        {
            CreateMap<PokemonDto, Pokemon>().ReverseMap();
        }

        #endregion  // ctors
    }
}
