namespace PlacasAPI.Interfaces.Services
{
    public interface IHtmlScrapingServiceConsultaPlaca
    {
        Task<string> SearchCar(string plate);
    }
}
