using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Domain.Entities.CourseE
{
    [Table("tbl_courses")]
    public class CourseE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_courses { get; set; }
        public string s_title { get; set; }
        public string s_description { get; set; }
        public int fk_tbl_courses_categories { get; set; }
        public int fk_tbl_courses_type { get; set; }
        public DateTime dt_startDate { get; set; }
        public DateTime dt_endDate { get; set; }
        public DateTime dt_registrationDeadline { get; set; }
        public bool bool_payment { get; set; }
        public bool bool_formal { get; set; }
        public bool bool_accompanist { get; set; }
        public bool bool_minors { get; set; }
        public bool bool_group { get; set; }
        public bool bool_limitSeats { get; set; }
        public int n_quantitySeats { get; set; }
        public string s_image { get; set; }
        public bool bool_active { get; set; }
        public int? fk_tbl_user { get; set; }
        public DateTime? dt_registration { get; set; }
        public string s_location { get; set; }
        public int fk_tbl_dependence { get; set; }
        
    }
}
