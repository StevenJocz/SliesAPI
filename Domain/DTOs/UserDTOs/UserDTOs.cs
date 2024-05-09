using SLIES.Domain.DTOs.GeneralesDTOs;
using SLIES.Domain.Entities.GeneralesE;
using SLIES.Domain.Entities.UserE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Domain.DTOs.UserDTOs
{
    public class UserDTOs
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public int tipoDocumento { get; set; }
        public string documento { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string celular { get; set; }
        public int pais { get; set; }
        public int departamento { get; set; }
        public int ciudad { get; set; }
        public string direccion { get; set; }
        public string foto { get; set; }
        public bool activo { get; set; }
        public DateTimeOffset fechaRegistro { get; set; }
        public string password { get; set; }

        public static UserDTOs CreateDTO(UserE userE)
        {
            UserDTOs userDTOs = new()
            {
                id = userE.id_user,
                nombre = userE.s_name,
                correo = userE.s_email,
                tipoDocumento = userE.fk_tbl_type_document,
                documento = userE.s_document,
                fechaNacimiento = userE.dt_birth,
                celular = userE.s_phone,
                pais = userE.fk_tbl_country,
                departamento = userE.fk_tbl_country_state,
                ciudad = userE.fk_tbl_country_state_city,
                direccion = userE.s_address,
                foto = userE.s_photo,
                activo = userE.byte_active,
                fechaRegistro = userE.dt_registration,
            };
            return userDTOs;
        }

        public static UserE CreateE(UserDTOs userDTOs)
        {
            UserE userE = new()
            {
                id_user = userDTOs.id,
                s_name = userDTOs.nombre,
                s_email = userDTOs.correo,
                fk_tbl_type_document = userDTOs.tipoDocumento,
                s_document = userDTOs.documento,
                dt_birth = userDTOs.fechaNacimiento,
                s_phone = userDTOs.celular,
                fk_tbl_country = userDTOs.pais,
                fk_tbl_country_state = userDTOs.departamento,
                fk_tbl_country_state_city = userDTOs.ciudad,
                s_address = userDTOs.direccion,
                s_photo = userDTOs.foto,
                byte_active = userDTOs.activo,
                dt_registration = userDTOs.fechaRegistro,
            };
            return userE;
        }
    }

    public class UsuariosDTOs
    {
        public int id { get; set; }
        public string foto { get; set; }
        public string nombre { get; set; }
        public string documento { get; set; }
        public string correo { get; set; }
        public string contacto { get; set; }
        public string? profesion { get; set; }
        public string? rol { get; set; }
    }

    public class UserInformationDTOs
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string correo { get; set; }
        public string tipoDocumento { get; set; }
        public string documento { get; set; }
        public string fechaNacimiento { get; set; }
        public string celular { get; set; }
        public string pais { get; set; }
        public string departamento { get; set; }
        public string ciudad { get; set; }
        public string direccion { get; set; }
        public string foto { get; set; }
        public bool activo { get; set; }
        public string fechaRegistro { get; set; }
        public int rol { get; set; }
        public bool esProfesor { get; set; }
    }
}
