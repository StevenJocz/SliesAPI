using SLIES.Domain.Entities.GeneralesE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Domain.DTOs.GeneralesDTOs
{
    public class CitiesDTOs
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public int estado { get; set; }

        public static CitiesDTOs CreateDTO(CitiesE citiesE)
        {
            CitiesDTOs citiesDTOs = new()
            {
                id = citiesE.id_city,
                nombre = citiesE.s_name,
                estado = citiesE.fk_tbl_country_state
            };
            return citiesDTOs;
        }

        public static CitiesE CreateE(CitiesDTOs citiesDTOs)
        {
            CitiesE citiesE = new()
            {
                id_city = citiesDTOs.id,
                s_name = citiesDTOs.nombre,
                fk_tbl_country_state = citiesDTOs.estado
            };
            return citiesE;
        }
    }
}
