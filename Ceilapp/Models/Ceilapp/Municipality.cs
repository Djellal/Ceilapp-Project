using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ceilapp.Models.ceilapp
{
    [Table("Municipalities", Schema = "public")]
    public partial class Municipality
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required]
        [MaxLength(250)]
        public string NameAr { get; set; }

        [Required]
        [MaxLength(10)]
        public string StateId { get; set; }

        public State State { get; set; }

        public ICollection<CourseRegistration> CourseRegistrations { get; set; }
    }
}