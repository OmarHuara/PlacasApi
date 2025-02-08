using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using PlacasAPI.Interfaces;
using PlacasAPI.Utils;

namespace PlacasAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlacasController : ControllerBase
    {
        private readonly IAutomovelService _automovelService;
        private readonly GetRandomPlate _getRandomPlate;
        private readonly ILogger<PlacasController> _logger;

        public PlacasController(ILogger<PlacasController> logger, IAutomovelService automovelService, GetRandomPlate getRandomPlate)
        {
            _automovelService = automovelService;
            _getRandomPlate = getRandomPlate;
            _logger = logger;
        }

        [HttpGet("/automovel/placa/{nrplaca}")]
        public async Task<IActionResult> Get([RegularExpression("^[a-zA-Z]{3}[0-9][A-Za-z0-9][0-9]{2}$")]string nrplaca)
        {
            var response = await _automovelService.SearchCar(nrplaca);
            if (response.HttpCode == HttpStatusCode.OK)
            {
                return Ok(response.ReturnData);
            }
            else
            {
                return StatusCode((int)response.HttpCode, response.ReturnError);
            }
        }

        [HttpGet("/automovel/placa/dinamica/{quantidade}")]
        public List<string> GerarPlacasDinamicas([RegularExpression("^[0-9]*$")] int quantidade)
        {
            var plates = _getRandomPlate.GenerateRandomPlates(quantidade);
            return plates;
        }
    }
}

            