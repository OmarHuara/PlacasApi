using PlacasAPI.Dtos;
using PlacasAPI.Models;
using PlacasAPI.Utils;

namespace PlacasAPI.Interfaces.Services
{
    public interface IAutomovelService
    {
        Task<ValueResult<AutomovelDto>> SearchCar(string plate);
        Task<ResponseGeneric<List<AutomovelDto>>> SearchCars(List<string> plates);
    }
}
