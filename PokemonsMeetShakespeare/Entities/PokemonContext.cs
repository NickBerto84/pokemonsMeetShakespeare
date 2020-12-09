using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace PokemonsMeetShakespeare.Entities
{
    public class PokemonContext : DbContext
    {
        #region members

        string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=PokemonDb;Trusted_Connection=True;";

        #endregion  // members


        #region properties

        public DbSet<Pokemon> Pokemons { get; set; }

        #endregion  // properties


        #region methods

        #region protected

        /// <summary>
        /// This method overrides the base method to further configure the model that was discovered by convention
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Pokemon>();
        }


        /// <summary>
        /// This method overrides the base method to configure the database (and other options) to be used for this context
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_connectionString);
        }

        #endregion  // protected

        #endregion  // methods
    }
}
