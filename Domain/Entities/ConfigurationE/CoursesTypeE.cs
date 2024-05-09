using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Domain.Entities.ConfigurationE
{
    [Table("tbl_courses_type")]
    public class CoursesTypeE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_courses_type { get; set; }
        public string s_name { get; set; }
        public bool byte_active { get; set; }
        public string s_code { get; set; }
    }
}
