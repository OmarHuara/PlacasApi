using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PlacasAPI.Dtos;
using PlacasAPI.Interfaces.Services;
using PlacasAPI.Utils;

namespace PlacasAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlacasController : PlacasBaseController
    {
        private readonly IAutomovelService _automovelService;
        public PlacasController(ILogger<PlacasController> logger, IAutomovelService automovelService) : base(logger)
        {
            _automovelService = automovelService ?? throw new NullReferenceException(typeof(IAutomovelService).Name);
        }

        [HttpGet("/automovel/placa/{nrplaca}")]
        [ProducesResponseType(typeof(IEnumerable<ResultError>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(AutomovelDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<AutomovelDto>> GetPlatesByIdentification([RegularExpression("^[a-zA-Z]{3}[0-9][A-Za-z0-9][0-9]{2}$")]string nrplaca)
        {
            var response = await _automovelService.SearchCar(nrplaca);
            return TreatsResponseValueResult(response);
        }

        [HttpGet("/automovel/placa/dinamica/{quantidade}")]
        public Task<ResponseGeneric<List<AutomovelDto>>> GerarPlacasDinamicas([RegularExpression("^[0-9]*$")] int quantidade)
        {
            var plates = new GetRandomPlate().GenerateRandomPlates(quantidade);
            return _automovelService.SearchCars(plates);
        }
    }
}

            