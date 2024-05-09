using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Domain.Entities.CourseE
{
    [Table("tbl_courses_modules_theme")]
    public class CoursesModulesThemeE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_courses_modules_theme { get; set; }
        public int fkt_bl_courses_modules { get; set; }
        public string s_title { get; set; }
        public string s_description { get; set; }
        public string s_video { get; set; }
        public string s_link { get; set; }
        public int fk_tbl_user { get; set; }
        public DateTime dt_registration { get; set; }
    }
}
