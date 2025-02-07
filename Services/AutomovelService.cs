using System.Dynamic;
using AutoMapper;
using Microsoft.OpenApi.Validations;
using PlacasAPI.Dtos;
using PlacasAPI.Interfaces;
using PlacasAPI.Models;
using PlacasAPI.Rest;
using PlacasAPI.Utils;

namespace PlacasAPI.Services
{
    public class AutomovelService : IAutomovelService
    {
        private readonly IMapper _mapper;
        private readonly HtmlParserService _htmlParserService;
        private readonly IHtmlScrapingService _htmlScrapingService;

        public AutomovelService(IMapper mapper, HtmlParserService htmlParserService, IHtmlScrapingService htmlScrapingService)
        {
            _mapper = mapper;
            _htmlParserService = htmlParserService;
            _htmlScrapingService = htmlScrapingService;
        }

        public async Task<ResponseGeneric<AutomovelDto>> SearchCar(string plate)
        {
            var htmlContent = _htmlScrapingService.SearchCar(plate);
            var content = _htmlParserService.ParseHtmlToList(htmlContent);
            var automovel = StructuredObject(content);
            return _mapper.Map<ResponseGeneric<AutomovelDto>>(automovel);
        }

        private ResponseGeneric<Automovel> StructuredObject(List<string> htmlContent)
        {
            var response = new ResponseGeneric<Automovel>();
            if (htmlContent.Count > 0)
            {
                var listResponse = _mapper.Map<Automovel>(htmlContent);
                response.WithData(listResponse);
            }
            else
            {
                dynamic errorDetails = new ExpandoObject();
                errorDetails.ErrorMessage = "Plate not found";
                response.WithError(errorDetails);
            }
            return response;
        }
    }
}
