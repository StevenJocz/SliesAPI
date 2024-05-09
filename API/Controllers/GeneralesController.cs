using Microsoft.AspNetCore.Mvc;
using SLIES.Domain.DTOs.ConfigurationDTOs;
using SLIES.Persistence.Commands;
using SLIES.Persistence.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SLIES.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralesController : ControllerBase
    {
        private readonly IGeneralesQueries _generalesQueries;
        private readonly ILogger<ConfigurationController> _logger;

        public GeneralesController(IGeneralesQueries generalesQueries, ILogger<ConfigurationController> logger)
        {
            _generalesQueries = generalesQueries;
            _logger = logger;
        }

        #region PAISES
        [HttpGet("GetCountry")]
        public async Task<IActionResult> ListCountry()
        {
            _logger.LogInformation("Iniciando GeneralesController.ListCountry...");
            try
            {
                var respuesta = await _generalesQueries.ListCountry();

                if (respuesta == null || !respuesta.Any())
                {
                    return BadRequest("No se encontraron paises resgitrados. Por favor, intenta nuevamente más tarde.");
                }
                else
                {
                    return Ok(respuesta);
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar GeneralesController.ListCountry");
                throw;
            }
        }
        #endregion

        #region PAISES
        [HttpGet("GetState")]
        public async Task<IActionResult> ListState(int idCountry)
        {
            _logger.LogInformation("Iniciando GeneralesController.ListState...");
            try
            {
                var respuesta = await _generalesQueries.ListState(idCountry);

                if (respuesta == null || !respuesta.Any())
                {
                    return BadRequest("No se encontraron estados registrados. Por favor, intenta nuevamente más tarde.");
                }
                else
                {
                    return Ok(respuesta);
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar GeneralesController.ListState");
                throw;
            }
        }
        #endregion

        #region CIUDADES
        [HttpGet("GetCities")]
        public async Task<IActionResult> ListCities(int idState)
        {
            _logger.LogInformation("Iniciando GeneralesController.ListCities...");
            try
            {
                var respuesta = await _generalesQueries.ListCities(idState);

                if (respuesta == null || !respuesta.Any())
                {
                    return BadRequest("No se encontraron ciudades registradas. Por favor, intenta nuevamente más tarde.");
                }
                else
                {
                    return Ok(respuesta);
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar GeneralesController.ListCities");
                throw;
            }
        }
        #endregion
    }
}
