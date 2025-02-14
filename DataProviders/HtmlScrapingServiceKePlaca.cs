using System.Dynamic;
using AutoMapper;
using HtmlAgilityPack;
using PlacasAPI.Dtos;
using PlacasAPI.Interfaces.Services;
using PlacasAPI.Models;
using PlacasAPI.Utils;

namespace PlacasAPI.Rest
{
    public class HtmlScrapingServiceKePlaca : IHtmlScrapingServiceKePlaca
    {
        private readonly HttpClient _httpClient;

        public HtmlScrapingServiceKePlaca(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public string SearchCar(string nrPlate, string url)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Add("User-Agent", "LAB_CONSULTA_PLACA");
                return _httpClient.GetStringAsync($"{url}/{nrPlate}").Result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "";
            }
        }
    }
}
