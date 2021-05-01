using apiDadosTempo.Entities;
using Microsoft.EntityFrameworkCore;

namespace apiDadosTempo.Context
{
    public class ContextData : DbContext
    {
        public ContextData(DbContextOptions<ContextData> options) : base(options)
        {

        }

        public DbSet<CidadeTemperatura> cidadeTemperatura { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }


    }
}
