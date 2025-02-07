using Microsoft.EntityFrameworkCore;
using PlacasAPI.Infrastructure.Persistence.EntityConfig;
using PlacasAPI.Models;

namespace PlacasAPI.Infrastructure.Persistence.Context
{
    public class PlateApiContext : DbContext
    {
        public DbSet<Automovel> Automoveis { get; set; }
        public PlateApiContext(DbContextOptions<PlateApiContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AutomovelConfig());
        }
    }
}
