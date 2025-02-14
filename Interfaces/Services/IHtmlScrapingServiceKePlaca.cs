using PlacasAPI.Dtos;
using PlacasAPI.Models;

namespace PlacasAPI.Interfaces.Services
{
    public interface IHtmlScrapingServiceKePlaca
    {
        string SearchCar(string plate, string url);
    }
}
