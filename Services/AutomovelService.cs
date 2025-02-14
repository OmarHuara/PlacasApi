using AutoMapper;
using Microsoft.Data.SqlClient;
using PlacasAPI.Dtos;
using PlacasAPI.Integration;
using PlacasAPI.Interfaces.Respositories;
using PlacasAPI.Interfaces.Services;
using PlacasAPI.Models;
using PlacasAPI.Utils;

namespace PlacasAPI.Services
{
    public class AutomovelService : IAutomovelService
    {
        private readonly IMapper _mapper;
        private readonly IPlateConsultIntegration _plateConsultIntegration;
        private readonly IAutomovelRepository _automovelRepository;

        public AutomovelService(IMapper mapper, IPlateConsultIntegration plateConsultIntegration, IAutomovelRepository automovelRepository)
        {
            _mapper = mapper;
            _plateConsultIntegration = plateConsultIntegration;
            _automovelRepository = automovelRepository;
        }

        public async Task<ValueResult<AutomovelDto>> SearchCar(string plate)
        {
            var automovelDb = await _automovelRepository.SearchByPlate(plate);
            if (automovelDb != null)
            {
                var resultDb = ValueResult<Automovel>.Success(automovelDb);
                return _mapper.Map<ValueResult<AutomovelDto>>(resultDb);
            }

            try
            {
                var content = await _plateConsultIntegration.GetDataFromTheWebsite(plate);
                if (content == null || content.Count == 0)
                {
                    return ValueResult<AutomovelDto>.Fail("Error, car data not found");
                }
                var automovel = StructuredObject(content);
                _automovelRepository.Add(automovel.Value);
                return _mapper.Map<ValueResult<AutomovelDto>>(automovel);

            }
            catch (SqlException sqlEx)
            {
                List<ResultError> errors = new List<ResultError>();
                errors.Add(new ResultError(sqlEx.Message));

                foreach (SqlError error in sqlEx.Errors)
                {
                    errors.Add(new ResultError(error.Number.ToString(), error.Message));
                }
                return ValueResult<AutomovelDto>.Fail(errors);
            }
        }

        

        private ValueResult<Automovel> StructuredObject(Dictionary<string, string> htmlContent)
        {
            var listResponse = _mapper.Map<Automovel>(htmlContent);
            return ValueResult<Automovel>.Success(listResponse);
            
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
                    var content = _plateConsultIntegration.GetDataFromTheWebsite(plate);
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
