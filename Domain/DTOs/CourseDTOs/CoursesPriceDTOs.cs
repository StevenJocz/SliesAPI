using SLIES.Domain.Entities.CourseE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Domain.DTOs.CourseDTOs
{
    public class CoursesPriceDTOs
    {
        public int idCursoPrecio { get; set; }
        public int idCurso { get; set; }
        public int idAsistente { get; set; }
        public int precio { get; set; }

        public static CoursesPriceDTOs CreateDTO(CoursesPriceE coursesPriceE)
        {
            CoursesPriceDTOs coursesPriceDTOs = new CoursesPriceDTOs
            {
                idCursoPrecio = coursesPriceE.id_courses_price,
                idCurso = coursesPriceE.fk_tbl_courses,
                idAsistente = coursesPriceE.fk_tbl_type_attendees,
                precio = coursesPriceE.m_price
            };
            return coursesPriceDTOs;
        }

        public static CoursesPriceE CreateE(CoursesPriceDTOs coursesPriceDTOs)
        {
            CoursesPriceE coursesPriceE = new CoursesPriceE
            {
                id_courses_price = coursesPriceDTOs.idCursoPrecio,
                fk_tbl_courses = coursesPriceDTOs.idCurso,
                fk_tbl_type_attendees = coursesPriceDTOs.idAsistente,
                m_price = coursesPriceDTOs.precio
            };
            return coursesPriceE;
        }
    }
}
