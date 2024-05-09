using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Domain.Entities.CourseE
{
    [Table("tbl_courses_teacher")]
    public class CoursesTeacherE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_courses_teacher { get; set; }
        public int fk_tbl_user_teacher { get; set; }
        public int fk_tbl_courses { get; set; }
    }
}
