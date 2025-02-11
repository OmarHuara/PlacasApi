using PlacasAPI.Dtos;
using PlacasAPI.Models;

namespace PlacasAPI.Interfaces.Services
{
    public interface IAutomovelService
    {
        Task<ResponseGeneric<AutomovelDto>> SearchCar(string plate);
        Task<ResponseGeneric<List<AutomovelDto>>> SearchCars(List<string> plates);
    }
}
