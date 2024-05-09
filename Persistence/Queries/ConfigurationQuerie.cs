using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SLIES.Domain.DTOs.ConfigurationDTOs;
using SLIES.Domain.DTOs.GenralesDTOs;
using SLIES.Domain.Entities.ConfigurationE;
using SLIES.Infrastructure;
using SLIES.Persistence.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Persistence.Queries
{
    public interface IConfigurationQuerie
    {
        // TypeDocument
        Task<List<TypeDocumentDTO>> ListTypeDocument(int accion);
        Task<List<TypeDocumentDTO>> ListTypeDocumentId(int id);

        // CoursesType
        Task<List<CoursesTypeDTOs>> ListCoursesType(int accion);
        Task<List<CoursesTypeDTOs>> ListCoursesTypeId(int id);

        // CoursesCategories
        Task<List<CoursesCategoriesDTOs>> ListCoursesCategories(int accion);
        Task<List<CoursesCategoriesDTOs>> ListCoursesCategoriesId(int id);

        // TypeAttendees
        Task<List<TypeAttendeesDTOs>> ListTypeAttendeess(int accion);
        Task<List<TypeAttendeesDTOs>> ListTypeAttendeesId(int id);

        // TypeAttendees
        Task<List<FrequentlyQuestionsDTOs>> ListFrequentlyQuestions(int accion);
        Task<List<FrequentlyQuestionsDTOs>> ListFrequentlyQuestionsId(int id);

        // Dependence
        Task<List<DependenceDTOs>> ListDependence(int accion);
        Task<List<DependenceDTOs>> ListDependenceId(int id);
    }

    public class ConfigurationQuerie : IConfigurationQuerie, IDisposable
    {
        private readonly SLIESDbContext _context = null;
        private readonly ILogger<ConfigurationQuerie> _logger;
        private readonly IConfiguration _configuration;

        public ConfigurationQuerie(ILogger<ConfigurationQuerie> logger, IConfiguration configuration)
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

        public async Task<List<TypeDocumentDTO>> ListTypeDocument(int accion)
        {
            _logger.LogTrace("Iniciando metodo ConfigurationQuerie.ListTypeDocument...");
            try
            {
                var expresion = (Expression<Func<TypeDocumentE, bool>>)null;
                if (accion == 1)
                {
                    expresion = expresion = x => x.byte_activo == true || x.byte_activo == false;
                }
                else if (accion == 2)
                {
                    expresion = expresion = x => x.byte_activo == true;
                }

                var typeDocument = await _context.TypeDocumentEs.Where(expresion).ToListAsync();
                var ListTypeDocument = new List<TypeDocumentDTO>();

                foreach (var item in typeDocument)
                {
                    var list = new TypeDocumentDTO
                    {
                        id = item.id_type_document,
                        abbreviacion = item.s_abbreviation,
                        nombre = item.s_name,
                        activo = item.byte_activo,
                    };

                    ListTypeDocument.Add(list);
                }

                return ListTypeDocument;
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ConfigurationQuerie.ListTypeDocument");
                throw;
            }

        }
        
        public async Task<List<TypeDocumentDTO>> ListTypeDocumentId(int id)
        {
            _logger.LogTrace("Iniciando metodo ConfigurationQuerie.ListTypeDocumentId...");
            try
            {

                var typeDocument = await _context.TypeDocumentEs.AsNoTracking().FirstOrDefaultAsync(x => x.id_type_document == id);
                var ListTypeDocument = new List<TypeDocumentDTO>();

                var list = new TypeDocumentDTO
                {
                    id = typeDocument.id_type_document,
                    abbreviacion = typeDocument.s_abbreviation,
                    nombre = typeDocument.s_name,
                    activo = typeDocument.byte_activo
                };

                ListTypeDocument.Add(list);

                return ListTypeDocument;
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ConfigurationQuerie.ListTypeDocumentId");
                throw;
            }

        }
        #endregion

        #region MODALIDAD
        public async Task<List<CoursesTypeDTOs>> ListCoursesType(int accion)
        {
            _logger.LogTrace("Iniciando metodo ConfigurationQuerie.ListCoursesType...");
            try
            {
                var expresion = (Expression<Func<CoursesTypeE, bool>>)null;
                if (accion == 1)
                {
                    expresion = expresion = x => x.byte_active == true || x.byte_active == false;
                }
                else if (accion == 2)
                {
                    expresion = expresion = x => x.byte_active == true;
                }

                var typeCourses = await _context.CoursesTypeEs.Where(expresion).ToListAsync();
                var ListTypeCourses = new List<CoursesTypeDTOs>();

                foreach (var item in typeCourses)
                {
                    var list = new CoursesTypeDTOs
                    {
                        id = item.id_courses_type,
                        codigo = item.s_code,
                        nombre = item.s_name,
                        activo = item.byte_active,

                    };

                    ListTypeCourses.Add(list);
                }

                return ListTypeCourses;
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ConfigurationQuerie.ListCoursesType");
                throw;
            }

        }

        public async Task<List<CoursesTypeDTOs>> ListCoursesTypeId(int id)
        {
            _logger.LogTrace("Iniciando metodo ConfigurationQuerie.ListCoursesTypeId...");
            try
            {

                var typeCourses = await _context.CoursesTypeEs.AsNoTracking().FirstOrDefaultAsync(x => x.id_courses_type == id);
                var ListTypeCourses = new List<CoursesTypeDTOs>();

                var list = new CoursesTypeDTOs
                {
                    id = typeCourses.id_courses_type,
                    codigo = typeCourses.s_code,
                    nombre = typeCourses.s_name,
                    activo = typeCourses.byte_active
                };

                ListTypeCourses.Add(list);

                return ListTypeCourses;
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ConfigurationQuerie.ListCoursesTypeId");
                throw;
            }

        }

        #endregion

        #region CATEGORIAS CURSOS
        public async Task<List<CoursesCategoriesDTOs>> ListCoursesCategories(int accion)
        {
            _logger.LogTrace("Iniciando metodo ConfigurationQuerie.ListCoursesCategories...");
            try
            {
                var expresion = (Expression<Func<CoursesCategoriesE, bool>>)null;
                if (accion == 1)
                {
                    expresion = expresion = x => x.byte_active == true || x.byte_active == false;
                }
                else if (accion == 2)
                {
                    expresion = expresion = x => x.byte_active == true;
                }

                var coursesCategories = await _context.CoursesCategoriesEs.Where(expresion).ToListAsync();
                var ListCoursesCategories = new List<CoursesCategoriesDTOs>();

                foreach (var item in coursesCategories)
                {
                    var list = new CoursesCategoriesDTOs
                    {
                        id = item.id_courses_categories,
                        nombre = item.s_name,
                        activo = item.byte_active,
                        codigo = item.s_code
                    };

                    ListCoursesCategories.Add(list);
                }

                return ListCoursesCategories;
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ConfigurationQuerie.ListCoursesCategories");
                throw;
            }

        }

        public async Task<List<CoursesCategoriesDTOs>> ListCoursesCategoriesId(int id)
        {
            _logger.LogTrace("Iniciando metodo ConfigurationQuerie.ListCoursesCategoriesId...");
            try
            {

                var coursesCategories = await _context.CoursesCategoriesEs.AsNoTracking().FirstOrDefaultAsync(x => x.id_courses_categories == id);
                var ListCoursesCategories = new List<CoursesCategoriesDTOs>();

                var list = new CoursesCategoriesDTOs
                {
                    id = coursesCategories.id_courses_categories,
                    codigo = coursesCategories.s_code,
                    nombre = coursesCategories.s_name,
                    activo = coursesCategories.byte_active,
                    
                };

                ListCoursesCategories.Add(list);

                return ListCoursesCategories;
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ConfigurationQuerie.ListCoursesCategoriesId");
                throw;
            }

        }

        #endregion

        #region TIPO ASISTENTE
        public async Task<List<TypeAttendeesDTOs>> ListTypeAttendeess(int accion)
        {
            _logger.LogTrace("Iniciando metodo ConfigurationQuerie.ListTypeAttendeess...");
            try
            {
                var expresion = (Expression<Func<TypeAttendeesE, bool>>)null;
                if (accion == 1)
                {
                    expresion = expresion = x => x.byte_active == true || x.byte_active == false;
                }
                else if (accion == 2)
                {
                    expresion = expresion = x => x.byte_active == true;
                }

                var typeAttendees = await _context.TypeAttendeesEs.Where(expresion).ToListAsync();
                var ListTypeAttendees = new List<TypeAttendeesDTOs>();

                foreach (var item in typeAttendees)
                {
                    var list = new TypeAttendeesDTOs
                    {
                        id = item.id_type_attendees,
                        nombre = item.s_name,
                        activo = item.byte_active,
                        codigo = item.s_code,
                    };

                    ListTypeAttendees.Add(list);
                }

                return ListTypeAttendees;
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ConfigurationQuerie.ListTypeAttendeess");
                throw;
            }

        }

        public async Task<List<TypeAttendeesDTOs>> ListTypeAttendeesId(int id)
        {
            _logger.LogTrace("Iniciando metodo ConfigurationQuerie.ListTypeAttendeesId...");
            try
            {

                var typeAttendees = await _context.TypeAttendeesEs.AsNoTracking().FirstOrDefaultAsync(x => x.id_type_attendees == id);
                var ListTypeAttendees = new List<TypeAttendeesDTOs>();

                var list = new TypeAttendeesDTOs
                {
                    id = typeAttendees.id_type_attendees,
                    nombre = typeAttendees.s_name,
                    activo = typeAttendees.byte_active,
                    codigo = typeAttendees.s_code,
                };

                ListTypeAttendees.Add(list);

                return ListTypeAttendees;
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ConfigurationQuerie.ListTypeAttendeesId");
                throw;
            }

        }

        #endregion

        #region  PREGUNTAS FRECUENTES
        public async Task<List<FrequentlyQuestionsDTOs>> ListFrequentlyQuestions(int accion)
        {
            _logger.LogTrace("Iniciando metodo ConfigurationQuerie.ListFrequentlyQuestionss...");
            try
            {
                var expresion = (Expression<Func<FrequentlyQuestionsE, bool>>)null;
                if (accion == 1)
                {
                    expresion = expresion = x => x.byte_active == true || x.byte_active == false;
                }
                else if (accion == 2)
                {
                    expresion = expresion = x => x.byte_active == true;
                }

                var frequentlyQuestions = await _context.FrequentlyQuestionsEs.Where(expresion).ToListAsync();
                var ListFrequentlyQuestions = new List<FrequentlyQuestionsDTOs>();

                foreach (var item in frequentlyQuestions)
                {
                    var list = new FrequentlyQuestionsDTOs
                    {
                        id = item.id_frequently_questions,
                        pregunta = item.s_question,
                        respuesta = item.s_answer,
                        activo = item.byte_active,
                    };

                    ListFrequentlyQuestions.Add(list);
                }

                return ListFrequentlyQuestions;
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ConfigurationQuerie.ListFrequentlyQuestions");
                throw;
            }

        }

        public async Task<List<FrequentlyQuestionsDTOs>> ListFrequentlyQuestionsId(int id)
        {
            _logger.LogTrace("Iniciando metodo ConfigurationQuerie.ListFrequentlyQuestionsId...");
            try
            {

                var frequentlyQuestionss = await _context.FrequentlyQuestionsEs.AsNoTracking().FirstOrDefaultAsync(x => x.id_frequently_questions == id);
                var ListFrequentlyQuestions = new List<FrequentlyQuestionsDTOs>();

                var list = new FrequentlyQuestionsDTOs
                {
                    id = frequentlyQuestionss.id_frequently_questions,
                    pregunta = frequentlyQuestionss.s_question,
                    respuesta = frequentlyQuestionss.s_answer,
                    activo = frequentlyQuestionss.byte_active
                };

                ListFrequentlyQuestions.Add(list);

                return ListFrequentlyQuestions;
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ConfigurationQuerie.ListFrequentlyQuestionsId");
                throw;
            }

        }

        #endregion

        #region DEPENDENCIA
        public async Task<List<DependenceDTOs>> ListDependence(int accion)
        {
            _logger.LogTrace("Iniciando metodo ConfigurationQuerie.ListDependence...");
            try
            {
                var expresion = (Expression<Func<DependenceE, bool>>)null;
                if (accion == 1)
                {
                    expresion = expresion = x => x.byte_active == true || x.byte_active == false;
                }
                else if (accion == 2)
                {
                    expresion = expresion = x => x.byte_active == true;
                }

                var typeDependence = await _context.DependenceEs.Where(expresion).ToListAsync();
                var ListDependence = new List<DependenceDTOs>();

                foreach (var item in typeDependence)
                {
                    var list = new DependenceDTOs
                    {
                        id = item.id_dependence,
                        nombre = item.s_name,
                        activo = item.byte_active,
                        codigo = item.s_code,
                    };

                    ListDependence.Add(list);
                }

                return ListDependence;
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ConfigurationQuerie.ListDependence");
                throw;
            }

        }

        public async Task<List<DependenceDTOs>> ListDependenceId(int id)
        {
            _logger.LogTrace("Iniciando metodo ConfigurationQuerie.ListDependenceId...");
            try
            {

                var Dependence = await _context.DependenceEs.AsNoTracking().FirstOrDefaultAsync(x => x.id_dependence == id);
                var ListDependence = new List<DependenceDTOs>();

                var list = new DependenceDTOs
                {
                    id = Dependence.id_dependence,
                    nombre = Dependence.s_name,
                    activo = Dependence.byte_active,
                    codigo = Dependence.s_code,
                };

                ListDependence.Add(list);

                return ListDependence;
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar ConfigurationQuerie.ListDependenceId");
                throw;
            }

        }

        #endregion
    }
}
