using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ceilapp.Models.ceilapp
{
    [Table("CourseRegistrations", Schema = "public")]
    public partial class CourseRegistration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        [MaxLength(20)]
        public string InscriptionCode { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstNameAr { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastNameAr { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        [MaxLength(10)]
        public string BirthStateId { get; set; }

        public State State { get; set; }

        [Required]
        public int BirthMunicipalityId { get; set; }

        public Municipality Municipality { get; set; }

        [Required]
        [MaxLength(250)]
        public string Address { get; set; }

        [Required]
        [MaxLength(20)]
        public string Tel { get; set; }

        public int? ProfessionId { get; set; }

        public Profession Profession { get; set; }

        [Required]
        public int CourseId { get; set; }

        public Course Course { get; set; }

        public int? CourseLevelId { get; set; }

        public CourseLevel CourseLevel { get; set; }

        public int? SessionId { get; set; }

        public Session Session { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; }

        [MaxLength(250)]
        public string Notes { get; set; }

        [Required]
        public decimal PaidFeeValue { get; set; }

        public bool IsReregistration { get; set; }

        public bool RegistrationTermsAccepted { get; set; }

        public bool RegistrationValidated { get; set; }

        public int? GroupId { get; set; }

        public Groupe Groupe { get; set; }

        [Required]
        public decimal FeeValue { get; set; }

        public ICollection<Compensation> Compensations { get; set; }

        public ICollection<Evaluation> Evaluations { get; set; }
    }
}