using System.Dynamic;
using AutoMapper;
using Microsoft.OpenApi.Validations;
using PlacasAPI.Dtos;
using PlacasAPI.Interfaces.Respositories;
using PlacasAPI.Interfaces.Services;
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
        private readonly IAutomovelRepository _automovelRepository;

        public AutomovelService(IMapper mapper, HtmlParserService htmlParserService, IHtmlScrapingService htmlScrapingService, IAutomovelRepository automovelRepository)
        {
            _mapper = mapper;
            _htmlParserService = htmlParserService;
            _htmlScrapingService = htmlScrapingService;
            _automovelRepository = automovelRepository;
        }

        public async Task<ResponseGeneric<AutomovelDto>> SearchCar(string plate)
        {
            var automovelDb = await _automovelRepository.SearchByPlate(plate);
            if (automovelDb != null)
            {
                var resultDb = StructuredObjectDb(automovelDb);
                return _mapper.Map<ResponseGeneric<AutomovelDto>>(resultDb);
            }

            var htmlContent = _htmlScrapingService.SearchCar(plate);
            var content = _htmlParserService.ParseHtmlToList(htmlContent, plate);
            var automovel = StructuredObject(content);
            _automovelRepository.Add(automovel.ReturnData);
            return _mapper.Map<ResponseGeneric<AutomovelDto>>(automovel);
        }
        private ResponseGeneric<Automovel> StructuredObjectDb(Automovel automovelDb)
        {
            var response = new ResponseGeneric<Automovel>();
            response.WithData(automovelDb);
            return response;
        }
        private ResponseGeneric<Automovel> StructuredObject(Dictionary<string, string> htmlContent)
        {
            var response = new ResponseGeneric<Automovel>();
            if (htmlContent != null)
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
        public async Task<ResponseGeneric<List<AutomovelDto>>> SearchCars(List<string> plates)
        {
            var result = new ResponseGeneric<List<AutomovelDto>>();
            var listAutomoveis = new List<AutomovelDto>();
            foreach (var plate in plates)
            {
                var automovel = await _automovelRepository.SearchByPlate(plate);
                if (automovel != null)
                {
                    var automovelResult = _mapper.Map<AutomovelDto>(automovel);
                    listAutomoveis.Add(automovelResult);
                }
                else
                {
                    var htmlContent = _htmlScrapingService.SearchCar(plate);
                    var content = _htmlParserService.ParseHtmlToList(htmlContent, plate);
                    if (content != null)
                    {
                        var automovelForDb = _mapper.Map<Automovel>(content);
                        _automovelRepository.Add(automovelForDb);
                        var automovelResponse = _mapper.Map<AutomovelDto>(automovelForDb);
                        listAutomoveis.Add(automovelResponse);
                    }
                    else
                    {
                        AutomovelDto automovelNotExist = new AutomovelDto();
                        automovelNotExist.placa = plate;
                        listAutomoveis.Add(automovelNotExist);
                    }
                }
            }
            return result.WithData(listAutomoveis);
        }
    }
}
