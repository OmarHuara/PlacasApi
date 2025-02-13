using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PlacasAPI.Dtos;
using PlacasAPI.Utils;

namespace PlacasAPI.Controllers
{
    public class PlacasBaseController : ControllerBase
    {
        private readonly ILogger _logger;

        public PlacasBaseController(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(ILogger));
        }

        internal ActionResult<AutomovelDto> TreatsResponseValueResult(ValueResult<AutomovelDto> result)
        {
            if (!result.IsSuccess())
            {
                _logger.LogWarning($"Error 400 - {JsonSerializer.Serialize(result.Errors)}");
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }

        internal void LogMessage(string message)
        {
            _logger.LogInformation(message);
        }

        internal void LogError(string errorMessage)
        {
            _logger.LogWarning(errorMessage);
        }
    }
}
