using PlacasAPI.Dtos;
using PlacasAPI.Models;

namespace PlacasAPI.Interfaces
{
    public interface IHtmlScrapingService
    {
        string SearchCar(string plate);
    }
}
