using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ceilapp.Models.ceilapp
{
    [Table("Professions", Schema = "public")]
    public partial class Profession
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        [Required]
        [MaxLength(300)]
        public string NameAr { get; set; }

        [Required]
        public decimal FeeValue { get; set; }

        public ICollection<CourseFee> CourseFees { get; set; }

        public ICollection<CourseRegistration> CourseRegistrations { get; set; }
    }
}