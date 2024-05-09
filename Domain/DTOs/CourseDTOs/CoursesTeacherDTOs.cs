using SLIES.Domain.Entities.CourseE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Domain.DTOs.CourseDTOs
{
    public class CoursesTeacherDTOs
    {
        public int idCursoProfesor { get; set; }
        public int idUsuarioProfesor { get; set; }
        public int idCurso { get; set; }

        public static CoursesTeacherDTOs CreateDTO(CoursesTeacherE coursesTeacherE)
        {
            CoursesTeacherDTOs coursesTeacherDTOs = new CoursesTeacherDTOs
            {
                idCursoProfesor = coursesTeacherE.id_courses_teacher,
                idUsuarioProfesor = coursesTeacherE.fk_tbl_user_teacher,
                idCurso = coursesTeacherE.fk_tbl_courses
            };
            return coursesTeacherDTOs;
        }

        public static CoursesTeacherE CreateE(CoursesTeacherDTOs coursesTeacherDTOs)
        {
            CoursesTeacherE coursesTeacherE = new CoursesTeacherE
            {
                id_courses_teacher = coursesTeacherDTOs.idCursoProfesor,
                fk_tbl_user_teacher = coursesTeacherDTOs.idUsuarioProfesor,
                fk_tbl_courses = coursesTeacherDTOs.idCurso
            };
            return coursesTeacherE;
        }
    }
}
