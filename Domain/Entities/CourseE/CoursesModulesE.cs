using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Domain.Entities.CourseE
{
    [Table("tbl_courses_modules")]
    public class CoursesModulesE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_courses_modules { get; set; }
        public int fk_tbl_courses { get; set; }
        public string s_title { get; set; }
        public int fk_tbl_user { get; set; }
        public DateTime dt_registration { get; set; }
    }
}
