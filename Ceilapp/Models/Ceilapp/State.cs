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
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string NameAr { get; set; }

        public ICollection<Municipality> Municipalities { get; set; }

        public ICollection<CourseRegistration> CourseRegistrations { get; set; }
    }
}