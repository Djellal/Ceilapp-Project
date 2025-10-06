using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ceilapp.Models.ceilapp
{
    [Table("CourseFees", Schema = "public")]
    public partial class CourseFee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ProfessionId { get; set; }

        public Profession Profession { get; set; }

        [Required]
        public int CourseId { get; set; }

        public Course Course { get; set; }

        [Required]
        public decimal FeeValue { get; set; }
    }
}