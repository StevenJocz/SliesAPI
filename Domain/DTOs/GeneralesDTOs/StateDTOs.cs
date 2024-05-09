using SLIES.Domain.Entities.GeneralesE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Domain.DTOs.GeneralesDTOs
{
    public class StateDTOs
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public int pais { get; set; }

        public static StateDTOs CreateDTO(StateE stateE)
        {
            StateDTOs stateDTOs = new()
            {
                id = stateE.id_state,
                nombre = stateE.s_name,
                pais = stateE.fk_tbl_country
            };
            return stateDTOs;
        }

        public static StateE CreateE(StateDTOs stateDTOs)
        {
            StateE stateE = new()
            {
                id_state = stateDTOs.id,
                s_name = stateDTOs.nombre,
                fk_tbl_country = stateDTOs.pais
            };
            return stateE;
        }

    }
}
