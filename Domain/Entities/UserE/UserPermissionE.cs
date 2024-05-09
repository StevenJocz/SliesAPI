using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Domain.Entities.UserE
{
    [Table("tbl_user_permission")]
    public class UserPermissionE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_user_permission { get; set; }
        public int fk_tbl_user_type { get; set; }
        public string s_path { get; set; }
        public string s_icon { get; set; }
        public string s_title { get; set; }
        public bool bool_active { get; set; }
    }
}
