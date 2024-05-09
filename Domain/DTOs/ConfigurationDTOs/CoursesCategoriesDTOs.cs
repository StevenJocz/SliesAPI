using SLIES.Domain.Entities.ConfigurationE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Domain.DTOs.ConfigurationDTOs
{
    public class CoursesCategoriesDTOs
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public bool activo { get; set; }
        

        public static CoursesCategoriesDTOs CreateDTO(CoursesCategoriesE coursesCategoriesE)
        {
            CoursesCategoriesDTOs coursesCategoriesDTOs = new()
            {
                id = coursesCategoriesE.id_courses_categories,
                nombre = coursesCategoriesE.s_name,
                activo = coursesCategoriesE.byte_active,
                codigo = coursesCategoriesE.s_code,
            };
            return coursesCategoriesDTOs;
        }

        public static CoursesCategoriesE CreateE(CoursesCategoriesDTOs coursesCategoriesDTOs)
        {
            CoursesCategoriesE coursesCategoriesE = new()
            {
                id_courses_categories = coursesCategoriesDTOs.id,
                s_name = coursesCategoriesDTOs.nombre,
                byte_active = coursesCategoriesDTOs.activo,
                s_code = coursesCategoriesDTOs.codigo
            };
            return coursesCategoriesE;
        }
    }
}
