using PlacasAPI.Interfaces.Services;
using PlacasAPI.Utils;

namespace PlacasAPI.Integration.KePlaca
{
    public class KePlacaIntegration : IPlateConsultIntegration
    {
        private readonly IHtmlScrapingServiceKePlaca _htmlScrapingService;

        public KePlacaIntegration(IHtmlScrapingServiceKePlaca htmlScrapingService)
        {
            _htmlScrapingService = htmlScrapingService;
        }
        public async Task<Dictionary<string, string>> GetDataFromTheWebsite(string plate)
        {
                var htmlContent = _htmlScrapingService.SearchCar(plate, "https://www.keplaca.com/placa");
                return new HtmlParserService().ParseHtmlToList(htmlContent, plate);
        }
    }
}
