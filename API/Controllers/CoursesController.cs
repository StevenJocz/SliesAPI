using Microsoft.AspNetCore.Mvc;
using SLIES.Domain.DTOs.CourseDTOs;
using SLIES.Domain.DTOs.UserDTOs;
using SLIES.Persistence.Commands;
using SLIES.Persistence.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SLIES.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseCommands _courseCommands;
        private readonly ICourseQuerie _courseQuerie;
        private readonly ILogger<ConfigurationController> _logger;

        public CoursesController(ICourseCommands courseCommands, ICourseQuerie courseQuerie, ILogger<ConfigurationController> logger)
        {
            _courseCommands = courseCommands;
            _courseQuerie = courseQuerie;
            _logger = logger;
        }

        #region CREAR CURSO
        [HttpPost("PostCourse")]
        public async Task<IActionResult> createCourse([FromBody] CourseRegisterDTOs courseRegisterDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando CoursesController.createCourse...");
                var respuesta = await _courseCommands.createCourse(courseRegisterDTOs);
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
                _logger.LogError("Error al iniciar CoursesController.createCourse...");
                throw;
            }
        }
        #endregion

        #region LISTA CURSO
        [HttpGet("GetListCourses")]
        public async Task<IActionResult> ListCourses(int accion)
        {
            _logger.LogInformation("Iniciando CoursesController.ListCourses...");
            try
            {
                var respuesta = await _courseQuerie.ListCourses(accion);

                if (respuesta == null || !respuesta.Any())
                {
                    return BadRequest("No se encontraron cursos registrados. Por favor, intenta nuevamente más tarde.");
                }
                else
                {
                    return Ok(respuesta);
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar CoursesController.ListCourses");
                throw;
            }
        }
        #endregion

        #region CURSO
        [HttpGet("GetIdCourses")]
        public async Task<IActionResult> CoursesId(int idCurso)
        {
            _logger.LogInformation("Iniciando CoursesController.CoursesId...");
            try
            {
                var respuesta = await _courseQuerie.CoursesId(idCurso);

                if (respuesta == null || !respuesta.Any())
                {
                    return BadRequest("No se encontraron el curso registrado. Por favor, intenta nuevamente más tarde.");
                }
                else
                {
                    return Ok(respuesta);
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ConfigurationController.CoursesId");
                throw;
            }
        }
        #endregion

        #region ACTUALIZAR CURSO
        [HttpPut("PutCourse")]
        public async Task<IActionResult> UpdateCourse([FromBody] CourseRegisterDTOs courseRegisterDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando CoursesController.updateCourse...");
                var respuesta = await _courseCommands.updateCourse(courseRegisterDTOs);
                return Ok(respuesta);
            
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar CoursesController.updateCourse...");
                throw;
            }
        }
        #endregion

        #region LISTA CURSO PROFESOR
        [HttpGet("GetListCoursesTeacher")]
        public async Task<IActionResult> ListCoursesTeacher(int idProfesor)
        {
            _logger.LogInformation("Iniciando CoursesController.ListCoursesTeacher...");
            try
            {
                var respuesta = await _courseQuerie.ListCoursesTeacher(idProfesor);

                if (respuesta == null || !respuesta.Any())
                {
                    return Ok("No se encontraron cursos registrados para este profesor. Por favor, intenta nuevamente más tarde.");
                }
                else
                {
                    return Ok(respuesta);
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar CoursesController.ListCoursesTeacher");
                return BadRequest("No se encontraron cursos registrados para este profesor. Por favor, intenta nuevamente más tarde.");
                throw;
            }
        }
        #endregion

        #region CREAR MÓDULO
        [HttpPost("PostCourseModulo")]
        public async Task<IActionResult> createCourseModulo([FromBody] CoursesModulesDTOs coursesModulesDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando CoursesController.createCourseModulo...");
                var respuesta = await _courseCommands.createCourseModulo(coursesModulesDTOs);
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
                _logger.LogError("Error al iniciar CoursesController.createCourseModulo...");
                throw;
            }
        }
        #endregion

        #region LISTA MÓDULO
        [HttpGet("GetListCourseModulos")]
        public async Task<IActionResult> ListCoursesModules(int accion, int id)
        {
            _logger.LogInformation("Iniciando CoursesController.ListCoursesModules...");
            try
            {
                var respuesta = await _courseQuerie.ListCoursesModules(accion, id);

                return Ok(respuesta);
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar CoursesController.ListCoursesModules");
                return BadRequest("No se encontraron módulos registrados. Por favor, intenta nuevamente más tarde.");
                throw;
            }
        }
        #endregion

        #region ACTUALIZAR MÓDULO
        [HttpPut("PutCourseModulo")]
        public async Task<IActionResult> updateCourseModulo([FromBody] CoursesModulesDTOs coursesModulesDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando CoursesController.updateCourseModulo...");
                var respuesta = await _courseCommands.updateCourseModulo(coursesModulesDTOs);
                return Ok(respuesta);

            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar CoursesController.updateCourseModulo...");
                throw;
            }
        }
        #endregion

        #region ELIMINAR MÓDULO
        [HttpDelete("DeleteCourseModulo")]
        public async Task<IActionResult> deleteCourseModulo(int idModulo)
        {
            try
            {
                _logger.LogInformation("Iniciando CoursesController.deleteCourseModulo...");
                var respuesta = await _courseCommands.deleteCourseModulo(idModulo);
                return Ok(respuesta);

            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar CoursesController.deleteCourseModulo...");
                throw;
            }
        }
        #endregion

        #region CREAR TEMA
        [HttpPost("PostCourseTema")]
        public async Task<IActionResult> createCourseTema([FromBody] CoursesModulesThemeDTOs coursesModulesThemeDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando CoursesController.createCourseTema...");
                var respuesta = await _courseCommands.createCourseTema(coursesModulesThemeDTOs);
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
                _logger.LogError("Error al iniciar CoursesController.createCourseTema...");
                throw;
            }
        }
        #endregion

        #region LISTA TEMA
        [HttpGet("GetListCoursesTemas")]
        public async Task<IActionResult> ListCoursesTemas(int accion, int id)
        {
            _logger.LogInformation("Iniciando CoursesController.ListCoursesTemas...");
            try
            {
                var respuesta = await _courseQuerie.ListCoursesTemas(accion, id);
               return Ok(respuesta);
                
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar CoursesController.ListCoursesTemas");
                return BadRequest("No se encontraron temas registrados. Por favor, intenta nuevamente más tarde.");
                throw;
            }
        }
        #endregion

        #region ACTUALIZAR TEMA
        [HttpPut("PutCourseTema")]
        public async Task<IActionResult> updateCourseTema([FromBody] CoursesModulesThemeDTOs coursesModulesThemeDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando CoursesController.updateCourseTema...");
                var respuesta = await _courseCommands.updateCourseTema(coursesModulesThemeDTOs);
                return Ok(respuesta);

            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar CoursesController.updateCourseTema...");
                throw;
            }
        }
        #endregion

        #region ELIMINAR TEMA
        [HttpDelete("DeleteCourseTema")]
        public async Task<IActionResult> deleteCourseTema(int idTema)
        {
            try
            {
                _logger.LogInformation("Iniciando CoursesController.deleteCourseTema...");
                var respuesta = await _courseCommands.deleteCourseTema(idTema);
                return Ok(respuesta);

            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar CoursesController.deleteCourseTema...");
                throw;
            }
        }
        #endregion

    }
}
