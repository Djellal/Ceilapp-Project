using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ceilapp.Models.ceilapp
{
    [Table("Evaluations", Schema = "public")]
    public partial class Evaluation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int CourseRegistrationId { get; set; }

        public CourseRegistration CourseRegistration { get; set; }

        [Required]
        public int CourseComponentId { get; set; }

        public CourseComponent CourseComponent { get; set; }

        [Required]
        public double Eval { get; set; }
    }
}