using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Domain.Entities.UserE
{
    [Table("tbl_user_academic_information")]
    public class UserAcademicInformationE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? id_user_academic_information { get; set; }
        public int? fk_tbl_user { get; set; }
        public string? s_posgrado { get; set; }
        public string? s_posgrado_institute { get; set; }
        public string? s_posgrado_agno { get; set; }
        public string? s_pregrado { get; set; }
        public string? s_pregrado_institute { get; set; }
        public string? s_pregrado_agno { get; set; }
    }
}
