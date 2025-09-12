using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ceilapp.Models.ceilapp
{
    [Table("Courses", Schema = "public")]
    public partial class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string NameAr { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public int CourseTypeId { get; set; }

        public CourseType CourseType { get; set; }

        [Required]
        public int Order { get; set; }

        public ICollection<CourseLevel> CourseLevels { get; set; }

        public ICollection<Groupe> Groupes { get; set; }

        public ICollection<CourseComponent> CourseComponents { get; set; }

        public ICollection<Evaluation> Evaluations { get; set; }

        public ICollection<CourseRegistration> CourseRegistrations { get; set; }
    }
}