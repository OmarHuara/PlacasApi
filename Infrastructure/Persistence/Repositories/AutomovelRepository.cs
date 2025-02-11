using Microsoft.EntityFrameworkCore;
using PlacasAPI.Infrastructure.Persistence.Context;
using PlacasAPI.Interfaces.Respositories;
using PlacasAPI.Models;

namespace PlacasAPI.Infrastructure.Persistence.Repositories
{
    public class AutomovelRepository : RepositoryBase<Automovel>, IAutomovelRepository
    {
        public AutomovelRepository(PlateApiContext context) : base(context)
        {
        }

        public async Task<Automovel?> SearchByPlate(string plate)
        =>  await _context.Automoveis.Where(c => c.plate == plate).FirstOrDefaultAsync();
    }
}
