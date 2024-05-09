using SLIES.Domain.Entities.UserE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Domain.DTOs.UserDTOs
{
    public class UserAcademicInformationDTOs
    {
        public int? id { get; set; }
        public int? idUsuario { get; set; }
        public string? posgrado { get; set; }
        public string? posgradoInstitute { get; set; }
        public string? posgradoAgno { get; set; }
        public string? pregrado { get; set; }
        public string? pregradoInstitute { get; set; }
        public string? pregradoAgno { get; set; }


        public static UserAcademicInformationDTOs CreateDTO(UserAcademicInformationE userAcademicInformationE)
        {
            UserAcademicInformationDTOs userAcademicInformationDTOs = new()
            {
                id = userAcademicInformationE.id_user_academic_information,
                idUsuario = userAcademicInformationE.fk_tbl_user,
                posgrado = userAcademicInformationE.s_posgrado,
                posgradoInstitute = userAcademicInformationE.s_posgrado_institute,
                posgradoAgno = userAcademicInformationE.s_posgrado_agno,
                pregrado = userAcademicInformationE.s_pregrado,
                pregradoInstitute = userAcademicInformationE.s_pregrado_institute,
                pregradoAgno = userAcademicInformationE.s_pregrado_agno,
            };
            return userAcademicInformationDTOs;
        }

        public static UserAcademicInformationE CreateE(UserAcademicInformationDTOs userAcademicInformationDTOs)
        {
            UserAcademicInformationE userAcademicInformationE = new()
            {
                id_user_academic_information = userAcademicInformationDTOs.id,
                fk_tbl_user = userAcademicInformationDTOs.idUsuario,
                s_posgrado = userAcademicInformationDTOs.posgrado,
                s_posgrado_institute = userAcademicInformationDTOs.posgradoInstitute,
                s_posgrado_agno = userAcademicInformationDTOs.posgradoAgno,
                s_pregrado = userAcademicInformationDTOs.pregrado,
                s_pregrado_institute = userAcademicInformationDTOs.pregradoInstitute,
                s_pregrado_agno = userAcademicInformationDTOs.pregradoAgno,
            };
            return userAcademicInformationE;
        }
    }
}
