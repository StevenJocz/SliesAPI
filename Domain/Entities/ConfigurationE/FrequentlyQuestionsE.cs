using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLIES.Domain.Entities.ConfigurationE
{
    [Table("tbl_frequently_questions")]
    public class FrequentlyQuestionsE
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_frequently_questions { get; set; }
        public string s_question { get; set; }
        public string s_answer { get; set; }
        public bool byte_active { get; set; }
    }
}
