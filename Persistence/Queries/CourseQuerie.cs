using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SLIES.Domain.DTOs.ConfigurationDTOs;
using SLIES.Domain.DTOs.CourseDTOs;
using SLIES.Domain.DTOs.UserDTOs;
using SLIES.Domain.Entities.ConfigurationE;
using SLIES.Domain.Entities.CourseE;
using SLIES.Domain.Entities.UserE;
using SLIES.Infrastructure;
using SLIES.Persistence.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Persistence.Queries
{
    public interface ICourseQuerie
    {
        Task<List<ListsCourseDTOs>> ListCourses(int accion);
        Task<List<CourseIdDTOs>> CoursesId(int idCurso);
        Task<List<ListsCourseDTOs>> ListCoursesTeacher(int idProfesor);
        Task<List<CoursesModulesDTOs>> ListCoursesModules(int accion, int id);
        Task<List<CoursesModulesThemeDTOs>> ListCoursesTemas(int accion, int id);
    }

    public class CourseQuerie : ICourseQuerie, IDisposable
    {
        private readonly SLIESDbContext _context = null;
        private readonly ILogger<CourseQuerie> _logger;
        private readonly IConfiguration _configuration;

        public CourseQuerie(ILogger<CourseQuerie> logger, IConfiguration configuration)
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

        #region LISTA CURSO
        public async Task<List<ListsCourseDTOs>> ListCourses(int accion)
        {
            _logger.LogTrace("Iniciando metodo CourseQuerie.ListCourses...");
            try
            {

                var expresion = (Expression<Func<CourseE, bool>>)null;
                if (accion == 1)
                {
                    expresion = expresion = x => x.bool_active == true || x.bool_active == false;
                }
                else if (accion == 2)
                {
                    DateTime fechaLimiteInscripcion = DateTime.Now.ToUniversalTime();
                    expresion = expresion = x => x.bool_active == true && x.dt_registrationDeadline >= fechaLimiteInscripcion;
                }

               
                var courses = await _context.CourseEs.Where(expresion).OrderByDescending(x => x.id_courses).ToListAsync();
                var ListCourses = new List<ListsCourseDTOs>();

                foreach (var item in courses)
                {
                    var coursesCategories = await _context.CoursesCategoriesEs.AsNoTracking().FirstOrDefaultAsync(x => x.id_courses_categories == item.fk_tbl_courses_categories);

                    var typeCourses = await _context.CoursesTypeEs.AsNoTracking().FirstOrDefaultAsync(x => x.id_courses_type == item.fk_tbl_courses_type);

                    var list = new ListsCourseDTOs
                    {
                        id = item.id_courses,
                        titulo = item.s_title,
                        descripcion = accion == 1 ? "" : item.s_description,
                        categoría = coursesCategories.s_name,
                        modalidad = typeCourses.s_name,
                        fechaInicio = item.dt_startDate,
                        fechaFin = item.dt_endDate,
                        fechaLimiteInscripcion = item.dt_registrationDeadline,
                        activo = item.bool_active,
                    };

                    ListCourses.Add(list);
                }

                return ListCourses;
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar CourseQuerie.ListCourses");
                throw;
            }

        }
        #endregion

        #region CURSO
        public async Task<List<CourseIdDTOs>> CoursesId(int idCurso)
        {
            _logger.LogTrace("Iniciando metodo CourseQuerie.CoursesId...");
            try
            {
                var courses = await _context.CourseEs.AsNoTracking().FirstOrDefaultAsync(x => x.id_courses == idCurso);

                var coursesCategories = await _context.CoursesCategoriesEs.AsNoTracking().FirstOrDefaultAsync(x => x.id_courses_categories == courses.fk_tbl_courses_categories);

                var typeCourses = await _context.CoursesTypeEs.AsNoTracking().FirstOrDefaultAsync(x => x.id_courses_type == courses.fk_tbl_courses_type);

                var teachers = await _context.CoursesTeacherEs.Where(x => x.fk_tbl_courses == courses.id_courses).ToListAsync();
                var ListTeachers = new List<UsuariosDTOs>();

                foreach (var item in teachers)
                {
                    var userTeacher = await _context.UserTeacherEs.AsNoTracking().FirstOrDefaultAsync(x => x.id_user_teacher == item.fk_tbl_user_teacher);
                    var user = await _context.UserEs.AsNoTracking().FirstOrDefaultAsync(x => x.id_user == userTeacher.fk_tbl_user);

                    var listTe = new UsuariosDTOs
                    {
                        id = userTeacher.id_user_teacher,
                        foto = user.s_photo,
                        nombre = user.s_name,
                        documento = "",
                        correo = "",
                        contacto = "",
                        profesion = userTeacher.s_profession,
                    };

                    ListTeachers.Add(listTe);
                }

                var precios = await _context.CoursesPriceEs.Where(x => x.fk_tbl_courses == courses.id_courses).ToListAsync();
                var ListPrecios = new List<preciosDTOs>();

                foreach (var itemP in precios)
                {
                    var tipoAsistente = await _context.TypeAttendeesEs.AsNoTracking().FirstOrDefaultAsync(x => x.id_type_attendees == itemP.fk_tbl_type_attendees);

                    var listP = new preciosDTOs
                    {
                        id = itemP.id_courses_price,
                        idTipoAsistente = itemP.fk_tbl_type_attendees,
                        tipoAsistente = tipoAsistente.s_name,
                        precio = itemP.m_price.ToString(),
                    };

                    ListPrecios.Add(listP);
                }

                var ListCourses = new List<CourseIdDTOs>();

                var list = new CourseIdDTOs
                {
                    id = courses.id_courses,
                    titulo = courses.s_title,
                    descripcion = courses.s_description,
                    categoría = courses.fk_tbl_courses_categories,
                    nombrecategoría = coursesCategories.s_name,
                    tipoCurso = courses.fk_tbl_courses_type,
                    nombreTipoCurso = typeCourses.s_name,
                    fechaInicio = courses.dt_startDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"), 
                    fechaFin = courses.dt_endDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                    fechaLimiteInscripcion = courses.dt_registrationDeadline.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                    pago = courses.bool_payment,
                    formal = courses.bool_formal,
                    acompanante = courses.bool_accompanist,
                    menorEdad = courses.bool_minors,
                    grupal = courses.bool_group,
                    limitarCupos = courses.bool_limitSeats,
                    cantidadCupos = courses.n_quantitySeats,
                    imagen = courses.s_image,
                    activo = courses.bool_active,
                    lugar = courses.s_location,
                    dependencia = courses.fk_tbl_dependence,
                    profesores = ListTeachers,
                    precios = ListPrecios,
                };

                ListCourses.Add(list);

                return ListCourses;
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar CourseQuerie.CoursesId");
                throw;
            }

        }
        #endregion

        #region LISTA CURSO PROFESOR
        public async Task<List<ListsCourseDTOs>> ListCoursesTeacher(int idProfesor)
        {
            _logger.LogTrace("Iniciando metodo CourseQuerie.ListCoursesTeacher...");
            try
            {
                var teacherUser = await _context.UserTeacherEs.AsNoTracking().FirstOrDefaultAsync(x => x.fk_tbl_user == idProfesor);

                var teacherCourses = await _context.CoursesTeacherEs.Where(x => x.fk_tbl_user_teacher == teacherUser.id_user_teacher).ToListAsync();

                var ListCourses = new List<ListsCourseDTOs>();

                foreach (var item in teacherCourses)
                {
                    var courses = await _context.CourseEs.AsNoTracking().FirstOrDefaultAsync(x => x.id_courses == item.fk_tbl_courses);

                    var coursesCategories = await _context.CoursesCategoriesEs.AsNoTracking().FirstOrDefaultAsync(x => x.id_courses_categories == courses.fk_tbl_courses_categories);

                    var typeCourses = await _context.CoursesTypeEs.AsNoTracking().FirstOrDefaultAsync(x => x.id_courses_type == courses.fk_tbl_courses_type);

                    var list = new ListsCourseDTOs
                    {
                        id = courses.id_courses,
                        titulo = courses.s_title,
                        categoría = coursesCategories.s_name,
                        modalidad = typeCourses.s_name,
                        fechaInicio = courses.dt_startDate,
                        fechaFin = courses.dt_endDate,
                        fechaLimiteInscripcion = courses.dt_registrationDeadline,
                        activo = courses.bool_active,
                    };

                    ListCourses.Add(list);
                }

                return ListCourses;
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar CourseQuerie.ListCoursesTeacher");
                throw;
            }

        }
        #endregion

        #region LISTA MÓDULO
        public async Task<List<CoursesModulesDTOs>> ListCoursesModules(int accion, int id)
        {
            _logger.LogTrace("Iniciando metodo CourseQuerie.ListCoursesModules...");
            try
            {
                var expresion = (Expression<Func<CoursesModulesE, bool>>)null;
                if (accion == 1)
                {
                    expresion = expresion = x => x.fk_tbl_courses == id;
                }
                else if (accion == 2)
                {
                    expresion = expresion = x => x.id_courses_modules == id;
                }

                var modulos = await _context.CoursesModulesEs.Where(expresion).OrderBy(x => x.id_courses_modules).ToListAsync();

                var ListModulos = new List<CoursesModulesDTOs>();

                foreach (var item in modulos)
                {
                    var user = await _context.UserEs.AsNoTracking().FirstOrDefaultAsync(x => x.id_user == item.fk_tbl_user);

                    var list = new CoursesModulesDTOs
                    {
                        id = item.id_courses_modules,
                        idCurso = item.fk_tbl_courses,
                        titulo = item.s_title,
                        idUsuario = item.fk_tbl_user,
                        fechaRegistro = item.dt_registration,
                        nombreUsuario = user.s_name
                    };

                    ListModulos.Add(list);
                }

                return ListModulos;
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar CourseQuerie.ListCoursesModules");
                throw;
            }

        }
        #endregion

        #region LISTA TEMAS
        public async Task<List<CoursesModulesThemeDTOs>> ListCoursesTemas(int accion, int id)
        {
            _logger.LogTrace("Iniciando metodo CourseQuerie.ListCoursesTemas...");
            try
            {
                var expresion = (Expression<Func<CoursesModulesThemeE, bool>>)null;
                if (accion == 1)
                {
                    expresion = expresion = x => x.fkt_bl_courses_modules == id;
                }
                else if (accion == 2)
                {
                    expresion = expresion = x => x.id_courses_modules_theme == id;
                }

                var temas = await _context.CoursesModulesThemeEs.Where(expresion).OrderBy(x => x.id_courses_modules_theme).ToListAsync();

                var ListTemas = new List<CoursesModulesThemeDTOs>();

                foreach (var item in temas)
                {
                    var user = await _context.UserEs.AsNoTracking().FirstOrDefaultAsync(x => x.id_user == item.fk_tbl_user);

                    var list = new CoursesModulesThemeDTOs
                    {
                        id = item.id_courses_modules_theme,
                        idModulo = item.fkt_bl_courses_modules,
                        titulo = item.s_title,
                        descripcion = item.s_description,
                        video = item.s_video,
                        enlace = item.s_link,
                        idUsuario = item.fk_tbl_user,
                        fechaRegistro = item.dt_registration,
                        nombreUsuario = user.s_name
                    };

                    ListTemas.Add(list);
                }

                return ListTemas;
            }
            catch (Exception)
            {
                _logger.LogError("Error al iniciar CourseQuerie.ListCoursesTemas");
                throw;
            }

        }
        #endregion
    }
}
