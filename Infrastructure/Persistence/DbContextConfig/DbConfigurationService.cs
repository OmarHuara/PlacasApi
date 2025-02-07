using Microsoft.EntityFrameworkCore;
using PlacasAPI.Infrastructure.Persistence.Context;

namespace PlacasAPI.Infrastructure.Persistence.DbContextConfig
{
    public class DbConfigurationService
    {
        public static void ConfigureServices(IServiceCollection servicesCollection, IConfigurationRoot configurationRoot)
        {
            var connectionString = configurationRoot.GetConnectionString("AutomovelDB")
                                   ?? throw new InvalidOperationException("Connection string `AutomovelDB` not found");

            servicesCollection.AddDbContext<PlateApiContext>(opt => opt.UseSqlServer(connectionString));
        }
    }
}
