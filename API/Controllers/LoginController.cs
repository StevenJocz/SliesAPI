using Microsoft.AspNetCore.Mvc;
using SLIES.Domain.DTOs.UserDTOs;
using SLIES.Persistence.Commands;
using SLIES.Persistence.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SLIES.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginQueries _loginQueries;
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILoginQueries loginQueries, ILogger<LoginController> logger)
        {
            _loginQueries = loginQueries;
            _logger = logger;
        }

        [HttpGet("GetAutenticar")]
        public async Task<IActionResult> Autenticar(string correo, string password)
        {
            _logger.LogInformation("Iniciando LoginController.Autenticar...");
            try
            {
                var resultado_autorizacion = await _loginQueries.Login(correo, password);
                if (resultado_autorizacion == null)
                    return Unauthorized();
                return Ok(resultado_autorizacion);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar LoginController.Autenticar...");
                throw;
            }
        }

    }
}
