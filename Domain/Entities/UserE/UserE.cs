using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Domain.Entities.UserE
{
    [Table("tbl_user")]
    public class UserE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_user { get; set; }
        public string s_name { get; set; }
        public string s_email { get; set; }
        public int fk_tbl_type_document { get; set; }
        public string s_document { get; set; }
        public DateTime dt_birth { get; set; }
        public string s_phone { get; set; }
        public int fk_tbl_country { get; set; }
        public int fk_tbl_country_state { get; set; }
        public int fk_tbl_country_state_city { get; set; }
        public string s_address { get; set; }
        public string s_photo { get; set; }
        public bool byte_active { get; set; }
        public DateTimeOffset dt_registration { get; set; }
    }
}
