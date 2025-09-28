using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ceilapp.Models.ceilapp
{
    [Table("CourseComponents", Schema = "public")]
    public partial class CourseComponent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Coeff { get; set; }

        [Required]
        public int CourseId { get; set; }

        public Course Course { get; set; }

        public ICollection<Evaluation> Evaluations { get; set; }
    }
}