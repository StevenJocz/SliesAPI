using SLIES.Domain.Entities.ConfigurationE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Domain.DTOs.ConfigurationDTOs
{
    public class DependenceDTOs
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public bool activo { get; set; }

        public static DependenceDTOs CreateDTO(DependenceE dependenceE)
        {
            DependenceDTOs dependenceDTOs = new()
            {
                id = dependenceE.id_dependence,
                nombre = dependenceE.s_name,
                activo = dependenceE.byte_active,
                codigo = dependenceE.s_code,
            };
            return dependenceDTOs;
        }

        public static DependenceE CreateE(DependenceDTOs dependenceDTOs)
        {
            DependenceE dependenceE = new()
            {
                id_dependence = dependenceDTOs.id,
                s_name = dependenceDTOs.nombre,
                byte_active = dependenceDTOs.activo,
                s_code = dependenceDTOs.codigo
            };
            return dependenceE;
        }
    }
}
