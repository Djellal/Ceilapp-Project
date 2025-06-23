using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ceilapp.Models.ceilapp
{
    [Table("CourseLevels", Schema = "public")]
    public partial class CourseLevel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string NameAr { get; set; }

        [Required]
        public int Duration { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public int CourseId { get; set; }

        public Course Course { get; set; }

        public int? NextLevelId { get; set; }

        public CourseLevel CourseLevel1 { get; set; }

        public ICollection<CourseLevel> CourseLevels1 { get; set; }

        public ICollection<CourseRegistration> CourseRegistrations { get; set; }
    }
}