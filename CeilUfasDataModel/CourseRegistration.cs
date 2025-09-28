using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataModel
{
    public class CourseRegistration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserId { get; set; } = "";

        
        [MaxLength(20)]
        public string InscriptionCode { get; set; } = "";

        
        [MaxLength(100)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = "";

        
        [MaxLength(100)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = "";

       
        [MaxLength(100)]
        [Display(Name = "First Name (Arabic)")]
        public string FirstNameAr { get; set; } = "";

        
        [MaxLength(100)]
        [Display(Name = "Last Name (Arabic)")]
        public string LastNameAr { get; set; } = "";

        
        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        
        [ForeignKey("BirthState")]
        [Display(Name = "Birth State")]
        public string BirthStateId { get; set; } = "";
        public State? BirthState { get; set; }

        
        [ForeignKey("BirthMunicipality")]
        [Display(Name = "Birth Municipality")]
        public int BirthMunicipalityId { get; set; }
        public Municipality BirthMunicipality { get; set; }

       
        [MaxLength(250)]
        public string Address { get; set; } = "";

        
        [MaxLength(20)]
        [Phone]
        public string Tel { get; set; } = "";

        
        [ForeignKey("Profession")]
        [Display(Name = "Profession")]
        public int? ProfessionId { get; set; }
        public Profession? Profession { get; set; }

        
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course? Course { get; set; }

        
        [ForeignKey("CourseLevel")]
        [Display(Name = "Course Level")]
        public int? CourseLevelId { get; set; }
        public CourseLevel? CourseLevel { get; set; }

        
        [ForeignKey("Session")]
        public int? SessionId { get; set; }
        public Session? Session { get; set; }




        [DataType(DataType.DateTime)]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        [MaxLength(250)]
        public string? Notes { get; set; }



        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Paid fee value")]
        public decimal PaidFeeValue { get; set; }
		
		[Display(Name = "is re-registration for courses")]
        public bool IsReregistration { get; set; }

        [Display(Name = "Accept the registration terms")]
        public bool RegistrationTermsAccepted { get; set; }
        
        public bool RegistrationValidated { get; set; }


        [ForeignKey("Group")]
        public int? GroupId { get; set; }
        public Groupe? Group { get; set; }
    }
}