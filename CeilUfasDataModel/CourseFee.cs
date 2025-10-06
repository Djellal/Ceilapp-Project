using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class CourseFee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Profession")]        
        public int ProfessionId { get; set; }
        public Profession? Profession { get; set; }


        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course? Course { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Paid fee value")]
        public decimal FeeValue { get; set; }
    }
}
