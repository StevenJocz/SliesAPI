using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Domain.Entities.GeneralesE
{
    [Table("tbl_country_state")]
    public class StateE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_state { get; set; }
        public string s_name { get; set; }
        public int fk_tbl_country { get; set; }
    }
}
