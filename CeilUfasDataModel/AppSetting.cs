using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModel
{
    public class AppSetting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(250)]
        public string OrganizationName { get; set; } = string.Empty;

        [MaxLength(250)]
        public string Address { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Tel { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(250)]
        public string WebSite { get; set; } = string.Empty;
        [MaxLength(250)]
        public string FB { get; set; } = string.Empty;
        [MaxLength(250)]
        public string LinkredIn { get; set; } = string.Empty;

        [MaxLength(250)]
        public string Youtube { get; set; } = string.Empty;

        [MaxLength(250)]
        public string Instagram { get; set; } = string.Empty;

        [MaxLength(250)]
        public string X { get; set; } = string.Empty;

        public string Logo { get; set; } = string.Empty;

        public string TermsAndConditions { get; set; } = string.Empty;

        [ForeignKey("CurrentSession")]
        public int? CurrentSessionId { get; set; }
        public Session? CurrentSession { get; set; }

        public bool IsRegistrationOpened { get; set; } = false;
        public int MaxRegistrationPerSession { get; set; } = 2;
    }
}
