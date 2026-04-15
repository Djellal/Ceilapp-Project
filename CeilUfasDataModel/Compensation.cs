using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel
{
    public class Compensation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("CourseRegistration")]
        [Display(Name = "Course Registration")]
        public int CourseRegistrationId { get; set; }
        public CourseRegistration? CourseRegistration { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Absence Date")]
        public DateTime AbsenceDate { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Absence From")]
        public TimeSpan AbsenceFrom { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Absence To")]
        public TimeSpan AbsenceTo { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Makeup Date")]
        public DateTime MakeupDate { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Makeup From")]
        public TimeSpan MakeupFrom { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Makeup To")]
        public TimeSpan MakeupTo { get; set; }

        [MaxLength(100)]
        [Display(Name = "Makeup Teacher")]
        public string MakeupTeacherId { get; set; } = "";
    }
}
