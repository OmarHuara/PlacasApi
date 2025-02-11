using PlacasAPI.Dtos;
using PlacasAPI.Models;

namespace PlacasAPI.Interfaces.Services
{
    public interface IHtmlScrapingService
    {
        string SearchCar(string plate);
    }
}
