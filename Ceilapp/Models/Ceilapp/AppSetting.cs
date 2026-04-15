using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ceilapp.Models.ceilapp
{
    [Table("AppSettings", Schema = "public")]
    public partial class AppSetting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string OrganizationName { get; set; }

        [Required]
        [MaxLength(250)]
        public string Address { get; set; }

        [Required]
        [MaxLength(100)]
        public string Tel { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(250)]
        public string WebSite { get; set; }

        [Column("FB")]
        [Required]
        [MaxLength(250)]
        public string Fb { get; set; }

        [Required]
        [MaxLength(250)]
        public string LinkedIn { get; set; }

        [Required]
        [MaxLength(250)]
        public string Youtube { get; set; }

        [Required]
        [MaxLength(250)]
        public string Instagram { get; set; }

        [Required]
        [MaxLength(250)]
        public string X { get; set; }

        [Required]
        public string Logo { get; set; }

        [Required]
        public string TermsAndConditions { get; set; }

        public int? CurrentSessionId { get; set; }

        public Session Session { get; set; }

        public bool IsRegistrationOpened { get; set; }

        [Required]
        public int MaxRegistrationPerSession { get; set; }

        [Required]
        public int MaxComponsationsPerCourse { get; set; }
    }
}