using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Domain.Entities.ConfigurationE
{
    [Table("tbl_type_document")]
    public class TypeDocumentE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_type_document { get; set; }
        public string s_abbreviation { get; set; }
        public string s_name { get; set; }
        public bool byte_activo { get; set; }
    }
}
