using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataModel
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(30)]
        public string Code { get; set; } = "";

        [MaxLength(250)]
        public string Name { get; set; } = "";

        [MaxLength(250)]
        public string NameAr { get; set; } = "";

        public bool IsActive { get; set; } = true;        

        public string Image { get; set; } = "";

        [ForeignKey("CourseType")]
        public int CourseTypeId { get; set; }
        public CourseType? CourseType { get; set; }
    }
}