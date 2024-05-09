using SLIES.Domain.Entities.UserE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Domain.DTOs.UserDTOs
{
    public class UserPermissionDTOs
    {
        public int id { get; set; }
        public int idUsuario { get; set; }
        public string path { get; set; }
        public string icon { get; set; }
        public string title { get; set; }
        public bool activo { get; set; }

        public static UserPermissionDTOs CreateDTO(UserPermissionE userPermissionE)
        {
            UserPermissionDTOs userPermissionDTOs = new UserPermissionDTOs
            {
                id = userPermissionE.id_user_permission,
                idUsuario = userPermissionE.fk_tbl_user_type,
                path = userPermissionE.s_path,
                icon = userPermissionE.s_icon,
                title = userPermissionE.s_title,
                activo = userPermissionE.bool_active,
            };
            return userPermissionDTOs;
        }

        public static UserPermissionE CreateE(UserPermissionDTOs userPermissionDTOs)
        {
            UserPermissionE userPermissionE = new UserPermissionE
            {
                id_user_permission = userPermissionDTOs.id,
                fk_tbl_user_type = userPermissionDTOs.idUsuario,
                s_path = userPermissionDTOs.path,
                s_icon = userPermissionDTOs.icon,
                s_title = userPermissionDTOs.title,
                bool_active = userPermissionDTOs.activo,
            };
            return userPermissionE;
        }
    }
}
