using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Evaluation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        [Required]
        [ForeignKey("CourseRegistration")]
        [Display(Name = "Course Registration")]
        public int CourseRegistrationId { get; set; }
        public CourseRegistration? CourseRegistration { get; set; }


        
        [Required]
        [ForeignKey("CourseComponent")]
        public int CourseComponentId { get; set; }
        public Course? CourseComponent { get; set; }


        public double Eval { get; set; } = 0;




    }
}
