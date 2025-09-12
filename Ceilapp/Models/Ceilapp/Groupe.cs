using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ceilapp.Models.ceilapp
{
    [Table("Groupes", Schema = "public")]
    public partial class Groupe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string GroupeName { get; set; }

        [Required]
        public string TeacherId { get; set; }

        [Required]
        public int CourseId { get; set; }

        public Course Course { get; set; }

        [Required]
        public int CourseLevelId { get; set; }

        public CourseLevel CourseLevel { get; set; }

        public int? CurrentSessionId { get; set; }

        public Session Session { get; set; }

        [Required]
        public int NbrPlaces { get; set; }

        public string Description { get; set; }

        public ICollection<CourseRegistration> CourseRegistrations { get; set; }
    }
}