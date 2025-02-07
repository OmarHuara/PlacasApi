using PlacasAPI.Dtos;
using PlacasAPI.Models;

namespace PlacasAPI.Interfaces
{
    public interface IAutomovelService
    {
        Task<ResponseGeneric<AutomovelDto>> SearchCar(string plate);
    }
}
