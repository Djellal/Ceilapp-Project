using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Groupe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(250)]
        public string GroupeName { get; set; } = string.Empty;

        public string TeacherId { get; set; } = "";

        [Required]
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course? Course { get; set; }



        [Required]
        [ForeignKey("CourseLevel")]
        [Display(Name = "Course Level")]
        public int CourseLevelId { get; set; }
        public CourseLevel? CourseLevel { get; set; }



        [ForeignKey("CurrentSession")]
        public int? CurrentSessionId { get; set; }
        public Session? CurrentSession { get; set; }


        public int NbrPlaces { get; set; }

        public string Description { get; set; }
    }
}
