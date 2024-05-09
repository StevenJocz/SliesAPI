using SLIES.Domain.Entities.ConfigurationE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Domain.DTOs.ConfigurationDTOs
{
    public class CoursesTypeDTOs
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public bool activo { get; set; }

        public static CoursesTypeDTOs CreateDTO(CoursesTypeE coursesTypeE)
        {
            CoursesTypeDTOs coursesTypeDTOs = new()
            {
                id = coursesTypeE.id_courses_type,
                nombre = coursesTypeE.s_name,
                activo = coursesTypeE.byte_active,
                codigo = coursesTypeE.s_code,
            };
            return coursesTypeDTOs;
        }

        public static CoursesTypeE CreateE(CoursesTypeDTOs coursesTypeDTOs)
        {
            CoursesTypeE coursesTypeE = new()
            {
                id_courses_type = coursesTypeDTOs.id,
                s_name = coursesTypeDTOs.nombre,
                byte_active = coursesTypeDTOs.activo,
                s_code = coursesTypeDTOs.codigo
            };
            return coursesTypeE;
        }
    }
}
