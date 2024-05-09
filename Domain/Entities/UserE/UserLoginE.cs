using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Domain.Entities.UserE
{
    [Table("tbl_user_login")]
    public class UserLoginE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_user_login { get; set; }
        public int fk_tbl_user_type { get; set; }
        public string s_email { get; set; }
        public string s_password { get; set; }
        public bool byte_active { get; set; }
        public int fk_tbl_user { get; set; }
    }
}
