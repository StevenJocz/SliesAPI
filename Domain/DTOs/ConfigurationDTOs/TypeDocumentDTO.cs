using SLIES.Domain.Entities.ConfigurationE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Domain.DTOs.ConfigurationDTOs
{
    public class TypeDocumentDTO
    {
        public int id { get; set; }
        public string abbreviacion { get; set; }
        public string nombre { get; set; }
        public bool activo { get; set; }

        public static TypeDocumentDTO CreateDTO(TypeDocumentE typeDocumentE)
        {
            TypeDocumentDTO typeDocumentDTO = new()
            {
                id = typeDocumentE.id_type_document,
                abbreviacion = typeDocumentE.s_abbreviation,
                nombre = typeDocumentE.s_name,
                activo = typeDocumentE.byte_activo
            };
            return typeDocumentDTO;
        }

        public static TypeDocumentE CreateE(TypeDocumentDTO typeDocumentDT)
        {
            TypeDocumentE typeDocumentE = new()
            {
                id_type_document = typeDocumentDT.id,
                s_abbreviation = typeDocumentDT.abbreviacion,
                s_name = typeDocumentDT.nombre,
                byte_activo = typeDocumentDT.activo
            };
            return typeDocumentE;
        }
    }
}
