using PlacasAPI.Models;

namespace PlacasAPI.Interfaces.Respositories
{
    public interface IAutomovelRepository : IRepositoryBase<Automovel>
    {
        Task<Automovel> SearchByPlate(string plate);
    }
}
