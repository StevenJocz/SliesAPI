using SLIES.Domain.Entities.UserE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Domain.DTOs.UserDTOs
{
    public class UserLoginDTOs
    {
        public int id { get; set; }
        public int tipoUsuario { get; set; }
        public string correo { get; set; }
        public string password { get; set; }
        public bool activo { get; set; }
        public int idUsuario { get; set; }

        public static UserLoginDTOs CreateDTO(UserLoginE userLoginE)
        {
            UserLoginDTOs userLoginDTOs = new()
            {
                id = userLoginE.id_user_login,
                tipoUsuario = userLoginE.fk_tbl_user_type,
                correo = userLoginE.s_email,
                password = userLoginE.s_password,
                activo = userLoginE.byte_active,
                idUsuario = userLoginE.fk_tbl_user,
            };
            return userLoginDTOs;
        }

        public static UserLoginE CreateE(UserLoginDTOs userLoginDTOs)
        {
            UserLoginE userLoginE = new()
            {
                id_user_login = userLoginDTOs.id,
                fk_tbl_user_type = userLoginDTOs.tipoUsuario,
                s_email = userLoginDTOs.correo,
                s_password = userLoginDTOs.password,
                byte_active = userLoginDTOs.activo,
                fk_tbl_user = userLoginDTOs.idUsuario,
            };
            return userLoginE;
        }
    }

    public class LoginDTOs
    {
        public string correo { get; set; }
        public string password { get; set; }
    }

    public class DateUserDTOs
    {
        public int idUsuario { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string foto { get; set; }
        public int tipoUsuario { get; set; }
    }

    public class Responselogin
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string Token { get; set; }
    }
}
