using Microsoft.AspNetCore.Mvc;
using SLIES.Domain.DTOs.ConfigurationDTOs;
using SLIES.Domain.Entities.ConfigurationE;
using SLIES.Persistence.Commands;
using SLIES.Persistence.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SLIES.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfigurationCommand _configurationCommand;
        private readonly IConfigurationQuerie _configurationQuerie;
        private readonly ILogger<ConfigurationController> _logger;

        public ConfigurationController(IConfigurationCommand configurationCommand, IConfigurationQuerie configurationQuerie, ILogger<ConfigurationController> logger)
        {
            _configurationCommand = configurationCommand;
            _configurationQuerie = configurationQuerie;
            _logger = logger;
        }

        #region TIPO DE DOCUMENTOS
        [HttpPost("PostTypeDocument")]
        public async Task<IActionResult> createTypeDocument([FromBody] TypeDocumentDTO typeDocumentDTO)
        {
            try
            {
                _logger.LogInformation("Iniciando ConfigurationController.createTypeDocument...");
                var respuesta = await _configurationCommand.createTypeDocument(typeDocumentDTO);
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
                _logger.LogError("Error al iniciar ConfigurationController.createTypeDocument...");
                throw;
            }
        }

        [HttpGet("GetTypeDocument")]
        public async Task<IActionResult> ListTypeDocument(int accion)
        {
            _logger.LogInformation("Iniciando ConfigurationController.ListTypeDocument...");
            try
            {
                var respuesta = await _configurationQuerie.ListTypeDocument(accion);

                if (respuesta == null || !respuesta.Any())
                {
                    return BadRequest("No se encontraron documentos registrados. Por favor, intenta nuevamente más tarde.");
                }
                else
                {
                    return Ok(respuesta);
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ConfigurationController.ListTypeDocument");
                throw;
            }
        }

        [HttpGet("GetIdTypeDocument")]
        public async Task<IActionResult> TypeDocumentId(int id)
        {
            _logger.LogInformation("Iniciando ConfigurationController.ListTypeDocument...");
            try
            {
                var respuesta = await _configurationQuerie.ListTypeDocumentId(id);

                if (respuesta == null || !respuesta.Any())
                {
                    return BadRequest("No se encontraron documentos registrados. Por favor, intenta nuevamente más tarde.");
                }
                else
                {
                    return Ok(respuesta);
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ConfigurationController.ListTypeDocument");
                throw;
            }
        }

        [HttpPut("PutTypeDocument")]
        public async Task<IActionResult> TypeDocumentPut([FromBody] TypeDocumentDTO typeDocumentDTO)
        {
            try
            {
                _logger.LogInformation("Iniciando ConfigurationController.TypeDocumentPut...");
                var respuesta = await _configurationCommand.UpdateTypeDocument(typeDocumentDTO);
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
                _logger.LogError("Error al iniciar ConfigurationController.TypeDocumentPut...");
                throw;
            }
        }
        #endregion

        #region MODALIDAD

        [HttpPost("PostCoursesType")]
        public async Task<IActionResult> createCoursesType([FromBody] CoursesTypeDTOs coursesTypeDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando ConfigurationController.createCoursesType...");
                var respuesta = await _configurationCommand.createCoursesType(coursesTypeDTOs);
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
                _logger.LogError("Error al iniciar ConfigurationController.createCoursesType...");
                throw;
            }
        }

        [HttpGet("GetCoursesType")]
        public async Task<IActionResult> ListCoursesType(int accion)
        {
            _logger.LogInformation("Iniciando ConfigurationController.ListCoursesType...");
            try
            {
                var respuesta = await _configurationQuerie.ListCoursesType(accion);

                if (respuesta == null || !respuesta.Any())
                {
                    return BadRequest("No se encontraron tipos de cursos registrados. Por favor, intenta nuevamente más tarde.");
                }
                else
                {
                    return Ok(respuesta);
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ConfigurationController.ListCoursesType");
                throw;
            }
        }

        [HttpGet("GetIdCoursesType")]
        public async Task<IActionResult> ListCoursesTypeId(int id)
        {
            _logger.LogInformation("Iniciando ConfigurationController.ListCoursesTypeId...");
            try
            {
                var respuesta = await _configurationQuerie.ListCoursesTypeId(id);

                if (respuesta == null || !respuesta.Any())
                {
                    return BadRequest("No se encontraron el tipo curso registrados. Por favor, intenta nuevamente más tarde.");
                }
                else
                {
                    return Ok(respuesta);
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ConfigurationController.ListCoursesTypeId");
                throw;
            }
        }

        [HttpPut("PutCoursesType")]
        public async Task<IActionResult> UpdateCoursesType([FromBody] CoursesTypeDTOs coursesTypeDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando ConfigurationController.UpdateCoursesType...");
                var respuesta = await _configurationCommand.UpdateCoursesType(coursesTypeDTOs);
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
                _logger.LogError("Error al iniciar ConfigurationController.UpdateCoursesType...");
                throw;
            }
        }
        #endregion

        #region CATEGORIAS CURSOS

        [HttpPost("PostCoursesCategories")]
        public async Task<IActionResult> createCoursesCategories([FromBody] CoursesCategoriesDTOs coursesCategoriesDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando ConfigurationController.createCoursesType...");
                var respuesta = await _configurationCommand.createCoursesCategories(coursesCategoriesDTOs);
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
                _logger.LogError("Error al iniciar ConfigurationController.createCoursesType...");
                throw;
            }
        }

        [HttpGet("GetCoursesCategories")]
        public async Task<IActionResult> ListCoursesCategories(int accion)
        {
            _logger.LogInformation("Iniciando ConfigurationController.ListCoursesCategories...");
            try
            {
                var respuesta = await _configurationQuerie.ListCoursesCategories(accion);

                if (respuesta == null || !respuesta.Any())
                {
                    return BadRequest("No se encontraron categorias registradas. Por favor, intenta nuevamente más tarde.");
                }
                else
                {
                    return Ok(respuesta);
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ConfigurationController.ListCoursesCategories");
                throw;
            }
        }

        [HttpGet("GetIdCoursesCategories")]
        public async Task<IActionResult> ListCoursesCategoriesId(int id)
        {
            _logger.LogInformation("Iniciando ConfigurationController.ListCoursesCategoriesId...");
            try
            {
                var respuesta = await _configurationQuerie.ListCoursesCategoriesId(id);

                if (respuesta == null || !respuesta.Any())
                {
                    return BadRequest("No se encontraron la categorias registrada. Por favor, intenta nuevamente más tarde.");
                }
                else
                {
                    return Ok(respuesta);
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ConfigurationController.ListCoursesCategoriesId");
                throw;
            }
        }

        [HttpPut("PutCategories")]
        public async Task<IActionResult> UpdateCoursesCategories([FromBody] CoursesCategoriesDTOs coursesCategoriesDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando ConfigurationController.UpdateCoursesCategories...");
                var respuesta = await _configurationCommand.UpdateCoursesCategories(coursesCategoriesDTOs);
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
                _logger.LogError("Error al iniciar ConfigurationController.UpdateCoursesCategories...");
                throw;
            }
        }
        #endregion

        #region TIPO ASISTENTE

        [HttpPost("PostTypeAttendees")]
        public async Task<IActionResult> createTypeAttendees([FromBody] TypeAttendeesDTOs typeAttendeesDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando ConfigurationController.createTypeAttendees...");
                var respuesta = await _configurationCommand.createTypeAttendees(typeAttendeesDTOs);
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
                _logger.LogError("Error al iniciar ConfigurationController.createTypeAttendees...");
                throw;
            }
        }

        [HttpGet("GetTypeAttendees")]
        public async Task<IActionResult> ListTypeAttendees(int accion)
        {
            _logger.LogInformation("Iniciando ConfigurationController.ListTypeAttendees...");
            try
            {
                var respuesta = await _configurationQuerie.ListTypeAttendeess(accion);

                if (respuesta == null || !respuesta.Any())
                {
                    return BadRequest("No se encontraron tipos de asistentes registrados registradas. Por favor, intenta nuevamente más tarde.");
                }
                else
                {
                    return Ok(respuesta);
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ConfigurationController.ListTypeAttendees");
                throw;
            }
        }

        [HttpGet("GetIdTypeAttendees")]
        public async Task<IActionResult> ListTypeAttendeesId(int id)
        {
            _logger.LogInformation("Iniciando ConfigurationController.ListTypeAttendeesId...");
            try
            {
                var respuesta = await _configurationQuerie.ListTypeAttendeesId(id);

                if (respuesta == null || !respuesta.Any())
                {
                    return BadRequest("No se encontraron el tipo de asistente registrada. Por favor, intenta nuevamente más tarde.");
                }
                else
                {
                    return Ok(respuesta);
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ConfigurationController.ListTypeAttendeesId");
                throw;
            }
        }

        [HttpPut("PutTypeAttendees")]
        public async Task<IActionResult> UpdateTypeAttendees([FromBody] TypeAttendeesDTOs typeAttendeesDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando ConfigurationController.UpdateTypeAttendees...");
                var respuesta = await _configurationCommand.UpdateTypeAttendees(typeAttendeesDTOs);
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
                _logger.LogError("Error al iniciar ConfigurationController.UpdateTypeAttendees...");
                throw;
            }
        }
        #endregion

        #region  PREGUNTAS FRECUENTES

        [HttpPost("PostFrequentlyQuestions")]
        public async Task<IActionResult> createFrequentlyQuestions([FromBody] FrequentlyQuestionsDTOs frequentlyQuestionsDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando ConfigurationController.createFrequentlyQuestions...");
                var respuesta = await _configurationCommand.createFrequentlyQuestions(frequentlyQuestionsDTOs);
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
                _logger.LogError("Error al iniciar ConfigurationController.createFrequentlyQuestions...");
                throw;
            }
        }

        [HttpGet("GetFrequentlyQuestions")]
        public async Task<IActionResult> ListFrequentlyQuestionss(int accion)
        {
            _logger.LogInformation("Iniciando ConfigurationController.ListFrequentlyQuestions...");
            try
            {
                var respuesta = await _configurationQuerie.ListFrequentlyQuestions(accion);

                if (respuesta == null || !respuesta.Any())
                {
                    return BadRequest("No se encontraron las preguntas registradas. Por favor, intenta nuevamente más tarde.");
                }
                else
                {
                    return Ok(respuesta);
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ConfigurationController.ListTypeAttendees");
                throw;
            }
        }

        [HttpGet("GetIdFrequentlyQuestions")]
        public async Task<IActionResult> ListFrequentlyQuestionsId(int id)
        {
            _logger.LogInformation("Iniciando ConfigurationController.ListFrequentlyQuestionsId...");
            try
            {
                var respuesta = await _configurationQuerie.ListFrequentlyQuestionsId(id);

                if (respuesta == null || !respuesta.Any())
                {
                    return BadRequest("No se encontraron la pregunta registrada. Por favor, intenta nuevamente más tarde.");
                }
                else
                {
                    return Ok(respuesta);
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ConfigurationController.ListFrequentlyQuestionsId");
                throw;
            }
        }

        [HttpPut("PutFrequentlyQuestions")]
        public async Task<IActionResult> UpdateFrequentlyQuestions([FromBody] FrequentlyQuestionsDTOs frequentlyQuestionssDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando ConfigurationController.UpdateTypeAttendees...");
                var respuesta = await _configurationCommand.UpdateFrequentlyQuestions(frequentlyQuestionssDTOs);
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
                _logger.LogError("Error al iniciar ConfigurationController.UpdateFrequentlyQuestion...");
                throw;
            }
        }
        #endregion

        #region DEPENDENCIA

        [HttpPost("PostDependence")]
        public async Task<IActionResult> createDependence([FromBody] DependenceDTOs dependenceDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando ConfigurationController.createDependence...");
                var respuesta = await _configurationCommand.createDependence(dependenceDTOs);
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
                _logger.LogError("Error al iniciar ConfigurationController.createDependence...");
                throw;
            }
        }

        [HttpGet("GetDependence")]
        public async Task<IActionResult> ListDependence(int accion)
        {
            _logger.LogInformation("Iniciando ConfigurationController.ListDependence...");
            try
            {
                var respuesta = await _configurationQuerie.ListDependence(accion);

                if (respuesta == null || !respuesta.Any())
                {
                    return BadRequest("No se encontraron dependencias registrados registradas. Por favor, intenta nuevamente más tarde.");
                }
                else
                {
                    return Ok(respuesta);
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ConfigurationController.ListDependence");
                throw;
            }
        }

        [HttpGet("GetIdDependence")]
        public async Task<IActionResult> ListDependenceId(int id)
        {
            _logger.LogInformation("Iniciando ConfigurationController.ListDependenceId...");
            try
            {
                var respuesta = await _configurationQuerie.ListDependenceId(id);

                if (respuesta == null || !respuesta.Any())
                {
                    return BadRequest("No se encontraron la dependencia registrada. Por favor, intenta nuevamente más tarde.");
                }
                else
                {
                    return Ok(respuesta);
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ConfigurationController.ListDependenceId");
                throw;
            }
        }

        [HttpPut("PutDependence")]
        public async Task<IActionResult> UpdateDependence([FromBody] DependenceDTOs dependenceDTOs)
        {
            try
            {
                _logger.LogInformation("Iniciando ConfigurationController.UpdateDependence...");
                var respuesta = await _configurationCommand.UpdateDependence(dependenceDTOs);
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
                _logger.LogError("Error al iniciar ConfigurationController.UpdateDependence...");
                throw;
            }
        }
        #endregion
    }
}
