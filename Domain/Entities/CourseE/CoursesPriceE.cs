using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Domain.Entities.CourseE
{
    [Table("tbl_courses_price")]
    public class CoursesPriceE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_courses_price { get; set; }
        public int fk_tbl_courses { get; set; }
        public int fk_tbl_type_attendees { get; set; }
        public int m_price { get; set; }

    }
}
