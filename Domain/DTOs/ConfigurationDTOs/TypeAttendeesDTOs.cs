using SLIES.Domain.Entities.ConfigurationE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Domain.DTOs.ConfigurationDTOs
{
    public class TypeAttendeesDTOs
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public bool activo { get; set; }

        public static TypeAttendeesDTOs CreateDTO(TypeAttendeesE typeAttendeesE)
        {
            TypeAttendeesDTOs typeAttendeesDTOs = new()
            {
                id = typeAttendeesE.id_type_attendees,
                nombre = typeAttendeesE.s_name,
                activo = typeAttendeesE.byte_active,
                codigo = typeAttendeesE.s_code,
            };
            return typeAttendeesDTOs;
        }

        public static TypeAttendeesE CreateE(TypeAttendeesDTOs typeAttendeesDTOs)
        {
            TypeAttendeesE typeAttendeesE = new()
            {
                id_type_attendees = typeAttendeesDTOs.id,
                s_name = typeAttendeesDTOs.nombre,
                byte_active = typeAttendeesDTOs.activo,
                s_code = typeAttendeesDTOs.codigo
            };
            return typeAttendeesE;
        }
    }
}
