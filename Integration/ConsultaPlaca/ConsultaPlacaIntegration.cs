using System.Text.Json;
using Microsoft.OpenApi.Validations;
using PlacasAPI.Dtos;
using PlacasAPI.Interfaces.Services;
using PlacasAPI.Utils;

namespace PlacasAPI.Integration.ConsultaPlaca
{
    public class ConsultaPlacaIntegration : IPlateConsultIntegration
    {
        private readonly IHtmlScrapingServiceConsultaPlaca _htmlScrapingService;
        public ConsultaPlacaIntegration(IHtmlScrapingServiceConsultaPlaca htmlScrapingService)
        {
            _htmlScrapingService = htmlScrapingService;
        }
        public async Task<Dictionary<string, string>> GetDataFromTheWebsite(string plate)
        {
            var dataCar = await _htmlScrapingService.SearchCar(plate);
            if (dataCar == "invalid")
            {
                return new Dictionary<string, string>();
            }
            var deserializeCar = JsonSerializer.Deserialize<Dictionary<string, string>>(dataCar);
            return mappingDictionary(deserializeCar, plate);
        }

        Dictionary<string, string> mappingDictionary(Dictionary<string, string> dataCar, string plate)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            result.Add("PLATE", plate);
            result.Add("MARCA", GetValueOrDefault(dataCar, "MARCA"));
            result.Add("MODELO", GetValueOrDefault(dataCar, "modelo"));
            result.Add("COR", GetValueOrDefault(dataCar, "cor"));
            result.Add("ANO", GetValueOrDefault(dataCar, "ano"));
            result.Add("ANOMODELO", GetValueOrDefault(dataCar, "anomod"));
            result.Add("COMBUSTIVEL", GetValueOrDefault(dataCar, "combustivel"));
            result.Add("CHASSI", GetValueOrDefault(dataCar, "chassi"));
            return result;
        }
        string GetValueOrDefault(Dictionary<string, string> dictionary, string key)
        {
            return dictionary.TryGetValue(key, out string value) ? value : "";
        }
    }

}
