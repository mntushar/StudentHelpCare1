using System.ComponentModel.DataAnnotations;

namespace StudentHelpCare.StudentHelpCare.Data.Entity
{
    public class StudentEntity
    {
        [Key]
        public long Id { get; set; }
        public string? Name { get; set; }
    }
}
