using PlacasAPI.Interfaces.Services;
using PlacasAPI.Utils;

namespace PlacasAPI.DataProviders
{
    public class HtmlScrapingServiceConsultaPlaca : IHtmlScrapingServiceConsultaPlaca
    {
        private readonly HttpClient _httpClient;
        private readonly ConsultaPlacaConfiguration _configuration;

        public HtmlScrapingServiceConsultaPlaca(IHttpClientFactory httpClientFactory, ConsultaPlacaConfiguration consultaPlacaConfiguration)
        {
            _httpClient = httpClientFactory.CreateClient(HttpClientDefaults.UrlConsultaPlacaComBR) ?? throw new NullReferenceException(typeof(IHttpClientFactory).Name);
            _configuration = consultaPlacaConfiguration ?? throw new NullReferenceException(typeof(string).Name);
        }

        public async Task<string> SearchCar(string plate)
        {
            var codedPlate = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("placa", $"{plate.Substring(0, 3)}-{plate.Substring(3)}")
            });
            var result = await _httpClient.PostAsync($"/api/?tk={_configuration.Token}", codedPlate);
            return await result.Content.ReadAsStringAsync();
        }

    }
}
