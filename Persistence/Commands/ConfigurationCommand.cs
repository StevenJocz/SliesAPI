using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SLIES.Infrastructure;
using SLIES.Domain.DTOs.GenralesDTOs;
using SLIES.Domain.DTOs.ConfigurationDTOs;

namespace SLIES.Persistence.Commands
{
    public interface IConfigurationCommand
    {
        // TypeDocument
        Task<RespuestasDTO> createTypeDocument(TypeDocumentDTO typeDocumentDTO);
        Task<RespuestasDTO> UpdateTypeDocument(TypeDocumentDTO typeDocumentDTO);

        // CoursesType
        Task<RespuestasDTO> createCoursesType(CoursesTypeDTOs coursesTypeDTOs);
        Task<RespuestasDTO> UpdateCoursesType(CoursesTypeDTOs coursesTypeDTOs);

        // CoursesCategories
        Task<RespuestasDTO> createCoursesCategories(CoursesCategoriesDTOs coursesCategoriesDTOs);
        Task<RespuestasDTO> UpdateCoursesCategories(CoursesCategoriesDTOs coursesCategoriesDTOs);

        // TypeAttendees
        Task<RespuestasDTO> createTypeAttendees(TypeAttendeesDTOs typeAttendeesDTOs);
        Task<RespuestasDTO> UpdateTypeAttendees(TypeAttendeesDTOs typeAttendeesDTOs);

        // FrequentlyQuestions
        Task<RespuestasDTO> createFrequentlyQuestions(FrequentlyQuestionsDTOs frequentlyQuestionsDTOs);
        Task<RespuestasDTO> UpdateFrequentlyQuestions(FrequentlyQuestionsDTOs frequentlyQuestionsDTOs);

        // Dependence
        Task<RespuestasDTO> createDependence(DependenceDTOs dependenceDTOs);
        Task<RespuestasDTO> UpdateDependence(DependenceDTOs dependenceDTOs);
    }

    public class ConfigurationCommand : IConfigurationCommand, IDisposable
    {
        private readonly SLIESDbContext _context = null;
        private readonly ILogger<ConfigurationCommand> _logger;
        private readonly IConfiguration _configuration;

        public ConfigurationCommand(ILogger<ConfigurationCommand> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            string? connectionString = _configuration.GetConnectionString("Connection");
            _context = new SLIESDbContext(connectionString);
        }

        #region implementacion Disponse
        bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposed = true;
            }
        }
        #endregion

        #region TIPO DE DOCUMENTOS

        public async Task<RespuestasDTO> createTypeDocument(TypeDocumentDTO typeDocumentDTO)
        {
            _logger.LogTrace("Iniciando metodo ConfigurationCommand.createTypeDocument...");
            try
            {
                var newTypeDocumentDTO = new TypeDocumentDTO
                {
                    id = typeDocumentDTO.id,
                    abbreviacion = typeDocumentDTO.abbreviacion,
                    nombre = typeDocumentDTO.nombre,
                    activo = typeDocumentDTO.activo,
                };

                var TypeDocumentE = TypeDocumentDTO.CreateE(newTypeDocumentDTO);
                await _context.TypeDocumentEs.AddAsync(TypeDocumentE);
                await _context.SaveChangesAsync();
                if (TypeDocumentE.id_type_document != 0)
                {
                    return new RespuestasDTO
                    {
                        resultado = true,
                        message = "¡Se ha añadido el tipo de documento exitosamente!",
                    };
                }
                else
                {
                    return new RespuestasDTO
                    {
                        resultado = false,
                        message = "¡No se pudo agregar el tipo de documento! Por favor, inténtalo de nuevo más tarde.",
                    };
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo ConfigurationCommand.createTypeDocument...");
                throw;
            }
        }

        public async Task<RespuestasDTO> UpdateTypeDocument(TypeDocumentDTO typeDocumentDTO)
        {
            _logger.LogTrace("Iniciando metodo ConfigurationCommand.UpdateTypeDocument...");
            try
            {
                var existingTypeDocument = await _context.TypeDocumentEs.FirstOrDefaultAsync(x => x.id_type_document == typeDocumentDTO.id);

                if (existingTypeDocument != null)
                {
                    existingTypeDocument.s_abbreviation = typeDocumentDTO.abbreviacion;
                    existingTypeDocument.s_name = typeDocumentDTO.nombre;
                    existingTypeDocument.byte_activo = typeDocumentDTO.activo;

                    _context.TypeDocumentEs.Update(existingTypeDocument);
                    await _context.SaveChangesAsync();

                    return new RespuestasDTO
                    {
                        resultado = true,
                        message = "¡Se ha actualizado el tipo de documento exitosamente!",
                    };
                }
                else
                {
                    return new RespuestasDTO
                    {
                        resultado = false,
                        message = "¡No se pudo encontrar el tipo de documento a actualizar! Por favor, verifica los datos.",
                    };
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo ConfigurationCommand.UpdateTypeDocument...");
                throw;
            }
        }
        #endregion

        #region MODALIDAD
        public async Task<RespuestasDTO> createCoursesType(CoursesTypeDTOs coursesTypeDTOs)
        {
            _logger.LogTrace("Iniciando metodo ConfigurationCommand.createCoursesType...");
            try
            {
                var newCoursesTypeDTO = new CoursesTypeDTOs
                {
                    id = coursesTypeDTOs.id,
                    codigo = coursesTypeDTOs.codigo,
                    nombre = coursesTypeDTOs.nombre,
                    activo = coursesTypeDTOs.activo
                };

                var CoursesTypeE = CoursesTypeDTOs.CreateE(newCoursesTypeDTO);
                await _context.CoursesTypeEs.AddAsync(CoursesTypeE);
                await _context.SaveChangesAsync();
                if (CoursesTypeE.id_courses_type != 0)
                {
                    return new RespuestasDTO
                    {
                        resultado = true,
                        message = "¡Se ha añadido el tipo de curso exitosamente!",
                    };
                }
                else
                {
                    return new RespuestasDTO
                    {
                        resultado = false,
                        message = "¡No se pudo agregar el tipo de curso! Por favor, inténtalo de nuevo más tarde.",
                    };
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo ConfigurationCommand.createCoursesType...");
                throw;
            }
        }

        public async Task<RespuestasDTO> UpdateCoursesType(CoursesTypeDTOs coursesTypeDTOs)
        {
            _logger.LogTrace("Iniciando metodo ConfigurationCommand.UpdateCoursesType...");
            try
            {
                var existingTypeDocument = await _context.CoursesTypeEs.FirstOrDefaultAsync(x => x.id_courses_type == coursesTypeDTOs.id);

                if (existingTypeDocument != null)
                {
                    existingTypeDocument.s_name = coursesTypeDTOs.nombre;
                    existingTypeDocument.byte_active = coursesTypeDTOs.activo;
                    existingTypeDocument.s_code = coursesTypeDTOs.codigo;

                    _context.CoursesTypeEs.Update(existingTypeDocument);
                    await _context.SaveChangesAsync();

                    return new RespuestasDTO
                    {
                        resultado = true,
                        message = "¡Se ha actualizado el tipo de curso exitosamente!",
                    };
                }
                else
                {
                    return new RespuestasDTO
                    {
                        resultado = false,
                        message = "¡No se pudo encontrar el tipo de curso a actualizar! Por favor, verifica los datos.",
                    };
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo ConfigurationCommand.UpdateCoursesType...");
                throw;
            }
        }


        #endregion

        #region CATEGORIAS CURSOS
        public async Task<RespuestasDTO> createCoursesCategories(CoursesCategoriesDTOs coursesCategoriesDTOs)
        {
            _logger.LogTrace("Iniciando metodo ConfigurationCommand.createCoursesCategories...");
            try
            {
                var newCoursesCategoriesDTOs = new CoursesCategoriesDTOs
                {
                    id = coursesCategoriesDTOs.id,
                    nombre = coursesCategoriesDTOs.nombre,
                    activo = coursesCategoriesDTOs.activo,
                    codigo = coursesCategoriesDTOs.codigo,
                };

                var CoursesCategoriesE = CoursesCategoriesDTOs.CreateE(newCoursesCategoriesDTOs);
                await _context.CoursesCategoriesEs.AddAsync(CoursesCategoriesE);
                await _context.SaveChangesAsync();
                if (CoursesCategoriesE.id_courses_categories != 0)
                {
                    return new RespuestasDTO
                    {
                        resultado = true,
                        message = "¡Se ha añadido la categoria de curso exitosamente!",
                    };
                }
                else
                {
                    return new RespuestasDTO
                    {
                        resultado = false,
                        message = "¡No se pudo agregar la categoria de curso! Por favor, inténtalo de nuevo más tarde.",
                    };
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo ConfigurationCommand.createCoursesCategories...");
                throw;
            }
        }

        public async Task<RespuestasDTO> UpdateCoursesCategories(CoursesCategoriesDTOs coursesCategoriesDTOs)
        {
            _logger.LogTrace("Iniciando metodo ConfigurationCommand.UpdateCoursesCategories...");
            try
            {
                var existingCoursesCategories = await _context.CoursesCategoriesEs.FirstOrDefaultAsync(x => x.id_courses_categories == coursesCategoriesDTOs.id);

                if (existingCoursesCategories != null)
                {
                    existingCoursesCategories.s_name = coursesCategoriesDTOs.nombre;
                    existingCoursesCategories.s_code = coursesCategoriesDTOs.codigo;
                    existingCoursesCategories.byte_active = coursesCategoriesDTOs.activo;

                    _context.CoursesCategoriesEs.Update(existingCoursesCategories);
                    await _context.SaveChangesAsync();

                    return new RespuestasDTO
                    {
                        resultado = true,
                        message = "¡Se ha actualizado la categoria de curso exitosamente!",
                    };
                }
                else
                {
                    return new RespuestasDTO
                    {
                        resultado = false,
                        message = "¡No se pudo encontrar la categoria de curso a actualizar! Por favor, verifica los datos.",
                    };
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo ConfigurationCommand.UpdateCoursesCategories...");
                throw;
            }
        }


        #endregion

        #region TIPO ASISTENTES
        public async Task<RespuestasDTO> createTypeAttendees(TypeAttendeesDTOs typeAttendeesDTOs)
        {
            _logger.LogTrace("Iniciando metodo ConfigurationCommand.createTypeAttendees...");
            try
            {
                var newTypeAttendeesDTOs = new TypeAttendeesDTOs
                {
                    id = typeAttendeesDTOs.id,
                    nombre = typeAttendeesDTOs.nombre,
                    activo = typeAttendeesDTOs.activo,
                    codigo = typeAttendeesDTOs.codigo,
                };

                var TypeAttendeesE = TypeAttendeesDTOs.CreateE(newTypeAttendeesDTOs);
                await _context.TypeAttendeesEs.AddAsync(TypeAttendeesE);
                await _context.SaveChangesAsync();
                if (TypeAttendeesE.id_type_attendees != 0)
                {
                    return new RespuestasDTO
                    {
                        resultado = true,
                        message = "¡Se ha añadido el tipo de asistente exitosamente!",
                    };
                }
                else
                {
                    return new RespuestasDTO
                    {
                        resultado = false,
                        message = "¡No se pudo agregar el tipo de asistente! Por favor, inténtalo de nuevo más tarde.",
                    };
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo ConfigurationCommand.createTypeAttendees...");
                throw;
            }
        }

        public async Task<RespuestasDTO> UpdateTypeAttendees(TypeAttendeesDTOs typeAttendeesDTOs)
        {
            _logger.LogTrace("Iniciando metodo ConfigurationCommand.UpdateTypeAttendees...");
            try
            {
                var existingTypeAttendees = await _context.TypeAttendeesEs.FirstOrDefaultAsync(x => x.id_type_attendees == typeAttendeesDTOs.id);

                if (existingTypeAttendees != null)
                {
                    existingTypeAttendees.s_name = typeAttendeesDTOs.nombre;
                    existingTypeAttendees.byte_active = typeAttendeesDTOs.activo;
                    existingTypeAttendees.s_code = typeAttendeesDTOs.codigo;

                    _context.TypeAttendeesEs.Update(existingTypeAttendees);
                    await _context.SaveChangesAsync();

                    return new RespuestasDTO
                    {
                        resultado = true,
                        message = "¡Se ha actualizado el tipo de asistente exitosamente!",
                    };
                }
                else
                {
                    return new RespuestasDTO
                    {
                        resultado = false,
                        message = "¡No se pudo encontrar el tipo de asistente a actualizar! Por favor, verifica los datos.",
                    };
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo ConfigurationCommand.UpdateTypeAttendees...");
                throw;
            }
        }

        #endregion

        #region PREGUNTAS FRECUENTES
        public async Task<RespuestasDTO> createFrequentlyQuestions(FrequentlyQuestionsDTOs frequentlyQuestionsDTOs)
        {
            _logger.LogTrace("Iniciando metodo ConfigurationCommand.createFrequentlyQuestions...");
            try
            {
                var newFrequentlyQuestionsDTOs = new FrequentlyQuestionsDTOs
                {
                    id = frequentlyQuestionsDTOs.id,
                    pregunta = frequentlyQuestionsDTOs.pregunta,
                    respuesta = frequentlyQuestionsDTOs.respuesta,
                    activo = frequentlyQuestionsDTOs.activo
                };

                var FrequentlyQuestionsE = FrequentlyQuestionsDTOs.CreateE(newFrequentlyQuestionsDTOs);
                await _context.FrequentlyQuestionsEs.AddAsync(FrequentlyQuestionsE);
                await _context.SaveChangesAsync();
                if (FrequentlyQuestionsE.id_frequently_questions != 0)
                {
                    return new RespuestasDTO
                    {
                        resultado = true,
                        message = "¡Se ha añadido la pregunta exitosamente!",
                    };
                }
                else
                {
                    return new RespuestasDTO
                    {
                        resultado = false,
                        message = "¡No se pudo agregar la pregunta! Por favor, inténtalo de nuevo más tarde.",
                    };
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo ConfigurationCommand.createFrequentlyQuestions...");
                throw;
            }
        }

        public async Task<RespuestasDTO> UpdateFrequentlyQuestions(FrequentlyQuestionsDTOs frequentlyQuestionsDTOs)
        {
            _logger.LogTrace("Iniciando metodo ConfigurationCommand.UpdateFrequentlyQuestions...");
            try
            {
                var existingFrequentlyQuestions = await _context.FrequentlyQuestionsEs.FirstOrDefaultAsync(x => x.id_frequently_questions == frequentlyQuestionsDTOs.id);

                if (existingFrequentlyQuestions != null)
                {
                    existingFrequentlyQuestions.s_question = frequentlyQuestionsDTOs.pregunta;
                    existingFrequentlyQuestions.s_answer = frequentlyQuestionsDTOs.respuesta;
                    existingFrequentlyQuestions.byte_active = frequentlyQuestionsDTOs.activo;

                    _context.FrequentlyQuestionsEs.Update(existingFrequentlyQuestions);
                    await _context.SaveChangesAsync();

                    return new RespuestasDTO
                    {
                        resultado = true,
                        message = "¡Se ha actualizado la pregunta exitosamente!",
                    };
                }
                else
                {
                    return new RespuestasDTO
                    {
                        resultado = false,
                        message = "¡No se pudo encontrar la pregunta a actualizar! Por favor, verifica los datos.",
                    };
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo ConfigurationCommand.UpdateFrequentlyQuestions...");
                throw;
            }
        }

        #endregion

        #region DEPENDENCIAS

        public async Task<RespuestasDTO> createDependence(DependenceDTOs dependenceDTOs)
        {
            _logger.LogTrace("Iniciando metodo ConfigurationCommand.createDependence...");
            try
            {
                var newDependenceDTOs = new DependenceDTOs
                {
                    id = dependenceDTOs.id,
                    codigo = dependenceDTOs.codigo,
                    nombre = dependenceDTOs.nombre,
                    activo = dependenceDTOs.activo,
                };

                var DependenceE = DependenceDTOs.CreateE(newDependenceDTOs);
                await _context.DependenceEs.AddAsync(DependenceE);
                await _context.SaveChangesAsync();
                if (DependenceE.id_dependence != 0)
                {
                    return new RespuestasDTO
                    {
                        resultado = true,
                        message = "¡Se ha añadido la dependencia exitosamente!",
                    };
                }
                else
                {
                    return new RespuestasDTO
                    {
                        resultado = false,
                        message = "¡No se pudo agregar la dependencia! Por favor, inténtalo de nuevo más tarde.",
                    };
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo ConfigurationCommand.createTypeDocument...");
                throw;
            }
        }

        public async Task<RespuestasDTO> UpdateDependence(DependenceDTOs dependenceDTOs)
        {
            _logger.LogTrace("Iniciando metodo ConfigurationCommand.UpdateDependence...");
            try
            {
                var existingDependence = await _context.DependenceEs.FirstOrDefaultAsync(x => x.id_dependence == dependenceDTOs.id);

                if (existingDependence != null)
                {
                    existingDependence.s_code = dependenceDTOs.codigo;
                    existingDependence.s_name = dependenceDTOs.nombre;
                    existingDependence.byte_active = dependenceDTOs.activo;

                    _context.DependenceEs.Update(existingDependence);
                    await _context.SaveChangesAsync();

                    return new RespuestasDTO
                    {
                        resultado = true,
                        message = "¡Se ha actualizado la dependencia exitosamente!",
                    };
                }
                else
                {
                    return new RespuestasDTO
                    {
                        resultado = false,
                        message = "¡No se pudo encontrar el tipo de documento a actualizar! Por favor, verifica los datos.",
                    };
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo ConfigurationCommand.UpdateDependence...");
                throw;
            }
        }
        #endregion

    }
}
