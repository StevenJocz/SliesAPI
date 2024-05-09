using SLIES.Domain.DTOs.ConfigurationDTOs;
using SLIES.Domain.DTOs.UserDTOs;
using SLIES.Domain.Entities.CourseE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Domain.DTOs.CourseDTOs
{
    public class CourseDTOs
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public int categoriaId { get; set; }
        public int tipoId { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public DateTime fechaLimiteInscripcion { get; set; }
        public bool pago { get; set; }
        public bool formal { get; set; }
        public bool acompanante { get; set; }
        public bool menorEdad { get; set; }
        public bool grupal { get; set; }
        public bool limitarCupos { get; set; }
        public int cantidadCupos { get; set; }
        public string imagen { get; set; }
        public bool activo { get; set; }
        public int? creador { get; set; }
        public DateTime? fechaRegistro { get; set; }
        public string lugar { get; set; }
        public int dependencia { get; set; }

        public static CourseDTOs CreateDTO(CourseE courseE)
        {
            CourseDTOs courseDTOs  = new()
            {
                id = courseE.id_courses,
                titulo = courseE.s_title,
                descripcion = courseE.s_description,
                categoriaId = courseE.fk_tbl_courses_categories,
                tipoId = courseE.fk_tbl_courses_type,
                fechaInicio = courseE.dt_startDate,
                fechaFin = courseE.dt_endDate,
                fechaLimiteInscripcion = courseE.dt_registrationDeadline,
                pago = courseE.bool_payment,
                formal = courseE.bool_formal,
                acompanante = courseE.bool_accompanist,
                menorEdad = courseE.bool_minors,
                grupal = courseE.bool_group,
                limitarCupos = courseE.bool_limitSeats,
                cantidadCupos = courseE.n_quantitySeats,
                imagen = courseE.s_image,
                activo = courseE.bool_active,
                creador = courseE.fk_tbl_user,
                fechaRegistro = courseE.dt_registration,
                lugar = courseE.s_location,
                dependencia = courseE.fk_tbl_dependence,

            };
            return courseDTOs;
        }

        public static CourseE CreateE(CourseDTOs courseDTOs)
        {
            CourseE courseE = new()
            {
                id_courses = courseDTOs.id,
                s_title = courseDTOs.titulo,
                s_description = courseDTOs.descripcion,
                fk_tbl_courses_categories = courseDTOs.categoriaId,
                fk_tbl_courses_type = courseDTOs.tipoId,
                dt_startDate = courseDTOs.fechaInicio,
                dt_endDate = courseDTOs.fechaFin,
                dt_registrationDeadline = courseDTOs.fechaLimiteInscripcion,
                bool_payment = courseDTOs.pago,
                bool_formal = courseDTOs.formal,
                bool_accompanist = courseDTOs.acompanante,
                bool_minors = courseDTOs.menorEdad,
                bool_group = courseDTOs.grupal,
                bool_limitSeats = courseDTOs.limitarCupos,
                n_quantitySeats = courseDTOs.cantidadCupos,
                s_image = courseDTOs.imagen,
                bool_active = courseDTOs.activo,
                fk_tbl_user = courseDTOs.creador,
                dt_registration = courseDTOs.fechaRegistro,
                s_location = courseDTOs.lugar,
                fk_tbl_dependence = courseDTOs.dependencia,

            };
            return courseE;
        }
    }

    public class CourseRegisterDTOs
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public int categoría { get; set; }
        public string? nombrecategoría { get; set; }
        public int tipoCurso { get; set; }
        public string? nombreTipoCurso { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public DateTime fechaLimiteInscripcion { get; set; }
        public bool pago { get; set; }
        public bool formal { get; set; }
        public bool acompanante { get; set; }
        public bool menorEdad { get; set; }
        public bool grupal { get; set; }
        public bool limitarCupos { get; set; }
        public int cantidadCupos { get; set; }
        public string imagen { get; set; }
        public string? baseImagen { get; set; }
        public bool activo { get; set; }
        public int? creador { get; set; }
        public string lugar { get; set; }
        public int dependencia { get; set; }
        public DateTime? fechaRegistro { get; set; }
        public List<UsuariosDTOs> profesores { get; set; }
        public List<preciosDTOs>? precios { get; set; }
    }

    public class CourseIdDTOs
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public int categoría { get; set; }
        public string? nombrecategoría { get; set; }
        public int tipoCurso { get; set; }
        public string? nombreTipoCurso { get; set; }
        public string fechaInicio { get; set; }
        public string fechaFin { get; set; }
        public string fechaLimiteInscripcion { get; set; }
        public bool pago { get; set; }
        public bool formal { get; set; }
        public bool acompanante { get; set; }
        public bool menorEdad { get; set; }
        public bool grupal { get; set; }
        public bool limitarCupos { get; set; }
        public int cantidadCupos { get; set; }
        public string imagen { get; set; }
        public bool activo { get; set; }
        public int? creador { get; set; }
        public string lugar { get; set; }
        public int dependencia { get; set; }
        public DateTime? fechaRegistro { get; set; }
        public List<UsuariosDTOs> profesores { get; set; }
        public List<preciosDTOs>? precios { get; set; }
    }
    public class preciosDTOs
    {
        public int? id { get; set; }
        public int idTipoAsistente { get; set; }
        public string tipoAsistente { get; set; }
        public string precio { get; set; }
    }

    public class ListsCourseDTOs
    {
        public int id { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public string categoría { get; set; }
        public string modalidad { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public DateTime fechaLimiteInscripcion { get; set; }
        public bool activo { get; set; }
    }


}
