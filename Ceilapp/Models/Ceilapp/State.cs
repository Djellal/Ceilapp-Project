using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ceilapp.Models.ceilapp
{
    [Table("States", Schema = "public")]
    public partial class State
    {
        [Key]
        [Required]
        [MaxLength(10)]
        public string Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required]
        [MaxLength(250)]
        public string NameAr { get; set; }

        public ICollection<Municipality> Municipalities { get; set; }

        public ICollection<CourseRegistration> CourseRegistrations { get; set; }
    }
}