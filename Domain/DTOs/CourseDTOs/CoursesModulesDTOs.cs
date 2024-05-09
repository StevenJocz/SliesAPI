using SLIES.Domain.Entities.CourseE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Domain.DTOs.CourseDTOs
{
    public class CoursesModulesDTOs
    {
        public int id { get; set; }
        public int idCurso{ get; set; }
        public string titulo { get; set; }
        public int idUsuario { get; set; }
        public DateTime fechaRegistro { get; set; }
        public string? nombreUsuario { get; set; }

        public static CoursesModulesDTOs CreateDTO(CoursesModulesE coursesModulesE)
        {
            CoursesModulesDTOs coursesModulesDTOs = new CoursesModulesDTOs
            {
                id = coursesModulesE.id_courses_modules,
                idCurso = coursesModulesE.fk_tbl_courses,
                titulo = coursesModulesE.s_title,
                idUsuario = coursesModulesE.fk_tbl_user,
                fechaRegistro = coursesModulesE.dt_registration
            };
            return coursesModulesDTOs;
        }

        public static CoursesModulesE CreateE(CoursesModulesDTOs coursesModulesDTOs)
        {
            CoursesModulesE coursesModulesE = new CoursesModulesE
            {
                id_courses_modules = coursesModulesDTOs.id,
                fk_tbl_courses = coursesModulesDTOs.idCurso,
                s_title = coursesModulesDTOs.titulo,
                fk_tbl_user = coursesModulesDTOs.idUsuario,
                dt_registration = coursesModulesDTOs.fechaRegistro
            };
            return coursesModulesE;
        }
    }
}
