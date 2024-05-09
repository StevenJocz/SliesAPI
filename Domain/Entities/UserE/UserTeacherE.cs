using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Domain.Entities.UserE
{
    [Table("tbl_user_teacher")]
    public class UserTeacherE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_user_teacher { get; set; }
        public int fk_tbl_user { get; set; }
        public string s_profession { get; set; }
        public bool bool_active { get; set; }
    }
}
