using Microsoft.EntityFrameworkCore;
using StudentHelpCare.StudentHelpCare.Data.Entity;

namespace StudentHelpCare.StudentHelpCare.Repository
{
    public class AppDbContext : DbContext
    {
        public DbSet<StudentEntity> Student { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }
    }
}
