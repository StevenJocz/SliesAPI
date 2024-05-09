using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SLIES.Domain.DTOs.CourseDTOs;
using SLIES.Domain.DTOs.GenralesDTOs;
using SLIES.Domain.Entities.CourseE;
using SLIES.Domain.Utilidades;
using SLIES.Infrastructure;
using static System.Net.Mime.MediaTypeNames;

namespace SLIES.Persistence.Commands
{
    public interface ICourseCommands
    {
        Task<RespuestasDTO> createCourse(CourseRegisterDTOs courseRegisterDTOs);
        Task<RespuestasDTO> updateCourse(CourseRegisterDTOs updatedCourseData);
        Task<RespuestasDTO> createCourseModulo(CoursesModulesDTOs coursesModulesDTOs);
        Task<RespuestasDTO> updateCourseModulo(CoursesModulesDTOs coursesModulesDTOs);
        Task<RespuestasDTO> createCourseTema(CoursesModulesThemeDTOs coursesModulesThemeDTOs);
        Task<RespuestasDTO> updateCourseTema(CoursesModulesThemeDTOs coursesModulesThemeDTOs);
        Task<RespuestasDTO> deleteCourseModulo(int IdModulo);
        Task<RespuestasDTO> deleteCourseTema(int idTema);
    }

    public class CourseCommands : ICourseCommands, IDisposable
    {
        private readonly SLIESDbContext _context = null;
        private readonly ILogger<CourseCommands> _logger;
        private readonly IConfiguration _configuration;
        private readonly ISaveImagen _saveImagen;

        public CourseCommands(ILogger<CourseCommands> logger, IConfiguration configuration, ISaveImagen saveImagen)
        {
            _logger = logger;
            _configuration = configuration;
            string? connectionString = _configuration.GetConnectionString("Connection");
            _context = new SLIESDbContext(connectionString);
            _saveImagen = saveImagen;
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


        #region CREAR CURSOS
        public async Task<RespuestasDTO> createCourse(CourseRegisterDTOs courseRegisterDTOs)
        {
            _logger.LogTrace("Iniciando metodo CourseCommands.createCourse...");
            try
            {
                var rutaPhoto = "";
                if (courseRegisterDTOs.imagen != null)
                {
                    string rutaImagen = "wwwroot/ImagenesCourse";
                    rutaPhoto = await _saveImagen.SaveImageAsync(courseRegisterDTOs.baseImagen, rutaImagen);
                }
               
                var curso = new CourseDTOs
                {
                    titulo = courseRegisterDTOs.titulo,
                    descripcion = courseRegisterDTOs.descripcion,
                    categoriaId = courseRegisterDTOs.categoría,
                    tipoId = courseRegisterDTOs.tipoCurso,
                    fechaInicio = courseRegisterDTOs.fechaInicio,
                    fechaFin = courseRegisterDTOs.fechaFin,
                    fechaLimiteInscripcion = courseRegisterDTOs.fechaLimiteInscripcion,
                    pago = courseRegisterDTOs.pago,
                    formal = courseRegisterDTOs.formal,
                    acompanante = courseRegisterDTOs.acompanante,
                    menorEdad = courseRegisterDTOs.menorEdad,
                    grupal = courseRegisterDTOs.grupal,
                    limitarCupos = courseRegisterDTOs.limitarCupos,
                    cantidadCupos = courseRegisterDTOs.cantidadCupos,
                    imagen = rutaPhoto,
                    activo = courseRegisterDTOs.activo,
                    creador = courseRegisterDTOs.creador,
                    fechaRegistro = DateTime.UtcNow,
                    lugar = courseRegisterDTOs.lugar,
                    dependencia = courseRegisterDTOs.dependencia
                };

                var cursosE = CourseDTOs.CreateE(curso);
                await _context.CourseEs.AddAsync(cursosE);
                await _context.SaveChangesAsync();

                if (cursosE.id_courses != 0)
                {
                    if (courseRegisterDTOs.profesores != null && courseRegisterDTOs.profesores.Count > 0)
                    {
                        foreach (var profesor in courseRegisterDTOs.profesores)
                        {
                            var profesores = new CoursesTeacherDTOs
                            {
                                idUsuarioProfesor = profesor.id,
                                idCurso = cursosE.id_courses
                            };

                            var profesorE = CoursesTeacherDTOs.CreateE(profesores);
                            await _context.CoursesTeacherEs.AddAsync(profesorE);
                            await _context.SaveChangesAsync();
                        }
                    }
                    

                    if (courseRegisterDTOs.precios != null && courseRegisterDTOs.precios.Count > 0)
                    {
                        foreach (var precio in courseRegisterDTOs.precios)
                        {
                            var precios = new CoursesPriceDTOs
                            {
                                idCurso = cursosE.id_courses,
                                idAsistente = precio.idTipoAsistente,
                                precio = int.Parse(precio.precio)
                            };

                            var preciosE = CoursesPriceDTOs.CreateE(precios);
                            await _context.CoursesPriceEs.AddAsync(preciosE);
                            await _context.SaveChangesAsync();
                        }
                    }

                    return new RespuestasDTO
                    {
                        resultado = true,
                        message = "¡Se ha añadido el curso exitosamente!",
                    };

                }
                else
                {
                    return new RespuestasDTO
                    {
                        resultado = false,
                        message = "¡No se pudo agregar el curso! Por favor, inténtalo de nuevo más tarde.",
                    };
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo CourseCommands.createCourse...");
                throw;
            }
        }
        #endregion

        #region ACTUALIZAR CURSO
        public async Task<RespuestasDTO> updateCourse(CourseRegisterDTOs updatedCourseData)
        {
            _logger.LogTrace("Iniciando metodo CourseCommands.updateCourse...");
            try
            {
               
                var existingCurso = await _context.CourseEs.FirstOrDefaultAsync(x => x.id_courses == updatedCourseData.id);

                if (existingCurso != null)
                {
                    var teachersToDelete = await _context.CoursesTeacherEs.Where(x => x.fk_tbl_courses == updatedCourseData.id).ToListAsync();
                    
                    if (teachersToDelete.Any())
                    {
                        _context.CoursesTeacherEs.RemoveRange(teachersToDelete);
                        await _context.SaveChangesAsync();
                    }

                    if (updatedCourseData.profesores != null && updatedCourseData.profesores.Count > 0)
                    {
                        foreach (var profesor in updatedCourseData.profesores)
                        {
                            var profesores = new CoursesTeacherDTOs
                            {
                                idUsuarioProfesor = profesor.id,
                                idCurso = updatedCourseData.id
                            };

                            var profesorE = CoursesTeacherDTOs.CreateE(profesores);
                            await _context.CoursesTeacherEs.AddAsync(profesorE);
                            await _context.SaveChangesAsync();
                        }
                    }

                    var preciosToDelete = await _context.CoursesPriceEs.Where(x => x.fk_tbl_courses == updatedCourseData.id).ToListAsync();

                    if (preciosToDelete.Any())
                    {
                        _context.CoursesPriceEs.RemoveRange(preciosToDelete);
                        await _context.SaveChangesAsync();
                    }

                    if (updatedCourseData.precios != null && updatedCourseData.precios.Count > 0)
                    {
                        foreach (var precio in updatedCourseData.precios)
                        {
                            var precios = new CoursesPriceDTOs
                            {
                                idCurso = updatedCourseData.id,
                                idAsistente = precio.idTipoAsistente,
                                precio = int.Parse(precio.precio)
                            };

                            var preciosE = CoursesPriceDTOs.CreateE(precios);
                            await _context.CoursesPriceEs.AddAsync(preciosE);
                            await _context.SaveChangesAsync();
                        }
                    }

                    var rutaPhoto = "";
                    if (updatedCourseData.baseImagen != "")
                    {
                        await _saveImagen.DeleteImage(updatedCourseData.imagen);

                        string rutaImagen = "wwwroot/ImagenesCourse";
                        rutaPhoto = await _saveImagen.SaveImageAsync(updatedCourseData.baseImagen, rutaImagen);
                    }

                    existingCurso.s_title = updatedCourseData.titulo;
                    existingCurso.s_description = updatedCourseData.descripcion;
                    existingCurso.fk_tbl_courses_categories = updatedCourseData.categoría;
                    existingCurso.fk_tbl_courses_type = updatedCourseData.tipoCurso;
                    existingCurso.dt_startDate = updatedCourseData.fechaInicio;
                    existingCurso.dt_endDate = updatedCourseData.fechaFin;
                    existingCurso.dt_registrationDeadline = updatedCourseData.fechaLimiteInscripcion;
                    existingCurso.bool_payment = updatedCourseData.pago;
                    existingCurso.bool_formal = updatedCourseData.formal;
                    existingCurso.bool_accompanist = updatedCourseData.acompanante;
                    existingCurso.bool_minors = updatedCourseData.menorEdad;
                    existingCurso.bool_group = updatedCourseData.grupal;
                    existingCurso.bool_limitSeats = updatedCourseData.limitarCupos;
                    existingCurso.n_quantitySeats = updatedCourseData.cantidadCupos;
                    existingCurso.bool_active = updatedCourseData.activo;
                    existingCurso.s_location = updatedCourseData.lugar;
                    existingCurso.fk_tbl_dependence = updatedCourseData.dependencia;

                    _context.CourseEs.Update(existingCurso);
                    await _context.SaveChangesAsync();

                    return new RespuestasDTO
                    {
                        resultado = true,
                        message = "¡Se ha actualizado el curso exitosamente!",
                    };
                }
                else
                {
                    return new RespuestasDTO
                    {
                        resultado = false,
                        message = "¡No se pudo encontrar el curso a actualizar!",
                    };
                }
                
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo CourseCommands.updateCourse...");
                throw;
            }
        }
        #endregion

        #region CREAR MÓDULOS
        public async Task<RespuestasDTO> createCourseModulo(CoursesModulesDTOs coursesModulesDTOs)
        {
            _logger.LogTrace("Iniciando metodo CourseCommands.createCourseModulo...");
            try
            {
                var modulo = new CoursesModulesDTOs
                {
                    idCurso = coursesModulesDTOs.idCurso,
                    titulo = coursesModulesDTOs.titulo,
                    idUsuario = coursesModulesDTOs.idUsuario,
                    fechaRegistro = DateTime.UtcNow
                };

                var moduloE = CoursesModulesDTOs.CreateE(modulo);
                await _context.CoursesModulesEs.AddAsync(moduloE);
                await _context.SaveChangesAsync();

                if (moduloE.id_courses_modules != 0)
                {
                    return new RespuestasDTO
                    {
                        resultado = true,
                        message = "¡Se ha añadido el módulo exitosamente!",
                    };
                }
                else
                {
                    return new RespuestasDTO
                    {
                        resultado = false,
                        message = "¡No se pudo agregar el módulo! Por favor, inténtalo de nuevo más tarde.",
                    };
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo CourseCommands.createCourseModulo...");
                throw;
            }
        }
        #endregion

        #region ACTUALIZAR MÓDULO
        public async Task<RespuestasDTO> updateCourseModulo(CoursesModulesDTOs coursesModulesDTOs)
        {
            _logger.LogTrace("Iniciando metodo CourseCommands.updateCourseModulo...");
            try
            {
                var existeModulo = await _context.CoursesModulesEs.FirstOrDefaultAsync(x => x.id_courses_modules == coursesModulesDTOs.id);

                if (existeModulo != null)
                {
                    existeModulo.s_title = coursesModulesDTOs.titulo;
                    existeModulo.fk_tbl_user = coursesModulesDTOs.idUsuario;
                    existeModulo.dt_registration = DateTime.UtcNow;

                    _context.CoursesModulesEs.Update(existeModulo);
                    await _context.SaveChangesAsync();

                    return new RespuestasDTO
                    {
                        resultado = true,
                        message = "¡Se ha actualizado el módulo exitosamente!",
                    };
                }
                else
                {
                    return new RespuestasDTO
                    {
                        resultado = false,
                        message = "¡No se pudo encontrar el módulo a actualizar!",
                    };
                }

            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo CourseCommands.updateCourseModulo...");
                throw;
            }
        }
        #endregion

        #region ELIMINAR MÓDULO
        public async Task<RespuestasDTO> deleteCourseModulo(int IdModulo)
        {
            _logger.LogTrace("Iniciando metodo CourseCommands.deleteCourseModulo...");
            try
            {
                var moduloE = await _context.CoursesModulesEs.FirstOrDefaultAsync(x => x.id_courses_modules == IdModulo);
                var temasE = await _context.CoursesModulesThemeEs.Where(x => x.fkt_bl_courses_modules == IdModulo).ToListAsync();

                if (temasE.Any())
                {
                    _context.CoursesModulesThemeEs.RemoveRange(temasE);
                    await _context.SaveChangesAsync();
                }

                _context.CoursesModulesEs.Remove(moduloE);
                await _context.SaveChangesAsync();

                if (await _context.CoursesModulesEs.FindAsync(IdModulo) == null)
                {
                    return new RespuestasDTO
                    {
                        resultado = true,
                        message = "¡Se ha eliminado el módulo exitosamente!",
                    };
                }
                else
                {
                    return new RespuestasDTO
                    {
                        resultado = false,
                        message = "¡No se pudo eliminar el módulo! Por favor, inténtalo de nuevo más tarde.",
                    };
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo CourseCommands.deleteCourseModulo...");
                throw;
            }
        }
        #endregion

        #region CREAR TEMA
        public async Task<RespuestasDTO> createCourseTema(CoursesModulesThemeDTOs coursesModulesThemeDTOs)
        {
            _logger.LogTrace("Iniciando metodo CourseCommands.createCourseTema...");
            try
            {
                var tema = new CoursesModulesThemeDTOs
                {
                    idModulo = coursesModulesThemeDTOs.idModulo,
                    titulo = coursesModulesThemeDTOs.titulo,
                    descripcion = coursesModulesThemeDTOs.descripcion,
                    video = coursesModulesThemeDTOs.video,
                    enlace = coursesModulesThemeDTOs.enlace,
                    idUsuario = coursesModulesThemeDTOs.idUsuario,
                    fechaRegistro = DateTime.UtcNow
                };

                var temaE = CoursesModulesThemeDTOs.CreateE(tema);
                await _context.CoursesModulesThemeEs.AddAsync(temaE);
                await _context.SaveChangesAsync();

                if (temaE.id_courses_modules_theme != 0)
                {
                    return new RespuestasDTO
                    {
                        resultado = true,
                        message = "¡Se ha añadido el tema exitosamente!",
                    };
                }
                else
                {
                    return new RespuestasDTO
                    {
                        resultado = false,
                        message = "¡No se pudo agregar el tema! Por favor, inténtalo de nuevo más tarde.",
                    };
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo CourseCommands.createCourseTema...");
                throw;
            }
        }
        #endregion

        #region ACTUALIZAR TEMA
        public async Task<RespuestasDTO> updateCourseTema(CoursesModulesThemeDTOs coursesModulesThemeDTOs)
        {
            _logger.LogTrace("Iniciando metodo CourseCommands.updateCourseTema...");
            try
            {
                var existeTema = await _context.CoursesModulesThemeEs.FirstOrDefaultAsync(x => x.id_courses_modules_theme == coursesModulesThemeDTOs.id);

                if (existeTema != null)
                {
                    existeTema.s_title = coursesModulesThemeDTOs.titulo;
                    existeTema.s_description = coursesModulesThemeDTOs.descripcion;
                    existeTema.s_video = coursesModulesThemeDTOs.video;
                    existeTema.s_link = coursesModulesThemeDTOs.enlace;
                    existeTema.fk_tbl_user = coursesModulesThemeDTOs.idUsuario;
                    existeTema.dt_registration = DateTime.UtcNow;

                    _context.CoursesModulesThemeEs.Update(existeTema);
                    await _context.SaveChangesAsync();

                    return new RespuestasDTO
                    {
                        resultado = true,
                        message = "¡Se ha actualizado el tema exitosamente!",
                    };
                }
                else
                {
                    return new RespuestasDTO
                    {
                        resultado = false,
                        message = "¡No se pudo encontrar el tema a actualizar!",
                    };
                }

            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo CourseCommands.updateCourseTema...");
                throw;
            }
        }
        #endregion

        #region ELIMINAR TEMA
        public async Task<RespuestasDTO> deleteCourseTema(int idTema)
        {
            _logger.LogTrace("Iniciando metodo CourseCommands.deleteCourseTema...");
            try
            {
                var temaE = await _context.CoursesModulesThemeEs.FirstOrDefaultAsync(x => x.id_courses_modules_theme == idTema);

                _context.CoursesModulesThemeEs.Remove(temaE);
                await _context.SaveChangesAsync();

                if (await _context.CoursesModulesThemeEs.FindAsync(idTema) == null)
                {
                    return new RespuestasDTO
                    {
                        resultado = true,
                        message = "¡Se ha eliminado el tema exitosamente!",
                    };
                }
                else
                {
                    return new RespuestasDTO
                    {
                        resultado = false,
                        message = "¡No se pudo eliminar el tema! Por favor, inténtalo de nuevo más tarde.",
                    };
                }
            }
            catch (Exception)
            {
                _logger.LogError("Error en el metodo CourseCommands.deleteCourseTema...");
                throw;
            }
        }
        #endregion

    }
}
