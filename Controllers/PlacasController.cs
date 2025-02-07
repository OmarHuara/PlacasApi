using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using PlacasAPI.Interfaces;

namespace PlacasAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlacasController : ControllerBase
    {
        private readonly IAutomovelService _automovelService;
        private readonly ILogger<PlacasController> _logger;

        public PlacasController(ILogger<PlacasController> logger, IAutomovelService automovelService)
        {
            _automovelService = automovelService;
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
            List<string> listaDePlacas = new List<string>();
            for (int i = 0; i < quantidade; i++)
            {
                listaDePlacas.Add(gerarPlaca());
            }
            return listaDePlacas;
        }

        private string gerarPlaca()
        {
            StringBuilder placa = new StringBuilder(6);
            placa.Append(GerandoAs3PrimeirasLetra());
            placa.Append(GerandoNumero());
            placa.Append(GerandoNumeroOuLetra());
            placa.Append(GerandoNumero());
            return placa.ToString();
        }

        private string GerandoNumeroOuLetra()
        {
            Random rnd = new();
            if (rnd.Next(0,2) == 0)
            {
                char letra = (char)rnd.Next(65, 90);
                return letra.ToString();
            }
            else
            {
                return GerandoNumero();
            }
        }

        private string GerandoNumero()
        {
            Random rnd = new();
            return rnd.Next(0, 10).ToString();
        }

        private string GerandoAs3PrimeirasLetra()
        {
            StringBuilder letras = new StringBuilder(3);
            Random rnd = new();
            letras.Append((char)rnd.Next(65, 90));
            letras.Append((char)rnd.Next(65, 90));
            letras.Append((char)rnd.Next(65, 90));
            return letras.ToString();
        }
    }
}

            