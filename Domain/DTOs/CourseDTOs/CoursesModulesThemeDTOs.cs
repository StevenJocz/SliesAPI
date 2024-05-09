using SLIES.Domain.Entities.CourseE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Domain.DTOs.CourseDTOs
{
    public class CoursesModulesThemeDTOs
    {
        public int id { get; set; }
        public int idModulo { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public string video { get; set; }
        public string enlace { get; set; }
        public int idUsuario { get; set; }
        public DateTime fechaRegistro { get; set; }
        public string? nombreUsuario { get; set; }

        public static CoursesModulesThemeDTOs CreateDTO(CoursesModulesThemeE coursesModulesThemeE)
        {
            CoursesModulesThemeDTOs coursesModulesThemeDTOs = new CoursesModulesThemeDTOs
            {
                id = coursesModulesThemeE.id_courses_modules_theme,
                idModulo = coursesModulesThemeE.fkt_bl_courses_modules,
                titulo = coursesModulesThemeE.s_title,
                descripcion = coursesModulesThemeE.s_description,
                video = coursesModulesThemeE.s_video,
                enlace = coursesModulesThemeE.s_link,
                idUsuario = coursesModulesThemeE.fk_tbl_user,
                fechaRegistro = coursesModulesThemeE.dt_registration
            };
            return coursesModulesThemeDTOs;
        }

        public static CoursesModulesThemeE CreateE(CoursesModulesThemeDTOs coursesModulesThemeDTOs)
        {
            CoursesModulesThemeE coursesModulesThemeE = new CoursesModulesThemeE
            {
                id_courses_modules_theme = coursesModulesThemeDTOs.id,
                fkt_bl_courses_modules = coursesModulesThemeDTOs.idModulo,
                s_title = coursesModulesThemeDTOs.titulo,
                s_description = coursesModulesThemeDTOs.descripcion,
                s_video = coursesModulesThemeDTOs.video,
                s_link = coursesModulesThemeDTOs.enlace,
                fk_tbl_user = coursesModulesThemeDTOs.idUsuario,
                dt_registration = coursesModulesThemeDTOs.fechaRegistro
            };
            return coursesModulesThemeE;
        }
    }
}
