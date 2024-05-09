using Microsoft.AspNetCore.Mvc;
using SLIES.Domain.DTOs.ConfigurationDTOs;
using SLIES.Domain.DTOs.UserDTOs;
using SLIES.Persistence.Commands;
using SLIES.Persistence.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SLIES.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserCommands _userCommands;
        private readonly IUserQueries _userQueries;
        private readonly ILogger<ConfigurationController> _logger;

        public UserController(IUserCommands userCommands, IUserQueries userQueries, ILogger<ConfigurationController> logger)
        {
            _userCommands = userCommands;
            _userQueries = userQueries;
            _logger = logger;
        }

        #region CREAR CUENTA
        [HttpPost("PostUser")]
        public async Task<IActionResult> createUser([FromBody] UserDTOs userDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando UserController.createUser...");
                var respuesta = await _userCommands.createUser(userDTOs);
                if (respuesta.resultado == false)
                {
                    return BadRequest(respuesta);
                }
                else
                {
                    return Ok(respuesta);
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar UserController.createUser...");
                throw;
            }
        }
        #endregion


        #region PERMISOS DE USUARIOS
        [HttpGet("GetUserPermission")]
        public async Task<IActionResult> UserPermission(int id)
        {
            _logger.LogInformation("Iniciando UserController.UserPermission...");
            try
            {
                var respuesta = await _userQueries.UserPermission(id);

                if (respuesta == null || !respuesta.Any())
                {
                    return BadRequest("No se encontraron permisos resgitrados. Por favor, intenta nuevamente más tarde.");
                }
                else
                {
                    return Ok(respuesta);
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar UserController.UserPermission");
                throw;
            }
        }
        #endregion

        #region LISTA USER
        [HttpGet("GetListUser")]
        public async Task<IActionResult> ListUser()
        {
            _logger.LogInformation("Iniciando UserController.ListUser...");
            try
            {
                var respuesta = await _userQueries.ListUser();

                if (respuesta == null || !respuesta.Any())
                {
                    return BadRequest("No se encontraron usuarios resgitrados. Por favor, intenta nuevamente más tarde.");
                }
                else
                {
                    return Ok(respuesta);
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar UserController.ListCountry");
                throw;
            }
        }
        #endregion

        #region USER
        [HttpGet("GetIdUser")]
        public async Task<IActionResult> UserId(int idUser)
        {
            _logger.LogInformation("Iniciando UserController.UserId...");
            try
            {
                var respuesta = await _userQueries.UserId(idUser);

                if (respuesta == null)
                {
                    return BadRequest("No se encontraron el usuario resgitrado. Por favor, intenta nuevamente más tarde.");
                }
                else
                {
                    return Ok(respuesta);
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar UserController.UserId");
                throw;
            }
        }
        #endregion

        #region LISTA USER
        [HttpGet("GetListUserTeacher")]
        public async Task<IActionResult> ListUserTeacher()
        {
            _logger.LogInformation("Iniciando UserController.ListUserTeacher...");
            try
            {
                var respuesta = await _userQueries.ListUserTeacher();

                if (respuesta == null || !respuesta.Any())
                {
                    return BadRequest("No se encontraron profesores. Por favor, intenta nuevamente más tarde.");
                }
                else
                {
                    return Ok(respuesta);
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar UserController.ListUserTeacher");
                throw;
            }
        }
        #endregion


        #region ASIGNAR PROFESOR
        [HttpPost("PostAssignTeacher")]
        public async Task<IActionResult> assignTeacher([FromBody] RegistreTeacherDTOs registreTeacherDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando UserController.assignTeacher...");
                var respuesta = await _userCommands.assignTeacher(registreTeacherDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar UserController.assignTeacher...");
                throw;
            }
        }
        #endregion

        #region ASIGNAR ADMINISTRADOR
        [HttpPost("PostAssignAdmin")]
        public async Task<IActionResult> assignAdmin([FromBody] RegistreTeacherDTOs registreTeacherDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando UserController.assignAdmin...");
                var respuesta = await _userCommands.assignAdmin(registreTeacherDTOs);
                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar UserController.assignAdmin...");
                throw;
            }
        }
        #endregion
    }
}
