using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ceilapp.Models.ceilapp
{
    [Table("Compensations", Schema = "public")]
    public partial class Compensation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int CourseRegistrationId { get; set; }

        public CourseRegistration CourseRegistration { get; set; }

        [Required]
        public DateTime AbsenceDate { get; set; }

        [Required]
        public TimeSpan AbsenceFrom { get; set; }

        [Required]
        public TimeSpan AbsenceTo { get; set; }

        [Required]
        public DateTime MakeupDate { get; set; }

        [Required]
        public TimeSpan MakeupFrom { get; set; }

        [Required]
        public TimeSpan MakeupTo { get; set; }

        [Required]
        [MaxLength(100)]
        public string MakeupTeacherId { get; set; }

        public bool IsApproved { get; set; }

        [MaxLength(100)]
        public string CourseLevel { get; set; }

        [MaxLength(100)]
        public string OriginGroup { get; set; }

        [MaxLength(100)]
        public string RecipientGroup { get; set; }
    }
}