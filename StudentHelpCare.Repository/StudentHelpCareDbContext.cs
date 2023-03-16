using Microsoft.EntityFrameworkCore;
using StudentHelpCare.StudentHelpCare.Data.Entity;

namespace StudentHelpCare.StudentHelpCare.Repository
{
    public class StudentHelpCareDbContext : DbContext
    {
        public DbSet<StudentEntity> Student { get; set; }

        public StudentHelpCareDbContext(DbContextOptions<StudentHelpCareDbContext> options)
        : base(options)
        {
        }
    }
}
