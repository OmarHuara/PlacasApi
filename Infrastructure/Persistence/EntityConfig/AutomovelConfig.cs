using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlacasAPI.Models;

namespace PlacasAPI.Infrastructure.Persistence.EntityConfig
{
    public class AutomovelConfig : IEntityTypeConfiguration<Automovel>
    {
        public void Configure(EntityTypeBuilder<Automovel> builder)
        {
            builder.HasKey(c => c.Id);
        }
    }
}
