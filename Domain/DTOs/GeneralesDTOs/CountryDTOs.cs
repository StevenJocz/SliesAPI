using SLIES.Domain.DTOs.ConfigurationDTOs;
using SLIES.Domain.Entities.ConfigurationE;
using SLIES.Domain.Entities.GeneralesE;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Domain.DTOs.GeneralesDTOs
{
    public class CountryDTOs
    {
        public int id { get; set; }
        public string nombre { get; set; }

        public static CountryDTOs CreateDTO(CountryE countryE)
        {
            CountryDTOs countryDTOs = new()
            {
                id = countryE.id_country,
                nombre = countryE.s_name
            };
            return countryDTOs;
        }

        public static CountryE CreateE(CountryDTOs countryDTOs)
        {
            CountryE countryE = new()
            {
                id_country = countryDTOs.id,
                s_name = countryDTOs.nombre
            };
            return countryE;
        }
    }
}
