using SLIES.Domain.Entities.UserE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Domain.DTOs.UserDTOs
{
    public class UserTeacherDTOs
    {
        public int idUsuarioProfesor { get; set; }
        public int idUsuario { get; set; }
        public string profesion { get; set; }
        public bool activo { get; set; }

        public static UserTeacherDTOs CreateDTO(UserTeacherE userTeacherE)
        {
            UserTeacherDTOs userTeacherDTOs = new UserTeacherDTOs
            {
                idUsuarioProfesor = userTeacherE.id_user_teacher,
                idUsuario = userTeacherE.fk_tbl_user,
                profesion = userTeacherE.s_profession,
                activo = userTeacherE.bool_active
            };
            return userTeacherDTOs;
        }

        public static UserTeacherE CreateE(UserTeacherDTOs userTeacherDTOs)
        {
            UserTeacherE userTeacherE = new UserTeacherE
            {
                id_user_teacher = userTeacherDTOs.idUsuarioProfesor,
                fk_tbl_user = userTeacherDTOs.idUsuario,
                s_profession = userTeacherDTOs.profesion,
                bool_active = userTeacherDTOs.activo,
            };
            return userTeacherE;
        }
    }

    public class RegistreTeacherDTOs
    {
        public int accion { get; set; }
        public int idUsuario { get; set; }
        public string profesion { get; set; }
        public string foto { get; set; }
    }



}
